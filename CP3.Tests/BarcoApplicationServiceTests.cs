using Moq;
using Xunit;
using CP3.Application.Services;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;
using CP3.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CP3.Application.Tests
{
    public class BarcoApplicationServiceTests
    {
        private readonly Mock<IBarcoRepository> _mockBarcoRepository;
        private readonly IBarcoApplicationService _barcoApplicationService;

        public BarcoApplicationServiceTests()
        {
            // Cria o mock do repositório
            _mockBarcoRepository = new Mock<IBarcoRepository>();
            // Instancia o serviço de aplicação, injetando o mock
            _barcoApplicationService = new BarcoApplicationService(_mockBarcoRepository.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldAddBarcoAndReturnDto()
        {
            // Arrange
            var barcoDto = new BarcoDto
            {
                Nome = "Barco Teste",
                Modelo = "Modelo X",
                Ano = 2022,
                Tamanho = 20.5
            };

            var barcoEntity = new BarcoEntity
            {
                Id = 1,
                Nome = barcoDto.Nome,
                Modelo = barcoDto.Modelo,
                Ano = barcoDto.Ano,
                Tamanho = barcoDto.Tamanho
            };

            _mockBarcoRepository.Setup(repo => repo.AddAsync(It.IsAny<BarcoEntity>()))
                                .ReturnsAsync(barcoEntity);

            // Act
            var result = await _barcoApplicationService.AddAsync(barcoDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(barcoDto.Nome, result.Nome);
            Assert.Equal(barcoDto.Modelo, result.Modelo);
            Assert.Equal(barcoDto.Ano, result.Ano);
            Assert.Equal(barcoDto.Tamanho, result.Tamanho);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfBarcos()
        {
            // Arrange
            var barcosEntities = new List<BarcoEntity>
            {
                new BarcoEntity { Id = 1, Nome = "Barco 1", Modelo = "Modelo 1", Ano = 2022, Tamanho = 15.5 },
                new BarcoEntity { Id = 2, Nome = "Barco 2", Modelo = "Modelo 2", Ano = 2023, Tamanho = 30.5 }
            };

            _mockBarcoRepository.Setup(repo => repo.GetAllAsync())
                                .ReturnsAsync(barcosEntities);

            // Act
            var result = await _barcoApplicationService.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnBarcoDto()
        {
            // Arrange
            var barcoEntity = new BarcoEntity { Id = 1, Nome = "Barco 1", Modelo = "Modelo 1", Ano = 2022, Tamanho = 15.5 };

            _mockBarcoRepository.Setup(repo => repo.GetByIdAsync(1))
                                .ReturnsAsync(barcoEntity);

            // Act
            var result = await _barcoApplicationService.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(barcoEntity.Nome, result.Nome);
            Assert.Equal(barcoEntity.Modelo, result.Modelo);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateBarcoAndReturnDto()
        {
            // Arrange
            var barcoDto = new BarcoDto { Id = 1, Nome = "Barco Atualizado", Modelo = "Modelo Atualizado", Ano = 2022, Tamanho = 18.5 };
            var updatedBarcoEntity = new BarcoEntity { Id = 1, Nome = "Barco Atualizado", Modelo = "Modelo Atualizado", Ano = 2022, Tamanho = 18.5 };

            _mockBarcoRepository.Setup(repo => repo.UpdateAsync(It.IsAny<BarcoEntity>()))
                                .ReturnsAsync(updatedBarcoEntity);

            // Act
            var result = await _barcoApplicationService.UpdateAsync(barcoDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(barcoDto.Nome, result.Nome);
            Assert.Equal(barcoDto.Modelo, result.Modelo);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrueWhenBarcoIsDeleted()
        {
            // Arrange
            _mockBarcoRepository.Setup(repo => repo.DeleteAsync(1))
                                .ReturnsAsync(true);

            // Act
            var result = await _barcoApplicationService.DeleteAsync(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalseWhenBarcoDoesNotExist()
        {
            // Arrange
            _mockBarcoRepository.Setup(repo => repo.DeleteAsync(999))
                                .ReturnsAsync(false);

            // Act
            var result = await _barcoApplicationService.DeleteAsync(999);

            // Assert
            Assert.False(result);
        }
    }
}
