using System.ComponentModel.DataAnnotations;

namespace MyContacts.am.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", ErrorMessage = "Not a valid form")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?\d{3}\)?-? *\d{3}-? *-?\d{4}$", ErrorMessage = "Not a valid phone number. MaxLength-10 ")]
        public int Phone { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string? Address { get; set; }
    }
}
