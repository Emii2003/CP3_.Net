using CP3.Domain.Entities;
using CP3.Domain.Interfaces.Dtos;

namespace CP3.Domain.Interfaces
{
    public interface IBarcoApplicationService
    {
        IEnumerable<BarcoEntity> ObterTodosBarcos();
        BarcoEntity ObterBarcoPorId(int id);
        BarcoEntity AdicionarBarco(IBarcoDto entity);
        BarcoEntity EditarBarco(int id, IBarcoDto entity);
        BarcoEntity RemoverBarco(int id);

    }
}
