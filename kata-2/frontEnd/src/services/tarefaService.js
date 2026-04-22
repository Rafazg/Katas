const API = 'http://localhost:5058/tasks';

async function handleResponse(res) {
  if (!res.ok) {
    const body = await res.json().catch(() => ({}));
    throw new Error(body.erro || `Erro HTTP ${res.status}`);
  }
  if (res.status === 204) return null;
  return res.json();
}

export const tarefaService = {
  listar: (status) => {
    const url = status ? `${API}?status=${status}` : API;
    return fetch(url).then(handleResponse);
  },
  criar: (titulo, prioridade) =>
    fetch(API, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ titulo, prioridade }),
    }).then(handleResponse),
  concluir: (id) =>
    fetch(`${API}/${id}`, {
      method: 'PATCH',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ status: 'Concluida' }),
    }).then(handleResponse),
  excluir: (id) =>
    fetch(`${API}/${id}`, { method: 'DELETE' }).then(handleResponse),
};
