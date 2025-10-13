using Serilog;
using System.Runtime.CompilerServices;
using EROrder.Core.Services.Interfaces;
using EROrder.Shared.ViewModels;

namespace EROrder.Core.Services
{
    public class ProductService : IProductService
    {
        public async Task<ProductListViewModel> GetProductsAsync([CallerMemberName] string methodName = "")
        {
            try
            {
                await Task.Yield(); // 模擬非同步操作

                var products = new List<ProductViewModel>
                {
                    new() 
                    {
                        Id = 1,
                        Name = "筆記型電腦",
                        Description = "高效能工作用筆記型電腦",
                        Price = 35000,
                        CreatedDate = DateTime.Now.AddDays(-30),
                        IsActive = true
                    },
                    new() 
                    {
                        Id = 2,
                        Name = "無線滑鼠",
                        Description = "藍芽無線滑鼠，適合辦公使用",
                        Price = 800,
                        CreatedDate = DateTime.Now.AddDays(-15),
                        IsActive = true
                    },
                    new() 
                    {
                        Id = 3,
                        Name = "機械鍵盤",
                        Description = "RGB背光機械鍵盤",
                        Price = 2500,
                        CreatedDate = DateTime.Now.AddDays(-5),
                        IsActive = false
                    }
                };

                var viewModel = new ProductListViewModel
                {
                    Products = products,
                    PageTitle = "產品列表",
                    TotalCount = products.Count
                };

                return viewModel;
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"[{methodName}] 取得產品列表時發生錯誤 {ex.Message}");
                return new ProductListViewModel
                {
                    Products = [],
                    PageTitle = "產品列表",
                    TotalCount = 0
                };
            }
        }
    }
}
