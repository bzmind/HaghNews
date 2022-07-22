using System.Linq;
using System.Web.Mvc;
using DataLayer.Repositories;

namespace Cms.Controllers
{
    public class SearchController : Controller
    {
        private readonly IPageRepository _pageRepository;

        public SearchController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        // GET: Search
        public ActionResult Index(string q)
        {
            ViewBag.name = q;

            return View(_pageRepository.GetAllPages().Where(p =>
                p.Title.Contains(q) || p.ShortDescription.Contains(q) || p.Tags.Contains(q) || p.Text.Contains(q)));
        }
    }
}