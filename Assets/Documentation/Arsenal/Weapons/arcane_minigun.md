# Minigun a Vapor Arcana

ID: `arcane_minigun` | Categoria: Metralhadora | [Asset Unity](../../../Data/Arsenal/Weapons/arcane_minigun.asset) | [Índice](../README.md)

## Identidade e uso

Metralhadora de supressão. Proposta de 1 s para aquecer os canos antes de atirar e movimento a 30% enquanto equipada. Cartuchos mantêm zonas temporárias de Controle e Bloqueio.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 18 |
| Cabeça por bala/bago/golpe | 28 |
| Cadência (tiros ou golpes/minuto) | 1200 |
| Modo | Automático |
| Mecânica especial | Aquecimento |
| Bagos por carga disparada | 1 |
| Cargas simultâneas máximas | 1 |
| Dano máximo simultâneo por alvo em HS | 28 |
| Pente total / reserva | 100 / 0 |
| Capacidade por mão (0 = não se aplica) | 0 |
| Recarga | 4 s |
| Preço proposto | 3500 CR |
| Alcance efetivo proposto | 45 m |
| Dispersão inicial / máxima | 0.018 / 0.15 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 0.3x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

[Fogo](../Spells/machinegun_fire.md), [Água](../Spells/machinegun_water.md), [Terra](../Spells/machinegun_earth.md), [Relâmpago](../Spells/machinegun_lightning.md), [Luz & Escuridão](../Spells/machinegun_light_dark.md)

## Direção de arte e áudio

Conjunto rotativo de canos de latão, reservatório de vapor protegido e travas de madeira grossa. Vapor ventila para os lados e nunca encobre a mira. Runa de temperatura avisa o aquecimento.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação

Sem bala antes de 1 s de aquecimento. Soltar gatilho cancela giro. 100 tiros totais, sem reserva inicial. Uma zona por carga ativada, não vinte zonas por segundo de gatilho.

## Estado da implementação

WeaponController exige aquecimento continuo antes de disparar. Soltar o gatilho, trocar arma ou abrir o menu zera o aquecimento. PlayerMovement aplica o peso equipado.

O runtime local consome estes parâmetros. A cena de treino usa silhuetas, efeitos e áudio procedurais; modelos e animações finais continuam pendentes. Ver [integração](../Unity_Integration.md).
