using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActivosFiljos.Server.Models.FixedAssetsDB
{
    [Table("FixedAssets", Schema = "dbo")]
    public partial class FixedAsset
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("AssetID")]
        public int AssetId { get; set; }

        [Required]
        public string AssetTag { get; set; }

        [Required]
        public string AssetName { get; set; }

        [Column("CategoryID")]
        public int? CategoryId { get; set; }

        public AssetCategory AssetCategory { get; set; }

        [Column("LocationID")]
        public int? LocationId { get; set; }

        public Location Location { get; set; }

        [Column("UserID")]
        public int? UserId { get; set; }

        public User User { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public decimal? PurchaseCost { get; set; }

        public string Manufacturer { get; set; }

        public string Model { get; set; }

        public string SerialNumber { get; set; }

        [Column("StatusID")]
        public int? StatusId { get; set; }

        public Status Status { get; set; }

        public DateTime? WarrantyExpiry { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ICollection<AssetAssignment> AssetAssignments { get; set; }

        public ICollection<AssetAttributeValue> AssetAttributeValues { get; set; }

        public ICollection<AssetInsurance> AssetInsurances { get; set; }

        public ICollection<Depreciation> Depreciations { get; set; }

        public ICollection<DisposalRecord> DisposalRecords { get; set; }

        public ICollection<Document> Documents { get; set; }

        public ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }

        public ICollection<Notification> Notifications { get; set; }

        public ICollection<ScheduledMaintenance> ScheduledMaintenances { get; set; }
    }
}