using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PainelDeTarefas.Domain.Entities;

namespace PainelDeTarefas.Infrastructure.Mappings;

public class TarefaItemMapping : IEntityTypeConfiguration<TarefaItem>
{
    public void Configure(EntityTypeBuilder<TarefaItem> builder)
    {
        builder.ToTable("Tarefas");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(t => t.Titulo)
            .HasColumnName("Titulo")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.Status)
            .HasColumnName("Status")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(t => t.Prioridade)
            .HasColumnName("Prioridade")
            .HasConversion<string>()
            .IsRequired();

        builder.Property(t => t.DataCriacao)
            .HasColumnName("CriadoEm")
            .IsRequired();
    }
}