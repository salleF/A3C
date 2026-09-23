# P90 Rúnica

ID: `runic_p90` | Categoria: SMGs | [Asset Unity](../../../Data/Arsenal/Weapons/runic_p90.asset) | [Índice](../README.md)

## Identidade e uso

SMG automática de avanço. Pente amplo e grande dispersão sustentada. Aceita os cinco cartuchos de SMG nos três slots Q/E/C.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 22 |
| Cabeça por bala/bago/golpe | 38 |
| Cadência (tiros ou golpes/minuto) | 800 |
| Modo | Automático |
| Mecânica especial | Padrão |
| Bagos por carga disparada | 1 |
| Cargas simultâneas máximas | 1 |
| Dano máximo simultâneo por alvo em HS | 38 |
| Pente total / reserva | 50 / 100 |
| Capacidade por mão (0 = não se aplica) | 0 |
| Recarga | 2.4 s |
| Preço proposto | 1050 CR |
| Alcance efetivo proposto | 30 m |
| Dispersão inicial / máxima | 0.01 / 0.11 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 1x / 1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

[Fogo](../Spells/smg_fire.md), [Água](../Spells/smg_water.md), [Terra](../Spells/smg_earth.md), [Relâmpago](../Spells/smg_lightning.md), [Luz & Escuridão](../Spells/smg_light_dark.md)

## Direção de arte e áudio

Corpo bullpup de madeira laminada com pente horizontal de cristal protegido por latão. Runas percorrem o trilho sem substituir a leitura da mira.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação no protótipo futuro

Segurar LMB consome até 50 tiros e então exige recarga. Verificar que todos os cinco cartuchos de SMG são compatíveis e cartuchos de rifle são rejeitados.

## Estado da implementação

WeaponController lê dano, cadência, pente, reserva e dispersão. Visual, alcance efetivo e regras especiais novos são dados de design; não há prefab exclusivo, áudio ou S.A.A. executável.

Esta entrega inclui a ficha e os parâmetros serializados. Não inclui modelo novo, animação, som, partículas ou mecânica jogável inédita. Ver [integração](../Unity_Integration.md).
