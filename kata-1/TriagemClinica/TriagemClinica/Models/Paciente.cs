namespace TriagemClinica.Models;

public enum NivelUrgencia
{
    Baixa,
    Media,
    Alta,
    Critica
}

public class Paciente
{
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public NivelUrgencia Urgencia { get; set; }
    public TimeSpan HorarioChegada { get; set; }

    
    public NivelUrgencia UrgenciaEfetiva => AplicarRegrasDePromocao();

    private NivelUrgencia AplicarRegrasDePromocao()
    {
        var urgencia = Urgencia;

        // Regra 1
        if (Idade >= 60 && urgencia == NivelUrgencia.Media)
            urgencia = NivelUrgencia.Alta;

        // Regra 2
        if (Idade < 18)
            urgencia = PromoverUrgencia(urgencia);

        return urgencia;
    }

    private NivelUrgencia PromoverUrgencia(NivelUrgencia nivel)
    {
        if (nivel == NivelUrgencia.Baixa)
            return NivelUrgencia.Media;

        if (nivel == NivelUrgencia.Media)
            return NivelUrgencia.Alta;

        if (nivel == NivelUrgencia.Alta)
            return NivelUrgencia.Critica;

        return nivel;
    }
}