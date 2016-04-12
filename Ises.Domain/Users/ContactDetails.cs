using System.ComponentModel.DataAnnotations.Schema;

namespace Ises.Domain.Users
{
    [ComplexType]
    public class ContactDetails
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PersonalPhone { get; set; }
        public string PersonalEmail { get; set; }
        public string HomeCity { get; set; }
    }
}
