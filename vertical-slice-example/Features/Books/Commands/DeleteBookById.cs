using MediatR;
using Microsoft.EntityFrameworkCore;
using vertical_slice_example.Data;
using vertical_slice_example.Exceptions;

namespace vertical_slice_example.Features.Books.Commands;

public class DeleteBookByIdCommand : IRequest
{
    public DeleteBookByIdCommand(int id)
    {
        Id = id;
    }

    public int Id { get; }
}

public class DeleteBookByIdCommandHandler : IRequestHandler<DeleteBookByIdCommand>
{
    private readonly DataContext _dbContext;

    public DeleteBookByIdCommandHandler(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(DeleteBookByIdCommand request, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (book == null) throw new NotFoundException("Book not found");

        _dbContext.Remove(book);
        await _dbContext.SaveChangesAsync();
    }
}