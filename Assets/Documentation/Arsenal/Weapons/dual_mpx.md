# Dual Hand Sig MPX

ID: `dual_mpx` | Categoria: SMGs | [Asset Unity](../../../Data/Arsenal/Weapons/dual_mpx.asset) | [Índice](../README.md)

## Identidade e uso

Par de SMGs, 20 tiros em cada mão. Proposta: LMB automático alterna mãos; RMB dispara uma de cada com maior dispersão. S.A.A. aplica um único efeito por disparo duplo, não um por arma.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 18 |
| Cabeça por bala/bago/golpe | 36 |
| Cadência (tiros ou golpes/minuto) | 600 |
| Modo | Automático |
| Mecânica especial | Dupla alternada |
| Bagos por carga disparada | 1 |
| Cargas simultâneas máximas | 2 |
| Dano máximo simultâneo por alvo em HS | 72 |
| Pente total / reserva | 40 / 80 |
| Capacidade por mão (0 = não se aplica) | 20 |
| Recarga | 2.6 s |
| Preço proposto | 1100 CR |
| Alcance efetivo proposto | 25 m |
| Dispersão inicial / máxima | 0.012 / 0.13 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 1x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

[Fogo](../Spells/smg_fire.md), [Água](../Spells/smg_water.md), [Terra](../Spells/smg_earth.md), [Relâmpago](../Spells/smg_lightning.md), [Luz & Escuridão](../Spells/smg_light_dark.md)

## Direção de arte e áudio

Duas estruturas compactas de latão claro, punhos de madeira e tubos de condução rúnica curtos. Cada mão tem indicador de munição próprio; não usar braços que cubram a região central.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação no protótipo futuro

Disparo duplo consome 2 balas e causa no máximo 72 em dois HS. Apenas uma carga e uma área elemental por acionamento. Reserva e recarga independem da animação.

## Estado da implementação

WeaponController não divide munição em duas mãos nem lê RMB. Requer controle dual; os 40 tiros do asset são a soma de 20+20. Parâmetros de dano são por bala.

Esta entrega inclui a ficha e os parâmetros serializados. Não inclui modelo novo, animação, som, partículas ou mecânica jogável inédita. Ver [integração](../Unity_Integration.md).
