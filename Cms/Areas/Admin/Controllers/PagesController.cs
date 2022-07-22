using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer.Models;
using DataLayer.Repositories;

namespace Cms.Areas.Admin.Controllers
{
    [Authorize]
    public class PagesController : Controller
    {
        private readonly IPageRepository _repository;
        private readonly IPageGroupRepository _pageGroupRepository;

        public PagesController(IPageRepository repository, IPageGroupRepository pageGroupRepository)
        {
            _repository = repository;
            _pageGroupRepository = pageGroupRepository;
        }

        // GET: Admin/Pages
        public ActionResult Index()
        {
            return View(_repository.GetAllPages().OrderByDescending(p => p.CreateDate));
        }

        // GET: Admin/Pages/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(_pageGroupRepository.GetAllGroups(), "GroupID", "GroupTitle");
            return View();
        }

        // POST: Admin/Pages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PageID,GroupID,Title,ShortDescription,Text,visit,ImageName,ShowInSlider,CreateDate,Tags")] Page page, HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                page.Visit = 0;
                page.CreateDate = DateTime.Now;

                if (imgUp != null)
                {
                    page.ImageName = Guid.NewGuid() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/PageImages/" + page.ImageName));
                }

                _repository.InsertPage(page);
                _repository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(_pageGroupRepository.GetAllGroups(), "GroupID", "GroupTitle", page.GroupId);
            return View(page);
        }

        // GET: Admin/Pages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var page = _repository.GetPageById(id.Value);
            if (page == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(_pageGroupRepository.GetAllGroups(), "GroupID", "GroupTitle", page.GroupId);
            return View(page);
        }

        // POST: Admin/Pages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PageID,GroupID,Title,ShortDescription,Text,visit,ImageName,ShowInSlider,CreateDate,Tags")] Page page, HttpPostedFileBase imgUp)
        {
            if (ModelState.IsValid)
            {
                if (imgUp != null)
                {
                    if (page.ImageName != null)
                    {
                        System.IO.File.Delete(Server.MapPath("/PageImages/" + page.ImageName));
                    }

                    page.ImageName = Guid.NewGuid() + Path.GetExtension(imgUp.FileName);
                    imgUp.SaveAs(Server.MapPath("/PageImages/" + page.ImageName));
                }

                _repository.UpdatePage(page);
                _repository.Save();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(_pageGroupRepository.GetAllGroups(), "GroupID", "GroupTitle", page.GroupId);
            return View(page);
        }

        // GET: Admin/Pages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var page = _repository.GetPageById(id.Value);
            if (page == null)
            {
                return HttpNotFound();
            }

            return View(page);
        }

        // POST: Admin/Pages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var page = _repository.GetPageById(id);

            if (page.ImageName != null)
            {
                System.IO.File.Delete(Server.MapPath("/PageImages/" + page.ImageName));
            }

            _repository.DeletePage(page);
            _repository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repository.Dispose();
                _pageGroupRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
