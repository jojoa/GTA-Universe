using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Models
{
    #region Roles

    public enum Roles : Byte
    {
        Player = 0,
        Supporter = 1,
        Admin = 2
    }

    #endregion

    #region Ban Types

    public enum BannedType : Byte
    {
        None = 0,
        Temporary = 1,
        Lifetime = 2
    }

    #endregion

    [Table("accounts")]
    public class AccountModel
    {
        [Key]
        [Column("id", Order = 1)]
        public int AccountId { get; set; }

        [Index(IsUnique = true)]
        [Column("sc_name", Order = 2, TypeName = "char"), StringLength(255), Required]
        public string SocialclubName { get; set; }

        [Column("password", Order = 3, TypeName = "char"), StringLength(72), Required]
        public string Password { get; set; }

        [Column("role", Order = 4), Required]
        public Roles Role { get; set; } = Roles.Player;

        [Column("last_ip", Order = 5, TypeName = "char"), StringLength(50), Required]
        public string LastIp { get; set; }

        [Column("last_seen", Order = 6), Required]
        public DateTime LastSeen { get; set; } = DateTime.Now;

        [Column("banned", Order = 7)]
        public bool IsBanned { get; set; } = false;

        [Column("banned_type", Order = 8)]
        public BannedType BanType { get; set; } = BannedType.None;

        // Relationship
        //public PocketModel Pocket { get; set; }
    }
}