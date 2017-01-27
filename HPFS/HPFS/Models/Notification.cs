using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPFS.Models
{
    public partial class Notification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public string Priority { get; set; }

        public bool IsRead { get; set; }

        public DateTime? NotificationDate { get; set; }

        public int? CalendarEventId { get; set; }

        public int? FileId { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [ForeignKey("UserId")]

        public virtual AspNetUser AspNetUser { get; set; }
    }

    public partial class NotificationViewModel
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public string Priority { get; set; }

        public bool IsRead { get; set; }

        public DateTime? NotificationDate { get; set; }

        public int? CalendarEventId { get; set; }

        public int? FileId { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [ForeignKey("UserId")]

        public virtual AspNetUser AspNetUser { get; set; }
    }
}