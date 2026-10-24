using MongoDbGenericRepository.Models;
using System.ComponentModel.DataAnnotations;

namespace CRM.IdentityServer.Entities {
    public class Client : Document{
        [Required]
        public string Secret { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public ApplicationTypes ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }

        [MaxLength(100)]
        public string AllowedOrigin { get; set; }

        public Client() { }
    }
}
