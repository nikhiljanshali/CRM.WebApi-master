using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;

namespace CRM.IdentityServer.Entities {
    [CollectionName("Users")]
    public class ApplicationUser : MongoIdentityUser<Guid> {
        public string Ident { get; set; }
        public string Code { get; set; }
        public string Origin { get; set; }

        public ApplicationUser() : base() { }
        public ApplicationUser(string userName, string email) : base(userName, email) { }
    }
}
