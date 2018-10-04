using System;
using System.Collections.Generic;
using System.Text;
using TestCommon.Models;

namespace TestCommon.Services
{
    public interface ICommentRepository
    {

        Comment Add(Comment comment);

        Comment ToAdd { get; set; }
        Comment Add();

    }
}
