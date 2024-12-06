using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActivosFiljos.Server.Models.FixedAssetsDB
{
    [Table("MaintenanceRecords", Schema = "dbo")]
    public partial class MaintenanceRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("MaintenanceID")]
        public int MaintenanceId { get; set; }

        [Column("AssetID")]
        public int? AssetId { get; set; }

        public FixedAsset FixedAsset { get; set; }

        [Required]
        public DateTime MaintenanceDate { get; set; }

        [Required]
        public string Description { get; set; }

        public decimal? Cost { get; set; }

        public string PerformedBy { get; set; }

        public DateTime? NextMaintenanceDate { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}