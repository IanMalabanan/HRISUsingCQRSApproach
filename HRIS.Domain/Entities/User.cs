using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRIS.Domain.Entities
{
    [Table("t_User", Schema = "dbo")]

    public class User
    {
        [Key]
        [Column("usrUserName")]
        public string UserName { get; set; }

        [Column("usrPassword")]
        public string Password { get; set; }

        [Column("usrIsPasswordForceChange")]
        public bool? IsPasswordForceChange { get; set; }

        [Column("usrLastDatePasswordChanged")]
        public DateTime? LastDatePasswordChanged { get; set; }

        [Column("usrLoginKey")]
        public string LoginKey { get; set; }

        [Column("usrFullName")]
        public string FullName { get; set; }

        [Column("usrLastName")]
        public string LastName { get; set; }

        [Column("usrFirstName")]
        public string FirstName { get; set; }

        [Column("usrMiddleName")]
        public string MiddleName { get; set; }

        [Column("usrPhoneNumber")]
        public string PhoneNumber { get; set; }

        [Column("usrEmailAddress")]
        public string EmailAddress { get; set; }

        [Column("usrCellphoneNumber")]
        public string CellphoneNumber { get; set; }

        [Column("usrActive")]
        public bool Active { get; set; }

        [Column("usrActiveNoDaysExpire")]
        public int? ActiveNoDaysExpire { get; set; }

        [Column("usrActiveTagDate")]
        public DateTime? ActiveTagDate { get; set; }

        [Column("usrActiveTagBy")]
        public string ActiveTagBy { get; set; }

        [Column("usrActiveRemarks")]
        public string ActiveRemarks { get; set; }

        [Column("usrUserTypeCode")]
        public string UserTypeCode { get; set; }

        [Column("usrUserTypeReference")]
        public string UserTypeReference { get; set; }

        [Column("usrChangedReferenceNumber")]
        public string ChangedReferenceNumber { get; set; }

        [Column("usrDateCreated")]
        public DateTime DateCreated { get; set; }

        [Column("usrCreatedBy")]
        public string CreatedBy { get; set; }

        [Column("usrDateModified")]
        public DateTime? DateModified { get; set; }

        [Column("usrModifiedBy")]
        public string ModifiedBy { get; set; }
    }
}
