# Cunha de Avanço

ID: `smg_earth` | Terra | SMGs | [Asset Unity](../../../Data/Arsenal/Cartridges/smg_earth.asset) | [Índice](../README.md)

## Papel tático

**Avanço + Controle.** Ergue uma cobertura baixa de pedra rúnica, suficiente para atravessar um corredor agachado ou dividir uma entrada. Não cria uma parede completa.

Esta é uma proposta de magia derivada das famílias e elementos do roteiro. O roteiro não nomeia as 25 magias nem fixa seus efeitos, custos ou tempos; esses detalhes exigem playtest.

## Compra e acionamento

- Preço proposto: **350 CR** por cartucho com **3 cargas**.
- Uma posição entre Q, E e C. Máximo de três cartuchos equipados. Elementos repetidos ocupam posições separadas.
- Intervalo mínimo entre ativações deste cartucho: **1 s**.
- Alcance máximo do ponto de impacto elemental: **20 m**.
- Armas compatíveis: [P90 Rúnica](../Weapons/runic_p90.md), [Thompson Arcana](../Weapons/arcane_thompson.md), [Dual Hand Sig MPX](../Weapons/dual_mpx.md).

Q/E/C arma o slot; o próximo disparo válido consome 1 carga e desarma o slot. Rajada, tiro duplo e pellets contam como um acionamento. Efeitos de impacto são criados uma vez no primeiro impacto válido dentro do alcance; efeitos no usuário ocorrem no disparo. Errar ou exceder alcance gasta a carga. Trocar arma, morrer ou recarregar cancela o preparo sem gasto. E perto do Dragão prioriza a interação de despertar e não arma cartucho.

## Efeitos configuráveis

| Efeito | Alvo | Origem | Duração | Raio | Intensidade |
|---|---|---|---|---|---|
| Barreira | Mundo | Impacto | 3 s | 0 m | Não se aplica |

- **Barreira:** Bloqueia movimento e balas de ambos os times. Profundidade proposta de 0,35 m; não gerar sobre jogador, objetivo ou saída única. Vida: 80 HP. Largura: 1.5 m. Altura: 1 m.

Duração zero significa efeito instantâneo. Raio zero significa somente o usuário ou a dimensão própria de uma barreira. Intensidade usa fração para lentidão, velocidade, ofuscamento e supressão; cura usa HP. Nenhum efeito desta proposta altera dano de bala, ignora escudo, drena escudo ou causa dano adicional. A cura pertence apenas a cartuchos de suporte da família Sniper.

## Como o adversário responde

Atirar na cunha, contornar ou usar Martelo de Forja. A baixa altura permite observar por cima.

## Apresentação no universo de Crushle

Pedras medievais comprimidas por grampos de cobre holográficos, base luminosa que mostra o prazo de dissolução.

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
