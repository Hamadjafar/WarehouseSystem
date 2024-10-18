using AutoMapper;
using DataAccessLayer.IRepositories;
using DomainLayer.Dtos;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Services.CountryService
{
    public class CountryService 
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CountryService> _logger;


        public CountryService(ICountryRepository countryRepository , IMapper mapper, ILogger<CountryService> logger)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<List<CountryDto>> GetAllCountriesAsync()
        {
            _logger.LogInformation("Fetching all countries from the database");
            var countries = await _countryRepository.GetAllCountriesAsync();

            _logger.LogInformation($"Successfully retrieved {countries.Count} countries");

            return _mapper.Map<List<CountryDto>>(countries);
        }
    }
}
