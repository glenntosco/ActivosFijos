using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActivosFiljos.Server.Models.FixedAssetsDB
{
    [Table("AssetCategories", Schema = "dbo")]
    public partial class AssetCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("CategoryID")]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ICollection<AssetAttribute> AssetAttributes { get; set; }

        public ICollection<FixedAsset> FixedAssets { get; set; }
    }
}