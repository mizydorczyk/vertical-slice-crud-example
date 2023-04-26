using MediatR;
using Microsoft.EntityFrameworkCore;
using vertical_slice_example.Data;
using vertical_slice_example.Domain;
using vertical_slice_example.Exceptions;

namespace vertical_slice_example.Features.Genres.Queries;

public class GetGenreByIdQuery : IRequest<Genre>
{
    public GetGenreByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; }
}

public class GetGenreByIdQueryHandler : IRequestHandler<GetGenreByIdQuery, Genre>
{
    private readonly DataContext _dbContext;

    public GetGenreByIdQueryHandler(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Genre> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        var genre = await _dbContext.Genres.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (genre == null) throw new NotFoundException("Genre not found");

        return genre;
    }
}