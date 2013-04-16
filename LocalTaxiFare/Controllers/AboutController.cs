using System.Web.Mvc;

namespace LocalTaxiFare.Controllers
{
    public class AboutController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}