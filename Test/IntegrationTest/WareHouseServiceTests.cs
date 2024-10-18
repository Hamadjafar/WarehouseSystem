using BusinessLayer.Services.WareHouseService;
using DataAccessLayer.IRepositories;
using DataAccessLayer.Repositories;
using DomainLayer.Dtos.wareHousDto;
using DomainLayer.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Test.IntegrationTest
{
    public class WareHouseServiceTests : BaseServiceTest
    {
        private readonly WareHouseService _service;
        private readonly IWareHouseRepository _repository; // Use the actual interface type
        private readonly Mock<ILogger<WareHouseService>> _loggerMock;

        public WareHouseServiceTests()
        {
           
            _repository = new WareHouseRepository(_dbContext, _mapper);

            _loggerMock = new Mock<ILogger<WareHouseService>>();

            _service = new WareHouseService(_repository, _mapper, _loggerMock.Object);
        }


        [Fact]
        public async Task Should_Create_Warehouse_Successfully()
        {
            // Arrange
            var warehouseInputDto = new WareHouseInputDto
            {
                Name = "Test Warehouse",
                CountryId = 1,
                Address = "nasreh street",
                City = "zarqa",
                WareHouseItemsDto = new List<WareHouseItemsDto>
                {
                  new WareHouseItemsDto
                {
                      ItemName = "Mobile",
                      SkuCode ="7868",
                      CostPrice=100,
                      MsrpPrice=100,
                      Qty=22
                },
                    new WareHouseItemsDto
                {
                      ItemName = "Laptop",
                      SkuCode ="534",
                      CostPrice=200,
                      MsrpPrice=200,
                      Qty=10
                }
                }
            };

            // Act
            await _service.CreateWareHouseAsync(warehouseInputDto);

            // Assert
            var warehouse = await _dbContext.Warehouses.FirstOrDefaultAsync();
            warehouse.Should().NotBeNull();
            warehouse.Name.Should().Be("Test Warehouse");
            warehouse.CountryId.Should().Be(1);
            warehouse.City.Should().Be("zarqa");
            warehouse.Items.Should().HaveCount(2);
        }

        [Fact]
        public async Task Should_ThrowException_When_DuplicateName()
        {
            // Arrange
            var warehouseInputDto = new WareHouseInputDto
            {
                Name = "Test Warehouse",
                CountryId = 1,
                Address = "nasreh street",
                City = "zarqa",
                WareHouseItemsDto = new List<WareHouseItemsDto>
        {
            new WareHouseItemsDto
            {
                ItemName = "Mobile",
                SkuCode ="7868",
                CostPrice=100,
                MsrpPrice=100,
                Qty=22
            },
            new WareHouseItemsDto
            {
                ItemName = "Laptop",
                SkuCode ="534",
                CostPrice=200,
                MsrpPrice=200,
                Qty=10
            }
        }
            };

            await _service.CreateWareHouseAsync(warehouseInputDto);

            // Assert
            var createdWarehouse = await _dbContext.Warehouses.FirstOrDefaultAsync(w => w.Name == "Test Warehouse");

            // Act
            Func<Task> act = async () => await _service.CreateWareHouseAsync(warehouseInputDto);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage($"A warehouse with the name '{createdWarehouse.Name}' already exists.");
        }


        [Fact]
        public async Task Should_ThrowException_When_CountryId_Less_Than_Or_Equal_Zero()
        {
            var warehouseInputDto = new WareHouseInputDto
            {
                Name = "Test Warehouse",
                CountryId = 0, // Invalid CountryId
                Address = "nasreh street",
                City = "zarqa",
            };

            // Act
            Func<Task> act = async () => await _service.CreateWareHouseAsync(warehouseInputDto);

            // Assert
            var exception = await act.Should().ThrowAsync<ArgumentException>();

            exception.WithMessage("Value cannot be less than or equal to zero. (Parameter 'CountryId')");
        }




        [Fact]
        public async Task Should_Get_All_Warehouses_Successfully()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            var warehouses = new List<Warehouse>
            {
                 new Warehouse { Id = 1, Name = "Warehouse 1", CountryId = 1, City = "City 1", Address = "Address 1" },
                 new Warehouse { Id = 2, Name = "Warehouse 2", CountryId = 1, City = "City 2", Address = "Address 2" },
                 new Warehouse { Id = 3, Name = "Warehouse 3", CountryId = 2, City = "City 3", Address = "Address 3" },
            };

            await _dbContext.Warehouses.AddRangeAsync(warehouses);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _service.GetAllWareHousesAsync(pageNumber, pageSize);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.TotalItems);
            Assert.Equal("Warehouse 1", result.WareHousesDto[0].Name);
            Assert.Equal("Warehouse 2", result.WareHousesDto[1].Name);
            Assert.Equal("Warehouse 3", result.WareHousesDto[2].Name);
        }

    }
}
