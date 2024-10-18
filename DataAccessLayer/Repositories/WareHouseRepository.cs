using AutoMapper;
using DataAccessLayer.IRepositories;
using DomainLayer.Dtos;
using DomainLayer.Dtos.wareHousDto;
using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Repositories
{
    public class WareHouseRepository : IWareHouseRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper; 

        public WareHouseRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task CreateWarehouse(Warehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException(nameof(warehouse));

            await _appDbContext.AddAsync(warehouse);
        }

        public async Task<WarehouseOutputDto> GetAllWareHouses(int pageNumber, int pageSize)
        {
            
            var totalItems = await _appDbContext.Warehouses.CountAsync();
            var warehouses = await (from wareHouse in _appDbContext.Warehouses
                                    join country in _appDbContext.Countries on wareHouse.CountryId equals country.Id
                                    select new WareHouseOutputDto
                                    {
                                        Id = wareHouse.Id,
                                        Name = wareHouse.Name,
                                        Address = wareHouse.Address,
                                        City = wareHouse.City,
                                        CountryId = country.Id,
                                        CountryName = country.Name
                                    })
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .AsNoTracking()
                        .ToListAsync();


            var warehousesDto = _mapper.Map<List<WareHouseOutputDto>>(warehouses);

            return new WarehouseOutputDto
            {
                TotalItems = totalItems,
                WareHousesDto = warehousesDto
            };
        }

        public async Task<List<WareHouseStatusDto>> GetWarehouseItemsStatus()
        {
            var result = await _appDbContext.Warehouses
            .Select(warehouse => new WareHouseStatusDto
            {
                Name = warehouse.Name,
                Count = warehouse.Items.Count()
            })
            .ToListAsync();

            return result;
        }

        public async Task<bool> IsWarehouseNameExists(string name)
        {
            return await _appDbContext.Warehouses.AsNoTracking().AnyAsync(x => x.Name == name);
        }

        public async Task Save()
        {
           await _appDbContext.SaveChangesAsync();
        }

        public async Task Update(Warehouse warehouse)
        {
            _appDbContext.Warehouses.Update(warehouse);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
