using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XpressUserMgt.Web.Models
{
    [Table("Groups")]
    public class Group
    {
        [Key, Column("GROUPID"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("GROUP_NAME")]
        public string GroupName { get; set; }

        [Column("LEADER_NAME")]
        public string LeaderName { get; set; }

        [Column("SEC_NAME")]
        public string SecretaryName { get; set; }

        [Column("TREASURER_NAME")]
        public string TreasurerName { get; set; }

        [Column("LEADER_PIX")]
        public byte[] LeaderImage { get; set; }

        [Column("LEADER_FILENAME")]
        public string LeaderFileName { get; set; }

        [Column("TREASURER_PIX")]
        public byte[] TreasurerImage { get; set; }

        [Column("TREASURER_FILENAME")]
        public string TreasurerFileName { get; set; }

        [Column("SEC_PIX")]
        public byte[] SecretaryImage { get; set; }

        [Column("SEC_FILENAME")]
        public string SecretaryFileName { get; set; }

        [Column("LONAME")]
        public string LOName { get; set; }

        [Column("LMNAME")]
        public string LMName { get; set; }

        [Column("BMNAME")]
        public string BMName { get; set; }

        [Column("LEADER_PHONE_NO")]
        public string LeaderPhoneNO { get; set; }

        [Column("SEC_PHONE_NO")]
        public string SecretaryPhoneNO { get; set; }

        [Column("TREASURER_PHONE_NO")]
        public string TreasurerPhoneNO { get; set; }

    }
    
    
    [NotMapped]
    public class GroupItem
    {
        public string? GroupName { get; set; }
        public string? LeaderName { get; set; }
    }
    [NotMapped]
    public class GroupItemAllRecord
    {
        
        public long GroupId { get; set; }
        public string? Group_Name { get; set; }
        public string? Leader_Name { get; set; }
       
        public string? Sec_Name { get; set; }
        
        public string? Treasurer_Name { get; set; }
       
        public byte[]? Leader_pix { get; set; }
        
        public string? Leader_FileName { get; set; }
        
        public byte[]? Treasurer_pix { get; set; }
       
        public string? Treasurer_FileName { get; set; }
        public byte[]? Sec_pix { get; set; }
        public string? Sec_FileName { get; set; }
        public string? LOName { get; set; }
        public string? LMName { get; set; }
        public string? BMName { get; set; }
        public string? Leader_Phone_NO { get; set; }
        public string? Sec_Phone_NO { get; set; }
        public string? Treasurer_Phone_NO { get; set; }
    }
}
