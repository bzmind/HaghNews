using System;
using System.Linq;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public interface IPageCommentRepository : IDisposable
    {
        IQueryable<PageComment> GetCommentByNewsId(int pageId);
        void AddComment(PageComment comment);
    }
}
