# Lever-Action Arcana (Choke)

ID: `lever_action` | Categoria: Escopetas | [Asset Unity](../../../Data/Arsenal/Weapons/lever_action.asset) | [Índice](../README.md)

## Identidade e uso

Escopeta de alavanca com seis bagos. Segurar LMB concentra por até 0,8 s; soltar dispara. Choke reduz dispersão até 35% do valor original e não aumenta dano. Soltar cedo ainda dispara com a concentração parcial.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 30 |
| Cabeça por bala/bago/golpe | 32 |
| Cadência (tiros ou golpes/minuto) | 140 |
| Modo | Múltiplos bagos |
| Mecânica especial | Choke |
| Bagos por carga disparada | 6 |
| Cargas simultâneas máximas | 1 |
| Dano máximo simultâneo por alvo em HS | 192 |
| Pente total / reserva | 5 / 15 |
| Capacidade por mão (0 = não se aplica) | 0 |
| Recarga | 2.8 s |
| Preço proposto | 1050 CR |
| Alcance efetivo proposto | 20 m |
| Dispersão inicial / máxima | 0.045 / 0.1 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 1x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

[Fogo](../Spells/shotgun_fire.md), [Água](../Spells/shotgun_water.md), [Terra](../Spells/shotgun_earth.md), [Relâmpago](../Spells/shotgun_lightning.md), [Luz & Escuridão](../Spells/shotgun_light_dark.md)

## Direção de arte e áudio

Alavanca de latão sob o punho, coronha de madeira e anéis de choke com inscrições giratórias. Concentração indicada por contração dos anéis e som crescente, sem preencher a tela.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação no protótipo futuro

Dano máximo: 180 no corpo e 192 em HS, carregada ou não. Cancelar carga ao trocar, recarregar ou morrer sem gastar munição. Apenas um tiro ao soltar LMB.

## Estado da implementação

O WeaponController atual dispara ao pressionar LMB. Requer implementação de carga/soltura e interpolação de spread para este asset.

Esta entrega inclui a ficha e os parâmetros serializados. Não inclui modelo novo, animação, som, partículas ou mecânica jogável inédita. Ver [integração](../Unity_Integration.md).
