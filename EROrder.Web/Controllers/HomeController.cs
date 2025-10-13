using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EROrder.Core.Services.Interfaces;
using EROrder.Shared.ViewModels;

namespace EROrder.Web.Controllers
{
    public class HomeController(IProductService productService) : Controller
    {
        private readonly IProductService _productService = productService;

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Products()
        {
            var viewModel = await _productService.GetProductsAsync();
            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
