using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace HPFS.Models
{
    public partial class Program
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(250)]
        public string ProgramName { get; set; }

        public string ProgramDescription { get; set; }

        public string ProgramGoals { get; set; }

        public string ProgramCoordinator { get; set; }

        [StringLength(50)]
        public string ProgramEmail { get; set; }

        [StringLength(20)]
        public string ProgramPhone { get; set; }

        [StringLength(250)]
        public string ProgramSchedule { get; set; }

        [StringLength(75)]
        public string ProgramLocation { get; set; }

        public string ProgramMap { get; set; }
    }
}
