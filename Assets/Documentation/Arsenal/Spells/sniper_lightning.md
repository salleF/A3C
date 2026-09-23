# Telêmetro Galvânico

ID: `sniper_lightning` | Relâmpago | Snipers | [Asset Unity](../../../Data/Arsenal/Cartridges/sniper_lightning.asset) | [Índice](../README.md)

## Papel tático

**Observação + Suporte.** Um pulso de medição registra posições no impacto; a descarga de retorno acelera brevemente o atirador para sair da linha exposta.

Esta é uma proposta de magia derivada das famílias e elementos do roteiro. O roteiro não nomeia as 25 magias nem fixa seus efeitos, custos ou tempos; esses detalhes exigem playtest.

## Compra e acionamento

- Preço proposto: **350 CR** por cartucho com **3 cargas**.
- Uma posição entre Q, E e C. Máximo de três cartuchos equipados. Elementos repetidos ocupam posições separadas.
- Intervalo mínimo entre ativações deste cartucho: **1 s**.
- Alcance máximo do ponto de impacto elemental: **90 m**.
- Armas compatíveis: [Scout Leve](../Weapons/light_scout.md), [AWP Pesada](../Weapons/heavy_awp.md).

Q/E/C arma o slot; o próximo disparo válido consome 1 carga e desarma o slot. Rajada, tiro duplo e pellets contam como um acionamento. Efeitos de impacto são criados uma vez no primeiro impacto válido dentro do alcance; efeitos no usuário ocorrem no disparo. Errar ou exceder alcance gasta a carga. Trocar arma, morrer ou recarregar cancela o preparo sem gasto. E perto do Dragão prioriza a interação de despertar e não arma cartucho.

## Efeitos configuráveis

| Efeito | Alvo | Origem | Duração | Raio | Intensidade |
|---|---|---|---|---|---|
| Revelação | Inimigos | Impacto | 2 s | 5 m | Não se aplica |
| Velocidade | Próprio usuário | Usuário | 1.5 s | 0 m | 15% |

- **Revelação:** Última posição vista, registrada uma vez.
- **Velocidade:** Bônus de 15% não aumenta a precisão em movimento nem remove a penalidade própria da arma.

Duração zero significa efeito instantâneo. Raio zero significa somente o usuário ou a dimensão própria de uma barreira. Intensidade usa fração para lentidão, velocidade, ofuscamento e supressão; cura usa HP. Nenhum efeito desta proposta altera dano de bala, ignora escudo, drena escudo ou causa dano adicional. A cura pertence apenas a cartuchos de suporte da família Sniper.

## Como o adversário responde

Reposicionar após o ping e punir o intervalo entre tiros. Não revela através de paredes nem teleporta projéteis.

## Apresentação no universo de Crushle

Marca de distância amarela e aro violeta no punho do atirador, com estalo único de capacitor.

Cor de referência: `#EEDB65`. Identificar elemento também por forma e áudio. Sinalizar começo, volume ocupado e fim; o portal de Crushle deve continuar separado visualmente da informação de combate. Luz & Escuridão usa um só slot e as duas metades atuam juntas.

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
