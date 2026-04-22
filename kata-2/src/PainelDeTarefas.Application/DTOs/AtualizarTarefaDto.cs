using System;
using System.Collections.Generic;
using System.Text;

namespace PainelDeTarefas.Application.DTOs
{
    public record AtualizarTarefaDto(
        string? Titulo,
        string? Prioridade,
        string? Status
        );
}
