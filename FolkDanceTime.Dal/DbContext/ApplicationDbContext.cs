using Duende.IdentityServer.EntityFramework.Options;
using FolkDanceTime.Dal.Entities;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

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
        public DbSet<ItemSet> ItemsSets { get; set; }
        public DbSet<ItemTransaction> ItemTransactions { get; set; }
        public DbSet<ItemSetTransaction> ItemSetTransactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyToCategory> PropertyToCategories { get; set; }
        public DbSet<PropertyValue> PropertyValues { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(u => u.Items)
                .WithOne(i => i.OwnerUser)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Entity<User>()
                .HasMany(u => u.OutgoingItemTransactions)
                .WithOne(it => it.SenderUser)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<User>()
                .HasMany(u => u.IncomingItemTransactions)
                .WithOne(it => it.ReceiverUser)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<User>()
                .HasMany(u => u.OutgoingItemSetTransactions)
                .WithOne(ist => ist.SenderUser)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<User>()
                .HasMany(u => u.IncomingItemSetTransactions)
                .WithOne(ist => ist.ReceiverUser)
                .OnDelete(DeleteBehavior.NoAction);

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
                .HasMany(set => set.Items)
                .WithOne(i => i.ItemSet);
            builder.Entity<ItemSet>()
                .HasMany(set => set.ItemSetTransactions)
                .WithOne(ist => ist.ItemSet);

            builder.Entity<ItemTransaction>()
                .HasOne(it => it.Item)
                .WithMany(i => i.ItemTransactions)
                .HasForeignKey(it => it.ItemId)
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
                .IsRequired();

            builder.Entity<ItemSetTransaction>()
                .HasOne(it => it.ItemSet)
                .WithMany(i => i.ItemSetTransactions)
                .HasForeignKey(it => it.ItemSetId)
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
                .IsRequired();

            builder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();
            builder.Entity<Category>()
                .HasMany(c => c.Items)
                .WithOne(i => i.Category);
            builder.Entity<Category>()
                .HasMany(c => c.CategoryToProperties)
                .WithOne(ctp => ctp.Category);

            builder.Entity<Property>()
                .HasMany(p => p.PropertyToCategories)
                .WithOne(ptc => ptc.Property);
            builder.Entity<Property>()
                .HasMany(p => p.PropertyValues)
                .WithOne(pv => pv.Property);

            builder.Entity<PropertyToCategory>()
                .HasOne(it => it.Property)
                .WithMany(u => u.PropertyToCategories)
                .HasForeignKey(it => it.PropertyId)
                .IsRequired();
            builder.Entity<PropertyToCategory>()
                .HasOne(it => it.Category)
                .WithMany(u => u.CategoryToProperties)
                .HasForeignKey(it => it.CategoryId)
                .IsRequired();

            builder.Entity<PropertyValue>()
                .HasOne(it => it.Item)
                .WithMany(u => u.PropertyValues)
                .HasForeignKey(it => it.ItemId)
                .IsRequired();
            builder.Entity<PropertyValue>()
                .HasOne(it => it.Property)
                .WithMany(u => u.PropertyValues)
                .HasForeignKey(it => it.PropertyId)
                .IsRequired();
        }
    }
}