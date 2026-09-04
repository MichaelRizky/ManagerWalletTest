using System;

namespace ManagerWalletTest.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public Guid RecipientId { get; set; }
        public decimal Amount { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User? Sender { get; set; }
        public User? Recipient { get; set; }
    }
}