using System.Data.Entity;
using System.Linq;
using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repositories;

namespace DataLayer.Services
{
    public class PageRepository : IPageRepository
    {
        private readonly CmsContext _context;
        public PageRepository(CmsContext context)
        {
            _context = context;
        }

        public IQueryable<Page> GetAllPages()
        {
            return _context.Pages;
        }

        public Page GetPageById(int pageId)
        {
            return _context.Pages.Find(pageId);
        }

        public void InsertPage(Page page)
        {
            _context.Pages.Add(page);
        }

        public void UpdatePage(Page page)
        {
            _context.Entry(page).State = EntityState.Modified;
        }

        public void DeletePage(Page page)
        {
            _context.Entry(page).State = EntityState.Deleted;
        }

        public void DeletePage(int pageId)
        {
            var page = GetPageById(pageId);
            DeletePage(page);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
