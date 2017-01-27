using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace HPFS.Models
{
    public partial class SlideShow
    {
        public SlideShow()
        {
            SlideShowImages = new HashSet<SlideShowImage>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string SlideShowName { get; set; }

        public int WebPageId { get; set; }

        [ForeignKey("WebPageId")]
        public virtual WebPage WebPage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SlideShowImage> SlideShowImages { get; set; }
    }
}
