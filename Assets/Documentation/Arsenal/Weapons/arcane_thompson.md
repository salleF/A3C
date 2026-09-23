# Thompson Arcana

ID: `arcane_thompson` | Categoria: SMGs | [Asset Unity](../../../Data/Arsenal/Weapons/arcane_thompson.asset) | [Índice](../README.md)

## Identidade e uso

SMG automática de cadência moderada e som grave. Avanço em duplas e controle de corredores curtos. Compartilha a família S.A.A. de SMGs.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 27 |
| Cabeça por bala/bago/golpe | 47 |
| Cadência (tiros ou golpes/minuto) | 545 |
| Modo | Automático |
| Mecânica especial | Padrão |
| Bagos por carga disparada | 1 |
| Cargas simultâneas máximas | 1 |
| Dano máximo simultâneo por alvo em HS | 47 |
| Pente total / reserva | 30 / 90 |
| Capacidade por mão (0 = não se aplica) | 0 |
| Recarga | 2.2 s |
| Preço proposto | 850 CR |
| Alcance efetivo proposto | 35 m |
| Dispersão inicial / máxima | 0.008 / 0.1 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 1x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

[Fogo](../Spells/smg_fire.md), [Água](../Spells/smg_water.md), [Terra](../Spells/smg_earth.md), [Relâmpago](../Spells/smg_lightning.md), [Luz & Escuridão](../Spells/smg_light_dark.md)

## Direção de arte e áudio

Madeira nobre, estrutura de latão envelhecido e pente de caixa com selo de oficina. Curvas clássicas contrastam com a câmara rúnica moderna.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação

Intervalo entre tiros igual a 60/545 segundos. Um HS não elimina alvo de 200 HP. Efeitos usam carga por disparo ativado, nunca por frame segurando o gatilho.

## Estado da implementação

WeaponController executa este asset com municao persistente por arma, cadencia, dispersao, alcance e recarga. A cena de treino permite selecionar o equipamento. Arte e audio finais permanecem pendentes.

O runtime local consome estes parâmetros. A cena de treino usa silhuetas, efeitos e áudio procedurais; modelos e animações finais continuam pendentes. Ver [integração](../Unity_Integration.md).
