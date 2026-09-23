# Baluarte de Cerco

ID: `machinegun_earth` | Terra | Metralhadora | [Asset Unity](../../../Data/Arsenal/Cartridges/machinegun_earth.asset) | [Índice](../README.md)

## Papel tático

**Controle + Bloqueio.** Parede larga de contenção força a troca de rota; o assentamento desacelera uma vez os inimigos expostos perto da base.

Esta é uma proposta de magia derivada das famílias e elementos do roteiro. O roteiro não nomeia as 25 magias nem fixa seus efeitos, custos ou tempos; esses detalhes exigem playtest.

## Compra e acionamento

- Preço proposto: **350 CR** por cartucho com **3 cargas**.
- Uma posição entre Q, E e C. Máximo de três cartuchos equipados. Elementos repetidos ocupam posições separadas.
- Intervalo mínimo entre ativações deste cartucho: **3 s**.
- Alcance máximo do ponto de impacto elemental: **30 m**.
- Armas compatíveis: [Minigun a Vapor Arcana](../Weapons/arcane_minigun.md).

Q/E/C arma o slot; o próximo disparo válido consome 1 carga e desarma o slot. Rajada, tiro duplo e pellets contam como um acionamento. Efeitos de impacto são criados uma vez no primeiro impacto válido dentro do alcance; efeitos no usuário ocorrem no disparo. Errar ou exceder alcance gasta a carga. Trocar arma, morrer ou recarregar cancela o preparo sem gasto. E perto do Dragão prioriza a interação de despertar e não arma cartucho.

## Efeitos configuráveis

| Efeito | Alvo | Origem | Duração | Raio | Intensidade |
|---|---|---|---|---|---|
| Barreira | Mundo | Impacto | 5 s | 0 m | Não se aplica |
| Lentidão | Inimigos | Impacto | 1 s | 3 m | 20% |

- **Barreira:** Parede de 0,5 m de profundidade. Rejeitar interseção com jogadores, Dragão e volume de interação do objetivo. Vida: 200 HP. Largura: 5 m. Altura: 2 m.
- **Lentidão:** Slow aplicado uma vez, antes da criação da parede, aos inimigos com visão livre.

Duração zero significa efeito instantâneo. Raio zero significa somente o usuário ou a dimensão própria de uma barreira. Intensidade usa fração para lentidão, velocidade, ofuscamento e supressão; cura usa HP. Nenhum efeito desta proposta altera dano de bala, ignora escudo, drena escudo ou causa dano adicional. A cura pertence apenas a cartuchos de suporte da família Sniper.

## Como o adversário responde

Destruir com fogo concentrado ou Martelo, contornar ou aguardar 5 s. Não gerar parede em rota sem alternativa.

## Apresentação no universo de Crushle

Pedras de fortaleza presas por aros de cobre, com rachaduras de energia que tornam visível a vida da barreira.

Cor de referência: `#9BBA68`. Identificar elemento também por forma e áudio. Sinalizar começo, volume ocupado e fim; o portal de Crushle deve continuar separado visualmente da informação de combate. Luz & Escuridão usa um só slot e as duas metades atuam juntas.

## Critérios de aceitação futuros

- Recusar arma de outra família, pistola, melee e Punishment; aceitar todas as primárias da família, inclusive AWP para Sniper.
- Uma ativação consome uma carga, inclusive erro; pellets, mãos e rajada não multiplicam áreas, cura ou revelação.
- Respeitar time, jogadores vivos, alcance e cobertura sólida. Não revelar jogadores atrás de parede nem propagar efeito a outra sala.
- Efeitos iguais não somam intensidade; prevalece o mais forte e cada fonte mantém sua expiração. Morte, fim da rodada e destruição do objeto removem a fonte.
- Fumaça bloqueia a visão dos dois times. Barreiras bloqueiam os dois times, são destrutíveis e não podem prender personagem ou impedir a interação com o Dragão.
- Perto do Dragão, segurar E por 4 s usa a interação e nunca gasta cartucho.

## Estado da implementação

Dados e ficha de design. Não há SAASystem nem executor de efeitos no protótipo; este asset não altera projéteis, movimento, HUD ou vida automaticamente. Quantidades são propostas de playtest.

Ver [contratos de integração](../Unity_Integration.md) antes de ligar ao combate. Não há VFX, modelo ou som final incluído.
