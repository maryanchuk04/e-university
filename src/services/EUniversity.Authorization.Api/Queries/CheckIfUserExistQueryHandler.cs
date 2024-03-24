using MediatR;

namespace EUniversity.Authorization.Api.Queries;

public class CheckIfUserExistQuery(string email) : IRequest<bool>
{
    public string Email { get; set; } = email;
}

public class CheckIfUserExistQueryHandler : IRequestHandler<CheckIfUserExistQuery, bool>
{
    public Task<bool> Handle(CheckIfUserExistQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return Task.FromResult(true);
        }
        catch (Exception)
        {
            return Task.FromResult(false);
        }
    }
}
