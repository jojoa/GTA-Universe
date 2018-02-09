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

    

    [Table("Accounts")]
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

        [ForeignKey("Inv-ID")] 
        [Column("Inventory", Order = 7)]
        public InventoryModel Inventory { get; set; }

        [Column("Inv-ID", Order = 8)]
        public int InvID { get; set; }

        [Column("Skin", Order = 9)]
        public List<SkinModel> Skin { get; set; }


        
    }
}