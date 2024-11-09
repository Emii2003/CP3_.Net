using CP3.Application.Dtos;
using CP3.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CP3.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarcoController : ControllerBase
    {
        private readonly IBarcoApplicationService _applicationService;

        public BarcoController(IBarcoApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        // Endpoint para obter todos os barcos
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BarcoDto>), 200)] // Resposta 200 com a lista de BarcoDto
        public async Task<IActionResult> Get()
        {
            var barcos = await _applicationService.GetAllAsync();

            if (barcos != null)
                return Ok(barcos);

            return BadRequest("Não foi possível obter os dados.");
        }

        // Endpoint para obter um barco por ID
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BarcoDto), 200)] // Resposta 200 com o BarcoDto
        [ProducesResponseType(404)] // Resposta 404 quando não encontrar
        public async Task<IActionResult> GetPorId(int id)
        {
            var barco = await _applicationService.GetByIdAsync(id);

            if (barco != null)
                return Ok(barco);

            return NotFound("Barco não encontrado.");
        }

        // Endpoint para adicionar um novo barco
        [HttpPost]
        [ProducesResponseType(typeof(BarcoDto), 201)] // Resposta 201 para criação de recurso
        [ProducesResponseType(400)] // Resposta 400 para erro de validação ou outros problemas
        public async Task<IActionResult> Post([FromBody] BarcoDto barcoDto)
        {
            try
            {
                var barco = await _applicationService.AddAsync(barcoDto);

                if (barco != null)
                    return CreatedAtAction(nameof(GetPorId), new { id = barco.Id }, barco); // Retorna 201 com o local do novo recurso

                return BadRequest("Não foi possível salvar os dados.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message, Status = HttpStatusCode.BadRequest });
            }
        }

        // Endpoint para atualizar um barco existente
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BarcoDto), 200)] // Resposta 200 com o BarcoDto atualizado
        [ProducesResponseType(400)] // Resposta 400 para erro de validação ou outro problema
        [ProducesResponseType(404)] // Resposta 404 caso não encontre o barco para atualização
        public async Task<IActionResult> Put(int id, [FromBody] BarcoDto barcoDto)
        {
            try
            {
                var barco = await _applicationService.UpdateAsync(id, barcoDto);

                if (barco != null)
                    return Ok(barco);

                return NotFound("Barco não encontrado para edição.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message, Status = HttpStatusCode.BadRequest });
            }
        }

        // Endpoint para excluir um barco
        [HttpDelete("{id}")]
        [ProducesResponseType(200)] // Resposta 200 quando excluir com sucesso
        [ProducesResponseType(404)] // Resposta 404 caso o barco não seja encontrado
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _applicationService.DeleteAsync(id);

            if (isDeleted)
                return Ok(new { Message = "Barco excluído com sucesso." });

            return NotFound("Barco não encontrado para exclusão.");
        }
    }
}

