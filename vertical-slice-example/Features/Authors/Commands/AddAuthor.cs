using FluentValidation;
using MediatR;
using vertical_slice_example.Data;
using vertical_slice_example.Domain;

namespace vertical_slice_example.Features.Authors.Commands;

public class AddAuthorCommand : IRequest<Author>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class AddAuthorCommandValidator : AbstractValidator<AddAuthorCommand>
{
    public AddAuthorCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
    }   
}

public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Author>
{
    private readonly DataContext _dbContext;

    public AddAuthorCommandHandler(DataContext dbContext)
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