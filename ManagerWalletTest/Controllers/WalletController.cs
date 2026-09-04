using ManagerWalletTest.Data;
using ManagerWalletTest.DTOs;
using ManagerWalletTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ManagerWalletTest.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WalletController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);

            if (wallet == null) return NotFound("Wallet not found.");

            return Ok(new { wallet.Id, wallet.Balance });
        }

        [HttpPost("topup")]
        public async Task<IActionResult> TopUp(TopUpRequest request)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
                if (wallet == null) return NotFound("Wallet not found.");

                wallet.Balance += request.Amount;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { message = "Top up successful", newBalance = wallet.Balance });
            }
            catch
            {
                await transaction.RollbackAsync();
                return StatusCode(500, "An error occurred during top up.");
            }
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer(TransferRequest request)
        {
            var senderId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            // Cari user penerima berdasarkan email
            var recipientUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.RecipientEmail);
            if (recipientUser == null)
                return NotFound("Recipient email not found.");

            if (senderId == recipientUser.Id)
                return BadRequest("You cannot transfer money to yourself.");

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var senderWallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == senderId);
                var recipientWallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == recipientUser.Id);

                if (senderWallet == null || recipientWallet == null)
                    return NotFound("Sender or recipient wallet not found.");

                if (senderWallet.Balance < request.Amount)
                    return BadRequest("Insufficient balance.");

                // Mutasi saldo
                senderWallet.Balance -= request.Amount;
                recipientWallet.Balance += request.Amount;

                // Catat transaksi
                var txn = new Transaction
                {
                    Id = Guid.NewGuid(),
                    SenderId = senderId,
                    RecipientId = recipientUser.Id,
                    Amount = request.Amount,
                    Notes = request.Notes
                };

                _context.Transactions.Add(txn);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { message = "Transfer successful", remainingBalance = senderWallet.Balance });
            }
            catch
            {
                await transaction.RollbackAsync();
                return StatusCode(500, "An error occurred during transfer.");
            }
        }
    }
}