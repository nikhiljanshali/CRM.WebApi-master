using MongoDbGenericRepository.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace CRM.IdentityServer.Entities {
    public class RefreshToken : Document {
        [Required]
        [MaxLength(50)]
        public string ClientId { get; set; }

        public string DeviceId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required(AllowEmptyStrings =false, ErrorMessage ="User name is required!")]
        public string UserName { get; set; }

        public DateTime IssuedUtc { get; set; }
        public DateTime ExpiresUtc { get; set; }

        [Required]
        public string ProtectedTicket { get; set; }

        public RefreshToken() { }
    }
}
