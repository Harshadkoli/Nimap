using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NimapProject.Models
{
    public class CategoryModel
    {
        [Required(ErrorMessage ="Please Enter category")]
        public string CategoryName { get; set; }
        public string message { get; set; }
        public int CategoryId { get; set; }
        public int id { get; set; }
    }
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}