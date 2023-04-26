using MediatR;
using Microsoft.EntityFrameworkCore;
using vertical_slice_example.Data;
using vertical_slice_example.Domain;
using vertical_slice_example.Exceptions;

namespace vertical_slice_example.Features.Books.Queries;

public class GetBookByIdQuery : IRequest<Book>
{
    public GetBookByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, Book>
{
    private readonly DataContext _dbContext;

    public GetBookByIdQueryHandler(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _dbContext.Books.Include(x => x.Genre).Include(x => x.Author).FirstOrDefaultAsync(x => x.Id == request.Id);
        if (book == null) throw new NotFoundException("Book not found");

        return book;
    }
}