using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Models
{
    

    #region Ban Types

    public enum BannedType : Byte
    {
        None = 0,
        Temporary = 1,
        Lifetime = 2
    }

    #endregion

    [Table("Banns")]
    public class BanModel
    {
        [Key]
        [Column("id", Order = 1)]
        public int Id { get; set; }

        [Column("is_banned", Order = 2), Required]
        public Boolean isBanned { get; set; } = false;

        [Column("ban_type", Order = 6), Required]
        public BannedType BanType { get; set; } = BannedType.None;

        [Column("ban_time", Order = 7), Required]
        public DateTime BanTime { get; set; } = DateTime.MinValue;

        [Column("ban_user", Order = 8), Required]
        public String BanIssuer { get; set; } = "";

        [Column("ban_reason", Order = 9), Required]
        public String BanReason { get; set; } = "No Reason";

        [Column("ban_issued", Order = 10), Required]
        public DateTime BanIssueTime { get; set; } = DateTime.UtcNow;

        // Relationship
        //public PocketModel Pocket { get; set; }
    }
}