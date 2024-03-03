using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Web.View_Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _owner;
        private readonly IUnitOfWork<PortfolioItem> _portfolio;

        public HomeController(IUnitOfWork<Owner> owner,
                              IUnitOfWork<PortfolioItem> portfolio)
        {
            _owner = owner;
            _portfolio = portfolio;
        }

        public IActionResult Index()
        {
            var homeVm = new HomeViewModel
            {
                Owner = _owner.Entity.GetAll().First(),
                PortfolioItems = _portfolio.Entity.GetAll().ToList()
            };
            return View(homeVm);
        }
        // configer download cv 
        [HttpGet]
        public ActionResult GetPdf()
        {
            string filePath = "~/file/LotfyE.pdf";
            Response.Headers.Add("Content-Disposition", "inline; filename=LotfyE.pdf");
            return File(filePath, "application/pdf");
        }
    }
}
