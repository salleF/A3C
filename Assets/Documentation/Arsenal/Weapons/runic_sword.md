# Espada Rúnica

ID: `runic_sword` | Categoria: Corpo a corpo | [Asset Unity](../../../Data/Arsenal/Weapons/runic_sword.asset) | [Índice](../README.md)

## Identidade e uso

Arma física. Golpe curto com LMB e bônus de 10% somente ao correr enquanto equipada. Não usa munição nem S.A.A. Trocar de arma remove o bônus.

## Configuração inicial

Valores são propostas de balanceamento, exceto as regras explicitamente fixadas no [roteiro](../../../../Core_gameplay.txt) e consolidadas no [guia](../README.md).

| Parâmetro | Valor |
|---|---|
| Corpo por bala/bago/golpe | 50 |
| Cabeça por bala/bago/golpe | 120 |
| Cadência (tiros ou golpes/minuto) | 80 |
| Modo | Corpo a corpo |
| Mecânica especial | Bônus de corrida |
| Bagos por carga disparada | 1 |
| Cargas simultâneas máximas | 1 |
| Dano máximo simultâneo por alvo em HS | 120 |
| Pente total / reserva | 0 / 0 |
| Capacidade por mão (0 = não se aplica) | 0 |
| Recarga | 0 s |
| Preço proposto | 400 CR |
| Alcance efetivo proposto | 2 m |
| Dispersão inicial / máxima | 0.005 / 0.08 |
| Penalidade de dispersão em movimento | 2.5x |
| Movimento equipada / bônus de sprint | 1x / 1.1x |

O dano simultâneo soma bagos e mãos do mesmo acionamento. A rajada da FAMAS é sequencial: seus três tiros não são somados como um único impacto instantâneo. Não somar cabeça e corpo do mesmo bago.

## Cartuchos compatíveis

Não aceita cartuchos S.A.A.

## Direção de arte e áudio

Lâmina reta com canal rúnico tênue, guarda de latão e punho de madeira de varinha. Perfil fino para não ocultar inimigos. Animações de saque, corrida, golpe e recuperação.

Usar madeira de varinha, latão e runas de contenção da A3C. Produzir vistas lateral, frontal e superior, vista em primeira pessoa e silhueta em terceira pessoa. Entregar pivôs de empunhadura, boca e peças móveis, materiais separados e escala em metros. Os modelos existentes são referências de protótipo, não modelos finais desta arma.

## Critérios de aceitação no protótipo futuro

Velocidade de caminhada não muda. Sprint recebe fator 1,10 e retorna ao valor anterior ao desequipar. Um golpe só registra uma vez por alvo, sem atravessar parede.

## Estado da implementação

FireMode.Melee é apenas definição. WeaponController ainda dispara projéteis para modos não automáticos. Requer detecção de golpe e integração com PlayerMovement antes de usar.

Esta entrega inclui a ficha e os parâmetros serializados. Não inclui modelo novo, animação, som, partículas ou mecânica jogável inédita. Ver [integração](../Unity_Integration.md).
