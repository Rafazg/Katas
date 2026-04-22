import { useState } from 'react';
import { TarefaForm }  from './components/TarefaForm';
import { FiltroBar }   from './components/FiltroBar';
import { TarefaLista } from './components/TarefaLista';
import { useTarefas }  from './hooks/useTarefas';

function App() {
  const [filtro, setFiltro] = useState(null);

  const { tarefas, loading, erro, pendentes, concluidas, criar, concluir, excluir } =
    useTarefas(filtro);

  return (
    <div className="min-h-screen bg-[#0a0f0a] text-[#e8eaf0] px-4 py-6 sm:px-6 sm:py-8 md:p-10">
      <div className="max-w-2xl mx-auto">

        {/* Header */}
        <header className="flex flex-col gap-4 sm:flex-row sm:items-end sm:justify-between
                           mb-8 pb-6 border-b border-[#1a2e1a]">
          <div className="flex items-center gap-3">
            <div className="w-10 h-10 bg-green-500 rounded-xl flex items-center
                            justify-center text-lg flex-shrink-0">
              ✦
            </div>
            <div>
              <h1 className="text-xl font-semibold tracking-tight">Painel de Tarefas</h1>
              <span className="text-xs text-gray-500 font-mono">task management</span>
            </div>
          </div>
          <div className="font-mono text-xs text-gray-500">
            <span className="text-green-400 font-medium">{pendentes}</span> pendentes
            {' · '}
            <span className="text-green-400 font-medium">{concluidas}</span> concluídas
          </div>
        </header>

        <TarefaForm onCriar={criar} />
        <FiltroBar filtroAtivo={filtro} onChange={setFiltro} />
        <TarefaLista
          tarefas={tarefas}
          loading={loading}
          erro={erro}
          onConcluir={concluir}
          onExcluir={excluir}
        />

      </div>
    </div>
  );
}

export default App;
