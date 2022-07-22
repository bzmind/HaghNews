using System;
using System.Linq;
using System.Web.Mvc;
using DataLayer.Models;
using DataLayer.Repositories;
using DataLayer.ViewModels;

namespace Cms.Controllers
{
    public class NewsController : Controller
    {
        private readonly IPageGroupRepository _pageGroupRepository;
        private readonly IPageRepository _pageRepository;
        private readonly IPageCommentRepository _commentRepository;
        public NewsController(IPageGroupRepository pageGroupRepository, IPageRepository pageRepository, IPageCommentRepository commentRepository)
        {
            _pageGroupRepository = pageGroupRepository;
            _pageRepository = pageRepository;
            _commentRepository = commentRepository;
        }

        // GET: CategoryNews
        public ActionResult ShowCategoryNews()
        {
            return PartialView(_pageGroupRepository.GetAllGroups().Select(g => new ShowCategoryNewsViewModel()
            {
                GroupId = g.GroupId,
                GroupTitle = g.GroupTitle,
                PageCount = g.Pages.Count
            }));
        }

        public ActionResult ShowMenuNews()
        {
            return PartialView(_pageGroupRepository.GetAllGroups());
        }

        public ActionResult ShowTopNews()
        {
            return PartialView(_pageRepository.GetAllPages().OrderByDescending(p => p.Visit).Take(5));
        }

        public ActionResult ShowLatestNews()
        {
            return PartialView(_pageRepository.GetAllPages().OrderByDescending(p => p.CreateDate).Take(10));
        }

        [Route("Archive")]
        public ActionResult NewsArchive(int currentPage = 1)
        {
            var take = 10;
            var skip = (currentPage - 1) * take;

            var newsCount = _pageRepository.GetAllPages().Count();

            // Baghimande
            int remainder;

            // in DivRem payin migire newsCount va take ro taghsim mikone, bad khareje ghesmat ke
            // ye adade sahih hast (addad sahih yani ashaar nist) yani 1,2,3..., ashaar nist ro
            // barmigardune tuye quotient, bad baghimande ro ham return mikone
            // tuye variable remainder balayi
            var quotient = Math.DivRem(newsCount, take, out remainder);

            // Hala migam agar baghimande nadasht yani masalan 4/2 = 2 yani sar rast bud va baghimande ya ezafi nayovord chizi
            // begir ViewBag.pageCount ro mosavie hamun quotient gharar bede ke adade sahih hast (addad sahih yani ashaar nist)
            // vali agar didi ye rize ham baghimande dasht, begir in quotient ke ye adad sahih bud ro be + 1 kon, yani yeki bebar bala
            // hamuna mal gerd kardan be adade balatar ro anjam dadim engar, baghimande ro andakhtim dur...
            ViewBag.pageCount = remainder == 0 ? quotient : quotient + 1;

            ViewBag.currentPage = currentPage;

            return View(_pageRepository.GetAllPages().OrderByDescending(p => p.CreateDate).Skip(skip).Take(take));
        }

        [Route("Group/{id}/{title}")]
        public ActionResult ShowNewsGroup(int id, string title, int currentPage = 1)
        {
            var take = 10;
            var skip = (currentPage - 1) * take;

            var newsCount = _pageRepository.GetAllPages().Where(p => p.GroupId == id).Count();

            int remainder;
            var quotient = Math.DivRem(newsCount, take, out remainder);
            ViewBag.pageCount = remainder == 0 ? quotient : quotient + 1;

            ViewBag.currentPage = currentPage;

            ViewBag.groupTitle = title;
            ViewBag.groupID = id;

            return View(_pageRepository.GetAllPages().Where(p => p.GroupId == id).OrderByDescending(p => p.CreateDate).Skip(skip).Take(take));
        }

        [Route("News/{id}")]
        public ActionResult ShowNews(int id)
        {
            var news = _pageRepository.GetPageById(id);

            if (news == null)
            {
                return HttpNotFound();
            }

            news.Visit += 1;
            _pageRepository.UpdatePage(news);
            _pageRepository.Save();

            return View(news);
        }

        public ActionResult AddComment(int id, string name, string email, string comment)
        {
            var newComment = new PageComment()
            {
                CreateDate = DateTime.Now,
                PageId = id,
                Comment = comment,
                Email = email,
                Name = name
            };

            _commentRepository.AddComment(newComment);

            return PartialView("ShowComments", _commentRepository.GetCommentByNewsId(id));
        }

        public ActionResult ShowComments(int id)
        {
            return PartialView(_commentRepository.GetCommentByNewsId(id));
        }
    }
}