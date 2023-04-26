using MediatR;
using Microsoft.AspNetCore.Mvc;
using vertical_slice_example.Features.Authors.Commands;
using vertical_slice_example.Features.Authors.Queries;

namespace vertical_slice_example.Features.Authors;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAllAuthors()
    {
        var authors = await _mediator.Send(new GetAllAuthorsQuery());
        return Ok(authors.Select(author => new AuthorDto
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDto>> GetAuthorById([FromRoute] int id)
    {
        var author = await _mediator.Send(new GetAuthorByIdQuery(id));

        return Ok(new AuthorDto
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName
        });
    }

    [HttpPost]
    public async Task<ActionResult<AuthorDto>> AddBook([FromBody] AddAuthorCommand command)
    {
        var result = await _mediator.Send(command);
        var author = await _mediator.Send(new GetAuthorByIdQuery(result.Id));

        return CreatedAtAction(nameof(GetAuthorById), new { id = result.Id }, new AuthorDto
        {
            Id = author.Id,
            FirstName = author.FirstName,
            LastName = author.LastName
        });
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateAuthorById([FromRoute] int id, UpdateAuthorByIdCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuthorById([FromRoute] int id)
    {
        await _mediator.Send(new DeleteAuthorByIdCommand(id));
        return NoContent();
    }
}