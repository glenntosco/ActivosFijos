using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ActivosFiljos.Server.Models.FixedAssetsDB
{
    [Table("UserRoles", Schema = "dbo")]
    public partial class UserRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserRoleID")]
        public int UserRoleId { get; set; }

        [Column("UserID")]
        public int? UserId { get; set; }

        public User User { get; set; }

        [Column("RoleID")]
        public int? RoleId { get; set; }

        public Role Role { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}