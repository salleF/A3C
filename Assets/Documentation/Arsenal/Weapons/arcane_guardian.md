# Guardian Arcano

ID: `arcane_guardian` | Categoria: Rifles | [Asset Unity](../../../Data/Arsenal/Weapons/arcane_guardian.asset) | [Índice](../README.md)

## Identidade e uso

Rifle semiautomático de precisão. HS limitado a 160, corrigindo os 195 da tabela antiga. Cartuchos de rifle para bloquear visão e reconhecer ângulos.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 65 |
| Cabeça por bala/bago/golpe | 160 |
| Cadência (tiros ou golpes/minuto) | 195 |
| Modo | Semiautomático |
| Mecânica especial | Padrão |
| Bagos por carga disparada | 1 |
| Cargas simultâneas máximas | 1 |
| Dano máximo simultâneo por alvo em HS | 160 |
| Pente total / reserva | 12 / 36 |
| Capacidade por mão (0 = não se aplica) | 0 |
| Recarga | 2.3 s |
| Preço proposto | 2250 CR |
| Alcance efetivo proposto | 70 m |
| Dispersão inicial / máxima | 0.002 / 0.05 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 1x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

[Fogo](../Spells/rifle_fire.md), [Água](../Spells/rifle_water.md), [Terra](../Spells/rifle_earth.md), [Relâmpago](../Spells/rifle_lightning.md), [Luz & Escuridão](../Spells/rifle_light_dark.md)

## Direção de arte e áudio

Madeira alongada, caixa de culatra de latão e mira rúnica simples. Silhueta esguia, distinta das snipers; não usar luneta de grande diâmetro.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação no protótipo futuro

LMB segurado não repete tiros. Um HS deixa 40 HP no alvo de colete pesado. Nenhuma configuração de cartucho ultrapassa o teto de rifle.

## Estado da implementação

WeaponController lê dano, cadência, pente, reserva e dispersão. Visual, alcance efetivo e regras especiais novos são dados de design; não há prefab exclusivo, áudio ou S.A.A. executável.

Esta entrega inclui a ficha e os parâmetros serializados. Não inclui modelo novo, animação, som, partículas ou mecânica jogável inédita. Ver [integração](../Unity_Integration.md).
