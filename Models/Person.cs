using System;
using System.Collections.Generic;
using System.Linq;
using Cloud9OrthoTest.DAL;
using System.ComponentModel.DataAnnotations;

namespace Cloud9OrthoTest.Models
{
    public class Person
    {
        public Guid ID { get; set; }
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }
        public virtual List<Addresses> Addresses { get; set; }
    }
}