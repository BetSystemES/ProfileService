using FizzWare.NBuilder;
using ProfileService.BusinessLogic.Entities;

namespace ProfileService.TestDataGeneratorsAndExtensions
{
    public static partial class DataGenerator
    {
        public static PersonalData PersonalDataGenerator(Guid personalId)
        {
            PersonalData personalData = Builder<PersonalData>
                .CreateNew()
                .With(x => x.PersonalId = personalId)
                .Build();

            return personalData;
        }
    }
}