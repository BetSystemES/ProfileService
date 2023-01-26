using System.ComponentModel.DataAnnotations;

namespace ProfileService.BusinessLogic
{
    public class PersonalData
    {
        [Key] public Guid PersonalId { get; set; }
        [Required] public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        [Required] public string Email { get; set; }

        public List<Bonus> Bonuses { get; set; }
    }
}