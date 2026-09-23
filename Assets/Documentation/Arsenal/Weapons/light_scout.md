# Scout Leve

ID: `light_scout` | Categoria: Snipers | [Asset Unity](../../../Data/Arsenal/Weapons/light_scout.asset) | [Índice](../README.md)

## Identidade e uso

Sniper leve: 85 no tórax e 160 no HS, conforme roteiro. Mobilidade para reconhecimento e suporte. Proposta de RMB para mira óptica, sem dano adicional.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 85 |
| Cabeça por bala/bago/golpe | 160 |
| Cadência (tiros ou golpes/minuto) | 100 |
| Modo | Semiautomático |
| Mecânica especial | Padrão |
| Bagos por carga disparada | 1 |
| Cargas simultâneas máximas | 1 |
| Dano máximo simultâneo por alvo em HS | 160 |
| Pente total / reserva | 8 / 24 |
| Capacidade por mão (0 = não se aplica) | 0 |
| Recarga | 2.5 s |
| Preço proposto | 2050 CR |
| Alcance efetivo proposto | 100 m |
| Dispersão inicial / máxima | 0.0015 / 0.04 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 1x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

[Fogo](../Spells/sniper_fire.md), [Água](../Spells/sniper_water.md), [Terra](../Spells/sniper_earth.md), [Relâmpago](../Spells/sniper_lightning.md), [Luz & Escuridão](../Spells/sniper_light_dark.md)

## Direção de arte e áudio

Cano fino, coronha leve de madeira e pequena luneta com lente rúnica. Cristal de observação na lateral, sem tamanho que confunda com AWP.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação

Tórax contra 200 deixa 115 HP totais; HS deixa 40. Usar os cinco cartuchos de Sniper. Scope não altera o dano.

## Estado da implementação

WeaponController executa este asset com municao persistente por arma, cadencia, dispersao, alcance e recarga. A cena de treino permite selecionar o equipamento. Arte e audio finais permanecem pendentes.

O runtime local consome estes parâmetros. A cena de treino usa silhuetas, efeitos e áudio procedurais; modelos e animações finais continuam pendentes. Ver [integração](../Unity_Integration.md).
