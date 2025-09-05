using AutoMapper;
using System.Net;
using WebApplication3.Dtos.Product;
using WebApplication3.Enum;
using WebApplication3.Models;
using WebApplication3.Repositories.Interfaces;
using WebApplication3.Services.Interfaces;

namespace WebApplication3.Services.Implementations;

public class ProductService(
    IGenericRepository<Product> productRepository,
    IMapper mapper,
    IWebHostEnvironment environment) : IProductService
{

    public async Task<Response<ProductDto>> CreateProductAsync(ProductCreateDto createDto, string createdBy)
    {
        try
        {
            var product = mapper.Map<Product>(createDto);
            product.CreatedBy = createdBy;
            product.Status = ProductStatus.Pending;

            if (createDto.Image != null)
            {
                var imageUrl = await SaveImageAsync(createDto.Image);
                product.ImageUrl = imageUrl;
            }

            await productRepository.AddAsync(product);
            await productRepository.SaveChangesAsync();

            var productDto = mapper.Map<ProductDto>(product);

            return new Response<ProductDto>
            {
                Status = true,
                StatusCode = HttpStatusCode.Created,
                Message = "Product created successfully",
                Data = productDto
            };
        }
        catch (Exception ex)
        {
            return new Response<ProductDto>
            {
                Status = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }

    public async Task<Response<IEnumerable<ProductDto>>> GetMyProductsAsync(string userId)
    {
        try
        {
            var products = await productRepository.FindAsync(p => p.CreatedBy == userId);
            var productDtos = mapper.Map<IEnumerable<ProductDto>>(products);

            return new Response<IEnumerable<ProductDto>>
            {
                Status = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Products retrieved successfully",
                Data = productDtos
            };
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<ProductDto>>
            {
                Status = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }

    public async Task<Response<IEnumerable<ProductDto>>> GetPendingProductsAsync()
    {
        try
        {
            var products = await productRepository.FindAsync(p => p.Status == ProductStatus.Pending);
            var productDtos = mapper.Map<IEnumerable<ProductDto>>(products);

            return new Response<IEnumerable<ProductDto>>
            {
                Status = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Pending products retrieved successfully",
                Data = productDtos
            };
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<ProductDto>>
            {
                Status = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }

    public async Task<Response<ProductDto>> ApproveProductAsync(int id)
    {
        try
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return new Response<ProductDto>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Product not found"
                };
            }

            product.Status = ProductStatus.Approved;
            productRepository.Update(product);
            await productRepository.SaveChangesAsync();

            var productDto = mapper.Map<ProductDto>(product);

            return new Response<ProductDto>
            {
                Status = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Product approved successfully",
                Data = productDto
            };
        }
        catch (Exception ex)
        {
            return new Response<ProductDto>
            {
                Status = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }

    public async Task<Response<ProductDto>> RejectProductAsync(int id)
    {
        try
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return new Response<ProductDto>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Product not found"
                };
            }

            product.Status = ProductStatus.Rejected;
            productRepository.Update(product);
            await productRepository.SaveChangesAsync();

            var productDto = mapper.Map<ProductDto>(product);

            return new Response<ProductDto>
            {
                Status = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Product rejected successfully",
                Data = productDto
            };
        }
        catch (Exception ex)
        {
            return new Response<ProductDto>
            {
                Status = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }

    public async Task<Response<IEnumerable<ProductDto>>> GetApprovedProductsAsync()
    {
        try
        {
            var products = await productRepository.FindAsync(p => p.Status == ProductStatus.Approved);
            var productDtos = mapper.Map<IEnumerable<ProductDto>>(products);

            return new Response<IEnumerable<ProductDto>>
            {
                Status = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Approved products retrieved successfully",
                Data = productDtos
            };
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<ProductDto>>
            {
                Status = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }

    public async Task<Response<ProductDto>> GetProductByIdAsync(int id)
    {
        try
        {
            var product = await productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return new Response<ProductDto>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Product not found"
                };
            }

            if (product.Status != ProductStatus.Approved)
            {
                return new Response<ProductDto>
                {
                    Status = false,
                    StatusCode = HttpStatusCode.NotFound,
                    Message = "Product not found"
                };
            }

            var productDto = mapper.Map<ProductDto>(product);

            return new Response<ProductDto>
            {
                Status = true,
                StatusCode = HttpStatusCode.OK,
                Message = "Product retrieved successfully",
                Data = productDto
            };
        }
        catch (Exception ex)
        {
            return new Response<ProductDto>
            {
                Status = false,
                StatusCode = HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
    }

    private async Task<string> SaveImageAsync(IFormFile image)
    {
        var webRootPath = environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var uploadsFolder = Path.Combine(webRootPath, "images", "products");
        
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(fileStream);
        }

        return "/images/products/" + uniqueFileName;
    }
}

