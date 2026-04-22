import csv
import json
import unicodedata
from datetime import datetime
from pathlib import Path


def parse_data(valor):
    """Converte string de data em diferentes formatos para date."""
    if not valor or not valor.strip():
        return None

    texto = valor.strip()

    # Remove parte de hora se vier como timestamp
    if 'T' in texto:
        texto = texto.split('T')[0]

    formatos = ['%Y-%m-%d', '%d/%m/%Y']
    for formato in formatos:
        try:
            return datetime.strptime(texto, formato).date()
        except ValueError:
            continue

    return None


def parse_valor(valor):
    """Converte string monetária para float (trata vírgula decimal e ponto de milhar)."""
    if not valor or not valor.strip():
        return None

    texto = valor.strip()

    if ',' in texto:
        # remove pontos de milhar, troca vírgula por ponto
        texto = texto.replace('.', '').replace(',', '.')

    return round(float(texto), 2)


def normalizar_cidade(cidade):
    """Normaliza cidade para Title Case sem acentos."""
    if not cidade or not cidade.strip():
        return None

    # Remove acentos Usando unicodedata
    sem_acento = unicodedata.normalize('NFD', cidade.strip())
    sem_acento = ''.join(c for c in sem_acento if unicodedata.category(c) != 'Mn')

    return sem_acento.title()


# Leitura dos CSVs

print(" Lendo arquivos CSV...")

def ler_csv(caminho):
    with open(caminho, encoding='utf-8') as f:
        return list(csv.DictReader(f))

pedidos  = ler_csv('data/pedidos.csv')
clientes = ler_csv('data/clientes.csv')
entregas = ler_csv('data/entregas.csv')

print(f"   pedidos : {len(pedidos)} registros")
print(f"   clientes: {len(clientes)} registros")
print(f"   entregas: {len(entregas)} registros")

# indexação para lookup rapido 

clientes_por_id  = {c['id_cliente']: c for c in clientes}
entregas_por_pedido = {e['id_pedido']: e for e in entregas}

# Tratamento e consolidação

print("\n Aplicando transformações...")

ids_pedidos_validos = {p['id_pedido'].strip() for p in pedidos}

orphans     = sum(1 for e in entregas if e['id_pedido'].strip() not in ids_pedidos_validos)
sem_cliente = 0
sem_valor   = 0
consolidado = []

for pedido in pedidos:
    id_pedido  = pedido['id_pedido']
    id_cliente = pedido.get('id_cliente', '').strip()

    # Ignora pedidos sem id_cliente
    if not id_cliente:
        sem_cliente += 1
        print(f"    Pedido {id_pedido} ignorado: sem id_cliente")
        continue

    # Busca cliente
    cliente = clientes_por_id.get(id_cliente)
    if not cliente:
        sem_cliente += 1
        print(f"    Pedido {id_pedido} ignorado: cliente {id_cliente} não encontrado")
        continue

    # Busca entrega
    entrega = entregas_por_pedido.get(id_pedido)

    # Parsing de datas
    data_pedido            = parse_data(pedido.get('data_pedido'))
    data_prevista_entrega  = parse_data(entrega.get('data_prevista'))  if entrega else None
    data_realizada_entrega = parse_data(entrega.get('data_realizada')) if entrega else None

    # Cálculo de atraso em dias
    atraso_dias = None
    if data_prevista_entrega and data_realizada_entrega:
        atraso_dias = (data_realizada_entrega - data_prevista_entrega).days

    # Parsing de valor monetário
    valor_total = parse_valor(pedido.get('valor_total'))
    if valor_total is None:
        sem_valor += 1
        print(f"     Pedido {id_pedido}: valor_total nulo, mantido como None")

    consolidado.append({
        'id_pedido':               id_pedido,
        'nome_cliente':            cliente.get('nome', '').strip(),
        'cidade_normalizada':      normalizar_cidade(cliente.get('cidade')),
        'estado':                  cliente.get('estado', '').strip().upper(),
        'valor_total':             valor_total,
        'status_pedido':           pedido.get('status', '').strip(),
        'data_pedido':             str(data_pedido) if data_pedido else None,
        'data_prevista_entrega':   str(data_prevista_entrega)  if data_prevista_entrega  else None,
        'data_realizada_entrega':  str(data_realizada_entrega) if data_realizada_entrega else None,
        'atraso_dias':             atraso_dias,
        'status_entrega':          entrega.get('status_entrega', '').strip() if entrega else None,
    })

# Exportação 

print("\n Exportando consolidado...")

Path('output').mkdir(exist_ok=True)

# CSV
if consolidado:
    cabecalhos = list(consolidado[0].keys())
    with open('output/consolidado.csv', 'w', newline='', encoding='utf-8') as f:
        writer = csv.DictWriter(f, fieldnames=cabecalhos)
        writer.writeheader()
        writer.writerows(consolidado)

# JSON
with open('output/consolidado.json', 'w', encoding='utf-8') as f:
    json.dump(consolidado, f, ensure_ascii=False, indent=2, default=str)

# Resumo
print("\n Pipeline concluído!")
print(f"   Registros consolidados : {len(consolidado)}")
print(f"   Pedidos sem cliente     : {sem_cliente}")
print(f"   Pedidos sem valor       : {sem_valor}")
print(f"   Entregas órfãs ignoradas: {orphans}")
print("\n    Arquivos gerados:")
print("      output/consolidado.csv")
print("      output/consolidado.json")