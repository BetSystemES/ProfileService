using ProfileService.BusinessLogic.Entities;
using ProfileService.BusinessLogic.Models;
using ProfileService.BusinessLogic.Models.Criterias;

using Bonus = ProfileService.BusinessLogic.Entities.Bonus;

namespace ProfileService.BusinessLogic.Contracts.Services
{
    public interface IProfileService
    {
        Task AddProfileData(ProfileData profileData, CancellationToken token);

        Task<ProfileData> GetProfileDataById(Guid guid, CancellationToken token);

        Task UpdateProfileData(ProfileData profileData, CancellationToken token);

        Task AddDiscount(Bonus bonus, CancellationToken token);

        Task<IEnumerable<Bonus>> GetDiscounts(Guid guid, CancellationToken token);

        Task<IEnumerable<Bonus>> GetDiscountsDepOnRole(Guid guid, bool isReadyToUse, CancellationToken token);

        Task UpdateDiscount(Bonus bonus, CancellationToken token);

        public Task<PagedResponse<Bonus>> GetPagedDiscounts(FilterCriteria filterCriteria, CancellationToken cancellationToken);
    }
}