using AutoMapper;
using Project.Application.Abstractions.Messaging;
using Project.Application.Abstractions.Repositories;
using Project.Domain.Models.Categories;
using Project.Domain.Responses;

namespace Project.Application.Features.Categories.Commands.Delete;

public class DeleteCategoryCommandHandler (IMapper mapper,
    IRepository<Category> categoryRepository) : ICommandHandler<DeleteCategoryCommand, Guid>
{
    public async Task<Response<Guid>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.Id, cancellationToken);
        await categoryRepository.DeleteAsync(category, cancellationToken);
        return Response<Guid>.Success(category.Id);
    }
    
}