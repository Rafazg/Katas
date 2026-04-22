using System;
using System.Collections.Generic;
using System.Text;
using PainelDeTarefas.Domain.Entities;
using PainelDeTarefas.Domain.Enums;

namespace PainelDeTarefas.Domain.Interfaces
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<TarefaItem>> ListarAsync(TarefaStatus? status);
        Task<TarefaItem?> BuscarPorIdAsync(Guid id);
        Task<TarefaItem> AdicionarAsync(TarefaItem tarefa);
        Task<TarefaItem> AtualizarAsync(TarefaItem tarefa);
        Task RemoverAsync(Guid id);
    }
}
