using System;
using System.Collections.Generic;
using WebTest.Core;

namespace WebTest.DAL
{
    public partial class User:BaseEntity
    {
        public int Id { get; set; }
        public Guid UserGuid { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int AffiliateId { get; set; }
        public bool IsTaxExempt { get; set; }
        public int VendorId { get; set; }
        public bool HasShoppingCartItems { get; set; }
        public bool RequireReLogin { get; set; }
        public int FailedLoginAttempts { get; set; }
        public DateTime CannotLoginUntilDateUtc { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public bool IsSystemAccount { get; set; }
        public string SystemName { get; set; }
        public string LastIpAddress { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? LastLoginDateUtc { get; set; }
        public DateTime LastActivityDateUtc { get; set; }
        public int RegisteredInStoreId { get; set; }
    }
}
