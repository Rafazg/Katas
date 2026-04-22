const BORDA = {
  alta:      'border-l-red-500',
  media:     'border-l-yellow-500',
  baixa:     'border-l-gray-600',
  concluida: 'border-l-green-600',
};

const BADGE = {
  alta:  'bg-red-950 text-red-400',
  media: 'bg-yellow-950 text-yellow-500',
  baixa: 'bg-[#0d150d] text-gray-500',
};

export function TarefaItem({ tarefa, onConcluir, onExcluir }) {
  const concluida  = tarefa.status === 'Concluida';
  const prioridade = tarefa.prioridade.toLowerCase();
  const data       = new Date(tarefa.criadoEm).toLocaleDateString('pt-BR', {
    day: '2-digit', month: 'short',
  });

  const bordaClass = concluida ? BORDA.concluida : BORDA[prioridade];

  return (
    <div className={`flex items-center gap-3 bg-[#111a11] border border-[#1a2e1a]
                     border-l-[3px] ${bordaClass} rounded-xl px-3 py-3 sm:px-4 sm:py-3.5
                     ${concluida ? 'opacity-60' : ''}
                     hover:border-[#2a3e2a] transition-colors`}
    >
      {/* Botão de check */}
      <button
        onClick={() => !concluida && onConcluir(tarefa.id)}
        className={`w-5 h-5 rounded-full border-2 flex-shrink-0 flex items-center
                    justify-center transition-all
                    ${concluida
                      ? 'bg-green-600 border-green-600 cursor-default'
                      : 'border-[#1a2e1a] hover:border-green-500 hover:bg-green-950'
                    }`}
        title={concluida ? 'Concluída' : 'Marcar como concluída'}
      >
        {concluida && <span className="text-white text-[10px] font-bold">✓</span>}
      </button>

      {/* Informações */}
      <div className="flex-1 min-w-0">
        <span className={`block text-sm font-medium truncate
                          ${concluida ? 'line-through text-gray-600' : 'text-[#e8eaf0]'}`}>
          {tarefa.titulo}
        </span>
        <div className="flex items-center gap-2 mt-1 flex-wrap">
          {!concluida && (
            <span className={`font-mono text-[10px] px-2 py-0.5 rounded font-medium ${BADGE[prioridade]}`}>
              {tarefa.prioridade}
            </span>
          )}
          <span className="font-mono text-[11px] text-gray-600">{data}</span>
        </div>
      </div>

      {/* Excluir */}
      <button
        onClick={() => onExcluir(tarefa.id)}
        className="text-xs text-red-500 border border-red-950 px-2.5 py-1.5 sm:px-3
                   rounded-lg hover:bg-red-950 transition-colors flex-shrink-0"
      >
        Excluir
      </button>
    </div>
  );
}
