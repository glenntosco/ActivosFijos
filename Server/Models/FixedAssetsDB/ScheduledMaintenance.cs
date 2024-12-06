using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActivosFiljos.Server.Models.FixedAssetsDB
{
    [Table("ScheduledMaintenance", Schema = "dbo")]
    public partial class ScheduledMaintenance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ScheduleID")]
        public int ScheduleId { get; set; }

        [Column("AssetID")]
        public int? AssetId { get; set; }

        public FixedAsset FixedAsset { get; set; }

        public string MaintenanceFrequency { get; set; }

        public DateTime? NextScheduledDate { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}