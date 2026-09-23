# Punishment

ID: `punishment` | Categoria: Trunfo | [Asset Unity](../../../Data/Arsenal/Weapons/punishment.asset) | [Índice](../README.md)

## Identidade e uso

Canhão mecânico temporário liberado por Lust & Greed. Uma única bala; 666 no HS. Não pode ser comprado, equipado livremente nem receber S.A.A. Disparar, acertando ou não, encerra o trunfo e retorna às pistolas recarregadas.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 150 |
| Cabeça por bala/bago/golpe | 666 |
| Cadência (tiros ou golpes/minuto) | 40 |
| Modo | Semiautomático |
| Mecânica especial | Punishment |
| Bagos por carga disparada | 1 |
| Cargas simultâneas máximas | 1 |
| Dano máximo simultâneo por alvo em HS | 666 |
| Pente total / reserva | 1 / 0 |
| Capacidade por mão (0 = não se aplica) | 0 |
| Recarga | 0 s |
| Preço proposto | 0 CR |
| Alcance efetivo proposto | 45 m |
| Dispersão inicial / máxima | 0.005 / 0.08 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 1x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

Não aceita cartuchos S.A.A.

## Direção de arte e áudio

Tambor de uma câmara, contrapeso de forja e trava de cobre marcada com seis entalhes repetidos. Saque pesado e clique inequívoco de única bala. O som do canhão contrasta com as pistolas.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação no protótipo futuro

HS elimina 200 HP. Corpo causa 150, deixando 50. Um erro também retorna às pistolas. Não encadear Punishment sem esvaziar novamente um par completo de 6+6.

## Estado da implementação

Requer estado especial no futuro controlador de Lust & Greed. O retorno deve restaurar 6+6 conforme o roteiro. O custo dessa recarga especial na reserva de 21 não foi definido e precisa ser resolvido na implementação da economia; não substituir por retorno parcial.

Esta entrega inclui a ficha e os parâmetros serializados. Não inclui modelo novo, animação, som, partículas ou mecânica jogável inédita. Ver [integração](../Unity_Integration.md).
