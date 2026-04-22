# Projeto Katas

## Sobre mim
- **Nome:** José Claudeniro Rafael da Silva  
- **Telefone:** 81973436188  
- **E-mail:** claudenirorafaelbjj@gmail.com  

---

## Stack Utilizada

- **Linguages:** C#, Python  
- **Frameworks:** .NET, Tailwindcss, React  
- **Arquitetura:** Clean Architecture  
- **Testes:** xUnit
---


# Instruções para rodar localmente


<details>
<summary>KATA 1</summary>

## Pré-requisitos
 
- [.NET 8 SDK](https://dotnet.microsoft.com/download)

## Rodando os testes
 
### 1. Restaurar dependências
 
```bash
cd kata-1\TriagemClinica\
dotnet restore
```
 
### 2. Executar todos os testes
 
```bash
dotnet test
```
 
### 3. Executar com detalhes por teste
 
```bash
dotnet test --logger "console;verbosity=detailed"
```

---
 
## Casos de borda cobertos nos testes
 
- Menor de 18 com urgência CRÍTICA mantém CRÍTICA (não ultrapassa o teto)
- Menor de 18 com urgência MÉDIA sobe para ALTA (regras 4 e 5 não se acumulam)
- Idoso com urgência CRÍTICA mantém CRÍTICA (já está no teto)
- FIFO respeitado entre pacientes do mesmo nível após promoção
</details>

---

<details>
<summary>KATA 2</summary>

## Pré-requisitos
 

- **.NET 10 SDK**
- **Node.js 18+**
- **dotnet-ef**

Instalar o dotnet-ef caso não tenha:
```bash
dotnet tool install --global dotnet-ef
```
---

## Rodando o Backend
### 1. Restaurar dependências
 
```bash
cd kata-2/src/PainelDeTarefas.Api
dotnet restore
```

### 2. Aplicar as migrations e criar o banco
 
```bash
dotnet ef database update --project ../PainelDeTarefas.Infrastructure
```
 
> O `paineldetarefas.db` será criado automaticamente na camada API.

### 3. Iniciar a API
 
 Vá até o caminho `kata-2/src/PainelDeTarefas.Api` e execute:
```bash
dotnet run
```

A API ficará disponível em:
 
```
http://localhost:5058
https://localhost:7106
```
 
### 4. Acessar a documentação Swagger
#### Utilizei swagger para facilitar os testes e a visualização dos endpoints.
```
http://localhost:5058/swagger
```
 
---
 
## Endpoints disponíveis
 
| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/tasks` | Lista todas as tarefas |
| `GET` | `/tasks?status=Pendente` | Filtra por status |
| `POST` | `/tasks` | Cria uma nova tarefa |
| `PATCH` | `/tasks/{id}` | Atualiza título, status ou prioridade |
| `DELETE` | `/tasks/{id}` | Remove uma tarefa |

---

## Rodando o Frontend
 
### 1. Instalar dependências
 
```bash
cd kata-2/frontend/painel-tarefas
npm install
```
 
### 2. Iniciar o servidor de desenvolvimento
 Vá até o caminho `kata-2/frontend` e execute:

```bash
npm run dev
```
 
O frontend ficará disponível em:
 
```
http://localhost:5173
```
</details>



# Comentários livres: o que você faria diferente com mais tempo?