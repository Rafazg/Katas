using PainelDeTarefas.Domain.Enums;
using PainelDeTarefas.Application.DTOs;
using PainelDeTarefas.Domain.Entities;
using PainelDeTarefas.Domain.Enums;
using PainelDeTarefas.Domain.Interfaces;
using DomainTarefaStatus = PainelDeTarefas.Domain.Enums.TarefaStatus;

namespace PainelDeTarefas.Application.Services;

public class TarefaService(ITarefaRepository repository) : ITarefaService
{
    public async Task<IEnumerable<TarefaResponseDto>> ListarAsync(string? status)
    {
        DomainTarefaStatus? filtro = null;

        if (!string.IsNullOrWhiteSpace(status))
        {
            if (!Enum.TryParse<DomainTarefaStatus>(status, ignoreCase: true, out var parsed))
                throw new ArgumentException($"Status inválido: {status}");

            filtro = parsed;
        }

        var tasks = await repository.ListarAsync(filtro);
        return tasks.Select(TarefaResponseDto.FromEntity);
    }

    public async Task<TarefaResponseDto> CriarAsync(CriarTarefaDto dto)
    {
        if (!Enum.TryParse<Prioridade>(dto.Prioridade, ignoreCase: true, out var prioridade))
            throw new ArgumentException($"Prioridade inválida: {dto.Prioridade}");

        var tarefa = new TarefaItem(dto.Titulo, prioridade);
        await repository.AdicionarAsync(tarefa);

        return TarefaResponseDto.FromEntity(tarefa);
    }

    public async Task<TarefaResponseDto> AtualizarAsync(Guid id, AtualizarTarefaDto dto)
    {
        var task = await repository.BuscarPorIdAsync(id)
            ?? throw new KeyNotFoundException($"Tarefa {id} não encontrada.");

        if (!string.IsNullOrWhiteSpace(dto.Titulo))
            task.AtualizarTitulo(dto.Titulo);

        if (!string.IsNullOrWhiteSpace(dto.Status))
        {
            if (!Enum.TryParse<DomainTarefaStatus>(dto.Status, ignoreCase: true, out var parsedStatus))
                throw new ArgumentException($"Status inválido: {dto.Status}");

            if (parsedStatus == DomainTarefaStatus.Concluida)
                task.MarcarComoConcluida();
        }

        if (!string.IsNullOrWhiteSpace(dto.Prioridade))
        {
            if (!Enum.TryParse<Prioridade>(dto.Prioridade, ignoreCase: true, out var parsedPrioridade))
                throw new ArgumentException($"Prioridade inválida: {dto.Prioridade}");

            task.AtualizarPrioridade(parsedPrioridade);
        }

        await repository.AtualizarAsync(task);
        return TarefaResponseDto.FromEntity(task);
    }


    public async Task RemoverAsync(Guid id)
    {
        var task = await repository.BuscarPorIdAsync(id)
            ?? throw new KeyNotFoundException($"Tarefa {id} não encontrada.");

        await repository.RemoverAsync(task.Id);
    }
}