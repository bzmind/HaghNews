using System;
using System.Linq;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public interface IPageRepository : IDisposable
    {
        IQueryable<Page> GetAllPages();
        Page GetPageById(int pageId);
        void InsertPage(Page page);
        void UpdatePage(Page page);
        void DeletePage(Page page);
        void DeletePage(int pageId);
        void Save();
    }
}
