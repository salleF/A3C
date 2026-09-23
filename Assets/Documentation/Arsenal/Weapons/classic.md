# Pistola Magitech (Classic)

ID: `classic` | Categoria: Pistolas | [Asset Unity](../../../Data/Arsenal/Weapons/classic.asset) | [Índice](../README.md)

## Identidade e uso

Inicial dos defensores A3C. LMB semiautomático, R recarrega. Mesmo comportamento numérico da Varinha Rústica; não aceita S.A.A.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 26 |
| Cabeça por bala/bago/golpe | 78 |
| Cadência (tiros ou golpes/minuto) | 160 |
| Modo | Semiautomático |
| Mecânica especial | Padrão |
| Bagos por carga disparada | 1 |
| Cargas simultâneas máximas | 1 |
| Dano máximo simultâneo por alvo em HS | 78 |
| Pente total / reserva | 12 / 36 |
| Capacidade por mão (0 = não se aplica) | 0 |
| Recarga | 1.8 s |
| Preço proposto | 0 CR |
| Alcance efetivo proposto | 35 m |
| Dispersão inicial / máxima | 0.005 / 0.08 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 1x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

Não aceita cartuchos S.A.A.

## Direção de arte e áudio

Silhueta curta e retangular, madeira de varinha polida no punho e ferrolho de latão com selo da academia. Câmara rúnica ciano discreta. Disparo com clique mecânico e pulso breve; mãos e mira devem ficar legíveis.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação no protótipo futuro

Comparar dano, intervalo, dispersão, velocidade do projétil, pente e recarga com a Varinha. Contra 200 HP, um HS deixa 122 HP totais.

## Estado da implementação

WeaponController lê dano, cadência, pente, reserva e dispersão. Visual, alcance efetivo e regras especiais novos são dados de design; não há prefab exclusivo, áudio ou S.A.A. executável.

Esta entrega inclui a ficha e os parâmetros serializados. Não inclui modelo novo, animação, som, partículas ou mecânica jogável inédita. Ver [integração](../Unity_Integration.md).
