using MediatR;
using vertical_slice_example.Data;
using vertical_slice_example.Domain;

namespace vertical_slice_example.Features.Authors.Commands;

public class AddAuthorCommand : IRequest<Author>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class AddBookCommandHandler : IRequestHandler<AddAuthorCommand, Author>
{
    private readonly DataContext _dbContext;

    public AddBookCommandHandler(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = new Author
        {
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        _dbContext.Authors.Add(author);
        await _dbContext.SaveChangesAsync();

        return author;
    }
}