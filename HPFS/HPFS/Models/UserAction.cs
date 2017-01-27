using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HPFS.Models
{
    public class UserAction
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
    public enum UserActionEnum
    {
        Clicked = 1,
        Navigated = 2,
        Deleted = 3,
        Created = 4,
        Updated = 5,
        Searched = 6,
        LoggedIn = 7,
        Downloaded = 8
    }
}