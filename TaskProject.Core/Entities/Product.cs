using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProject.Core.Entities
{
    public class Product
    {
        public Product()
        {
            Name = "";
            ManufactureEmail = "";
            ManufacturePhone = "";

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ProductDate { get; set; }
        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public bool IsAvailable { get; set; } 
        public string AuthorId { get; set; }
    }
}
