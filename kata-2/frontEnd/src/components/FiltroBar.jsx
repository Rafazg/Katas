const FILTROS = [
  { label: 'Todas',      valor: null },
  { label: 'Pendentes',  valor: 'Pendente' },
  { label: 'Concluídas', valor: 'Concluida' },
];

export function FiltroBar({ filtroAtivo, onChange }) {
  return (
    <div className="flex gap-2 mb-4 flex-wrap">
      {FILTROS.map(f => (
        <button
          key={f.label}
          onClick={() => onChange(f.valor)}
          className={`px-4 py-1.5 rounded-full text-xs font-medium border transition-all
            ${filtroAtivo === f.valor
              ? 'bg-green-600/20 border-green-600 text-green-400'
              : 'bg-transparent border-[#1a2e1a] text-gray-500 hover:border-green-700 hover:text-gray-300'
            }`}
        >
          {f.label}
        </button>
      ))}
    </div>
  );
}
