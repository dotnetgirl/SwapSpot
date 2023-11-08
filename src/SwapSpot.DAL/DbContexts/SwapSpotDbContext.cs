using Microsoft.EntityFrameworkCore;
using SwapSpot.Domain.Entities.Users;
using SwapSpot.Domain.Authorizations;
using SwapSpot.Domain.Entities.Assets;
using SwapSpot.Domain.Entities.Photos;
using SwapSpot.Domain.Entities.Messages;
using SwapSpot.Domain.Entities.Addresses;

namespace SwapSpot.DAL.DbContexts;

public class SwapSpotDbContext : DbContext
{
    public SwapSpotDbContext(DbContextOptions<SwapSpotDbContext> options)
            : base(options)
    {
    }

    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<UserAssetPhoto> UserAssetPhotos { get; set; }
    DbSet<UserPhoto> UserPhotos { get; set; }
    DbSet<Message> Messages { get; set; }
    DbSet<Address> Addresses { get; set; }
    DbSet<UserAsset> UserAssets { get; set; }
    DbSet<Permission> Permissions { get; set; }
    DbSet<RolePermission> RolePermissions { get; set; }

    //Fluent Api

    // Seed
}
