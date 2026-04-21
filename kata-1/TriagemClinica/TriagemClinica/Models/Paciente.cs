namespace TriagemClinica.Models;

public enum NivelUrgencia
{
    Baixa = 3,
    Media = 2,
    Alta = 1,
    Critica = 0
}

public class Paciente
{
    public string Nome { get; set; } = string.Empty;
    public int Idade { get; set; }
    public NivelUrgencia Urgencia { get; set; }
    public TimeSpan HorarioChegada { get; set; }

    // Urgência efetiva após aplicar as regras de promoção
    public NivelUrgencia UrgenciaEfetiva => AplicarRegrasDePromocao();

    private NivelUrgencia AplicarRegrasDePromocao()
    {
        var urgencia = Urgencia;

        // Regra 4: idosos (60+) com MÉDIA sobem para ALTA
        if (Idade >= 60 && urgencia == NivelUrgencia.Media)
            urgencia = NivelUrgencia.Alta;

        // Regra 5: menores de 18 ganham +1 nível (valor menor = mais prioritário)
        if (Idade < 18 && urgencia != NivelUrgencia.Critica)
            urgencia = (NivelUrgencia)((int)urgencia - 1);

        return urgencia;
    }
}