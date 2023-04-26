using MediatR;
using Microsoft.EntityFrameworkCore;
using vertical_slice_example.Data;
using vertical_slice_example.Domain;
using vertical_slice_example.Exceptions;

namespace vertical_slice_example.Features.Authors.Queries;

public class GetAuthorByIdQuery : IRequest<Author>
{
    public GetAuthorByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}

public class GetBookByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
{
    private readonly DataContext _dbContext;

    public GetBookByIdQueryHandler(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Author> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (author == null) throw new NotFoundException("Author not found");

        return author;
    }
}