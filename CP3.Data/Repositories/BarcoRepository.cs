using CP3.Data.Repositories;
using CP3.Data.AppData;
using CP3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CP3.Data.Tests
{
    public class BarcoRepositoryTests
    {
        private readonly BarcoRepository _barcoRepository;
        private readonly ApplicationContext _context;

        public BarcoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                          .UseInMemoryDatabase(databaseName: "TestDb")
                          .Options;

            _context = new ApplicationContext(options);
            _barcoRepository = new BarcoRepository(_context);
        }

        [Fact]
        public async Task AddAsync_ShouldAddBarco()
        {
            // Arrange
            var barco = new BarcoEntity
            {
                Nome = "Barco Teste",
                Modelo = "Modelo X",
                Ano = 2022,
                Tamanho = 20.5
            };

            // Act
            var addedBarco = await _barcoRepository.AddAsync(barco);

            // Assert
            Assert.NotNull(addedBarco);
            Assert.Equal("Barco Teste", addedBarco.Nome);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnBarcos()
        {
