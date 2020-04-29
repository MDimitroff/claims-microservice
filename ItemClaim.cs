using System;
using System.ComponentModel.DataAnnotations;

namespace Claims
{
    public class ItemClaim
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Item { get; set; }

        public string Summary { get; set; }

        public DateTime Date { get; set; }
    }
}
