using PainelDeTarefas.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PainelDeTarefas.Application.DTOs
{
    public record TarefaResponseDto(
        Guid Id,
        string Titulo,
        string Status,
        string Prioridade,
        DateTime CriadoEm
     )
     {
        public static TarefaResponseDto FromEntity(TarefaItem tarefa) =>
            new TarefaResponseDto(
                tarefa.Id,
                tarefa.Titulo,
                tarefa.Status.ToString(),
                tarefa.Prioridade.ToString(),
                tarefa.DataCriacao
            );
    }
}
