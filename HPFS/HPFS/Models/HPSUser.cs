namespace HPFS
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HPSUser
    {
        public HPSUser()
        {
            // Default properties
            CreatedOn = DateTime.Now;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        public DateTime? CreatedOn { get; set; }

        [StringLength(50)]
        public string FitBitUserId { get; set; }

        public string UserId { get; set; }

        [StringLength(50)]
        public string RoleName { get; set; }

        [ForeignKey("UserId")]
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
