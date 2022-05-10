using Duende.IdentityServer.EntityFramework.Options;
using FolkDanceTime.Dal.Entities;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace FolkDanceTime.Dal.DbContext
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<User>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemSet> ItemSets { get; set; }
        public DbSet<ItemTransaction> ItemTransactions { get; set; }
        public DbSet<ItemSetTransaction> ItemSetTransactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyValue> PropertyValues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(u => u.Items)
                .WithOne(i => i.OwnerUser);
            builder.Entity<User>()
                .HasMany(u => u.ItemSets)
                .WithOne(i => i.OwnerUser);
            builder.Entity<User>()
                .HasMany(u => u.OutgoingItemTransactions)
                .WithOne(it => it.SenderUser);
            builder.Entity<User>()
                .HasMany(u => u.IncomingItemTransactions)
                .WithOne(it => it.ReceiverUser);
            builder.Entity<User>()
                .HasMany(u => u.OutgoingItemSetTransactions)
                .WithOne(ist => ist.SenderUser);
            builder.Entity<User>()
                .HasMany(u => u.IncomingItemSetTransactions)
                .WithOne(ist => ist.ReceiverUser);

            builder.Entity<Item>()
                .HasIndex(i => i.Name)
                .IsUnique();
            builder.Entity<Item>()
                .HasOne(i => i.OwnerUser)
                .WithMany(u => u.Items)
                .HasForeignKey(i => i.OwnerUserId)
                .IsRequired();
            builder.Entity<Item>()
                .HasOne(i => i.Category)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.CategoryId)
                .IsRequired();
            builder.Entity<Item>()
                .HasOne(i => i.ItemSet)
                .WithMany(set => set.Items)
                .HasForeignKey(i => i.ItemSetId);
            builder.Entity<Item>()
                .HasMany(i => i.ItemTransactions)
                .WithOne(it => it.Item);
            builder.Entity<Item>()
                .HasMany(i => i.PropertyValues)
                .WithOne(pv => pv.Item);

            builder.Entity<ItemSet>()
                .HasOne(i => i.OwnerUser)
                .WithMany(u => u.ItemSets)
                .HasForeignKey(i => i.OwnerUserId)
                .IsRequired();
            builder.Entity<ItemSet>()
                .HasMany(set => set.Items)
                .WithOne(i => i.ItemSet);
            builder.Entity<ItemSet>()
                .HasMany(set => set.ItemSetTransactions)
                .WithOne(ist => ist.ItemSet);

            builder.Entity<ItemTransaction>()
                .HasOne(it => it.Item)
                .WithMany(i => i.ItemTransactions)
                .HasForeignKey(it => it.ItemId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            builder.Entity<ItemTransaction>()
                .HasOne(it => it.ReceiverUser)
                .WithMany(u => u.IncomingItemTransactions)
                .HasForeignKey(it => it.ReceiverUserId)
                .IsRequired();
            builder.Entity<ItemTransaction>()
                .HasOne(it => it.SenderUser)
                .WithMany(u => u.OutgoingItemTransactions)
                .HasForeignKey(it => it.SenderUserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Entity<ItemSetTransaction>()
                .HasOne(it => it.ItemSet)
                .WithMany(i => i.ItemSetTransactions)
                .HasForeignKey(it => it.ItemSetId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();
            builder.Entity<ItemSetTransaction>()
                .HasOne(it => it.ReceiverUser)
                .WithMany(u => u.IncomingItemSetTransactions)
                .HasForeignKey(it => it.ReceiverUserId)
                .IsRequired();
            builder.Entity<ItemSetTransaction>()
                .HasOne(it => it.SenderUser)
                .WithMany(u => u.OutgoingItemSetTransactions)
                .HasForeignKey(it => it.SenderUserId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();
            builder.Entity<Category>()
                .HasMany(c => c.Items)
                .WithOne(i => i.Category);
            builder.Entity<Category>()
                .HasMany(c => c.Properties)
                .WithOne(p => p.Category);

            builder.Entity<Property>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Properties)
                .HasForeignKey(p => p.CategoryId)
                .IsRequired();
            builder.Entity<Property>()
                .HasMany(p => p.PropertyValues)
                .WithOne(pv => pv.Property);

            builder.Entity<PropertyValue>()
                .HasOne(it => it.Item)
                .WithMany(u => u.PropertyValues)
                .HasForeignKey(it => it.ItemId)
                .IsRequired();
            builder.Entity<PropertyValue>()
                .HasOne(it => it.Property)
                .WithMany(u => u.PropertyValues)
                .HasForeignKey(it => it.PropertyId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            // TODO új regisztrációt hozzáadni a Dancer role-hoz
            // Api projecten jobb klikk / Add / New Scaffolded Item / bal oldalon Identity

            var passwordHasher = new PasswordHasher<User>();

            var adminId = Guid.NewGuid().ToString();

            var admin = new User
            {
                Id = adminId,
                UserName = "admin@folkdancetime.com",
                Email = "admin@folkdancetime.com",
                NormalizedUserName = "ADMIN@FOLKDANCETIME.COM",
                NormalizedEmail = "ADMIN@FOLKDANCETIME.COM",
                LockoutEnabled = true,
                EmailConfirmed = true,
            };
            admin.PasswordHash = passwordHasher.HashPassword(admin, "P@ssword1");

            builder.Entity<User>()
                .HasData(admin);

            builder.Entity<IdentityUserClaim<string>>()
                .HasData(
                    new IdentityUserClaim<string>
                    {
                        Id = 1,
                        UserId = adminId,
                        ClaimType = ClaimTypes.Role,
                        ClaimValue = "Admin",
                    }
                );

            builder.Entity<IdentityRole>()
                .HasData(new[]
                {
                    new IdentityRole
                    {
                        Id = "AdminRoleId",
                        Name = "Admin",
                        NormalizedName = "ADMIN",
                    },
                    new IdentityRole
                    {
                        Id = "DancerRoleId",
                        Name = "Dancer",
                        NormalizedName = "DANCER",
                    },
                });

            builder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>
                {
                    UserId = adminId,
                    RoleId = "AdminRoleId",
                });

            builder.Entity<IdentityRoleClaim<string>>()
                .HasData(
                    new IdentityRoleClaim<string>
                    {
                        Id = 1,
                        RoleId = "AdminRoleId",
                        ClaimType = ClaimTypes.Role,
                        ClaimValue = "Admin",
                    }
                );
        }
    }
}