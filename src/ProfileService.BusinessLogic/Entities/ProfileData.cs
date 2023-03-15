using System.ComponentModel.DataAnnotations;

namespace ProfileService.BusinessLogic.Entities
{
    public class ProfileData
    {
        [Key] public Guid ProfileId { get; set; }
        [Required] public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required] public string Email { get; set; }

        public List<Bonus> Bonuses { get; set; }
    }
}