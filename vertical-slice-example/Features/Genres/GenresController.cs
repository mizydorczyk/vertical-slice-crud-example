using MediatR;
using Microsoft.AspNetCore.Mvc;
using vertical_slice_example.Features.Genres.Queries;

namespace vertical_slice_example.Features.Genres;

[Route("api/[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly IMediator _mediator;

    public GenresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GenreDto>>> GetAllGenres()
    {
        var genres = await _mediator.Send(new GetAllGenresQuery());
        return Ok(genres.Select(genre => new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GenreDto>> GetGenreById([FromRoute] int id)
    {
        var genre = await _mediator.Send(new GetGenreByIdQuery(id));

        return Ok(new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name
        });
    }
}