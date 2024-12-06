using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActivosFiljos.Server.Models.FixedAssetsDB
{
    [Table("DisposalRecords", Schema = "dbo")]
    public partial class DisposalRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("DisposalID")]
        public int DisposalId { get; set; }

        [Column("AssetID")]
        public int? AssetId { get; set; }

        public FixedAsset FixedAsset { get; set; }

        [Required]
        public DateTime DisposalDate { get; set; }

        public string DisposalMethod { get; set; }

        public decimal? SalePrice { get; set; }

        public string Reason { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}