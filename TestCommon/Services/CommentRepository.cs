using System;
using System.Collections.Generic;
using System.Text;
using TestCommon.Data;
using TestCommon.Models;

namespace TestCommon.Services
{
    class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext context;

        public CommentRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Comment ToAdd { get; set; }

        public Comment Add(Comment comment)
        {
            context.Add(comment);
            context.SaveChanges();
            return comment;
        }

        public Comment Add() => Add(ToAdd);
    }
}
