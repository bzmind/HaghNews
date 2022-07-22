using System.Linq;
using System.Web.Mvc;
using DataLayer.Repositories;

namespace Cms.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPageRepository _pageRepository;
        public HomeController(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Slider()
        {
            return PartialView(_pageRepository.GetAllPages().Where(p => p.ShowInSlider == true));
        }
    }
}