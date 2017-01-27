using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace HPFS.Models
{
    public partial class SlideShowImage
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string SlideShowHeading { get; set; }

        [StringLength(50)]
        public string SlideShowText { get; set; }

        public int SlideShowPosition { get; set; }

        public int FileId { get; set; }

        public int SlideShowId { get; set; }

        [ForeignKey("FileId")]
        public virtual HPSFile HPSFile { get; set; }

        [ForeignKey("SlideShowId")]
        public virtual SlideShow SlideShow { get; set; }
    }
}
