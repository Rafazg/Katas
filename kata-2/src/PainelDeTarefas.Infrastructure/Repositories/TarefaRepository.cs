using Microsoft.EntityFrameworkCore;
using PainelDeTarefas.Infrastructure.Context;
using PainelDeTarefas.Domain.Entities;
using PainelDeTarefas.Domain.Enums;
using PainelDeTarefas.Domain.Interfaces;
using PainelDeTarefas.Infrastructure.Context;

namespace PainelDeTarefas.Infrastructure.Repositories;

public class TarefaRepository(AppDbContext context) : ITarefaRepository
{
    public async Task<IEnumerable<TarefaItem>> ListarAsync(TarefaStatus? status)
    {
        var query = context.Tarefas.AsQueryable();

        if (status.HasValue)
            query = query.Where(t => t.Status == status.Value);

        return await query
            .OrderByDescending(t => t.Prioridade)
            .ThenBy(t => t.DataCriacao)
            .ToListAsync();
    }

    public async Task<TarefaItem?> BuscarPorIdAsync(Guid id) =>
        await context.Tarefas.FirstOrDefaultAsync(t => t.Id == id);

    public async Task<TarefaItem> AdicionarAsync(TarefaItem tarefa)
    {
        await context.Tarefas.AddAsync(tarefa);
        await context.SaveChangesAsync();
        return tarefa;
    }

    public async Task<TarefaItem> AtualizarAsync(TarefaItem tarefa)
    {
        context.Tarefas.Update(tarefa);
        await context.SaveChangesAsync();
        return tarefa;
    }

    public async Task RemoverAsync(Guid id)
    {
        var tarefa = await BuscarPorIdAsync(id);
        if (tarefa is not null)
        {
            context.Tarefas.Remove(tarefa);
            await context.SaveChangesAsync();
        }
    }
}