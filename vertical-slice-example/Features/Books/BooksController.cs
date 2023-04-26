using MediatR;
using Microsoft.AspNetCore.Mvc;
using vertical_slice_example.Features.Books.Commands;
using vertical_slice_example.Features.Books.Queries;

namespace vertical_slice_example.Features.Books;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
    {
        var books = await _mediator.Send(new GetAllBooksQuery());
        return Ok(books.Select(book => new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Publisher = book.Publisher,
            Description = book.Description,
            ReleaseDate = book.ReleaseDate,
            Author = $"{book.Author.FirstName} {book.Author.LastName}",
            Genre = book.Genre.Name
        }));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookDto>> GetBookById([FromRoute] int id)
    {
        var book = await _mediator.Send(new GetBookByIdQuery(id));

        return Ok(new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Publisher = book.Publisher,
            Description = book.Description,
            ReleaseDate = book.ReleaseDate,
            Author = $"{book.Author.FirstName} {book.Author.LastName}",
            Genre = book.Genre.Name
        });
    }

    [HttpPost]
    public async Task<ActionResult<BookDto>> AddBook([FromBody] AddBookCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetBookById), new { id = result.Id }, null);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdateBookById([FromRoute] int id, UpdateBookByIdCommand command)
    {
        command.Id = id;
        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBookById([FromRoute] int id)
    {
        await _mediator.Send(new DeleteBookByIdCommand(id));
        return NoContent();
    }
}