using System.Runtime.CompilerServices;
using EROrder.Shared.ViewModels;

namespace EROrder.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductListViewModel> GetProductsAsync([CallerMemberName] string methodName = "");
    }
}
