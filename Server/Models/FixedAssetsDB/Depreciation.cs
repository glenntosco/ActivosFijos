using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActivosFiljos.Server.Models.FixedAssetsDB
{
    [Table("Depreciation", Schema = "dbo")]
    public partial class Depreciation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("DepreciationID")]
        public int DepreciationId { get; set; }

        [Column("AssetID")]
        public int? AssetId { get; set; }

        public FixedAsset FixedAsset { get; set; }

        public string DepreciationMethod { get; set; }

        public int? UsefulLifeYears { get; set; }

        
        public decimal? SalvageValue { get; set; }

        public DateTime? DepreciationStartDate { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}