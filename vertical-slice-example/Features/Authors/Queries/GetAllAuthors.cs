using MediatR;
using Microsoft.EntityFrameworkCore;
using vertical_slice_example.Data;
using vertical_slice_example.Domain;

namespace vertical_slice_example.Features.Authors.Queries;

public class GetAllAuthorsQuery : IRequest<IEnumerable<Author>>
{
}

public class GetAllAuthorsQueryHandler : IRequestHandler<GetAllAuthorsQuery, IEnumerable<Author>>
{
    private readonly DataContext _dbContext;

    public GetAllAuthorsQueryHandler(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Author>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Authors.ToListAsync();
    }
}