using CP3.Application.Dtos;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CP3.Application.Services
{
    public class BarcoService : IBarcoService
    {
        private readonly IBarcoRepository _barcoRepository;

        public BarcoService(IBarcoRepository barcoRepository)
        {
            _barcoRepository = barcoRepository;
        }

        // Adicionar um novo barco
        public async Task<BarcoDto> AddAsync(BarcoDto barcoDto)
        {
            if (barcoDto == null) throw new ArgumentNullException(nameof(barcoDto));

            var barco = new Barco
            {
                Nome = barcoDto.Nome,
                Modelo = barcoDto.Modelo,
                Ano = barcoDto.Ano,
                Tamanho = barcoDto.Tamanho
            };

            var addedBarco = await _barcoRepository.AddAsync(barco);

            return new BarcoDto
            {
                Id = addedBarco.Id,
                Nome = addedBarco.Nome,
                Modelo = addedBarco.Modelo,
                Ano = addedBarco.Ano,
                Tamanho = addedBarco.Tamanho
            };
        }

        // Obter todos os barcos
        public async Task<IEnumerable<BarcoDto>> GetAllAsync()
        {
            var barcos = await _barcoRepository.GetAllAsync();
            var barcoDtos = new List<BarcoDto>();

            foreach (var barco in barcos)
            {
                barcoDtos.Add(new BarcoDto
                {
                    Id = barco.Id,
                    Nome = barco.Nome,
                    Modelo = barco.Modelo,
                    Ano = barco.Ano,
                    Tamanho = barco.Tamanho
                });
            }

            return barcoDtos;
        }

        // Obter um barco por Id
        public async Task<BarcoDto> GetByIdAsync(int id)
        {
            var barco = await _barcoRepository.GetByIdAsync(id);
            if (barco == null) return null;

            return new BarcoDto
            {
                Id = barco.Id,
                Nome = barco.Nome,
                Modelo = barco.Modelo,
                Ano = barco.Ano,
                Tamanho = barco.Tamanho
            };
        }

        // Atualizar um barco
        public async Task<BarcoDto> UpdateAsync(BarcoDto barcoDto)
        {
            if (barcoDto == null) throw new ArgumentNullException(nameof(barcoDto));

            var barco = new Barco
            {
                Id = barcoDto.Id,
                Nome = barcoDto.Nome,
                Modelo = barcoDto.Modelo,
                Ano = barcoDto.Ano,
                Tamanho = barcoDto.Tamanho
            };

            var updatedBarco = await _barcoRepository.UpdateAsync(barco);
            if (updatedBarco == null) return null;

            return new BarcoDto
            {
                Id = updatedBarco.Id,
                Nome = updatedBarco.Nome,
                Modelo = updatedBarco.Modelo,
                Ano = updatedBarco.Ano,
                Tamanho = updatedBarco.Tamanho
            };
        }

        // Excluir um barco
        public async Task<bool> DeleteAsync(int id)
        {
            return await _barcoRepository.DeleteAsync(id);
        }
    }
}
