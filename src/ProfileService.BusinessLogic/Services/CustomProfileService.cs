using System.Linq.Expressions;
using ProfileService.BusinessLogic.Contracts.DataAccess;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Contracts.Services;
using ProfileService.BusinessLogic.Entities;
using ProfileService.BusinessLogic.Helpers;
using ProfileService.BusinessLogic.Extensions;
using ProfileService.BusinessLogic.Models;
using ProfileService.BusinessLogic.Models.Criterias;
using ProfileService.BusinessLogic.Models.Enums;

using Bonus = ProfileService.BusinessLogic.Entities.Bonus;

namespace ProfileService.BusinessLogic.Services
{
    public class CustomProfileService : IProfileService
    {
        private readonly IProfileRepository _profileDataRepository;
        private readonly IBonusRepository _bonusRepository;
        private readonly IBonusProvider _bonusProvider;
        private readonly IProfileProvider _profileProvider;

        private readonly IDataContext _context;

        public CustomProfileService(IProfileRepository profileDataRepository,
            IBonusRepository bonusRepository,
            IProfileProvider profileProvider,
            IBonusProvider bonusProvider,
            IDataContext context)
        {
            _profileDataRepository = profileDataRepository;
            _bonusRepository = bonusRepository;
            _profileProvider = profileProvider;
            _bonusProvider = bonusProvider;
            _context = context;
        }

        public async Task AddProfileData(ProfileData profileData, CancellationToken token)
        {
            await _profileDataRepository.Add(profileData, token);
            await _context.SaveChanges(token);
        }

        public async Task<ProfileData> GetProfileDataById(Guid guid, CancellationToken token)
        {
            var result = await _profileProvider.Get(guid, token);
            return result;
        }

        public async Task UpdateProfileData(ProfileData profileData, CancellationToken token)
        {
            await _profileDataRepository.Update(profileData, token);
            await _context.SaveChanges(token);
        }

        public async Task DeleteProfileData(ProfileData profileData, CancellationToken token)
        {
            await _profileDataRepository.Remove(profileData, token);
            await _context.SaveChanges(token);
        }


        public async Task AddDiscount(Bonus bonus, CancellationToken token)
        {
            await _bonusRepository.Add(bonus, token);
            await _context.SaveChanges(token);
        }

        public async Task<IEnumerable<Bonus>> GetDiscounts(Guid guid, bool isReadyToUse, CancellationToken token)
        {
            var predicate = GetDepOnRoleExpression(guid, isReadyToUse);
            var result = await _bonusProvider.FindBy(predicate, token);
            return result;
        }

        public async Task UpdateDiscount(Bonus bonus, CancellationToken token)
        {
            await _bonusRepository.Update(bonus, token);
            await _context.SaveChanges(token);
        }

        public async Task DeleteDiscount(Bonus bonus, CancellationToken token)
        {
            await _bonusRepository.Remove(bonus, token);
            await _context.SaveChanges(token);
        }

        public async Task DeleteDiscounts(IEnumerable<Bonus> bonuses, CancellationToken token)
        {
            await _bonusRepository.RemoveRange(bonuses, token);
            await _context.SaveChanges(token);
        }

        public async Task<PagedResponse<Bonus>> GetPagedDiscounts(FilterCriteria filterCriteria, CancellationToken cancellationToken)
        {
            var expression = GetFilterExpression(filterCriteria);
            var order = GetOrderByFunc(filterCriteria);

            var (skip, take) = ((PaginationCriteria)filterCriteria).GetPaginationCriteria();

            var pagedBonuses = await _bonusProvider.GetPaged(expression, order, skip, take, cancellationToken);
            var totalCount = await _bonusProvider.GetCount(expression, cancellationToken);
            var pagedResponse = new PagedResponse<Bonus>()
            {
                TotalCount = totalCount,
                Data = pagedBonuses
            };

            return pagedResponse;
        }

        private Expression<Func<Bonus, bool>> GetDepOnRoleExpression(Guid guid, bool isReadyToUse)
        {
            var predicate = PredicateBuilderHelper.Create<Bonus>(x => x.ProfileId == guid);

            if (isReadyToUse)
            {
                predicate = predicate.And(x => x.IsEnabled == isReadyToUse);
            }

            return predicate;
        }

        private Expression<Func<Bonus, bool>> GetFilterExpression(FilterCriteria filter)
        {
            var predicate = PredicateBuilderHelper.True<Bonus>();

            if (filter.IsEnabled.HasValue)
            {
                predicate = predicate.And(x => x.IsEnabled == filter.IsEnabled);
            }

            if (filter.UserIds != null && filter.UserIds.Any())
            {
                predicate = predicate.And(x => filter.UserIds.Contains(x.ProfileId));
            }

            if (!string.IsNullOrEmpty(filter.SearchCriteria))
            {
                predicate = predicate.And(x => x.ProfileData.FirstName.ToLower().Contains(filter.SearchCriteria.ToLower()))
                    .Or(x => x.ProfileData.LastName.ToLower().Contains(filter.SearchCriteria.ToLower()))
                    .Or(x => x.ProfileId.ToString().ToLower().Contains(filter.SearchCriteria.ToLower()))
                    .Or(x => x.BonusId.ToString().ToLower().Contains(filter.SearchCriteria.ToLower()))
                    .Or(x => x.ProfileData.Email.ToLower().Contains(filter.SearchCriteria.ToLower()));
            }

            return predicate;
        }

        private Func<IQueryable<Bonus>, IOrderedQueryable<Bonus>> GetOrderByFunc(FilterCriteria filter)
        {
            Func<IQueryable<Bonus>, IOrderedQueryable<Bonus>> orderByExpression = null;

            if (!string.IsNullOrEmpty(filter.ColumnName) && filter.OrderDirection.HasValue && filter.OrderDirection != OrderDirection.Unspecified)
            {
                orderByExpression = OrderHelper.GetOrderBy<Bonus>(filter.ColumnName, filter.OrderDirection.Value);
            }

            return orderByExpression;
        }
    }
}