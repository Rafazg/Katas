# Processo Seletivo Unimed Caruaru

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
## Justificativa da Escolha
 * **C# / .NET:** por já ter utilizado o conjunto em outros projetos pessoais, ser uma tecnologia sólida, tendo uma boa base de documentações e alinhada com o contexto da empresa
 * **React com TailwindCSS:** O React facilita a construção de interfaces reutilizáveis e organizadas, enquanto o Tailwind agiliza a estilização sem a necessidade de criar arquivos CSS separados.
 * **Python:** Amplamente usada em análise e processamento de dados e utilizada na stack da empresa
 * **SQLite com EF Core:** por ser uma solução leve e fácil de executar, sem necessidade de configuração adicional
 * **xUnit:** Framework de testes padrão para aplicações em .NET
 * **** 

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
npm start 
```
 
O frontend ficará disponível em:
 
```
http://localhost:5173
```
</details>


---
<details>
<summary>KATA 3</summary>

# Kata 3 — Análise de Engenharia de Software
## Analise e decisões estão presentes no arquivo PLANO.md

</details>

---

<details>
<summary>KATA 4</summary>

# Kata 4 — Pipeline de Relatório

## Pré-requisitos
- [Python 3.8+](https://www.python.org/downloads/)

- Verifique a instalação:
 
```bash
python --version
```
> Nenhuma instalação de pacotes externos é necessária. O projeto utiliza apenas bibliotecas da biblioteca padrão do Python.

### Passo 1 — Rodar o pipeline
 
```bash
cd kata-3
python pipeline.py
```
O pipeline irá:
- Ler os três arquivos CSV da pasta `data/`
- Aplicar as limpezas e normalizações nos dados
- Gerar os arquivos consolidados na pasta `output/`

### Passo 2 — Exibir os indicadores
 
```bash
python indicadores.py
```
> O `pipeline.py` precisa ser executado antes do `indicadores.py`, pois os indicadores leem o arquivo `output/consolidado.json` gerado pelo pipeline.
</details>



## Comentários livres: o que eu faria diferente com mais tempo

Com mais tempo, eu focaria principalmente em melhorar a qualidade e a organização do projeto.

Eu adicionaria mais testes automatizados, cobrindo melhor os cenários de borda e garantindo mais segurança para evoluir o código. Também refinaria alguns pontos da arquitetura, buscando deixar a separação de responsabilidades ainda mais clara.

No frontend, melhoraria a experiência do usuário com feedbacks visuais, como estados de carregamento e mensagens mais claras.

Em relação à persistência de dados, gostaria de evoluir a aplicação para utilizar Oracle, pois já tenho experiência prática com o ambiente e com PL/SQL no meu dia a dia como técnico de suporte. Isso permitiria trabalhar melhor com modelagem e consultas mais próximas de um cenário real da empresa.

Por fim, também consideraria evoluções funcionais, como autenticação de usuários e melhorias no gerenciamento de tarefas, aproximando mais o sistema de um produto real.
