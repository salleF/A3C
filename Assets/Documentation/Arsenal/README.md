# Catálogo de armas e magia do A3C

**20 armas equipáveis, 1 trunfo e 25 cartuchos**, com fichas individuais e assets de dados configuráveis no Unity. A Punishment é o trunfo de Lust & Greed, não uma compra adicional. Cada primária compartilha os cinco cartuchos da sua família.

[Abrir catálogo Unity](../../Data/Arsenal/ArsenalCatalog.asset) | [Como integrar](Unity_Integration.md) | [Revisão do projeto](../A3C_Project_Review.md) | [Lore de Crushle](../A3C_Lore_Crushle.md) | [Roteiro](../../../Core_gameplay.txt)

## Autoridade das regras

`Core_gameplay.txt` prevalece sobre o documento de arsenal anterior. A lore define materiais, mundo e a arena: academia militar e arcana, competições holo-arcanas, madeira de varinha, latão e runas, fortaleza fragmentada por um portal. Os atacantes representam rebeldes no exercício; o catálogo não inventa uma nova guerra canônica.

Regras fixadas pelo roteiro:

- 100 HP de vida real, separados do escudo. Cura de suporte afeta somente vida real, até 100 HP.
- Rifles têm no máximo 160 de HS. Scout: 85 corpo / 160 HS. AWP: 190 corpo / 350 ou mais HS. Punishment: 666 HS.
- Somente HS de AWP e Punishment eliminam instantaneamente um alvo com 200 HP. Somar todos os bagos e mãos simultâneos ao avaliar o limite.
- Classic e Varinha têm simetria de dano e cadência. Esta proposta também iguala os demais parâmetros de combate, deixando somente apresentação diferente.
- TriShot: 2 cargas por mão, 3 bagos em tiro alternado e 6 em duplo. O parêntese ambíguo "4/4" do roteiro não foi interpretado como quatro cargas por mão; prevalece a frase explícita de duas por lado, também usada pelo script legado. Reserva de 32 é proposta.
- Lust & Greed: 6+6 carregadas, 21 de reserva. Esvaziar 12 sem recarregar libera uma bala de Punishment. Acertos não são exigidos. R recarrega a mão menos carregada.
- Espada acelera corrida; Foice faz sweep; Martelo causa dano adicional contra barreiras mágicas.
- S.A.A. somente em primárias, até três slots Q/E/C e cinco elementos. Luz & Escuridão é um único elemento dual. AWP está incluída como primária.
- Famílias: SMG Avanço/Controle; Shotgun Controle/Bloqueio; Rifle Bloqueio/Observação; Sniper Observação/Suporte; Machine Gun Controle/Bloqueio.

## Propostas desta entrega

Preços de armas, reservas não fixadas, cadência, dispersão, dano não canônico, dimensões, tempos e efeitos são ponto de partida para playtest, não decisões aprovadas por teste. Nenhum playtest foi executado nesta máquina.

1. Magias são utilidades sem dano adicional, para preservar as duas exceções de hitkill. Removidos da proposta antiga: dano que ignora escudo, HS extra, ricochetes letais e revelação global.
2. Cartucho comprado contém três cargas. Armar Q/E/C afeta o próximo acionamento; não converte automaticamente todo o pente. Rajada, pellets e disparo duplo geram uma única aplicação.
3. Somente Sniper, cuja família inclui Suporte, oferece cura de HP reais. Efeitos de escudo não se confundem com cura.
4. Choke concentra a dispersão e não aumenta dano. Máximo por disparo: TriShot duplo 180 HS; Sawed-Off 192 HS; Lever-Action 192 HS.
5. Punishment deve retornar às pistolas carregadas com 6+6, como exige o roteiro. O custo dessa recarga especial na reserva de 21 não foi definido pelo roteiro e fica pendente para a implementação da economia; não alterar silenciosamente o retorno para um pente parcial.
6. E prioriza despertar o Dragão quando houver interação válida. Não consome magia enquanto o defensor sustenta a interação.
7. As duas mãos de Lust & Greed compartilham cadência e não disparam simultaneamente, evitando 204 de HS em um único acionamento.

