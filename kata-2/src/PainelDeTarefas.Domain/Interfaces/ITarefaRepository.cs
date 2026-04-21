using System;
using System.Collections.Generic;
using System.Text;
using PainelDeTarefas.Domain.Entities;
using PainelDeTarefas.Domain.Enums;

namespace PainelDeTarefas.Domain.Interfaces
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<TarefaItem>> ListarAsync(TarefaItem? status);
        Task<TarefaItem?> BuscarPorIdAsync(Guid id);
        Task<TarefaItem> AdicionarAsync(TarefaItem task);
        Task<TarefaItem> AtualizarAsync(TarefaItem task);
        Task RemoverAsync(Guid id);
    }
}
