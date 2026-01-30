#nullable enable
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

public partial class FrmLoginBase : ComponentBase
{
    protected LoginDto Model { get; set; } = new();
    protected bool IsProcessing { get; set; }
    protected string? Message { get; set; }
    protected string MessageCss { get; set; } = "alert-info";

    [Inject] public IAuthService AuthService { get; set; } = default!;
    [Inject] public NavigationManager Nav { get; set; } = default!;

    protected async Task HandleValidSubmit()
    {
        IsProcessing = true;
        Message = null;
        try
        {
            var ok = await AuthService.LoginAsync(Model.UserName!, Model.Password!);
            if (ok)
            {
                Message = "Login succeeded";
                MessageCss = "alert-success";
                // navigate to home/dashboard
                Nav.NavigateTo("/products");
            }
            else
            {
                Message = "Invalid username or password";
                MessageCss = "alert-danger";
            }
        }
        catch
        {
            Message = "An error occurred during login";
            MessageCss = "alert-danger";
        }
        finally
        {
            IsProcessing = false;
        }
    }

    protected void Cancel()
    {
        // In the VB6 app Cancel closed the form. In web app, navigate away.
        Nav.NavigateTo("/");
    }
}