using System.ComponentModel.DataAnnotations;

namespace MyContacts.am.ModelsView
{
    public class UserModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }
        public string Address { get; set; }
    }
}
