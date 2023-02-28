using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.CreateNote;

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Guid>
{
    private INotesDbContext _dbContext;

    public CreateNoteCommandHandler(INotesDbContext dbContext) => _dbContext = dbContext;
    
    public async Task<Guid> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = new Note
        {
            Id = new Guid(),
            UserId = request.UserId,
            Title = request.Title,
            Details = request.Details,
            CreationDate = DateTime.Now,
            EditDate = null,
        };

        await _dbContext.Notes.AddAsync(note, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return note.Id;
    }
}