using MediatR;
using Microsoft.EntityFrameworkCore;
using vertical_slice_example.Data;
using vertical_slice_example.Exceptions;

namespace vertical_slice_example.Features.Authors.Commands;

public class DeleteAuthorByIdCommand : IRequest
{
    public DeleteAuthorByIdCommand(int id)
    {
        Id = id;
    }

    public int Id { get; }
}

public class DeleteAuthorByIdCommandHandler : IRequestHandler<DeleteAuthorByIdCommand>
{
    private readonly DataContext _dbContext;

    public DeleteAuthorByIdCommandHandler(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteAuthorByIdCommand request, CancellationToken cancellationToken)
    {
        var author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (author == null) throw new NotFoundException("Author not found");

        _dbContext.Remove(author);
        await _dbContext.SaveChangesAsync();
    }
}