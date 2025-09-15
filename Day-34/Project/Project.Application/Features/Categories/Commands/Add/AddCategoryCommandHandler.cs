using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Categories;
using Project.Domain.Responses;

namespace Project.Application.Features.Categories.Commands.Add;

public class AddCategoryCommandHandler(IMapper mapper, IRepository<Category> categoryRepository) : ICommandHandler<AddCategoryCommand, Guid>
{
    public async Task<Response<Guid>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = mapper.Map<Category>(request);
        await categoryRepository.AddAsync(category, cancellationToken);
        return  Response<Guid>.Created(category.Id);
    }
}