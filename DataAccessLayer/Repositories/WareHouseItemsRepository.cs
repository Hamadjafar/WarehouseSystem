using AutoMapper;
using DataAccessLayer.IRepositories;
using DomainLayer.Dtos;
using DomainLayer.Dtos.wareHousDto;
using Microsoft.EntityFrameworkCore;


namespace DataAccessLayer.Repositories
{
    public class WareHouseItemsRepository : IWareHouseItemsRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public WareHouseItemsRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<WareHouseItemsOutputDto> GetItemsById(int id, int pageNumber, int pageSize)
        {
            var totalItems = await _appDbContext.WarehouseItems.CountAsync(x => x.WareHouseId == id);

            var items = await _appDbContext.WarehouseItems
                .Where(x => x.WareHouseId == id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            var warehousesDto = _mapper.Map<List<WareHouseItemsDto>>(items);

            return new WareHouseItemsOutputDto
            {
                WareHouseItemsDto = warehousesDto,
                ItemsCount = totalItems
            };
        }


        public async Task<TopHighAndLowItemsDto> GetTopHighAndLowItemsByQuantity()
        {
            var topHighItems = await _appDbContext.WarehouseItems
                .OrderByDescending(item => item.Qty)
                .Take(10)
                .Select(item => new ItemsQuantityDto
                {
                    Name = item.ItemName,
                    Quantity = item.Qty
                })
                .ToListAsync();

            var topLowItems = await _appDbContext.WarehouseItems
                .Where(item => !topHighItems.Select(x => x.Name).Contains(item.ItemName)) 
                .OrderBy(item => item.Qty)
                .Take(10)
                .Select(item => new ItemsQuantityDto
                {
                    Name = item.ItemName,
                    Quantity = item.Qty
                })
                .ToListAsync();

            return new TopHighAndLowItemsDto
            {
                TopHighItems = topHighItems,
                TopLowItems = topLowItems
            };
        }

    }
}
