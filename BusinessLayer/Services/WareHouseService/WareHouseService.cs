using AutoMapper;
using DataAccessLayer.IRepositories;
using DomainLayer.Dtos.wareHousDto;
using DomainLayer.Entities;
using Infrastructure.Shared;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Services.WareHouseService
{
    public class WareHouseService
    {
        private readonly IWareHouseRepository _wareHouseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<WareHouseService> _logger;

        public WareHouseService(IWareHouseRepository wareHouseRepository, IMapper mapper, ILogger<WareHouseService> logger)
        {
            _wareHouseRepository = wareHouseRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task CreateWareHouseAsync(WareHouseInputDto wareHouseInput)
        {
            Guard.AssertArgumentNotNull(wareHouseInput, nameof(wareHouseInput));
            Guard.AssertArgumentNotLessThanOrEqualToZero(wareHouseInput.CountryId, nameof(wareHouseInput.CountryId));

            _logger.LogInformation($"Attempting to create a new warehouse: {wareHouseInput.Name}");

            if (await _wareHouseRepository.IsWarehouseNameExists(wareHouseInput.Name.Trim()))
                throw new InvalidOperationException($"A warehouse with the name '{wareHouseInput.Name}' already exists.");


            var warehouse = _mapper.Map<Warehouse>(wareHouseInput);

            if (warehouse.Items == null)
                warehouse.Items = new List<WarehouseItems>();


            await _wareHouseRepository.CreateWarehouse(warehouse);
            await _wareHouseRepository.Save();

            foreach (var item in warehouse.Items)
            {
                item.WareHouseId = warehouse.Id;
            }
            await _wareHouseRepository.Update(warehouse);

            _logger.LogInformation($"Warehouse {warehouse.Id} created successfully.");

        }

        public async Task<WarehouseOutputDto> GetAllWareHousesAsync(int pageNumber, int pageSize)
        {

            _logger.LogInformation("Fetching all warehouses.");

            var result = await _wareHouseRepository.GetAllWareHouses(pageNumber, pageSize);

            _logger.LogInformation($"Successfully retrieved {result.TotalItems} warehouses");

            return result;
        }
    }
}
