using System.ComponentModel.DataAnnotations;

namespace Login_service.DTO
{
    public class UserResponseDTO
    {
        public int? id { get; set; }
        public string? photo { get; set; }
        public string? name { get; set; }
        public string? phone { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public bool? confirm { get; set; }
        public string? message { get; set; }
    }
}
