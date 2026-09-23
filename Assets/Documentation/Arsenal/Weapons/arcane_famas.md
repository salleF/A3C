# FAMAS Arcano

ID: `arcane_famas` | Categoria: Rifles | [Asset Unity](../../../Data/Arsenal/Weapons/arcane_famas.asset) | [Índice](../README.md)

## Identidade e uso

Rifle de rajada de três tiros sequenciais por clique. Cada bala consome uma unidade do pente. Uma ativação S.A.A. pertence à rajada e gera um único efeito no primeiro impacto válido.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 35 |
| Cabeça por bala/bago/golpe | 86 |
| Cadência (tiros ou golpes/minuto) | 500 |
| Modo | Rajada |
| Mecânica especial | Padrão |
| Bagos por carga disparada | 1 |
| Cargas simultâneas máximas | 1 |
| Dano máximo simultâneo por alvo em HS | 86 |
| Pente total / reserva | 25 / 75 |
| Capacidade por mão (0 = não se aplica) | 0 |
| Recarga | 2.2 s |
| Preço proposto | 1650 CR |
| Alcance efetivo proposto | 50 m |
| Dispersão inicial / máxima | 0.005 / 0.08 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 1x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

[Fogo](../Spells/rifle_fire.md), [Água](../Spells/rifle_water.md), [Terra](../Spells/rifle_earth.md), [Relâmpago](../Spells/rifle_lightning.md), [Luz & Escuridão](../Spells/rifle_light_dark.md)

## Direção de arte e áudio

Corpo bullpup de madeira escura e alça de latão com três marcas luminosas. O mecanismo da câmara produz ritmo de três cliques bem separados.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação

Clique emite 3 tiros separados por 0,12 s. Com 2 balas, emite apenas 2. Troca, morte ou recarga cancela a sequência restante. Cada HS é 86; a rajada não é uma explosão simultânea.

## Estado da implementação

WeaponController executa a rajada em intervalos configurados, com consumo por bala e uma unica ativacao arcana. Troca, morte e recarga cancelam a sequencia.

O runtime local consome estes parâmetros. A cena de treino usa silhuetas, efeitos e áudio procedurais; modelos e animações finais continuam pendentes. Ver [integração](../Unity_Integration.md).
