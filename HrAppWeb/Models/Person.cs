using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HrAppWeb.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100,ErrorMessage="Display Order must be between 1 and 100 only!")]
        public int DisplayOrder { get; set; }
        public DateTime AddedDateTime { get; set; } = DateTime.Now;
    }
}