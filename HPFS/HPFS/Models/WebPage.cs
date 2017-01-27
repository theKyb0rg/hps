using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace HPFS.Models
{
    public partial class WebPage
    {
        public WebPage()
        {
            SlideShows = new HashSet<SlideShow>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(250)]
        public string WebPageName { get; set; }

        public string WebPageContent { get; set; }

        [StringLength(250)]
        public string WebPageDescription { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SlideShow> SlideShows { get; set; }

    }
}
