import { TarefaItem } from './TarefaItem';

export function TarefaLista({ tarefas, loading, erro, onConcluir, onExcluir }) {
  if (loading) {
    return (
      <div className="text-center py-12 text-gray-600 font-mono text-sm">
        carregando tarefas...
      </div>
    );
  }

  if (erro) {
    return (
      <div className="text-center py-12 text-gray-600">
        <div className="text-4xl mb-3">⚠️</div>
        <p className="text-sm">Não foi possível conectar à API.</p>
        <p className="text-xs text-red-500 mt-1">{erro}</p>
      </div>
    );
  }

  if (!tarefas.length) {
    return (
      <div className="text-center py-12 text-gray-600">
        <div className="text-4xl mb-3">📭</div>
        <p className="text-sm">Nenhuma tarefa encontrada.</p>
      </div>
    );
  }

  return (
    <div className="flex flex-col gap-2">
      {tarefas.map(t => (
        <TarefaItem
          key={t.id}
          tarefa={t}
          onConcluir={onConcluir}
          onExcluir={onExcluir}
        />
      ))}
    </div>
  );
}
