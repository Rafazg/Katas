#  Engenharia — Kata 2: Painel de Tarefas

## 1. Como organizei o backend

Utilizei ASP.NET Core com Clean Architecture, buscando separar bem as responsabilidades entre as camadas e facilitar futuras mudanças. Essa abordagem permite trocar a forma de persistência sem impactar as regras de negócio.

### Por que usei persistência SQLite?

Visando velocidade e praticidade.

Focar na lógica e na arquitetura sem perder tempo configurando banco. Assim o projeto roda direto com um `dotnet run`.

Em um cenário real, eu trocaria isso por **OracleDB com EF Core** sem mudar a estrutura geral.

---

## 2. Pensando em produção (confiabilidade)

Se fosse um sistema real, algumas melhorias seriam aplicadas.

### Observabilidade

Implementaria logs estruturados com **Serilog**, gerando saída em JSON.

Isso permite:

* rastrear requisições
* entender erros com mais contexto
* medir tempo de resposta

---

### Health checks

Criaria um endpoint `/health` usando `AspNetCore.Diagnostics.HealthChecks`.

Isso facilita integração com ferramentas como Kubernetes ou ECS, que precisam saber se a aplicação está saudável.

---

### Tratamento global de erros

Adicionaria um middleware central para tratar exceções.

Objetivos:

* evitar expor stack trace para o usuário
* manter um padrão de resposta de erro
* centralizar a lógica de tratamento

---

## 3. E se tiver múltiplos usuários?

Hoje o sistema é simples, mas dá pra evoluir sem quebrar.

### O que precisaria mudar:

1. **Entities**

   * Adicionar `UsuarioId` (Guid) em `TarefaItem`

2. **Repository**

   * Passar a filtrar tudo por `UsuarioId`

3. **Autenticação**

   * Implementar JWT

4. **Controller**

   * Em vez de receber o usuário por parâmetro, extrair do token
     (`ClaimTypes.NameIdentifier`)

5. **Banco de dados**

   * Criar tabela de usuários
   * Relacionamento 1 (um usuário → várias tarefas)



