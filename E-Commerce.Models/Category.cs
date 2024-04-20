using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Display Order")]
        [Range(0, 100)]
        public int DisplayOrder { get; set; }
    }
}