## Armas

Os danos abaixo são por bala, bago ou golpe, nunca pelo conjunto de uma escopeta. Pente é o total somado das mãos. Os 0 CR da Punishment indicam que ela não é comprável.

| Arma | Categoria | Corpo / cabeça | Pente + reserva | CR proposto |
|---|---|---|---|---|
| [Pistola Magitech (Classic)](Weapons/classic.md) | Pistolas | 26 / 78 | 12 + 36 | 0 |
| [Varinha Rústica](Weapons/rustic_wand.md) | Pistolas | 26 / 78 | 12 + 36 | 0 |
| [Ghost](Weapons/ghost.md) | Pistolas | 26 / 78 | 15 + 45 | 500 |
| [TriShot Arcano](Weapons/trishot.md) | Pistolas | 22 / 30 | 4 + 32 | 800 |
| [Lust & Greed](Weapons/lust_greed.md) | Pistolas | 34 / 102 | 12 + 21 | 900 |
| [Punishment](Weapons/punishment.md) | Trunfo | 150 / 666 | 1 + 0 | 0 |
| [Espada Rúnica](Weapons/runic_sword.md) | Corpo a corpo | 50 / 120 | 0 + 0 | 400 |
| [Foice de Ceifador](Weapons/reaper_scythe.md) | Corpo a corpo | 45 / 110 | 0 + 0 | 550 |
| [Martelo de Forja](Weapons/forge_hammer.md) | Corpo a corpo | 60 / 130 | 0 + 0 | 700 |
| [P90 Rúnica](Weapons/runic_p90.md) | SMGs | 22 / 38 | 50 + 100 | 1050 |
| [Thompson Arcana](Weapons/arcane_thompson.md) | SMGs | 27 / 47 | 30 + 90 | 850 |
| [Dual Hand Sig MPX](Weapons/dual_mpx.md) | SMGs | 18 / 36 | 40 + 80 | 1100 |
| [Sawed-Off Rúnica](Weapons/runic_sawed_off.md) | Escopetas | 20 / 24 | 5 + 10 | 850 |
| [Lever-Action Arcana (Choke)](Weapons/lever_action.md) | Escopetas | 30 / 32 | 5 + 15 | 1050 |
| [FAMAS Arcano](Weapons/arcane_famas.md) | Rifles | 35 / 86 | 25 + 75 | 1650 |
| [Vandal Rúnico (AK)](Weapons/runic_vandal.md) | Rifles | 40 / 160 | 25 + 75 | 2900 |
| [Phantom Arcano (M4)](Weapons/arcane_phantom.md) | Rifles | 39 / 156 | 30 + 90 | 2900 |
| [Guardian Arcano](Weapons/arcane_guardian.md) | Rifles | 65 / 160 | 12 + 36 | 2250 |
| [Scout Leve](Weapons/light_scout.md) | Snipers | 85 / 160 | 8 + 24 | 2050 |
| [AWP Pesada](Weapons/heavy_awp.md) | Snipers | 190 / 350 | 5 + 15 | 4700 |
| [Minigun a Vapor Arcana](Weapons/arcane_minigun.md) | Metralhadora | 18 / 28 | 100 + 0 | 3500 |

## Magias

Cada linha corresponde a um arquivo próprio de ficha e a um `ArcaneCartridgeData`. As cinco famílias por cinco elementos cobrem 25 combinações, reutilizadas pelas armas compatíveis.

