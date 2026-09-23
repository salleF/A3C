# Phantom Arcano (M4)

ID: `arcane_phantom` | Categoria: Rifles | [Asset Unity](../../../Data/Arsenal/Weapons/arcane_phantom.asset) | [Índice](../README.md)

## Identidade e uso

Rifle automático silenciado para controle de ângulos. HS abaixo do teto de 160. Proposta: traçante invisível e menor penalidade de movimento, mantendo feedback de impacto.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 39 |
| Cabeça por bala/bago/golpe | 156 |
| Cadência (tiros ou golpes/minuto) | 550 |
| Modo | Automático |
| Mecânica especial | Padrão |
| Bagos por carga disparada | 1 |
| Cargas simultâneas máximas | 1 |
| Dano máximo simultâneo por alvo em HS | 156 |
| Pente total / reserva | 30 / 90 |
| Capacidade por mão (0 = não se aplica) | 0 |
| Recarga | 2.2 s |
| Preço proposto | 2900 CR |
| Alcance efetivo proposto | 50 m |
| Dispersão inicial / máxima | 0.005 / 0.08 |
| Penalidade de dispersão em movimento | 2.25x |
| Movimento equipada / bônus de sprint | 1x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

[Fogo](../Spells/rifle_fire.md), [Água](../Spells/rifle_water.md), [Terra](../Spells/rifle_earth.md), [Relâmpago](../Spells/rifle_lightning.md), [Luz & Escuridão](../Spells/rifle_light_dark.md)

## Direção de arte e áudio

Cano abafado de latão fosco, coronha vazada de madeira e ranhuras com luz ciano baixa. Perfil mais reto e leve que o Vandal.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação no protótipo futuro

HS contra 200 deixa 44 HP. Verificar que silenciamento altera apresentação, sem reduzir dano ou permitir cartuchos de outra família.

## Estado da implementação

Dano e cadência são lidos pelo controlador. silenced/visibleTracer ainda não são aplicados, e o projétil procedural continua visível.

Esta entrega inclui a ficha e os parâmetros serializados. Não inclui modelo novo, animação, som, partículas ou mecânica jogável inédita. Ver [integração](../Unity_Integration.md).
