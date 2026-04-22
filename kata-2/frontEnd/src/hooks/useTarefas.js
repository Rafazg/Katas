import { useState, useEffect, useCallback } from 'react';
import { tarefaService } from '../services/tarefaService';

export function useTarefas(filtro) {
  const [tarefas,  setTarefas]  = useState([]);
  const [loading,  setLoading]  = useState(true);
  const [erro,     setErro]     = useState(null);

  const carregar = useCallback(async () => {
    setLoading(true);
    setErro(null);
    try {
      const data = await tarefaService.listar(filtro);
      setTarefas(data);
    } catch (e) {
      setErro(e.message);
    } finally {
      setLoading(false);
    }
  }, [filtro]);

  useEffect(() => { carregar(); }, [carregar]);

  const criar = async (titulo, prioridade) => {
    await tarefaService.criar(titulo, prioridade);
    await carregar();
  };

  const concluir = async (id) => {
    await tarefaService.concluir(id);
    await carregar();
  };

  const excluir = async (id) => {
    await tarefaService.excluir(id);
    await carregar();
  };

  const pendentes  = tarefas.filter(t => t.status === 'Pendente').length;
  const concluidas = tarefas.filter(t => t.status === 'Concluida').length;

  return { tarefas, loading, erro, pendentes, concluidas, criar, concluir, excluir };
}
