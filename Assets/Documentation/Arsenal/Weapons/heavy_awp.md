# AWP Pesada

ID: `heavy_awp` | Categoria: Snipers | [Asset Unity](../../../Data/Arsenal/Weapons/heavy_awp.asset) | [Índice](../README.md)

## Identidade e uso

Sniper pesada: 190 no tórax, 350 no HS. Uma das duas exceções de hitkill em 200 HP. Como arma primária, participa do S.A.A. de Observação e Suporte, sem dano extra.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 190 |
| Cabeça por bala/bago/golpe | 350 |
| Cadência (tiros ou golpes/minuto) | 40 |
| Modo | Semiautomático |
| Mecânica especial | Padrão |
| Bagos por carga disparada | 1 |
| Cargas simultâneas máximas | 1 |
| Dano máximo simultâneo por alvo em HS | 350 |
| Pente total / reserva | 5 / 15 |
| Capacidade por mão (0 = não se aplica) | 0 |
| Recarga | 3.2 s |
| Preço proposto | 4700 CR |
| Alcance efetivo proposto | 120 m |
| Dispersão inicial / máxima | 0.001 / 0.04 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 0.85x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

[Fogo](../Spells/sniper_fire.md), [Água](../Spells/sniper_water.md), [Terra](../Spells/sniper_earth.md), [Relâmpago](../Spells/sniper_lightning.md), [Luz & Escuridão](../Spells/sniper_light_dark.md)

## Direção de arte e áudio

Coronha maciça de madeira, cano longo com braçadeiras de latão e luneta de contenção etérea. Ferrolho pesado visível e relatório sonoro que informa a janela entre tiros.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação

Tórax em colete pesado cheio deixa exatamente 10 HP reais. HS elimina. O cartucho não converte tiro de corpo em hitkill nem multiplica o dano de cabeça.

## Estado da implementação

WeaponController executa este asset com municao persistente por arma, cadencia, dispersao, alcance e recarga. A cena de treino permite selecionar o equipamento. Arte e audio finais permanecem pendentes.

O runtime local consome estes parâmetros. A cena de treino usa silhuetas, efeitos e áudio procedurais; modelos e animações finais continuam pendentes. Ver [integração](../Unity_Integration.md).
