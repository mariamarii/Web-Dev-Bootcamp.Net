using WebApplication3.Dtos.Product;
using WebApplication3.Models;

namespace WebApplication3.Services.Interfaces;

public interface IProductService
{
    Task<Response<ProductDto>> CreateProductAsync(ProductCreateDto createDto, string createdBy);
    Task<Response<IEnumerable<ProductDto>>> GetMyProductsAsync(string userId);
    Task<Response<IEnumerable<ProductDto>>> GetPendingProductsAsync();
    Task<Response<ProductDto>> ApproveProductAsync(int id);
    Task<Response<ProductDto>> RejectProductAsync(int id);
    Task<Response<IEnumerable<ProductDto>>> GetApprovedProductsAsync();
    Task<Response<ProductDto>> GetProductByIdAsync(int id);
}
