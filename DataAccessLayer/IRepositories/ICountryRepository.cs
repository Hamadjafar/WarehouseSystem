using DomainLayer.Entities;


namespace DataAccessLayer.IRepositories
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetAllCountriesAsync();
    }
}
