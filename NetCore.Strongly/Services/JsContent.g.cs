
namespace NetCore.Strongly.Services
{
    partial class JsContent
    {
        public const string content = @"function stronglySent(n){buildBindings();$.post(n,JSON.stringify(stronglyData),n=>console.log(n),""json"")}function buildBindings(){var n,t,i;for(n in stronglyData.bindings)t=$('[binding-key=""'+n+'""]'),t.length>0&&(i={},$.each(t.serializeArray(),function(){i[this.name]=this.value}),stronglyData.bindings[n].rawData=JSON.stringify(i))}";
    }
}
                         