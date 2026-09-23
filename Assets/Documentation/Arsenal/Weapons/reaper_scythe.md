# Foice de Ceifador

ID: `reaper_scythe` | Categoria: Corpo a corpo | [Asset Unity](../../../Data/Arsenal/Weapons/reaper_scythe.asset) | [Índice](../README.md)

## Identidade e uso

Golpe físico em arco horizontal de 120 graus. Pode atingir vários inimigos próximos, uma vez por alvo a cada golpe. Não atravessa cobertura e não usa S.A.A.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 45 |
| Cabeça por bala/bago/golpe | 110 |
| Cadência (tiros ou golpes/minuto) | 65 |
| Modo | Corpo a corpo |
| Mecânica especial | Varredura |
| Bagos por carga disparada | 1 |
| Cargas simultâneas máximas | 1 |
| Dano máximo simultâneo por alvo em HS | 110 |
| Pente total / reserva | 0 / 0 |
| Capacidade por mão (0 = não se aplica) | 0 |
| Recarga | 0 s |
| Preço proposto | 550 CR |
| Alcance efetivo proposto | 2.5 m |
| Dispersão inicial / máxima | 0.005 / 0.08 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 1x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

Não aceita cartuchos S.A.A.

## Direção de arte e áudio

Haste longa de madeira escura, contrapeso de cobre e lâmina curva segmentada por runas de contenção. Varredura claramente antecipada pelo ombro.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação no protótipo futuro

Dois inimigos dentro do arco podem receber dano; um atrás de parede não. Colliders de cabeça e corpo do mesmo alvo não duplicam dano.

## Estado da implementação

Requer controlador melee com deduplicação por HealthSystem e teste de oclusão. O modo genérico de projétil não executa sweep.

Esta entrega inclui a ficha e os parâmetros serializados. Não inclui modelo novo, animação, som, partículas ou mecânica jogável inédita. Ver [integração](../Unity_Integration.md).
