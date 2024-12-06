using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActivosFiljos.Server.Models.FixedAssetsDB
{
    [Table("Documents", Schema = "dbo")]
    public partial class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("DocumentID")]
        public int DocumentId { get; set; }

        [Column("AssetID")]
        public int? AssetId { get; set; }

        public FixedAsset FixedAsset { get; set; }

        public string DocumentName { get; set; }

        public string DocumentType { get; set; }

        public string FilePath { get; set; }

        public DateTime? UploadedAt { get; set; }

        public int? UploadedBy { get; set; }

        public User User { get; set; }
    }
}