| Cartucho | Família | Elemento | Papéis | CR proposto |
|---|---|---|---|---|
| [Passo de Brasa](Spells/smg_fire.md) | SMGs | Fogo | Avanço + Controle | 200 |
| [Corrente de Invasão](Spells/smg_water.md) | SMGs | Água | Avanço + Controle | 200 |
| [Cunha de Avanço](Spells/smg_earth.md) | SMGs | Terra | Avanço + Controle | 350 |
| [Arranque de Íon](Spells/smg_lightning.md) | SMGs | Relâmpago | Avanço + Controle | 350 |
| [Passagem do Eclipse](Spells/smg_light_dark.md) | SMGs | Luz & Escuridão | Avanço + Controle | 600 |
| [Fornalha de Soleira](Spells/shotgun_fire.md) | Escopetas | Fogo | Controle + Bloqueio | 200 |
| [Geada de Emboscada](Spells/shotgun_water.md) | Escopetas | Água | Controle + Bloqueio | 200 |
| [Mureta de Cerco](Spells/shotgun_earth.md) | Escopetas | Terra | Controle + Bloqueio | 350 |
| [Trava Galvânica](Spells/shotgun_lightning.md) | Escopetas | Relâmpago | Controle + Bloqueio | 350 |
| [Véu de Limiar](Spells/shotgun_light_dark.md) | Escopetas | Luz & Escuridão | Controle + Bloqueio | 600 |
| [Cortina de Cinzas](Spells/rifle_fire.md) | Rifles | Fogo | Bloqueio + Observação | 200 |
| [Neblina de Sondagem](Spells/rifle_water.md) | Rifles | Água | Bloqueio + Observação | 200 |
| [Bastião Sismográfico](Spells/rifle_earth.md) | Rifles | Terra | Bloqueio + Observação | 350 |
| [Cortina de Íons](Spells/rifle_lightning.md) | Rifles | Relâmpago | Bloqueio + Observação | 350 |
| [Observatório do Eclipse](Spells/rifle_light_dark.md) | Rifles | Luz & Escuridão | Bloqueio + Observação | 600 |
| [Farol de Brasas](Spells/sniper_fire.md) | Snipers | Fogo | Observação + Suporte | 200 |
| [Orvalho de Vigília](Spells/sniper_water.md) | Snipers | Água | Observação + Suporte | 200 |
| [Marco de Sentinela](Spells/sniper_earth.md) | Snipers | Terra | Observação + Suporte | 350 |
| [Telêmetro Galvânico](Spells/sniper_lightning.md) | Snipers | Relâmpago | Observação + Suporte | 350 |
| [Vigília do Eclipse](Spells/sniper_light_dark.md) | Snipers | Luz & Escuridão | Observação + Suporte | 600 |
| [Caldeira de Contenção](Spells/machinegun_fire.md) | Metralhadora | Fogo | Controle + Bloqueio | 200 |
| [Dique de Vapor](Spells/machinegun_water.md) | Metralhadora | Água | Controle + Bloqueio | 200 |
| [Baluarte de Cerco](Spells/machinegun_earth.md) | Metralhadora | Terra | Controle + Bloqueio | 350 |
| [Grade de Supressão](Spells/machinegun_lightning.md) | Metralhadora | Relâmpago | Controle + Bloqueio | 350 |
| [Domínio do Eclipse](Spells/machinegun_light_dark.md) | Metralhadora | Luz & Escuridão | Controle + Bloqueio | 600 |

## Editar e conferir

Os arquivos `.asset` são os dados editáveis. As fichas são a especificação humana e devem acompanhar mudanças de balanceamento. Não há regeneração automática que sobrescreva ajustes no Inspector.

Abrir o projeto com a versão indicada em `ProjectSettings/ProjectVersion.txt` e usar **A3C > Arsenal > Selecionar catalogo**. Selecionar uma arma ou cartucho mostra seus parâmetros e um aviso sobre o estado da implementação. Os assets novos não substituem silenciosamente o Akimbo da cena existente.

Executar `python Tools/validate_arsenal.py` na raiz para conferir cobertura, referências, limites competitivos, consistência das fichas e metadados. A checagem estática não substitui importação, compilação C# e Play Mode no Unity.
