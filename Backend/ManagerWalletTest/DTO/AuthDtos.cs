using System.ComponentModel.DataAnnotations;

namespace ManagerWalletTest.DTOs
{
    public record RegisterRequest(
        [Required, EmailAddress] string Email,
        [Required, MinLength(6)] string Password,
        [Required] string ConfirmPassword
    );

    public record LoginRequest(
        [Required, EmailAddress] string Email,
        [Required] string Password
    );
}