#nullable enable
using System.Threading;
using System.Threading.Tasks;

public interface IAuthService
{
    Task<bool> LoginAsync(string userName, string password, CancellationToken cancellationToken = default);
}