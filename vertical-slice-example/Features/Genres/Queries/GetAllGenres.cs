using MediatR;
using Microsoft.EntityFrameworkCore;
using vertical_slice_example.Data;
using vertical_slice_example.Domain;

namespace vertical_slice_example.Features.Genres.Queries;

public class GetAllGenresQuery : IRequest<IEnumerable<Genre>>
{
}

public class GetAllGenresQueryHandler : IRequestHandler<GetAllGenresQuery, IEnumerable<Genre>>
{
    private readonly DataContext _dbContext;

    public GetAllGenresQueryHandler(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Genre>> Handle(GetAllGenresQuery request, CancellationToken cancellationToken)
    {
        return await _dbContext.Genres.ToListAsync();
    }
}