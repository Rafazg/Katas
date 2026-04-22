using Microsoft.EntityFrameworkCore;
using PainelDeTarefas.Domain.Entities;
using PainelDeTarefas.Infrastructure.Mappings;

namespace PainelDeTarefas.Infrastructure.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<TarefaItem> Tarefas => Set<TarefaItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TarefaItemMapping());
    }
}