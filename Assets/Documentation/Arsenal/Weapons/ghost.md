# Ghost

ID: `ghost` | Categoria: Pistolas | [Asset Unity](../../../Data/Arsenal/Weapons/ghost.asset) | [Índice](../README.md)

## Identidade e uso

Pistola mecânica silenciada, sem traçantes. LMB semiautomático; não usa cartuchos elementais. O impacto ainda informa ao alvo que houve dano.

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
| Pente total / reserva | 15 / 45 |
| Capacidade por mão (0 = não se aplica) | 0 |
| Recarga | 1.8 s |
| Preço proposto | 500 CR |
| Alcance efetivo proposto | 50 m |
| Dispersão inicial / máxima | 0.005 / 0.08 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 1x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

Não aceita cartuchos S.A.A.

## Direção de arte e áudio

Cano longo com abafador de latão escurecido, madeira negra e gravura lunar discreta. Estalo abafado, pouco clarão e nenhum rastro no ar.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação

Garantir ausência de traçante em ambos os times e em espectadores. Silenciamento não deve esconder aviso de dano ao alvo.

## Estado da implementação

WeaponController executa tiro e recarga; projeteis ficam invisiveis e o som procedural usa volume reduzido. Arte e audio finais permanecem pendentes.

O runtime local consome estes parâmetros. A cena de treino usa silhuetas, efeitos e áudio procedurais; modelos e animações finais continuam pendentes. Ver [integração](../Unity_Integration.md).
