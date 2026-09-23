# Punishment

ID: `punishment` | Categoria: Trunfo | [Asset Unity](../../../Data/Arsenal/Weapons/punishment.asset) | [Índice](../README.md)

## Identidade e uso

Canhão mecânico temporário liberado por Lust & Greed. Uma única bala; 666 no HS. Não pode ser comprado, equipado livremente nem receber S.A.A. Disparar, acertando ou não, encerra o trunfo e retorna às pistolas recarregadas.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 150 |
| Cabeça por bala/bago/golpe | 666 |
| Cadência (tiros ou golpes/minuto) | 40 |
| Modo | Semiautomático |
| Mecânica especial | Punishment |
| Bagos por carga disparada | 1 |
| Cargas simultâneas máximas | 1 |
| Dano máximo simultâneo por alvo em HS | 666 |
| Pente total / reserva | 1 / 0 |
| Capacidade por mão (0 = não se aplica) | 0 |
| Recarga | 0 s |
| Preço proposto | 0 CR |
| Alcance efetivo proposto | 45 m |
| Dispersão inicial / máxima | 0.005 / 0.08 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 1x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

Não aceita cartuchos S.A.A.

## Direção de arte e áudio

Tambor de uma câmara, contrapeso de forja e trava de cobre marcada com seis entalhes repetidos. Saque pesado e clique inequívoco de única bala. O som do canhão contrasta com as pistolas.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação

HS elimina 200 HP. Corpo causa 150, deixando 50. Um erro também retorna às pistolas. Não encadear Punishment sem esvaziar novamente um par completo de 6+6.

## Estado da implementação

Trunfo executado por WeaponController e WeaponState. Nao pode ser comprado/equipado diretamente no treino. O tiro retorna a Lust & Greed com 6+6 sem debitar a reserva.

O runtime local consome estes parâmetros. A cena de treino usa silhuetas, efeitos e áudio procedurais; modelos e animações finais continuam pendentes. Ver [integração](../Unity_Integration.md).
