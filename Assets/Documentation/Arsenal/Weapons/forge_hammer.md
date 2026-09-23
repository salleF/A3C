# Martelo de Forja

ID: `forge_hammer` | Categoria: Corpo a corpo | [Asset Unity](../../../Data/Arsenal/Weapons/forge_hammer.asset) | [Índice](../README.md)

## Identidade e uso

Golpe físico pesado. Bônus de 50% contra barreiras e paredes mágicas destrutíveis; sem bônus contra jogadores ou arquitetura permanente de Crushle. Não usa S.A.A.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 60 |
| Cabeça por bala/bago/golpe | 130 |
| Cadência (tiros ou golpes/minuto) | 55 |
| Modo | Corpo a corpo |
| Mecânica especial | Quebra-barreira |
| Bagos por carga disparada | 1 |
| Cargas simultâneas máximas | 1 |
| Dano máximo simultâneo por alvo em HS | 130 |
| Pente total / reserva | 0 / 0 |
| Capacidade por mão (0 = não se aplica) | 0 |
| Recarga | 0 s |
| Preço proposto | 700 CR |
| Alcance efetivo proposto | 2 m |
| Dispersão inicial / máxima | 0.005 / 0.08 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 1x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

Não aceita cartuchos S.A.A.

## Direção de arte e áudio

Cabeça quadrada de forja com placas de latão e runas de ressonância. Punho grosso de madeira. Impacto em barreira emite rachadura luminosa localizada.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação no protótipo futuro

Corpo de jogador recebe 60; barreira recebe 90. Não danificar paredes estáticas do mapa. Um alvo por golpe frontal e nenhuma colisão através da cobertura.

## Estado da implementação

Requer controlador melee e interface de barreira destrutível. barrierDamageMultiplier não é lido pelo runtime atual.

Esta entrega inclui a ficha e os parâmetros serializados. Não inclui modelo novo, animação, som, partículas ou mecânica jogável inédita. Ver [integração](../Unity_Integration.md).
