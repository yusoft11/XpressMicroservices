using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace XpressUserMgt.Web.Models
{
    [Table("Membership")]
    public class MemberShip
    {
        [Key, Column("ID"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("GROUP_ID")]
        public long GroupId { get; set; }

        [Column("FULLNAME")]
        public string FullName { get; set; }

        [Column("ADDRESS")]
        public string Address { get; set; }

        [Column("PHONE_NO")]
        public string PhoneNo { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("PICTURE")]
        public byte[] Picture { get; set; }

        [Column("FILENAME")]
        public string FileName { get; set; }

        [Column("NATURE_OF_BUSINESS")]
        public string NatureOfBusiness { get; set; }

        [Column("BUSINESS_ADDRESS")]
        public string BusinessAddress { get; set; }

        [Column("NEXT_OF_KIN")]
        public string NextOfKin { get; set; }

        [Column("ADDRESS_OF_NEXT_OF_KIN")]
        public string AddressOfNextOfKin { get; set; }

        [Column("LORSM_NO")]
        public int LORSMNO { get; set; }

        [Column("LMRSM_NO")]
        public int LMRSMNO { get; set; }

        [Column("BMRSM_NO")]
        public int BMRSMNO { get; set; }

        [Column("LO_NAME")]
        public string LOName { get; set; }

        [Column("LM_NAME")]
        public string LMName { get; set; }

        [Column("BM_NAME")]
        public string BMName { get; set; }

        [Column("DATE_JOINED")]
        public DateTime JoinedDate { get; set; }

        [Column("DATE_MODIFIED")]
        public DateTime? ModifiedDate { get; set; }

        [Column("NEXT_OF_KIN_PHONE_NO")]
        public string NextOfKinPhoneNO { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }
    }
}
