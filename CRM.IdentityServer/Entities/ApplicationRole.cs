using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;

namespace CRM.IdentityServer.Entities {
	[CollectionName("Roles")]
    public class ApplicationRole : MongoIdentityRole<Guid> {

		public ApplicationRole() : base() {}
		public ApplicationRole(string roleName) : base(roleName) {}
	}
}
