using ProfileService.BusinessLogic.Contracts.DataAccess;
using ProfileService.BusinessLogic.Contracts.DataAccess.Providers;
using ProfileService.BusinessLogic.Contracts.DataAccess.Repositories;
using ProfileService.BusinessLogic.Contracts.Services;
using ProfileService.BusinessLogic.Entities;
using ProfileService.BusinessLogic.Helpers;
using ProfileService.BusinessLogic.Models;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Bonus = ProfileService.BusinessLogic.Entities.Bonus;

namespace ProfileService.BusinessLogic.Services
{
    public class CustomProfileService : IProfileService
    {
        private readonly IProfileRepository _profileDataRepository;
        private readonly IBonusRepository _bonusRepository;
        private readonly IFilter<Bonus> _bonusFilter;
        private readonly IProvider<Bonus> _bonusProvider;
        private readonly IProvider<ProfileData> _profileDataProvider;

        private readonly IDataContext _context;

        public CustomProfileService(IProfileRepository profileDataRepository, IBonusRepository bonusRepository,
            IProvider<Bonus> bonusProvider, IFilter<Bonus> bonusFilter,
            IProvider<ProfileData> profileDataProvider,
            IDataContext context)
        {
            _profileDataRepository = profileDataRepository;
            _bonusRepository = bonusRepository;
            _bonusProvider = bonusProvider;
            _bonusFilter = bonusFilter;
            _profileDataProvider = profileDataProvider;
            _context = context;
        }

        public async Task AddProfileData(ProfileData profileData, CancellationToken token)
        {
            await _profileDataRepository.Add(profileData, token);
            await _context.SaveChanges(token);
        }

        public async Task<ProfileData> GetProfileDataById(Guid guid, CancellationToken token)
        {
            var result = await _profileDataProvider.Get(guid, token);
            return result;
        }

        public async Task UpdateProfileData(ProfileData profileData, CancellationToken token)
        {
            await _profileDataRepository.Update(profileData, token);
            await _context.SaveChanges(token);
        }

        public async Task AddDiscount(Bonus bonus, CancellationToken token)
        {
            await _bonusRepository.Add(bonus, token);
            await _context.SaveChanges(token);
        }

        public async Task<IEnumerable<Bonus>> GetDiscounts(Guid guid, CancellationToken token)
        {
            var result = await _bonusFilter.FindBy(x => x.ProfileId == guid, token);
            return result;
        }

        public async Task<IEnumerable<Bonus>> GetDiscountsDepOnRole(Guid guid, bool isReadyToUse,
            CancellationToken token)
        {
            var result = new List<Bonus>();

            if (isReadyToUse)
            {
                result = await _bonusFilter.FindBy(
                    x => x.ProfileId == guid
                         && x.IsEnabled == isReadyToUse,
                    token);
            }
            else
            {
                result = await _bonusFilter.FindBy(x => x.ProfileId == guid, token);
            }

            return result;
        }

        public async Task<IEnumerable<Bonus>> GetDiscountsWithFilter(Guid guid, PaginationCriteria paginationCriteria,
            CancellationToken token)
        {
            var result = await _bonusFilter
                .FindByPageFilter(x => x.ProfileId == guid, paginationCriteria, token);
            return result;
        }

        public async Task UpdateDiscount(Bonus bonus, CancellationToken token)
        {
            await _bonusRepository.Update(bonus, token);
            await _context.SaveChanges(token);
        }


        private Expression<Func<Bonus, bool>> GetFilterExpression(FilterCriteria filter)
        {
            var predicate = PredicateBuilderHelper.True<Entities.Bonus>();
            predicate = predicate.And(x => x.IsEnabled == filter.IsEnabled).Or(x=>x.IsAlreadyUsed);

            if (HasValue)
            {
                predicate = predicate.And(x => x.IsEnabled == filter.IsEnabled);
            }
            
            predicate = predicate.And(x => x.IsEnabled == filter.IsEnabled);

            return predicate;
        }

        private Func<IQueryable<Bonus>, IOrderedQueryable<Bonus>> GetOrderByFunc(FilterCriteria filter)
        {
            Func<IQueryable<Bonus>, IOrderedQueryable<Bonus>> orderByExpression = null;

            if (!string.IsNullOrEmpty(filter.ColumnName) && filter.SortDirection.HasValue)
            {
                orderByExpression = SortHelper.GetOrderBy<Bonus>(filter.ColumnName, filter.SortDirection.Value);
            }
            return orderByExpression;
        }


        public async Task GetBonuses(FilterCriteria filterCriteria, CancellationToken cancellationToken)
        {
            var expression = GetFilterExpression(filterCriteria);
            var order = GetOrderByFunc(filterCriteria);

            (var skip, var take) = ((PaginationCriteria)filterCriteria).GetPaginationCriteria();

            IQueryable<Bonus> kInts;
            var res = kInts.Where(expression).OrderBy(order).Take(take).Skip(skip).ToListAsync();

        }

    }
}