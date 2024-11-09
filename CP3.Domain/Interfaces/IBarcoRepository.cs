using CP3.Domain.Entities;

namespace CP3.Domain.Interfaces
{
    public interface IBarcoRepository
    {
        BarcoEntity? ObterPorId(int id);
        IEnumerable<BarcoEntity>? ObterTodos();
        BarcoEntity? Adicionar(BarcoEntity cliente);
        BarcoEntity? Editar(BarcoEntity cliente);
        BarcoEntity? Remover(int id);

    }
}
