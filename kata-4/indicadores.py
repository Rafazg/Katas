import json
from pathlib import Path
from collections import defaultdict

# ── Carrega o consolidado ──────────────────────────────────────────────────────

arquivo = Path('output/consolidado.json')
if not arquivo.exists():
    print("❌ Arquivo output/consolidado.json não encontrado. Rode pipeline.py primeiro.")
    exit(1)

with open(arquivo, encoding='utf-8') as f:
    dados = json.load(f)

print("=" * 55)
print("  INDICADORES DE DESEMPENHO — LOGÍSTICA")
print("=" * 55)
print(f"  Base: {len(dados)} pedidos consolidados")
print("=" * 55)

# ── 1. Total de pedidos por status ─────────────────────────────────────────────

print("\n PEDIDOS POR STATUS")
print("-" * 35)

por_status = defaultdict(int)
for p in dados:
    status = p.get('status_pedido') or 'sem_status'
    por_status[status] += 1

for status, total in sorted(por_status.items(), key=lambda x: -x[1]):
    print(f"  {status:<20} {total:>4} pedido(s)")

# ── 2. Ticket médio por estado ─────────────────────────────────────────────────

print("\n TICKET MÉDIO POR ESTADO")
print("-" * 35)

por_estado = defaultdict(list)
for p in dados:
    if p.get('valor_total') is not None:
        estado = p.get('estado') or 'N/A'
        por_estado[estado].append(p['valor_total'])

for estado in sorted(por_estado):
    valores = por_estado[estado]
    media = sum(valores) / len(valores)
    print(f"  {estado:<6} R$ {media:.2f}")

# ── 3. Entregas no prazo vs. com atraso ───────────────────────────────────────

print("\n ENTREGAS: PRAZO vs. ATRASO")
print("-" * 35)

com_entrega = [p for p in dados if p.get('data_realizada_entrega')]
no_prazo    = [p for p in com_entrega if (p.get('atraso_dias') or 0) <= 0]
com_atraso  = [p for p in com_entrega if (p.get('atraso_dias') or 0) > 0]
nao_entregues = len(dados) - len(com_entrega)

if com_entrega:
    pct_prazo  = len(no_prazo)   / len(com_entrega) * 100
    pct_atraso = len(com_atraso) / len(com_entrega) * 100
    print(f"  No prazo      {len(no_prazo):>4} ({pct_prazo:.1f}%)")
    print(f"  Com atraso    {len(com_atraso):>4} ({pct_atraso:.1f}%)")
    print(f"  Não entregues {nao_entregues:>4}")
else:
    print("  Sem dados de entrega suficientes")

# ── 4. Top 3 cidades com maior volume ─────────────────────────────────────────

print("\n TOP 3 CIDADES POR VOLUME DE PEDIDOS")
print("-" * 35)

por_cidade = defaultdict(int)
for p in dados:
    cidade = p.get('cidade_normalizada') or 'N/A'
    por_cidade[cidade] += 1

top_3 = sorted(por_cidade.items(), key=lambda x: -x[1])[:3]

for i, (cidade, total) in enumerate(top_3, start=1):
    print(f"  {i}. {cidade:<20} {total} pedido(s)")

# ── 5. Média de atraso para pedidos atrasados ─────────────────────────────────

print("\n MÉDIA DE ATRASO (pedidos com atraso)")
print("-" * 35)

atrasados = [p for p in dados if p.get('atraso_dias') and p['atraso_dias'] > 0]

if atrasados:
    media_atraso = sum(p['atraso_dias'] for p in atrasados) / len(atrasados)
    print(f"  Pedidos com atraso: {len(atrasados)}")
    print(f"  Média de atraso   : {media_atraso:.1f} dias")
else:
    print("  Nenhum pedido com atraso encontrado")

print("\n" + "=" * 55)
