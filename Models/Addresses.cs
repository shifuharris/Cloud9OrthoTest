using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Cloud9OrthoTest.Models
{
    public class Addresses
    {
        public Guid ID { get; set; }
        public Guid PersonID { get; set; }
        [Required]
        public String Address { get; set; }
        [Required]
        public String City { get; set; }
        [Required]
        public String State { get; set; }
        [Required]
        public Int16 Zip { get; set; }
    }
}