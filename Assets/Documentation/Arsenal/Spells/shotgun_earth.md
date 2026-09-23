# Mureta de Cerco

ID: `shotgun_earth` | Terra | Escopetas | [Asset Unity](../../../Data/Arsenal/Cartridges/shotgun_earth.asset) | [Índice](../README.md)

## Papel tático

**Controle + Bloqueio.** Uma única mureta aparece no primeiro impacto válido do disparo, mesmo que seis ou oito bagos acertem. Fecha a parte baixa de uma passagem.

Esta é uma proposta de magia derivada das famílias e elementos do roteiro. O roteiro não nomeia as 25 magias nem fixa seus efeitos, custos ou tempos; esses detalhes exigem playtest.

## Compra e acionamento

- Preço proposto: **350 CR** por cartucho com **3 cargas**.
- Uma posição entre Q, E e C. Máximo de três cartuchos equipados. Elementos repetidos ocupam posições separadas.
- Intervalo mínimo entre ativações deste cartucho: **1 s**.
- Alcance máximo do ponto de impacto elemental: **15 m**.
- Armas compatíveis: [Sawed-Off Rúnica](../Weapons/runic_sawed_off.md), [Lever-Action Arcana (Choke)](../Weapons/lever_action.md).

Q/E/C arma o slot; o próximo disparo válido consome 1 carga e desarma o slot. Rajada, tiro duplo e pellets contam como um acionamento. Efeitos de impacto são criados uma vez no primeiro impacto válido dentro do alcance; efeitos no usuário ocorrem no disparo. Errar ou exceder alcance gasta a carga. Trocar arma, morrer ou recarregar cancela o preparo sem gasto. E perto do Dragão prioriza a interação de despertar e não arma cartucho.

## Efeitos configuráveis

| Efeito | Alvo | Origem | Duração | Raio | Intensidade |
|---|---|---|---|---|---|
| Barreira | Mundo | Impacto | 4 s | 0 m | Não se aplica |

- **Barreira:** Mureta de 0,4 m de profundidade. Rejeitar colocação que intercepte jogador ou Dragão. Vida: 140 HP. Largura: 3 m. Altura: 1.2 m.

Duração zero significa efeito instantâneo. Raio zero significa somente o usuário ou a dimensão própria de uma barreira. Intensidade usa fração para lentidão, velocidade, ofuscamento e supressão; cura usa HP. Nenhum efeito desta proposta altera dano de bala, ignora escudo, drena escudo ou causa dano adicional. A cura pertence apenas a cartuchos de suporte da família Sniper.

## Como o adversário responde

Destruir, mirar por cima ou contornar. Martelo recebe seu bônus de barreira; cobertura não protege dos ângulos laterais.

## Apresentação no universo de Crushle

Blocos de calcário de Crushle com juntas rúnicas verdes e rachaduras que revelam a vida restante.

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
