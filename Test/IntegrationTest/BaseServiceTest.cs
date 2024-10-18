using AutoMapper;
using BusinessLayer.MappingProfile;
using DataAccessLayer;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Test.IntegrationTest
{
    public class BaseServiceTest
    {
        public readonly IMapper _mapper;
        public readonly AppDbContext _dbContext;
        public BaseServiceTest()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(connection).Options;
            _dbContext = new AppDbContext(options);
            _dbContext.Database.EnsureCreated();

            // Set up the AutoMapper configuration
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }
    }
}
