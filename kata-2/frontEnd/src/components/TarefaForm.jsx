import { useState } from 'react';

export function TarefaForm({ onCriar }) {
  const [titulo,     setTitulo]     = useState('');
  const [prioridade, setPrioridade] = useState('Media');
  const [erro,       setErro]       = useState('');
  const [loading,    setLoading]    = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (titulo.trim().length < 3) {
      setErro('O título precisa ter pelo menos 3 caracteres.');
      return;
    }
    setErro('');
    setLoading(true);
    try {
      await onCriar(titulo.trim(), prioridade);
      setTitulo('');
    } catch (e) {
      setErro(e.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="bg-[#111a11] border border-[#1a2e1a] rounded-2xl p-4 sm:p-5 mb-4">
      <span className="block text-xs font-medium text-gray-500 tracking-widest uppercase mb-3">
        Nova tarefa
      </span>

      <form className="flex flex-col gap-2 sm:flex-row" onSubmit={handleSubmit}>
        <input
          type="text"
          placeholder="Descreva a tarefa..."
          value={titulo}
          onChange={e => setTitulo(e.target.value)}
          disabled={loading}
          className="flex-1 bg-[#0d150d] border border-[#1a2e1a] rounded-lg px-4 py-2.5
                     text-[#e8eaf0] text-sm placeholder-gray-600
                     outline-none focus:border-green-600 transition-colors disabled:opacity-50
                     w-full"
        />

        <div className="flex gap-2">
          <select
            value={prioridade}
            onChange={e => setPrioridade(e.target.value)}
            disabled={loading}
            className="flex-1 sm:flex-none bg-[#0d150d] border border-[#1a2e1a] rounded-lg
                       px-3 py-2.5 text-[#e8eaf0] text-sm outline-none
                       focus:border-green-600 transition-colors cursor-pointer disabled:opacity-50"
          >
            <option value="Alta">🔴 Alta</option>
            <option value="Media">🟡 Média</option>
            <option value="Baixa">⚪ Baixa</option>
          </select>

          <button
            type="submit"
            disabled={loading}
            className="flex-1 sm:flex-none bg-green-600 hover:bg-green-500 text-white
                       text-sm font-medium px-5 py-2.5 rounded-lg transition-all
                       active:scale-95 disabled:opacity-50 disabled:cursor-not-allowed
                       whitespace-nowrap"
          >
            {loading ? '...' : 'Adicionar'}
          </button>
        </div>
      </form>

      {erro && <p className="text-red-400 text-xs mt-2">{erro}</p>}
    </div>
  );
}
