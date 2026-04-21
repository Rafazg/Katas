# Análise — Kata 1: Fila de Triagem

## 1. Estrutura de dados escolhida

Optei por uma lista comum (`List<Paciente>`) como entrada, retornando
`IReadOnlyList<Paciente>` ordenada via LINQ.

Pensei em usar uma `PriorityQueue`, mas ela exige definir a prioridade no momento da inserção.
Isso complicaria, porque a urgência pode mudar depois (por causa das regras de promoção).

Com a lista + ordenação, a lógica fica mais simples:

- A regra de negócio continua dentro do Paciente (UrgenciaEfetiva)
- A fila só organiza os dados
- Fica mais fácil testar cada parte separadamente

## 2. Complexidade de tempo

A ordenação usa OrderBy().ThenBy(), que tem complexidade O(n log n).

Na prática, isso funciona bem para volumes normais.
Mas se estivermos falando de algo muito grande (ex: 1 milhão de pacientes), vale considerar:

Processar os dados em partes (batches)
Ou usar uma estrutura como PriorityQueue para inserções contínuas, com custo menor por operação

## 3. Interação entre as regras 4 e 5

As regras não se acumulam, pois nenhuma pessoa pode ter simultaneamente menos
de 18 e mais de 60 anos. Ainda assim, elas precisam ser aplicadas na ordem
correta quando existe promoção encadeada.

**Cenário: 15 anos + urgência MÉDIA**
- Regra 4 não se aplica (paciente não é idoso)
- Regra 5 se aplica: MÉDIA → ALTA (+1 nível)
- Resultado: o paciente entra na fila como **ALTA**

Se o paciente tivesse 15 anos e urgência ALTA, subiria para **CRÍTICA** — o
teto do sistema. A implementação trata esse limite explicitamente para evitar
que o cast numérico produza um vAs regras não entram em conflito, porque:

Um paciente não pode ser menor de 18 e maior de 60 ao mesmo tempo

Mesmo assim, é importante entender o comportamento:

Exemplo:
paciente com 15 anos e urgência MÉDIA

Regra de idoso não se aplica
Regra de menor se aplica → sobe um nível (MÉDIA → ALTA)

Resultado: ALTA

Outro caso:
15 anos + urgência ALTA

Sobe para CRÍTICA (limite máximo)

A implementação já protege esse limite para evitar valores inválidos

## 4. Extensibilidade para uma Regra 6

A promoção de urgência fica encapsulada no método `AplicarRegrasDePromocao()`
dentro de `Paciente`.
Se o sistema crescer (ex: regras diferentes por hospital), dá pra evoluir usando uma abordagem mais flexível:

Criar uma interface para cada regra:
`IRegraDePromocao`, aplicando o princípio Open/Closed:

```csharp
public interface IRegraDePromocao
{
    NivelUrgencia Aplicar(Paciente paciente, NivelUrgencia urgenciaAtual);
}
```

Assim, novas regras seriam adicionadas sem modificar código existente.

---

## Parte C — Modelagem de banco

```sql

-- TABELA: PACIENTES

CREATE TABLE pacientes (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    idade INT NOT NULL
);


-- TABELA: FILA_TRIAGEM

CREATE TABLE fila_triagem (
    id SERIAL PRIMARY KEY,
    paciente_id INT NOT NULL,
    nivel_urgencia VARCHAR(10) NOT NULL,
    horario_chegada TIMESTAMP NOT NULL,

    FOREIGN KEY (paciente_id) REFERENCES pacientes(id)
);


-- TABELA: ATENDIMENTOS

CREATE TABLE atendimentos (
    id SERIAL PRIMARY KEY,
    paciente_id INT NOT NULL,
    horario_inicio TIMESTAMP,
    horario_fim TIMESTAMP,

    FOREIGN KEY (paciente_id) REFERENCES pacientes(id)
);
```