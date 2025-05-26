using System.ComponentModel.DataAnnotations;

namespace Login_service.Models
{
    public class User
    {
        [Key]
        public int? id {  get; set; }
        [Required]
        public string? photo { get; set; }
        [Required]
        [StringLength(maximumLength:250)]
        public string? name { get; set; }
        [StringLength(maximumLength:100)]
        public string? phone { get; set; }
        [Required]
        [StringLength(maximumLength:150)]
        public string? email { get; set; }
        [Required]
        [StringLength(maximumLength: 150)]
        public string? password { get; set; }
    }
}
