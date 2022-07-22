using System;
using System.Linq;
using DataLayer.Models;

namespace DataLayer.Repositories
{
    public interface IPageGroupRepository : IDisposable
    {
        IQueryable<PageGroup> GetAllGroups();
        PageGroup GetGroupById(int groupId);
        void InsertGroup(PageGroup pageGroup);
        void UpdateGroup(PageGroup pageGroup);
        void DeleteGroup(PageGroup pageGroup);
        void DeleteGroup(int groupId);
        void Save();
    }
}
