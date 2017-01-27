using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPFS.Models
{
    public class Contact
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Company { get; set; }

        [StringLength(20)]
        public string Telephone { get; set; }

        [StringLength(20)]
        public string Fax { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        [StringLength(75)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Monday { get; set; }

        [StringLength(20)]
        public string Tuesday { get; set; }

        [StringLength(20)]
        public string Wednesday { get; set; }

        [StringLength(20)]
        public string Thursday { get; set; }

        [StringLength(20)]
        public string Friday { get; set; }

        [StringLength(20)]
        public string Saturday { get; set; }

        [StringLength(20)]
        public string Sunday { get; set; }

        [StringLength(300)]
        public string BannerMessage { get; set; }

        [StringLength(7)]
        public string BannerColor { get; set; }
    }
}