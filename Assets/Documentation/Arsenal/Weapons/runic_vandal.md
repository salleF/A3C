# Vandal Rúnico (AK)

ID: `runic_vandal` | Categoria: Rifles | [Asset Unity](../../../Data/Arsenal/Weapons/runic_vandal.asset) | [Índice](../README.md)

## Identidade e uso

Rifle automático de alto impacto. HS fixado em 160 pelo roteiro. Bom primeiro tiro e dispersão crescente; cartuchos de Bloqueio e Observação.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 40 |
| Cabeça por bala/bago/golpe | 160 |
| Cadência (tiros ou golpes/minuto) | 495 |
| Modo | Automático |
| Mecânica especial | Padrão |
| Bagos por carga disparada | 1 |
| Cargas simultâneas máximas | 1 |
| Dano máximo simultâneo por alvo em HS | 160 |
| Pente total / reserva | 25 / 75 |
| Capacidade por mão (0 = não se aplica) | 0 |
| Recarga | 2.4 s |
| Preço proposto | 2900 CR |
| Alcance efetivo proposto | 60 m |
| Dispersão inicial / máxima | 0.005 / 0.08 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 1x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

[Fogo](../Spells/rifle_fire.md), [Água](../Spells/rifle_water.md), [Terra](../Spells/rifle_earth.md), [Relâmpago](../Spells/rifle_lightning.md), [Luz & Escuridão](../Spells/rifle_light_dark.md)

## Direção de arte e áudio

Coronha de madeira nobre, carregador curvo com nervuras de cobre e câmara rúnica reforçada. O recuo deve comunicar peso sem deslocar artificialmente a direção do projétil.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação

Um HS contra 100 de vida + 100 de escudo deixa 40 de vida. Nenhum cartucho acrescenta dano ou ignora escudo. Rajadas curtas recuperam precisão.

## Estado da implementação

WeaponController executa este asset com municao persistente por arma, cadencia, dispersao, alcance e recarga. A cena de treino permite selecionar o equipamento. Arte e audio finais permanecem pendentes.

O runtime local consome estes parâmetros. A cena de treino usa silhuetas, efeitos e áudio procedurais; modelos e animações finais continuam pendentes. Ver [integração](../Unity_Integration.md).
