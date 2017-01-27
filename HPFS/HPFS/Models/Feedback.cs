using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace HPFS.Models
{
    public class FeedBack
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int FeedBackNavRating { get; set; }

        public int FeedBackAccRating { get; set; }

        public int FeedBackAppRating { get; set; }

        public int FeedBackRecRating { get; set; }

        public double FeedBackAvg { get; set; }

        public DateTime FeedBackDate { get; set; }

        [StringLength(25)]
        public string FeedBackArea { get; set; }

        [StringLength(500)]
        public string FeedBackComment { get; set; }
    }
}