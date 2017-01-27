using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace HPFS.Models
{
    public partial class HPSFile
    {
        //public HPSFile()
        //{
        //    SlideShowImages = new HashSet<SlideShowImage>();
        //}

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(250)]
        public string FileName { get; set; }

        public int FileSize { get; set; }

        public byte[] FileData { get; set; }

        public byte[] Thumbnail { get; set; }

        [StringLength(50)]
        public string FileContentType { get; set; }

        public DateTime? FileDate { get; set; }

        [StringLength(5)]
        public string FileExtension { get; set; }

        [StringLength(100)]
        public string RoleName { get; set; }
        
        [StringLength(128)]
        public string UserId { get; set; }

        public int FolderId { get; set; }

        [ForeignKey("FolderId")]
        public virtual Folder Folder { get; set;}

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SlideShowImage> SlideShowImages { get; set; }

    }

    public partial class HPSFileViewModel
    {
        //public HPSFileViewModel()
        //{
        //    SlideShowImages = new HashSet<SlideShowImage>();
        //}

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(250)]
        public string FileName { get; set; }

        public int FileSize { get; set; }

        public byte[] FileData { get; set; }

        public byte[] Thumbnail { get; set; }

        [StringLength(50)]
        public string FileContentType { get; set; }

        public DateTime? FileDate { get; set; }

        [StringLength(5)]
        public string FileExtension { get; set; }

        [StringLength(100)]
        public string RoleName { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }

        public int FolderId { get; set; }

        public string FolderName {
            get
            {
                return this.Folder.FolderName;
            }
        }

        [ForeignKey("FolderId")]
        public virtual Folder Folder { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SlideShowImage> SlideShowImages { get; set; }

    }
}
