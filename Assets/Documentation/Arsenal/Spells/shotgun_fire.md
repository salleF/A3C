# Fornalha de Soleira

ID: `shotgun_fire` | Fogo | Escopetas | [Asset Unity](../../../Data/Arsenal/Cartridges/shotgun_fire.asset) | [Índice](../README.md)

## Papel tático

**Controle + Bloqueio.** Cria uma cortina de fuligem quente para isolar uma porta curta. A pressão térmica reduz o avanço inimigo, sem adicionar dano aos bagos.

Esta é uma proposta de magia derivada das famílias e elementos do roteiro. O roteiro não nomeia as 25 magias nem fixa seus efeitos, custos ou tempos; esses detalhes exigem playtest.

## Compra e acionamento

- Preço proposto: **200 CR** por cartucho com **3 cargas**.
- Uma posição entre Q, E e C. Máximo de três cartuchos equipados. Elementos repetidos ocupam posições separadas.
- Intervalo mínimo entre ativações deste cartucho: **1 s**.
- Alcance máximo do ponto de impacto elemental: **15 m**.
- Armas compatíveis: [Sawed-Off Rúnica](../Weapons/runic_sawed_off.md), [Lever-Action Arcana (Choke)](../Weapons/lever_action.md).

Q/E/C arma o slot; o próximo disparo válido consome 1 carga e desarma o slot. Rajada, tiro duplo e pellets contam como um acionamento. Efeitos de impacto são criados uma vez no primeiro impacto válido dentro do alcance; efeitos no usuário ocorrem no disparo. Errar ou exceder alcance gasta a carga. Trocar arma, morrer ou recarregar cancela o preparo sem gasto. E perto do Dragão prioriza a interação de despertar e não arma cartucho.

## Efeitos configuráveis

| Efeito | Alvo | Origem | Duração | Raio | Intensidade |
|---|---|---|---|---|---|
| Fumaça | Mundo | Impacto | 3 s | 2 m | Não se aplica |
| Lentidão | Inimigos | Impacto | 3 s | 2 m | 20% |

- **Fumaça:** Fumaça opaca em ambos os sentidos.
- **Lentidão:** Lentidão de 20% só enquanto dentro da zona.

Duração zero significa efeito instantâneo. Raio zero significa somente o usuário ou a dimensão própria de uma barreira. Intensidade usa fração para lentidão, velocidade, ofuscamento e supressão; cura usa HP. Nenhum efeito desta proposta altera dano de bala, ignora escudo, drena escudo ou causa dano adicional. A cura pertence apenas a cartuchos de suporte da família Sniper.

## Como o adversário responde

Esperar 3 s, contornar a porta ou atravessar aceitando lentidão. A nuvem também bloqueia visão aliada.

## Apresentação no universo de Crushle

Fuligem cor de cobre, brasas baixas e arco de runas com borda legível no chão.

Cor de referência: `#F48438`. Identificar elemento também por forma e áudio. Sinalizar começo, volume ocupado e fim; o portal de Crushle deve continuar separado visualmente da informação de combate. Luz & Escuridão usa um só slot e as duas metades atuam juntas.

## Critérios de aceitação

- Recusar arma de outra família, pistola, melee e Punishment; aceitar todas as primárias da família, inclusive AWP para Sniper.
- Uma ativação consome uma carga, inclusive erro; pellets, mãos e rajada não multiplicam áreas, cura ou revelação.
- Respeitar time, jogadores vivos, alcance e cobertura sólida. Não revelar jogadores atrás de parede nem propagar efeito a outra sala.
- Efeitos iguais não somam intensidade; prevalece o mais forte e cada fonte mantém sua expiração. Morte, fim da rodada e destruição do objeto removem a fonte.
- Fumaça bloqueia a visão dos dois times. Barreiras bloqueiam os dois times, são destrutíveis e não podem prender personagem ou impedir a interação com o Dragão.
- Perto do Dragão, segurar E por 4 s usa a interação e nunca gasta cartucho.

## Estado da implementação

ArcaneSystem consome slots e cargas; ArcaneWorld executa os efeitos locais com times, alcance e oclusao. A cena de treino fornece controles e HUD. Quantidades sao propostas de playtest; VFX e audio finais permanecem pendentes.

Ver [contratos de integração](../Unity_Integration.md) antes de ligar ao combate. Não há VFX, modelo ou som final incluído.
