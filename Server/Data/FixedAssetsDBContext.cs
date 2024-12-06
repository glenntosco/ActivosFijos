using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ActivosFiljos.Server.Models.FixedAssetsDB;

namespace ActivosFiljos.Server.Data
{
    public partial class FixedAssetsDBContext : DbContext
    {
        public FixedAssetsDBContext()
        {
        }

        public FixedAssetsDBContext(DbContextOptions<FixedAssetsDBContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment>()
              .HasOne(i => i.FixedAsset)
              .WithMany(i => i.AssetAssignments)
              .HasForeignKey(i => i.AssetId)
              .HasPrincipalKey(i => i.AssetId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment>()
              .HasOne(i => i.User)
              .WithMany(i => i.AssetAssignments)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.UserId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute>()
              .HasOne(i => i.AssetCategory)
              .WithMany(i => i.AssetAttributes)
              .HasForeignKey(i => i.CategoryId)
              .HasPrincipalKey(i => i.CategoryId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue>()
              .HasOne(i => i.FixedAsset)
              .WithMany(i => i.AssetAttributeValues)
              .HasForeignKey(i => i.AssetId)
              .HasPrincipalKey(i => i.AssetId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue>()
              .HasOne(i => i.AssetAttribute)
              .WithMany(i => i.AssetAttributeValues)
              .HasForeignKey(i => i.AttributeId)
              .HasPrincipalKey(i => i.AttributeId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance>()
              .HasOne(i => i.FixedAsset)
              .WithMany(i => i.AssetInsurances)
              .HasForeignKey(i => i.AssetId)
              .HasPrincipalKey(i => i.AssetId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation>()
              .HasOne(i => i.FixedAsset)
              .WithMany(i => i.Depreciations)
              .HasForeignKey(i => i.AssetId)
              .HasPrincipalKey(i => i.AssetId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord>()
              .HasOne(i => i.FixedAsset)
              .WithMany(i => i.DisposalRecords)
              .HasForeignKey(i => i.AssetId)
              .HasPrincipalKey(i => i.AssetId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Document>()
              .HasOne(i => i.FixedAsset)
              .WithMany(i => i.Documents)
              .HasForeignKey(i => i.AssetId)
              .HasPrincipalKey(i => i.AssetId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Document>()
              .HasOne(i => i.User)
              .WithMany(i => i.Documents)
              .HasForeignKey(i => i.UploadedBy)
              .HasPrincipalKey(i => i.UserId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset>()
              .HasOne(i => i.AssetCategory)
              .WithMany(i => i.FixedAssets)
              .HasForeignKey(i => i.CategoryId)
              .HasPrincipalKey(i => i.CategoryId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset>()
              .HasOne(i => i.Location)
              .WithMany(i => i.FixedAssets)
              .HasForeignKey(i => i.LocationId)
              .HasPrincipalKey(i => i.LocationId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset>()
              .HasOne(i => i.Status)
              .WithMany(i => i.FixedAssets)
              .HasForeignKey(i => i.StatusId)
              .HasPrincipalKey(i => i.StatusId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset>()
              .HasOne(i => i.User)
              .WithMany(i => i.FixedAssets)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.UserId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord>()
              .HasOne(i => i.FixedAsset)
              .WithMany(i => i.MaintenanceRecords)
              .HasForeignKey(i => i.AssetId)
              .HasPrincipalKey(i => i.AssetId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Notification>()
              .HasOne(i => i.FixedAsset)
              .WithMany(i => i.Notifications)
              .HasForeignKey(i => i.AssetId)
              .HasPrincipalKey(i => i.AssetId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Notification>()
              .HasOne(i => i.User)
              .WithMany(i => i.Notifications)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.UserId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance>()
              .HasOne(i => i.FixedAsset)
              .WithMany(i => i.ScheduledMaintenances)
              .HasForeignKey(i => i.AssetId)
              .HasPrincipalKey(i => i.AssetId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole>()
              .HasOne(i => i.Role)
              .WithMany(i => i.UserRoles)
              .HasForeignKey(i => i.RoleId)
              .HasPrincipalKey(i => i.RoleId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole>()
              .HasOne(i => i.User)
              .WithMany(i => i.UserRoles)
              .HasForeignKey(i => i.UserId)
              .HasPrincipalKey(i => i.UserId);

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment>()
              .Property(p => p.UpdatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute>()
              .Property(p => p.UpdatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue>()
              .Property(p => p.UpdatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory>()
              .Property(p => p.UpdatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance>()
              .Property(p => p.UpdatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation>()
              .Property(p => p.UpdatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord>()
              .Property(p => p.UpdatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Document>()
              .Property(p => p.UploadedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset>()
              .Property(p => p.UpdatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Location>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Location>()
              .Property(p => p.UpdatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord>()
              .Property(p => p.UpdatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Notification>()
              .Property(p => p.IsRead)
              .HasDefaultValueSql(@"((0))");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Notification>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Role>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Role>()
              .Property(p => p.UpdatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance>()
              .Property(p => p.UpdatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole>()
              .Property(p => p.UpdatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.User>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.User>()
              .Property(p => p.UpdatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Status>()
              .Property(p => p.CreatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Status>()
              .Property(p => p.UpdatedAt)
              .HasDefaultValueSql(@"(getdate())");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment>()
              .Property(p => p.CreatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment>()
              .Property(p => p.UpdatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute>()
              .Property(p => p.CreatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute>()
              .Property(p => p.UpdatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue>()
              .Property(p => p.CreatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue>()
              .Property(p => p.UpdatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory>()
              .Property(p => p.CreatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory>()
              .Property(p => p.UpdatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance>()
              .Property(p => p.CreatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance>()
              .Property(p => p.UpdatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation>()
              .Property(p => p.CreatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation>()
              .Property(p => p.UpdatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord>()
              .Property(p => p.CreatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord>()
              .Property(p => p.UpdatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Document>()
              .Property(p => p.UploadedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset>()
              .Property(p => p.CreatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset>()
              .Property(p => p.UpdatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Location>()
              .Property(p => p.CreatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Location>()
              .Property(p => p.UpdatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord>()
              .Property(p => p.CreatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord>()
              .Property(p => p.UpdatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Notification>()
              .Property(p => p.CreatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Role>()
              .Property(p => p.CreatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Role>()
              .Property(p => p.UpdatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance>()
              .Property(p => p.CreatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance>()
              .Property(p => p.UpdatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole>()
              .Property(p => p.CreatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole>()
              .Property(p => p.UpdatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.User>()
              .Property(p => p.CreatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.User>()
              .Property(p => p.UpdatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Status>()
              .Property(p => p.CreatedAt)
              .HasColumnType("datetime");

            builder.Entity<ActivosFiljos.Server.Models.FixedAssetsDB.Status>()
              .Property(p => p.UpdatedAt)
              .HasColumnType("datetime");
            this.OnModelBuilding(builder);
        }

        public DbSet<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAssignment> AssetAssignments { get; set; }

        public DbSet<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttribute> AssetAttributes { get; set; }

        public DbSet<ActivosFiljos.Server.Models.FixedAssetsDB.AssetAttributeValue> AssetAttributeValues { get; set; }

        public DbSet<ActivosFiljos.Server.Models.FixedAssetsDB.AssetCategory> AssetCategories { get; set; }

        public DbSet<ActivosFiljos.Server.Models.FixedAssetsDB.AssetInsurance> AssetInsurances { get; set; }

        public DbSet<ActivosFiljos.Server.Models.FixedAssetsDB.Depreciation> Depreciations { get; set; }

        public DbSet<ActivosFiljos.Server.Models.FixedAssetsDB.DisposalRecord> DisposalRecords { get; set; }

        public DbSet<ActivosFiljos.Server.Models.FixedAssetsDB.Document> Documents { get; set; }

        public DbSet<ActivosFiljos.Server.Models.FixedAssetsDB.FixedAsset> FixedAssets { get; set; }

        public DbSet<ActivosFiljos.Server.Models.FixedAssetsDB.Location> Locations { get; set; }

        public DbSet<ActivosFiljos.Server.Models.FixedAssetsDB.MaintenanceRecord> MaintenanceRecords { get; set; }

        public DbSet<ActivosFiljos.Server.Models.FixedAssetsDB.Notification> Notifications { get; set; }

        public DbSet<ActivosFiljos.Server.Models.FixedAssetsDB.Role> Roles { get; set; }

        public DbSet<ActivosFiljos.Server.Models.FixedAssetsDB.ScheduledMaintenance> ScheduledMaintenances { get; set; }

        public DbSet<ActivosFiljos.Server.Models.FixedAssetsDB.UserRole> UserRoles { get; set; }

        public DbSet<ActivosFiljos.Server.Models.FixedAssetsDB.User> Users { get; set; }

        public DbSet<ActivosFiljos.Server.Models.FixedAssetsDB.Status> Statuses { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    }
}