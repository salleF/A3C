# TriShot Arcano

ID: `trishot` | Categoria: Pistolas | [Asset Unity](../../../Data/Arsenal/Weapons/trishot.asset) | [Índice](../README.md)

## Identidade e uso

Dois canos triplos serrados, com 2 cargas por mão (4 carregadas no total). LMB alterna mãos e solta 3 bagos; RMB consome uma carga de cada mão e solta 6. Se uma mão estiver vazia, RMB usa apenas a disponível. Recarga preserva a reserva. Não usa S.A.A.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 22 |
| Cabeça por bala/bago/golpe | 30 |
| Cadência (tiros ou golpes/minuto) | 200 |
| Modo | Múltiplos bagos |
| Mecânica especial | Dupla alternada |
| Bagos por carga disparada | 3 |
| Cargas simultâneas máximas | 2 |
| Dano máximo simultâneo por alvo em HS | 180 |
| Pente total / reserva | 4 / 32 |
| Capacidade por mão (0 = não se aplica) | 2 |
| Recarga | 1.2 s |
| Preço proposto | 800 CR |
| Alcance efetivo proposto | 15 m |
| Dispersão inicial / máxima | 0.045 / 0.09 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 1x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

Não aceita cartuchos S.A.A.

## Direção de arte e áudio

Par compacto com três bocas triangulares por arma. Coronha curta em madeira grossa, aros de cobre e gatilhos mecânicos. Mostrar duas câmaras por lado para leitura da munição.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação no protótipo futuro

LMB consome 1 de uma mão; RMB, 1 de cada. Seis HS simultâneos somam 180, nunca 200. Com 1 carga de reserva e ambas vazias, a recarga distribui somente 1. Cadência compartilhada impede dois disparos no mesmo frame.

## Estado da implementação

TriShotAkimbo é um controlador legado independente que não lê este asset. Seus danos, cadência e recarga precisam ser corrigidos e ligados a WeaponData. Não adicionar junto com WeaponController: ambos leem o mesmo mouse.

Esta entrega inclui a ficha e os parâmetros serializados. Não inclui modelo novo, animação, som, partículas ou mecânica jogável inédita. Ver [integração](../Unity_Integration.md).
