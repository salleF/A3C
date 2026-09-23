# Lust & Greed

ID: `lust_greed` | Categoria: Pistolas | [Asset Unity](../../../Data/Arsenal/Weapons/lust_greed.asset) | [Índice](../README.md)

## Identidade e uso

Lust: LMB. Greed: RMB. Cada mão guarda 6 balas; reserva inicial de 21. R recarrega somente a mão com menos munição (empate favorece Lust). Esvaziar as 12 sem recarga intermediária libera Punishment, mesmo errando os tiros. A recarga manual cancela a sequência. Usar cadência compartilhada, sem disparo duplo simultâneo.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 34 |
| Cabeça por bala/bago/golpe | 102 |
| Cadência (tiros ou golpes/minuto) | 120 |
| Modo | Semiautomático |
| Mecânica especial | Dupla independente |
| Bagos por carga disparada | 1 |
| Cargas simultâneas máximas | 1 |
| Dano máximo simultâneo por alvo em HS | 102 |
| Pente total / reserva | 12 / 21 |
| Capacidade por mão (0 = não se aplica) | 6 |
| Recarga | 1.8 s |
| Preço proposto | 900 CR |
| Alcance efetivo proposto | 50 m |
| Dispersão inicial / máxima | 0.005 / 0.08 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 1x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

Não aceita cartuchos S.A.A.

## Direção de arte e áudio

Pistolas irmãs com punhos de madeira, Lust em cobre rubro e Greed em latão verde. Seis marcas mecânicas por mão; encaixe central para sacar o canhão do trunfo. Sem magia consumível nas pistolas.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação no protótipo futuro

Seis disparos de cada mão sem recarga, mesmo no ar, liberam o trunfo. Recarregar após o 11º impede a liberação. Dois cliques no mesmo frame respeitam uma única cadência, com prioridade LMB. Após Punishment, conferir retorno a 6+6 e documentar o custo escolhido para a recarga especial.

## Estado da implementação

Não existe controlador para munição independente ou sequência Punishment. magazineSize=12 é o total, magazinePerHand=6 descreve a divisão. O controlador genérico não reproduz o comportamento.

Esta entrega inclui a ficha e os parâmetros serializados. Não inclui modelo novo, animação, som, partículas ou mecânica jogável inédita. Ver [integração](../Unity_Integration.md).
