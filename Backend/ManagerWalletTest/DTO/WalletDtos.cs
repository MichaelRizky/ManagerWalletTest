using System.ComponentModel.DataAnnotations;

namespace ManagerWalletTest.DTOs
{
    public record TransferRequest(
        [Required, EmailAddress] string RecipientEmail,
        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")] decimal Amount,
        string Notes = ""
    );

    public record TopUpRequest(
        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")] decimal Amount
    );
}