using AspNetCore.Identity.MongoDbCore.Models;
using IdentityServer4.EntityFramework.Entities;
using MongoDbGenericRepository.Attributes;

namespace MongoDBIdentity.Models
{
    [CollectionName("Users")]
    public class ApplicationUsers : MongoIdentityUser<Guid>
    {
        public virtual ICollection<UserClaim> Claims { get; set; }

    }
}
