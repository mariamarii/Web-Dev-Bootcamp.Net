using Project.Application.Features.Categories.Commands.Add;
using Project.Application.Features.Categories.Commands.Delete;
using Project.Application.Features.Categories.Commands.Update;
using Project.Application.Features.Categories.Dtos;

namespace Project.Application.Mapping.Category;

public class CategoryProfile : AutoMapper.Profile
{
    public CategoryProfile()
    {
        CreateMap<Domain.Models.Categories.Category, AddCategoryCommand>().ReverseMap();
        CreateMap<Domain.Models.Categories.Category, CategoryDto>().ReverseMap();
        CreateMap<Domain.Models.Categories.Category, UpdateCategoryCommand>().ReverseMap();
        CreateMap<Domain.Models.Categories.Category, DeleteCategoryCommand>().ReverseMap();
    }
}