using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PainelDeTarefas.Application.DTOs
{

    public record CriarTarefaDto(
        [Required, MinLength(3)] string Titulo,
        string Prioridade = "Media"
        );
}
