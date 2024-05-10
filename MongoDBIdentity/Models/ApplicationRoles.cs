using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace MongoDBIdentity.Models
{
    [CollectionName("Roles")]
    public class ApplicationRoles : MongoIdentityRole<Guid>
    {
    }
}
