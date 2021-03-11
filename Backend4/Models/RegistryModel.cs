using System;
using System.ComponentModel.DataAnnotations;
using Backend4.Services;

namespace Backend4.Models
{
    public class RegistryModel
    {
        [Required] public String FirstName { get; set; }
        [Required] public String LastName { get; set; }
        public Int32 Date { get; set; }
        public Int32 Month { get; set; }
        public Int32 Year { get; set; }
        [Required] public Gender Gender { get; set; }
        public Boolean HasClone { get; set; }
    }
}