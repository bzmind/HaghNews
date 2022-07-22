using System.Data.Entity;
using System.Linq;
using DataLayer.Context;
using DataLayer.Models;
using DataLayer.Repositories;

namespace DataLayer.Services
{
    public class PageGroupRepository : IPageGroupRepository
    {
        private readonly CmsContext _context;
        public PageGroupRepository(CmsContext context)
        {
            _context = context;
        }

        public IQueryable<PageGroup> GetAllGroups()
        {
            return _context.PageGroups;
        }

        public PageGroup GetGroupById(int groupId)
        {
            return _context.PageGroups.Find(groupId);
        }

        public void InsertGroup(PageGroup pageGroup)
        {
            _context.PageGroups.Add(pageGroup);
        }

        public void UpdateGroup(PageGroup pageGroup)
        {
            _context.Entry(pageGroup).State = EntityState.Modified;
        }

        public void DeleteGroup(PageGroup pageGroup)
        {
            _context.PageGroups.Remove(pageGroup);
        }

        public void DeleteGroup(int groupId)
        {
            var page = GetGroupById(groupId);
            DeleteGroup(page);
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
