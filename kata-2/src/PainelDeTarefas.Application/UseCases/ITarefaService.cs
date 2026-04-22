using System;
using System.Collections.Generic;
using System.Text;
using PainelDeTarefas.Application.DTOs;

namespace PainelDeTarefas.Application.Services
{
    public interface ITarefaService
    {
        Task<IEnumerable<TarefaResponseDto>> ListarAsync(string? status);
        Task<TarefaResponseDto> CriarAsync(CriarTarefaDto dto);
        Task<TarefaResponseDto> AtualizarAsync(Guid id, AtualizarTarefaDto dto);
        Task RemoverAsync(Guid id);
    }
}
