using System.Net;
using System.Web.Mvc;
using DataLayer.Models;
using DataLayer.Repositories;

namespace Cms.Areas.Admin.Controllers
{
    [Authorize]
    public class PageGroupsController : Controller
    {
        private readonly IPageGroupRepository _repository;
        public PageGroupsController(IPageGroupRepository repository)
        {
            _repository = repository;
        }

        // GET: Admin/PageGroups
        public ActionResult Index()
        {
            return View(_repository.GetAllGroups());
        }

        // GET: Admin/PageGroups/Create
        public ActionResult Create()
        {
            return PartialView();
        }

        // POST: Admin/PageGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,GroupTitle")] PageGroup pageGroup)
        {
            if (ModelState.IsValid)
            {
                _repository.InsertGroup(pageGroup);
                _repository.Save();
                return RedirectToAction("Index");
            }

            return View(pageGroup);
        }

        // GET: Admin/PageGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pageGroup = _repository.GetGroupById(id.Value);
            if (pageGroup == null)
            {
                return HttpNotFound();
            }

            return PartialView(pageGroup);
        }

        // POST: Admin/PageGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID, GroupTitle")] PageGroup pageGroup)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateGroup(pageGroup);
                _repository.Save();
                return RedirectToAction("Index");
            }

            return View(pageGroup);
        }

        // GET: Admin/PageGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pageGroup = _repository.GetGroupById(id.Value);
            if (pageGroup == null)
            {
                return HttpNotFound();
            }

            return PartialView(pageGroup);
        }

        // POST: Admin/PageGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var pageGroup = _repository.GetGroupById(id);
            _repository.DeleteGroup(pageGroup);
            _repository.Save();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
