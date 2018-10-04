using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text;

namespace NetCore.Strongly.Services
{
    class TypeHandler
    {

        readonly Dictionary<string, MethodMeta> methods;
        readonly Dictionary<string, PropertyMeta> properties;

        public TypeHandler()
        {
            methods = new Dictionary<string, MethodMeta>();
            properties = new Dictionary<string, PropertyMeta>();
        }

        internal bool TryExecute(string key, IServiceProvider serviceProvider, out object output)
        {
            if (methods.ContainsKey(key))
            {
                output = methods[key].Execute(serviceProvider);
                return true;
            }
            else
            {
                output = null;
                return false;
            }
        }

        internal string GetMethodKey<TContext>(Expression<Action<TContext>> expression)
        {
            if (expression.Body is MethodCallExpression method) return GetMethodKey<TContext>(method);
            throw new InvalidOperationException("Invalid lamda provided. function is expected.");
        }

        internal string GetMethodKey<TContext, T>(Expression<Func<TContext, T>> expression)
        {
            if (expression.Body is MethodCallExpression method) return GetMethodKey<TContext>(method);
            throw new InvalidOperationException("Invalid lamda provided. function is expected.");
        }

        string GetMethodKey<TContext>(MethodCallExpression method)
        {
            var meta = new MethodMeta(typeof(TContext), method.Method);
            string key = methods.SingleOrDefault(t => t.Value.FullType == meta.FullType && t.Value.MethodName == meta.MethodName).Key;
            if (key == null)
            {
                key = Guid.NewGuid().ToString();
                methods[key] = meta;
            }
            return key;
        }

        internal bool TrySet(PropertyContext context, IServiceProvider serviceProvider)
        {
            if (properties.ContainsKey(context.Key))
            {
                var property = properties[context.Key];
                var data = JsonConvert.DeserializeObject(WebUtility.HtmlDecode(context.RawData), property.PropertyType);
                property.Set(data, serviceProvider);
                return true;
            }
            else return false;
        }

        internal bool TrySet(string key, object value, IServiceProvider serviceProvider)
        {
            if (properties.ContainsKey(key))
            {
                properties[key].Set(value, serviceProvider);
                return true;
            }
            else return false;
        } 

        internal string GetPropertyKey<TContext, T>(Expression<Func<TContext, T>> expression)
        {
            if (expression.Body is MemberExpression member)
            {
                var meta = new PropertyMeta(typeof(TContext), member.Member, typeof(T));
                string key = properties.SingleOrDefault(p => p.Value.FullType == meta.FullType && p.Value.Name == meta.Name).Key;
                if (key == null)
                {
                    key = Guid.NewGuid().ToString();
                    properties[key] = meta;
                }
                return key;
            }
            else throw new InvalidOperationException("Invalid lamda provided. only properties or fields allowed");
        }

        class MethodMeta
        {

            internal Type MainType { get; }

            internal MethodInfo Method { get; }

            string _FullType;
            internal string FullType => _FullType ?? (_FullType = MainType?.AssemblyQualifiedName);

            string _MethodName;
            internal string MethodName => _MethodName ?? (_MethodName = Method?.Name);

            public MethodMeta(Type mainType, MethodInfo method)
            {
                MainType = mainType;
                Method = method;
            }

            internal object Execute(IServiceProvider provider)
                => Method.Invoke(provider.GetService(MainType), new object[] { });

        }

        class PropertyMeta
        {

            internal Type MainType { get; }

            internal Type PropertyType { get; }

            internal MemberInfo Member { get; }

            string _FullType;
            internal string FullType => _FullType ?? (_FullType = MainType?.AssemblyQualifiedName);

            PropertyInfo _Info;
            internal PropertyInfo Info => _Info ?? (_Info = MainType?.GetProperty(Name));

            string _Name;
            internal string Name => _Name ?? (_Name = Member?.Name);

            internal PropertyMeta(Type mainType, MemberInfo member, Type propertyType)
            {
                MainType = mainType;
                Member = member;
                PropertyType = propertyType;
            }

            internal void Set(object value, IServiceProvider provider)
            {
                Info.SetValue(provider.GetService(MainType), value);
            }

        }

    }
}
