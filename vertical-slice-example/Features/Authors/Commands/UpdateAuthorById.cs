using MediatR;
using Microsoft.EntityFrameworkCore;
using vertical_slice_example.Data;
using vertical_slice_example.Exceptions;

namespace vertical_slice_example.Features.Authors.Commands;

public class UpdateAuthorByIdCommand : IRequest
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class UpdateAuthorByIdCommandHandler : IRequestHandler<UpdateAuthorByIdCommand>
{
    private readonly DataContext _dbContext;

    public UpdateAuthorByIdCommandHandler(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Handle(UpdateAuthorByIdCommand request, CancellationToken cancellationToken)
    {
        var author = await _dbContext.Authors.FirstOrDefaultAsync(x => x.Id == request.Id);
        if (author == null) throw new NotFoundException("Author not found");

        author.FirstName = request.FirstName;
        author.LastName = request.LastName;

        _dbContext.Update(author);
        await _dbContext.SaveChangesAsync();
    }
}