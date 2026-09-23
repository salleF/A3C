# Integração do catálogo no Unity

Esta entrega é de **fichas e assets configuráveis**, conforme o escopo escolhido. Há 21 definições de arma (20 equipáveis e o trunfo Punishment), 25 definições de cartucho e um catálogo central. Mecânicas novas não foram conectadas automaticamente à cena.

## Abrir e navegar

1. Abrir a raiz do projeto no Unity indicado por `ProjectSettings/ProjectVersion.txt`: `6000.6.1f1` no checkout de origem. Não converter a cena com uma versão antiga para contornar a falta de editor.
2. Aguardar importação dos pacotes e conferir o Console.
3. Usar **A3C > Arsenal > Selecionar catalogo** para selecionar `Assets/Data/Arsenal/ArsenalCatalog.asset`.
4. O catálogo referencia os dois itens iniciais, todas as armas e todos os cartuchos. Selecionar os assets para editar os valores no Inspector.
5. Ler `Implementation Notes` antes de equipar qualquer asset. As fichas em `Assets/Documentation/Arsenal` descrevem o comportamento esperado, inclusive o que ainda não existe no runtime.

Os dados usam [ScriptableObject](https://docs.unity3d.com/6000.0/Documentation/Manual/class-ScriptableObject.html), que permite armazenar e referenciar parâmetros pelo Inspector. Não são prefabs, scripts de execução ou modelos 3D. O projeto continua abrindo a `SampleScene`; nenhum objeto, material ou modelo existente foi substituído.

## Contrato de WeaponData

Os campos originais de `WeaponData`, sua classe, namespace e GUID foram preservados. Os valores de `FireMode` originais continuam 0, 1, 2 e 3; `Melee` foi acrescentado como 4. `WeaponCategory.Unspecified = 0` mantém o asset legado sem atribuir uma família que ele não tinha.

| Campo | Unidade e uso |
|---|---|
| bodyDamage / headshotDamage | Dano por bala, bago ou golpe. Não somar ambos para o mesmo impacto. |
| pelletsPerShot | Quantidade de bagos por carga de uma mão. |
| maxSimultaneousShots | Número máximo de cargas disparadas juntas: 2 para TriShot e MPX; 1 nas demais. |
| magazineSize | Total carregado, somando as duas mãos quando houver. |
| magazinePerHand | Capacidade por mão; zero para arma simples. |
| maxReserveAmmo | Reserva inicial proposta, exceto Lust & Greed com 21 conforme roteiro. |
| fireRateRPM | Acionamentos por minuto; em rajada, intervalo das balas é também descrito por burstInterval. |
| burstInterval | Segundos entre balas dentro da rajada; FAMAS: 0,12 s. Novo gatilho só após o fim da rajada e sua cadência. |
| effectiveRange | Alcance de balanceamento em metros. O controlador atual não aplica queda de dano nem esse limite. |
| baseSpread / maxSpread | Deslocamento adimensional na direção de tiro, conforme cálculo já usado pelo protótipo; não são graus. |
| chargedSpreadMultiplier | Multiplicador de dispersão na carga máxima do Choke. Não aumenta dano. |
| equippedMoveMultiplier | Multiplicador da velocidade base enquanto equipada. |
| sprintMultiplier | Bônus adicional somente ao correr; Espada: 1,10. |
| meleeArcDegrees / meleeRange | Arco em graus e alcance em metros para o futuro controlador melee. |
| specialWeapon / shotsToUnlockSpecial | Referência à Punishment e sequência de 12 tiros para Lust & Greed. A sequência exige pente completo e nenhuma recarga intermediária. |
| SupportsArcaneCartridges | Derivado da categoria, sem flag que permita habilitar S.A.A. em pistola. Inclui AWP como Sniper. |
| designSheetPath | Caminho da especificação humana relativo à raiz do projeto. |

Nenhum runtime existente lê os novos campos de mecânica. Antes de experimentar outra arma na cena, duplicar a cena para teste e corrigir a tag `Head` descrita na revisão. Não usar o controlador genérico para afirmar que melee, rajada, Choke, dual ou aquecimento estão implementados.

## Contrato de ArcaneCartridgeData

Cada asset define exatamente uma família e um elemento. `LightDark` é um único valor: as duas metades atuam na mesma ativação. `IsCompatibleWith` e `ArsenalCatalog.FindCartridge` ajudam a consultar o catálogo, mas não equipam nem executam habilidades.

| Campo | Contrato |
|---|---|
| chargesPerPurchase | Três ativações disponíveis ao comprar um cartucho, sem regeneração durante a rodada. |
| cooldownSeconds | Intervalo mínimo entre ativações daquele cartucho, não recarga de suas cargas. |
| castRangeMetres | Alcance máximo do impacto que gera o efeito. A bala mantém a própria trajetória e dano. |
| effects | Lista de efeitos de uma mesma carga. Não dividir o consumo pelo tamanho desta lista. |
| target | Self: usuário. Allies: aliados vivos, incluindo usuário. Enemies: inimigos vivos. World: volume visual ou físico para os dois times. |
| anchor | Caster: posição do usuário no disparo. Impact: primeiro impacto válido dentro do alcance. |
| durationSeconds | Zero para cura instantânea; positivo para a vida de área, buff, marca ou barreira. |
| radiusMetres | Raio em metros. Zero quando o efeito é individual ou a barreira usa largura/altura próprias. |
| amount | Fração entre 0 e 1 para lentidão, bônus de velocidade, intensidade de ofuscamento e aumento de dispersão. HP reais para cura. Zero para fumaça, barreira e revelação. |
| barrierHealth / barrierWidth / barrierHeight | Vida destrutível e dimensões em metros. Profundidade e orientação estão nas regras da ficha. Não indicam geometria já criada. |
| rules | Restrições específicas que o futuro executor precisa implementar; não são código executável. |

Todos os cartuchos desta proposta têm dano adicional zero. Cura deve chamar uma operação de vida real com teto de 100 e recusar alvos mortos, sem converter excedente em escudo. Revelação é um ping da última posição, nunca um rastreamento global. Testar oclusão no instante do pulso, antes de criar a barreira do mesmo cartucho.

Para a origem `Caster`, aplicar o efeito uma vez ao disparar, mesmo que a bala erre. Para `Impact`, aplicar somente no primeiro impacto válido e no máximo uma vez por acionamento. Num tiro de vários bagos ou numa rajada, um identificador de acionamento compartilhado deverá impedir áreas repetidas.

## Sequência de execução a implementar

1. Na loja, oferecer somente cartuchos compatíveis com a primária e preencher no máximo Q/E/C. A loja e economia ainda não existem no protótipo.
2. Manter munição, cargas restantes, slot armado, cooldown e buffs no estado do jogador, nunca no ScriptableObject compartilhado. Dois jogadores podem referenciar o mesmo asset sem compartilhar consumo.
3. Q/E/C arma o próximo acionamento. Trocar o slot antes de disparar só muda a seleção. Trocar de arma, recarregar ou morrer cancela preparo sem consumir carga. Equipar novamente não repõe cargas.
4. Se o alvo de interação do defensor for o Dragão e estiver em alcance, E fica reservado à interação por 4 s, inclusive durante a espera entre frames; liberar E interrompe a interação. Não gastar slot E.
5. Quando um disparo realmente consome munição, consumir exatamente uma carga do slot armado e desarmá-lo. Gatilho vazio, recarga ou aquecimento ainda sem tiro não consomem carga.
6. Instanciar os efeitos no usuário imediatamente e os de impacto no primeiro contato válido em alcance. Erro gasta carga; impacto fora de alcance não cria efeito. Não multiplicar pelo número de bagos, mãos ou balas de uma rajada.
7. Validar times, cobertura, alvo vivo e bloqueio do objetivo. Fumaça e barreiras afetam visão/colisão de ambos os times; buffs e controles obedecem ao alvo configurado. Não somar intensidades iguais: prevalece a maior enquanto sua fonte durar.
8. Remover fontes ao expirar, destruir a barreira, morrer o alvo ou terminar a rodada. Não alterar os valores base permanentemente. Em rede, autoridade de dano, consumo e colocação deve ficar no servidor; o protótipo atual não implementa essa autoridade.

## Arte e modelos

`projectilePrefab` fica vazio nos novos assets. É uma ausência intencional: não há modelo, áudio, partícula ou animação original para cada item no repositório. Usar as fichas como briefing. O projétil esférico de fallback continua sendo comportamento antigo de `WeaponController` e não é apresentação final, especialmente para Ghost e Phantom.

Preservar os arquivos `.meta` ao mover assets. Eles mantêm os identificadores usados pelas referências, conforme a [documentação de metadados da Unity](https://docs.unity3d.com/6000.0/Documentation/Manual/AssetMetadata.html). Cada arquivo e pasta nova desta entrega possui metadado.

## Validação e limites

Na raiz, executar:

```powershell
python Tools/validate_arsenal.py
git diff --check
```

O validador usa apenas a biblioteca padrão do Python. Confere os 21 itens esperados, a matriz completa de 25 cartuchos, regras de dano, simetria das iniciais, munição especial, elegibilidade, metadados, referências, campos das classes e números das fichas. Não abre Unity nem simula física, shader ou controles.

Não foi encontrado Unity Editor nesta máquina. Permanecem pendentes importação e compilação reais no editor, inspeção visual dos assets no Inspector e Play Mode. Também não houve teste de balanceamento 5v5, desempenho ou legibilidade visual de Crushle.
