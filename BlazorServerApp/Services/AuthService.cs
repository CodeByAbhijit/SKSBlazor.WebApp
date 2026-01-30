#nullable enable
using System.Threading;
using System.Threading.Tasks;

public class AuthService : IAuthService
{
    // POC stubbed authentication. Replace with real repo/EF.
    public Task<bool> LoginAsync(string userName, string password, CancellationToken cancellationToken = default)
    {
        // Demo credentials: admin / password
        var ok = userName == "admin" && password == "password";
        return Task.FromResult(ok);
    }

    // Simple convenience overload used by the component
    public Task<bool> LoginAsync(string userName, string password) => LoginAsync(userName, password, default);
}