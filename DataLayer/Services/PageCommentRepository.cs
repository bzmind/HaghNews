using System.Linq;
using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repositories;

namespace DataLayer.Services
{
    public class PageCommentRepository : IPageCommentRepository
    {
        private readonly CmsContext _context;
        public PageCommentRepository(CmsContext context)
        {
            _context = context;
        }

        public IQueryable<PageComment> GetCommentByNewsId(int pageId)
        {
            return _context.PageComments.Where(c => c.PageId == pageId);
        }

        public void AddComment(PageComment comment)
        {
            _context.PageComments.Add(comment);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
