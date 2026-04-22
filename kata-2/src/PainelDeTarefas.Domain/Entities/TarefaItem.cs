using PainelDeTarefas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace PainelDeTarefas.Domain.Entities
{
    public class TarefaItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Titulo { get; private set; } = string.Empty;
        public TarefaStatus Status { get; private set; } = TarefaStatus.Pendente;
        public Prioridade Prioridade { get; private set; } = Prioridade.Baixa;
        public DateTime DataCriacao { get; private set; } = DateTime.UtcNow;

        //construtor sem parametro para o EF Core
        protected TarefaItem()
        {

        }

        public TarefaItem(string titulo, Prioridade prioridade) 
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new ArgumentException("O título da tarefa não pode ser vazio.", nameof(titulo));
            }

            Titulo = titulo;
            Prioridade = prioridade;
        }

        public void AtualizarTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new ArgumentException("O título da tarefa não pode ser vazio.", nameof(titulo));
            }
            Titulo = titulo;
        }

        public void AtualizarPrioridade(Prioridade prioridade)
        {
            Prioridade = prioridade;
        }

        public void MarcarComoConcluida()
        {
            Status = TarefaStatus.Concluida;
        }

    }
}
