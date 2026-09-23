# Sawed-Off Rúnica

ID: `runic_sawed_off` | Categoria: Escopetas | [Asset Unity](../../../Data/Arsenal/Weapons/runic_sawed_off.asset) | [Índice](../README.md)

## Identidade e uso

Escopeta curta de emboscada, com oito bagos por cartucho. LMB dispara uma carga. Não há modo duplo nesta proposta, preservando o teto de dano instantâneo.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 20 |
| Cabeça por bala/bago/golpe | 24 |
| Cadência (tiros ou golpes/minuto) | 200 |
| Modo | Múltiplos bagos |
| Mecânica especial | Padrão |
| Bagos por carga disparada | 8 |
| Cargas simultâneas máximas | 1 |
| Dano máximo simultâneo por alvo em HS | 192 |
| Pente total / reserva | 5 / 10 |
| Capacidade por mão (0 = não se aplica) | 0 |
| Recarga | 2.5 s |
| Preço proposto | 850 CR |
| Alcance efetivo proposto | 12 m |
| Dispersão inicial / máxima | 0.06 / 0.12 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 1x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

[Fogo](../Spells/shotgun_fire.md), [Água](../Spells/shotgun_water.md), [Terra](../Spells/shotgun_earth.md), [Relâmpago](../Spells/shotgun_lightning.md), [Luz & Escuridão](../Spells/shotgun_light_dark.md)

## Direção de arte e áudio

Canos curtos serrados com cinta de cobre e reservatório rúnico compacto. Madeira marcada por fuligem. Recarga visível de cartuchos de latão, sem adornos sobre a linha de mira.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação

Oito bagos de corpo somam 160; oito HS somam 192. Não causar 200 em um acionamento contra o mesmo alvo. Magia cria uma área única, mesmo acertando com vários bagos.

## Estado da implementação

WeaponController executa este asset com municao persistente por arma, cadencia, dispersao, alcance e recarga. A cena de treino permite selecionar o equipamento. Arte e audio finais permanecem pendentes.

O runtime local consome estes parâmetros. A cena de treino usa silhuetas, efeitos e áudio procedurais; modelos e animações finais continuam pendentes. Ver [integração](../Unity_Integration.md).
