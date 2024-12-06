using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActivosFiljos.Server.Models.FixedAssetsDB
{
    [Table("AssetAttributes", Schema = "dbo")]
    public partial class AssetAttribute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("AttributeID")]
        public int AttributeId { get; set; }

        [Required]
        public string AttributeName { get; set; }

        [Column("CategoryID")]
        public int? CategoryId { get; set; }

        public AssetCategory AssetCategory { get; set; }

        public string DataType { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public ICollection<AssetAttributeValue> AssetAttributeValues { get; set; }
    }
}