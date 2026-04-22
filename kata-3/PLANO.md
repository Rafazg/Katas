# PLANO DE AÇÃO — SISTEMA LEGADO EM COLAPSO

## 1. Diagnóstico

### 1. Endpoint lento (8–12 segundos)

* **Causa provável:** consultas ineficientes, ausência de índices ou excesso de processamento no banco.
* **Risco:** degradação da experiência do usuário e possível perda de vendas.
* **Classificação:** Urgente e Importante.

---

### 2. Pedidos duplicados

* **Causa provável:** ausência de controle de idempotência ou falhas de concorrência.
* **Risco:** impacto financeiro direto e inconsistência de dados.
* **Classificação:** Urgente e Importante.

---

### 3. Correção direta em produção (sem PR/teste)

* **Causa provável:** falta de processo de deploy estruturado e cultura de qualidade.
* **Risco:** introdução de novos bugs e perda de rastreabilidade.
* **Classificação:** Importante (alta prioridade estrutural).

---

### 4. Arquivo com mais de 4.000 linhas

* **Causa provável:** acúmulo técnico e ausência de separação de responsabilidades.
* **Risco:** dificuldade de manutenção, alto risco de regressão.
* **Classificação:** Importante.

---

### 5. Ausência de testes automatizados

* **Causa provável:** falta de cultura de testes e pressão por entrega rápida.
* **Risco:** baixa confiabilidade do sistema e medo de alterações.
* **Classificação:** Urgente e Importante.

---

## 2. Plano de Ação (Prioridades)

### Ação 1 — Implementar testes automatizados básicos

* **Descrição:** criar testes unitários para regras críticas (ex: criação de pedidos, cálculo de frete).
* **Esforço estimado:** 2 a 3 dias
* **Critério de sucesso:** cobertura mínima das funcionalidades críticas e redução de falhas em produção.

---

### Ação 2 — Corrigir duplicidade de pedidos

* **Descrição:** implementar controle de idempotência (ex: chave única por requisição) e revisar transações no banco.
* **Esforço estimado:** 1 a 2 dias
* **Critério de sucesso:** nenhum novo caso de duplicidade após implantação.

---

### Ação 3 — Otimizar endpoint de consulta

* **Descrição:** analisar queries, adicionar índices e reduzir processamento desnecessário.
* **Esforço estimado:** 2 dias
* **Critério de sucesso:** tempo de resposta inferior a 2 segundos em horário de pico.

---

## 3. Decisão de Arquitetura

**Escolha:** Refatoração incremental

**Justificativa:**

Dado que o sistema está em produção, sem testes automatizados e com alta carga diária, a refatoração incremental é a abordagem mais segura.

Reescrever o módulo inteiro (big bang) traria alto risco de regressão, especialmente sem uma base de testes confiável para validação.

A estratégia incremental permite:

* reduzir riscos
* manter o sistema funcionando durante as melhorias
* introduzir testes gradualmente
* melhorar a qualidade do código aos poucos

---

## 4. Requisitos Não Funcionais ignorados

### 1. Desempenho

* **Problema:** endpoint lento (8–12s)
* **Impacto:** experiência ruim e perda de usuários
* **Métrica:** tempo médio de resposta < 2 segundos

---

### 2. Manutenibilidade

* **Problema:** arquivo de 4.000 linhas e ausência de organização
* **Impacto:** dificuldade de evolução e alto risco de bugs
* **Métrica:** redução do tamanho médio das classes e aumento da cobertura de testes

---

### 3. Confiabilidade

* **Problema:** duplicidade de pedidos e ausência de testes
* **Impacto:** inconsistência de dados e falhas em produção
* **Métrica:** taxa de erros próxima de zero em operações críticas

