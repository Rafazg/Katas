using TriagemClinica.Models;

namespace TriagemClinica.Services;

public class FilaDeTriagem
{
    public IReadOnlyList<Paciente> OrdenarFila(IEnumerable<Paciente> pacientes)
    {
        if (pacientes is null)
            throw new ArgumentNullException(nameof(pacientes));

        return pacientes
            .OrderBy(p => (int)p.UrgenciaEfetiva)
            .ThenBy(p => p.HorarioChegada)
            .ToList();
    }
}