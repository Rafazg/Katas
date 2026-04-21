# Requisitos — Kata 2: Painel de Tarefas

## 1. Ambiguidades 

### Ambiguidade 1 — O que é "minhas tarefas"?
Ficou a dúvida se cada usuário teria suas próprias tarefas (o que exigiria login/autenticação)ou se seria algo mais simples.

**Pergunta:** O painel exibe tarefas de um único usuário fixo ou cada usuário tem suas próprias tarefas (exige autenticação)?

**Resposta / Decisão:** Mantive o escopo simples, sem autenticação.
Todas as tarefas pertencem a um contexto compartilhado. A parte de usuários pode ser adicionada no futuro sem impactar a base atual.

---

### Ambiguidade 2 — O que é "situação" de uma tarefa?
Poderia ter vários estados (em andamento, cancelada, arquivada…), mas isso não estava definido.

**Pergunta:** Existem mais estados além de pendente e concluída?

**Decisão tomada:** Dois estados apenas — `Pendente` e `Concluida`.
suficientes para atender o requisito descrito. Novos estados podem
ser adicionados via enum sem quebrar a estrutura atual.

---

### Ambiguidade 3 — Tarefas excluídas somem para sempre?
 Apagar de vez ou manter histórico?

**Pergunta:** Há necessidade de soft delete (manter histórico)
ou o delete é permanente?

**Decisão tomada:** Delete permanente (hard delete). Sem requisito
explícito de auditoria ou histórico neste escopo.

---

### Ambiguidade 4 — O filtro substitui ou complementa a listagem?
Quando filtrar por “pendentes”, as concluídas aparecem de alguma forma ou somem?

**Pergunta:** Ao filtrar por "pendentes", as concluídas ficam ocultas
ou marcadas de forma diferente?

**Decisão tomada:** O filtro oculta completamente os outros itens.
Sem filtro, todas as tarefas são exibidas.

---

## 2. Requisitos Funcionais (RF)

- RF01: O sistema deve listar todas as tarefas cadastradas 
- RF02: O sistema deve permitir criar uma nova tarefa com título e prioridade 
- RF03: O sistema deve permitir marcar uma tarefa como concluída 
- RF04: O sistema deve permitir excluir uma tarefa permanentemente 
- RF05: O sistema deve permitir filtrar tarefas por status (pendente/concluída) 
- RF06: O sistema deve exibir indicação visual do status e prioridade da tarefa 

## 3. Requisitos Não Funcionais (RNF)

- RNF01: A API deve retornar respostas em JSON 
- RNF02: Erros devem retornar códigos HTTP adequados (400, 404, 422) 
- RNF03: O frontend deve funcionar sem dependências externas de framework 
- RNF04: O tempo de resposta da API deve ser inferior a 200ms para listas pequenas 

## 4. Tratamento do requisito de prioridade

O cliente mencionou prioridade como algo secundário, um tipo de "pode ficar pra depois".

**Decisão:** implementei prioridade desde o início por dois motivos:

1. O custo de adicionar agora (campo no modelo + select no frontend) é
   mínimo comparado ao custo de refatorar depois
2. Evita refatorações futuras (como migrations ou mudanças na API)

No backlog, isso ficaria como:

[MELHORIA] Exibir e ordenar tarefas por prioridade
Status: implementado preventivamente (sem destaque na UI principal)