using TriagemClinica.Models;
using TriagemClinica.Services;

namespace TriagemClinica.Tests
{
    public class FilaDeTriagemTests
    {
        private readonly FilaDeTriagem _fila = new();

        // Regra 1/2 — Ordenação básica por urgência
        [Fact]
        public void DeveOrdenarPorNivelDeUrgencia_IndependenteDaChegada()
        {
            var pacientes = new List<Paciente>
        {
            new() { Nome = "Ana",    Idade = 35, Urgencia = NivelUrgencia.Baixa,   HorarioChegada = TimeSpan.Parse("08:00") },
            new() { Nome = "Bruno",  Idade = 40, Urgencia = NivelUrgencia.Critica, HorarioChegada = TimeSpan.Parse("08:10") },
            new() { Nome = "Carla",  Idade = 28, Urgencia = NivelUrgencia.Media,   HorarioChegada = TimeSpan.Parse("07:50") },
            new() { Nome = "Daniel", Idade = 50, Urgencia = NivelUrgencia.Alta,    HorarioChegada = TimeSpan.Parse("08:05") },
        };

            var resultado = _fila.OrdenarFila(pacientes);

            Assert.Equal("Bruno", resultado[0].Nome);
            Assert.Equal("Daniel", resultado[1].Nome);
            Assert.Equal("Carla", resultado[2].Nome);
            Assert.Equal("Ana", resultado[3].Nome);
        }



        // Regra 3 — FIFO dentro do mesmo nível
        [Fact]
        public void DeveManter_FIFO_DentroDoMesmoNivelDeUrgencia()
        {
            var pacientes = new List<Paciente>
        {
            new() { Nome = "Pedro", Idade = 30, Urgencia = NivelUrgencia.Alta, HorarioChegada = TimeSpan.Parse("09:30") },
            new() { Nome = "Julia", Idade = 25, Urgencia = NivelUrgencia.Alta, HorarioChegada = TimeSpan.Parse("09:15") },
            new() { Nome = "Rafa",  Idade = 45, Urgencia = NivelUrgencia.Alta, HorarioChegada = TimeSpan.Parse("09:00") },
        };

            var resultado = _fila.OrdenarFila(pacientes);

            Assert.Equal("Rafa", resultado[0].Nome);
            Assert.Equal("Julia", resultado[1].Nome);
            Assert.Equal("Pedro", resultado[2].Nome);
        }


        // Regra 4 — Idoso com MÉDIA sobe para ALTA
        [Fact]
        public void IdosoComUrgenciaMedia_DeveSerPromovidoParaAlta()
        {
            var pacientes = new List<Paciente>
        {
            new() { Nome = "Marcos",  Idade = 35, Urgencia = NivelUrgencia.Alta,  HorarioChegada = TimeSpan.Parse("10:00") },
            new() { Nome = "Osvaldo", Idade = 68, Urgencia = NivelUrgencia.Media, HorarioChegada = TimeSpan.Parse("09:55") },
        };

            var resultado = _fila.OrdenarFila(pacientes);

            Assert.Equal("Osvaldo", resultado[0].Nome);
            Assert.Equal(NivelUrgencia.Alta, resultado[0].UrgenciaEfetiva);
        }



        // Regra 5 — Menor de 18 ganha +1 nível
        [Fact]
        public void MenorDeIdadeComUrgenciaBaixa_DeveSerPromovidoParaMedia()
        {
            var menor = new Paciente { Nome = "Bia", Idade = 15, Urgencia = NivelUrgencia.Baixa, HorarioChegada = TimeSpan.Parse("11:00") };

            Assert.Equal(NivelUrgencia.Media, menor.UrgenciaEfetiva);
        }

        [Fact]
        public void MenorDeIdadeComUrgenciaAlta_DeveSerPromovidoParaCritica()
        {
            var menor = new Paciente { Nome = "Caio", Idade = 10, Urgencia = NivelUrgencia.Alta, HorarioChegada = TimeSpan.Parse("11:00") };

            Assert.Equal(NivelUrgencia.Critica, menor.UrgenciaEfetiva);
        }

        [Fact]
        public void MenorDeIdadeJaCritica_DeveManterCritica()
        {
            var menor = new Paciente { Nome = "Lara", Idade = 5, Urgencia = NivelUrgencia.Critica, HorarioChegada = TimeSpan.Parse("11:00") };

            Assert.Equal(NivelUrgencia.Critica, menor.UrgenciaEfetiva);
        }



        
        // Caso de borda — Regras 4 e 5
        [Fact]
        public void Paciente15AnosComUrgenciaMedia_DeveChgarEmAlta()
        {
            // Regra 5 se aplica (< 18), Regra 4 não se aplica (< 60)
            // MÉDIA + 1 nível = ALTA
            var paciente = new Paciente { Nome = "Theo", Idade = 15, Urgencia = NivelUrgencia.Media, HorarioChegada = TimeSpan.Parse("08:00") };

            Assert.Equal(NivelUrgencia.Alta, paciente.UrgenciaEfetiva);
        }
    }
}
