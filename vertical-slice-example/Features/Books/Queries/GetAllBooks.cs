using MediatR;
using Microsoft.EntityFrameworkCore;
using vertical_slice_example.Data;
using vertical_slice_example.Domain;

namespace vertical_slice_example.Features.Books.Queries;

public class GetAllBooksQuery : IRequest<IEnumerable<Book>>
{
}

public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IEnumerable<Book>>
{
    private readonly DataContext _dbContext;

    public GetAllBooksQueryHandler(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Books.Include(x => x.Genre).Include(x => x.Author).ToListAsync();
    }
}