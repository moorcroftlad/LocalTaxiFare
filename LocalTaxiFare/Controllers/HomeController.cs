using System.Web.Mvc;

namespace LocalTaxiFare.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Index()
        {
            return View("Index");
        }
    }
}