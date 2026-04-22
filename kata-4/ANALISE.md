# Análise — Kata 3: Pipeline de Dados

## 1. Principais decisões de tratamento

### Datas em formatos mistos
Implementei uma função `parse_data` que detecta o formato por expressão regular
antes de fazer o parsing. Os formatos suportados são `AAAA-MM-DD`,
`DD/MM/AAAA` e timestamps ISO (`AAAA-MM-DDTHH:MM:SS`). Para timestamps, apenas
a parte da data é aproveitada, descartando o horário.

### Valores monetários
A função `parse_valor` normaliza strings como `"1.750,00"` removendo pontos de
milhar e trocando a vírgula decimal por ponto antes de converter para Float.

### Campos nulos em colunas obrigatórias
Pedidos sem `id_cliente` são descartados do consolidado, pois sem essa
chave não há como identificar o cliente — o registro seria inútil no relatório.
Pedidos sem `valor_total` são mantidos com o campo nulo, pois ainda contêm
informações relevantes de status e entrega.

### Registros órfãos (orphan records)
Entregas cujo `id_pedido` não existe em `pedidos.csv` são simplesmente ignoradas
durante a construção do consolidado. A contagem de órfãos é exibida no resumo
final para fins de auditoria, mas eles não comprometem o pipeline.

### Normalização de cidades
Aplicada a função `normalizar_cidade`, que faz `capitalize` em cada palavra
após um `split(' ')`. Isso resolve variações como `"sao paulo"`, `"SAO PAULO"`
e `"São Paulo"` — todas viram `"Sao Paulo"`. A remoção de acentos não foi feita
pois exigiria uma biblioteca externa, e o `capitalize` já é suficiente para
uniformizar a grafia.

---

## 2. O pipeline é idempotente?

**Sim.** Rodá-lo múltiplas vezes produz sempre o mesmo resultado.

Os arquivos de saída são sobrescritos a cada execução (`CSV.open` com modo `'w'`
e `File.write`), não concatenados. A lógica de transformação é puramente
funcional — não há estado externo, contadores persistentes ou efeitos
colaterais acumulativos. Para os mesmos arquivos de entrada, a saída será
sempre idêntica.

---

## 3. Pipeline com 10 milhões de linhas

A abordagem atual carrega tudo em memória com `CSV.read`, o que se tornaria
inviável nessa escala. As mudanças seriam:

**Processamento em stream:** substituir `CSV.read` por `CSV.foreach`, lendo
uma linha por vez sem carregar o arquivo inteiro em memória.

**Banco de dados intermediário:** usar Oracel ou PostgreSQL para fazer os
JOINs entre pedidos, clientes e entregas — operação natural de banco que
escala muito melhor do que hashes em memória.

**Ferramenta especializada:** para volumes reais de produção, a escolha seria
migrar para Apache Spark, dbt ou um pipeline de dados com Airflow, onde
esse tipo de transformação é um caso de uso nativo.

---

## 4. Testes para garantir a qualidade das transformações

Os testes seriam escritos com **pytest** e cobririam:

**Funções de parsing:**
- `parse_data` recebe `"15/02/2024"` → retorna `Date.new(2024, 2, 15)`
- `parse_data` recebe `"2024-02-15T10:00:00"` → retorna `Date.new(2024, 2, 15)`
- `parse_data` recebe `nil` ou `""` → retorna `nil`
- `parse_valor` recebe `"1.750,00"` → retorna `1750.0`
- `parse_valor` recebe `""` → retorna `nil`

**Normalização:**
- `normalizar_cidade` recebe `"SAO PAULO"` → retorna `"Sao Paulo"`
- `normalizar_cidade` recebe `"sao paulo"` → retorna `"Sao Paulo"`

**Regras de negócio do consolidado:**
- Pedido sem `id_cliente` não aparece no consolidado
- Entrega órfã não gera linha no consolidado
- `atraso_dias` calculado corretamente (positivo, negativo e nulo)

**Idempotência:**
- Rodar o pipeline duas vezes gera arquivos byte-a-byte idênticos
