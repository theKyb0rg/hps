using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPFS.Models
{


    public class ActivityLog
    {
        public ActivityLog()
        {
            // Default properties
            DateAccessed = DateTime.Now;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string PageAccessed { get; set; }

        public DateTime? DateAccessed { get; set; }

        public int UserActionID { get; set; }

        [ForeignKey("UserActionID")]
        public virtual UserAction UserAction { get; set; }
    }
}