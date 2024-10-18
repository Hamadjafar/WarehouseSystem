using DataAccessLayer.IRepositories;
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CountryRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return await _appDbContext.Countries.AsNoTracking().ToListAsync();
        }
    }
}
