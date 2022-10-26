using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AbbyWeb.Model
{
    public class Category
    {
      
            [Key]
            public int id { get; set; }
            [Required]
            public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 200, ErrorMessage = "Display order must be in range of 1-200!!!")]
        public int DisplayOrder { get; set; }
      
        
    }
}
