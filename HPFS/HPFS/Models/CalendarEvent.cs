using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace HPFS.Models
{
    public partial class CalendarEvent
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(250)]
        public string CalendarEventName { get; set; }

        public string CalendarEventDescription { get; set; }

        public DateTime? CalendarEventDate { get; set; }

        [StringLength(50)]
        public string RoleName { get; set; }

        public int? FileId { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
