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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserAsset>()
            .HasOne(ua => ua.User)
            .WithMany(u => u.Assets)
            .HasForeignKey(ua => ua.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Address>()
            .HasOne(a => a.User)
            .WithOne()
            .HasForeignKey<Address>(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<UserPhoto>()
            .HasOne(up => up.User)
            .WithMany()
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        //Seeed
        //Task.Run(() =>
        //{
        //    Seed(modelBuilder);
        //}).Wait();
    }
    private void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User() { FirstName = "John", LastName = "Doe", Phone = "123-456-7890", Email = "john.doe@email.com", Password = "password123", RoleId = 1 },
            new User() { FirstName = "Jane", LastName = "Smith", Phone = "987-654-3210", Email = "jane.smith@email.com", Password = "securepass", RoleId = 2 },
            new User() { FirstName = "Alex", LastName = "Johnson", Phone = "555-123-4567", Email = "alex.johnson@email.com", Password = "pass123", RoleId = 1 },
            new User() { FirstName = "Emily", LastName = "Williams", Phone = "777-888-9999", Email = "emily.williams@email.com", Password = "qwerty", RoleId = 3 },
            new User() { FirstName = "Chris", LastName = "Brown", Phone = "444-333-2222", Email = "chris.brown@email.com", Password = "abc123", RoleId = 2 },
            new User() { FirstName = "Eva", LastName = "Miller", Phone = "111-222-3333", Email = "eva.miller@email.com", Password = "passpass", RoleId = 1 },
            new User() { FirstName = "Sam", LastName = "Taylor", Phone = "666-999-4444", Email = "sam.taylor@email.com", Password = "letmein", RoleId = 3 },
            new User() { FirstName = "Olivia", LastName = "Davis", Phone = "222-555-8888", Email = "olivia.davis@email.com", Password = "mypassword", RoleId = 2 },
            new User() { FirstName = "Daniel", LastName = "Anderson", Phone = "888-777-6666", Email = "daniel.anderson@email.com", Password = "daniel123", RoleId = 1 },
            new User() { FirstName = "Sophia", LastName = "White", Phone = "333-666-9999", Email = "sophia.white@email.com", Password = "sophia456", RoleId = 3 }
        );

        modelBuilder.Entity<Address>().HasData(
            new Address() { UserId = 1, City = "New York", Street = "Broadway", Floor = "5th", Home = "Apt 501" },
            new Address() { UserId = 2, City = "Los Angeles", Street = "Hollywood Blvd", Floor = "2nd", Home = "Suite 203" },
            new Address() { UserId = 3, City = "Chicago", Street = "Michigan Ave", Floor = "8th", Home = "Unit 801" },
            new Address() { UserId = 4, City = "San Francisco", Street = "Market St", Floor = "12th", Home = "Apt 1203" },
            new Address() { UserId = 5, City = "Miami", Street = "Ocean Dr", Floor = "4th", Home = "Condo 402" },
            new Address() { UserId = 6, City = "Seattle", Street = "Pike Pl", Floor = "3rd", Home = "Apt 301" },
            new Address() { UserId = 7, City = "Dallas", Street = "Main St", Floor = "6th", Home = "Suite 601" },
            new Address() { UserId = 8, City = "Denver", Street = "Colfax Ave", Floor = "9th", Home = "Unit 902" },
            new Address() { UserId = 9, City = "Atlanta", Street = "Peachtree St", Floor = "7th", Home = "Apt 701" },
            new Address() { UserId = 10, City = "Boston", Street = "Newbury St", Floor = "11th", Home = "Apt 1101" }
        );

        modelBuilder.Entity<UserAsset>().HasData(
            new UserAsset() { UserId = 1, Name = "Laptop", Description = "Dell XPS 13", IsExchanged = false },
            new UserAsset() { UserId = 2, Name = "Camera", Description = "Canon EOS R5", IsExchanged = true },
            new UserAsset() { UserId = 3, Name = "Guitar", Description = "Fender Stratocaster", IsExchanged = false },
            new UserAsset() { UserId = 4, Name = "Fitness Tracker", Description = "Fitbit Charge 4", IsExchanged = true },
            new UserAsset() { UserId = 5, Name = "Headphones", Description = "Sony WH-1000XM4", IsExchanged = false },
            new UserAsset() { UserId = 6, Name = "Smartwatch", Description = "Apple Watch Series 6", IsExchanged = true },
            new UserAsset() { UserId = 7, Name = "Drone", Description = "DJI Mavic Air 2", IsExchanged = false },
            new UserAsset() { UserId = 8, Name = "VR Headset", Description = "Oculus Quest 2", IsExchanged = true },
            new UserAsset() { UserId = 9, Name = "Bicycle", Description = "Trek FX 2", IsExchanged = false },
            new UserAsset() { UserId = 10, Name = "Book Collection", Description = "Various genres", IsExchanged = true }
        );

        modelBuilder.Entity<UserAssetPhoto>().HasData(
            new UserAssetPhoto() { UserAssetId = 1, Name = "Laptop Photo", Path = "/photos/laptop.jpg", Extension = "jpg", Size = 1024, Type = "image/jpeg" },
            new UserAssetPhoto() { UserAssetId = 2, Name = "Camera Photo", Path = "/photos/camera.jpg", Extension = "jpg", Size = 2048, Type = "image/jpeg" },
            new UserAssetPhoto() { UserAssetId = 3, Name = "Guitar Photo", Path = "/photos/guitar.jpg", Extension = "jpg", Size = 1536, Type = "image/jpeg" },
            new UserAssetPhoto() { UserAssetId = 4, Name = "Fitness Tracker Photo", Path = "/photos/fitnesstracker.jpg", Extension = "jpg", Size = 512, Type = "image/jpeg" },
            new UserAssetPhoto() { UserAssetId = 5, Name = "Headphones Photo", Path = "/photos/headphones.jpg", Extension = "jpg", Size = 768, Type = "image/jpeg" },
            new UserAssetPhoto() { UserAssetId = 6, Name = "Smartwatch Photo", Path = "/photos/smartwatch.jpg", Extension = "jpg", Size = 1280, Type = "image/jpeg" },
            new UserAssetPhoto() { UserAssetId = 7, Name = "Drone Photo", Path = "/photos/drone.jpg", Extension = "jpg", Size = 2560, Type = "image/jpeg" },
            new UserAssetPhoto() { UserAssetId = 8, Name = "VR Headset Photo", Path = "/photos/vrheadset.jpg", Extension = "jpg", Size = 1792, Type = "image/jpeg" },
            new UserAssetPhoto() { UserAssetId = 9, Name = "Bicycle Photo", Path = "/photos/bicycle.jpg", Extension = "jpg", Size = 768, Type = "image/jpeg" },
            new UserAssetPhoto() { UserAssetId = 10, Name = "Book Collection Photo", Path = "/photos/books.jpg", Extension = "jpg", Size = 1024, Type = "image/jpeg" }
        );

        modelBuilder.Entity<UserPhoto>().HasData(
            new UserPhoto() { UserId = 1, Name = "John's Photo", Path = "/photos/john.jpg", Extension = "jpg", Size = 1024, Type = "image/jpeg" },
            new UserPhoto() { UserId = 2, Name = "Jane's Photo", Path = "/photos/jane.jpg", Extension = "jpg", Size = 2048, Type = "image/jpeg" },
            new UserPhoto() { UserId = 3, Name = "Alex's Photo", Path = "/photos/alex.jpg", Extension = "jpg", Size = 1536, Type = "image/jpeg" },
            new UserPhoto() { UserId = 4, Name = "Emily's Photo", Path = "/photos/emily.jpg", Extension = "jpg", Size = 512, Type = "image/jpeg" },
            new UserPhoto() { UserId = 5, Name = "Chris's Photo", Path = "/photos/chris.jpg", Extension = "jpg", Size = 768, Type = "image/jpeg" },
            new UserPhoto() { UserId = 6, Name = "Eva's Photo", Path = "/photos/eva.jpg", Extension = "jpg", Size = 1280, Type = "image/jpeg" },
            new UserPhoto() { UserId = 7, Name = "Sam's Photo", Path = "/photos/sam.jpg", Extension = "jpg", Size = 2560, Type = "image/jpeg" },
            new UserPhoto() { UserId = 8, Name = "Olivia's Photo", Path = "/photos/olivia.jpg", Extension = "jpg", Size = 1792, Type = "image/jpeg" },
            new UserPhoto() { UserId = 9, Name = "Daniel's Photo", Path = "/photos/daniel.jpg", Extension = "jpg", Size = 768, Type = "image/jpeg" },
            new UserPhoto() { UserId = 10, Name = "Sophia's Photo", Path = "/photos/sophia.jpg", Extension = "jpg", Size = 1024, Type = "image/jpeg" }
        );

        modelBuilder.Entity<Permission>().HasData(
            new Permission() { Name = "Read" },
            new Permission() { Name = "Write" },
            new Permission() { Name = "Delete" },
            new Permission() { Name = "Upload" },
            new Permission() { Name = "Download" },
            new Permission() { Name = "Execute" },
            new Permission() { Name = "Admin" },
            new Permission() { Name = "ManageUsers" },
            new Permission() { Name = "ManageRoles" },
            new Permission() { Name = "CustomPermission" }
        );

        modelBuilder.Entity<Role>().HasData(
            new Role() { Name = "Admin" },
            new Role() { Name = "Manager" },
            new Role() { Name = "User" },
            new Role() { Name = "SuperAdmin" }
        );

        modelBuilder.Entity<RolePermission>().HasData(
            new RolePermission() { RoleId = 1, PermissonId = 1 }, // Admin - Read
            new RolePermission() { RoleId = 1, PermissonId = 2 }, // Admin - Write
            new RolePermission() { RoleId = 1, PermissonId = 3 }, // Admin - Delete
            new RolePermission() { RoleId = 1, PermissonId = 7 }, // Admin - Admin
            new RolePermission() { RoleId = 1, PermissonId = 8 }, // Admin - ManageUsers
            new RolePermission() { RoleId = 1, PermissonId = 9 }, // Admin - ManageRoles

            new RolePermission() { RoleId = 2, PermissonId = 1 }, // Manager - Read
            new RolePermission() { RoleId = 2, PermissonId = 2 }, // Manager - Write
            new RolePermission() { RoleId = 2, PermissonId = 3 }, // Manager - Delete

            new RolePermission() { RoleId = 3, PermissonId = 1 }, // User - Read

            new RolePermission() { RoleId = 4, PermissonId = 7 } // SuperAdmin - Admin
        );
    }
}
