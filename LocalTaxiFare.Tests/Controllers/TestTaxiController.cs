using Geography;
using LocalTaxiFare.Controllers;
using LocalTaxiFare.Models;
using NUnit.Framework;

namespace LocalTaxiFare.Tests.Controllers
{
    [TestFixture]
    public class TestTaxiController : ICreateTaxiViewModels
    {
        [Test]
        public void DisplaysIndex()
        {
            var viewResult = new TaxiController(this).Index("53.479251", "-2.247926");

            var viewName = viewResult.ViewName;
            Assert.That(viewName, Is.EqualTo("Index"));
        }

        [Test]
        public void ReturnsViewModel()
        {
            var viewResult = new TaxiController(this).Index("53.479251", "-2.247926");

            var model = viewResult.Model;
            Assert.That(model, Is.TypeOf<TaxisViewModel>());
        }

        public TaxisViewModel Create(Location location)
        {
            return new TaxisViewModel();
        }
    }
}