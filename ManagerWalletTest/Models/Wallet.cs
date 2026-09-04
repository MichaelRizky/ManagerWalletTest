using System;
using System.ComponentModel.DataAnnotations;

namespace ManagerWalletTest.Models
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        public User? User { get; set; }
    }
}