# Revisão do projeto A3C

Base inspecionada: `8686851ee9f54d3a92410c4d4d8d0f327cd4a487`, branch `main` de `salleF/A3C`. Análise estática do código, cenas serializadas, geometrias OBJ, importadores e documentação. Não houve execução do Unity ou renderização da cena.

O foco desta entrega é continuar fichas e assets configuráveis. Os problemas abaixo foram confirmados no código de origem e ficam como próximos trabalhos de runtime. Nenhuma dessas correções foi aplicada silenciosamente à cena, ao combate ou aos modelos.

## Código: problemas confirmados

| Prioridade | Evidência na base | Efeito e próximo passo |
|---|---|---|
| Alta | `ProjectSettings/TagManager.asset:6` contém `tags: []`; `Projectile.cs:95` e `:105` usam `CompareTag("Head")`; `TargetDummy.cs:22` atribui essa tag. | Registrar `Head` e criar hitboxes de cabeça antes de testar headshots. O catálogo ter HS correto não resolve a configuração ausente. |
| Alta | `TriShotAkimbo.cs:133` calcula `toAdd`, mas as linhas 138 e 139 sempre deixam as duas mãos com 2. | Com ambas vazias e uma bala de reserva, surgem quatro carregadas. Distribuir somente `toAdd` e testar reservas de 0 a 4. |
| Alta | `TriShotAkimbo.cs:14` configura 160 de HS por bago e `CastPellets(6)` é chamado no tiro duplo. | Seis HS podem somar 960, contrariando a regra dos dois hitkills. O novo asset propõe 30 por bago, mas o script legado ainda precisa passar a lê-lo. |
| Alta | `TriShotAkimbo.cs:48` e `:75` aceitam disparos sem uma trava de cadência. | Taxa depende dos cliques, não do RPM do catálogo. Usar um limite compartilhado entre LMB/RMB; não anexar dois controladores lendo o mesmo mouse. |
| Alta | `HealthSystem.cs:67` define teto construtivo de 200 de escudo e `:109` soma 20 sem clamp. | O total pode passar de 200 e crescer indefinidamente. Fixar teto competitivo de 100 de escudo, iniciar em zero a cada rodada e preservar HP real. O roteiro exige +20 por abate; a aplicação desse teto preserva a economia de 200 HP máximos. |
| Alta | `TargetDummy.cs:65` desativa o próprio GameObject antes do `WaitForSeconds` da rotina de retorno. | A coroutine é interrompida e não chega a reativar o boneco. Ocultar renderers/colliders ou usar um gerenciador externo de respawn. A desativação interromper coroutines é documentada pela [Unity](https://docs.unity3d.com/6000.0/Documentation/ScriptReference/MonoBehaviour.StartCoroutine.html). |
| Média | `WeaponController.cs:63` diferencia somente FullAuto dos outros modos; não usa `burstCount`. | FAMAS definida como Burst ainda dispara um tiro por clique. Implementar sequência com cancelamento em troca, morte e recarga. |
| Média | `WeaponController.cs:38` troca arma sem cancelar o `Invoke(FinishReload)` agendado em `:166`. | Uma recarga antiga pode terminar sobre a arma nova. Cancelar a invocação e manter munição por instância equipada, sem repor reserva ao trocar. |
| Média | `Projectile.cs:107` só chama `health.TakeDamage`; `RegisterKill` é chamado em `TargetDummy.cs:43`, não no dano a HealthSystem. | O colete construtivo recebe abates em bonecos, mas não no caminho de jogadores reais. Registrar transição vivo para morto uma única vez e atribuir o atacante. |

Os caminhos de scripts da tabela são relativos a `Assets/_Scripts/Combat`. As linhas identificam a base e os arquivos de runtime preservados.

## Objetivo e mapa Crushle

`Assets/Scenes/SampleScene.unity` é a única cena habilitada em `ProjectSettings/EditorBuildSettings.asset`. A cena tem 73 GameObjects serializados, incluindo `Mapa` (`:2455`), `Player` (`:4266`), `HUD` (`:9640`) e `Torre` (`:46284`). Há 65 referências ao script de geometria ProBuilder de GUID `8233d90336aea43098adf6dbabd606a2`, entre blocos, cilindros e peças de escada. Esse inventário descreve a estrutura; não prova a qualidade visual, os tempos de rotação ou o balanceamento das rotas.

O jogador usa o asset legado `Assets/_Scripts/Akimbo.asset`, referenciado em `SampleScene.unity:4301`. O HUD tem `dragon: {fileID: 0}` em `:9675`. Nenhuma das duas cenas contém componente `SleepingDragon` ou `TargetDummy` referenciado pelo GUID local. `Assets/_Recovery/0.unity` tem 30 GameObjects e não deve ser promovida à cena principal sem comparação no editor.

Em `Assets/_Scripts/Objective/SleepingDragon.cs`, o Dragão começa plantado (`:9`), `defuseRange` é declarado mas não usado (`:12`), a corrupção máxima apenas gera log a cada frame (`:30`), e `ProcessDefuse` (`:36`) não tem chamador nos scripts existentes. Não há fluxo jogável completo de transportar, plantar, validar lado/alcance, despertar e fechar rodada.

Ordem sugerida para evoluir o mapa:

1. Nomear setores, rotas, coberturas e spawns. Marcar Bombsites A/B e volumes de plant/interação em uma cópia de teste da cena, mantendo a referência atual.
2. Medir tempo de contato inicial, rotação A/B, exposição de entrada e linhas longas. Validar com walk 6 e sprint 9 do controlador atual, depois com a Espada. Não inventar medidas finais a partir dos nomes dos objetos.
3. Testar alcance de escopeta, leitura de headshot e coberturas na altura do personagem. Corrigir colisão e escala antes de detalhar materiais.
4. Introduzir os motivos da lore: portal central como ponto de orientação, blocos suspensos fora das linhas críticas, arcos medievais e raízes luminosas. Cor violeta do portal não deve se confundir com marca de inimigo ou área elemental.
5. Definir volumes onde barreiras mágicas podem surgir sem prender jogador, vedar rota única ou bloquear a interação com o Dragão. Testar Martelo e os cinco cartuchos de cada família no mesmo conjunto de portas.
6. Só então fazer a passagem de arte, iluminação, oclusão, som de setores e perfil de desempenho. Não há dados de FPS nesta revisão.

## Modelos e animação

Inventário dos arquivos OBJ, medido nas coordenadas exportadas, antes de transformações da cena:

| Arquivo em Assets/Models | Vértices | Faces | Triângulos após triangulação simples | Dimensões X/Y/Z |
|---|---|---|---|---|
| `rifle.obj` | 1019 | 505 | 1026 | 27,137 / 6,910 / 2,112 |
| `Low Poly SuperHero.obj` | 238 | 242 | 472 | 3,362 / 5,746 / 1,058 |

Os triângulos são contados como n-2 por face; a triangulação e normais finais ainda devem ser conferidas no importador. As dimensões são unidades do arquivo, não tamanho final de personagens na cena. Ambos os importadores têm `globalScale: 1`; conferir unidade de exportação e transforms antes de modificar qualquer escala.

- `rifle.obj:3` referencia `rifle.mtl`; `Low Poly SuperHero.obj:3` referencia `Low Poly SuperHero.mtl`. Nenhum desses MTL está versionado. Entregar materiais URP explícitos e conferir o vínculo, sem afirmar que a malha já tem os materiais da lore.
- Os seis arquivos de geometria/animação de `Modelos/` são cópias byte a byte dos correspondentes em `Assets/Models/`. Não foram apagados. Definir se `Modelos` será fonte de autoria ou arquivo de referência; arquivos Blender de origem não estão presentes.
- Existem quatro FBX de rifle: Idle, Walk, Run e Firing. Em `PlayerAnimator.controller`, só há transições condicionadas por `Speed > 0.1` e `< 0.1` (`:209` e `:122`). Dois estados não têm transições (`:40`, `:66`), e nenhuma transição usa `IsFiring`. Conectar corrida e disparo, verificar avatar, loops e camada de braços no editor.
- O conjunto atual não contém modelos exclusivos das 20 armas nem das 25 magias. As novas fichas descrevem silhueta, materiais, áudio e animações esperadas; não apresentam os assets de dados como arte pronta.

## Documentação e conflitos resolvidos no catálogo

| Proposta anterior | Roteiro adotado |
|---|---|
| Guardian 195 HS | Teto de rifle 160 HS |
| Scout 80 no corpo | 85 no corpo |
| Punishment por seis acertos | Esvaziar 12 tiros sem recarga intermediária, inclusive tiros errados |
| AWP excluída de S.A.A. como TODO | S.A.A. disponível às primárias, incluindo Sniper pesada |
| Cura em SMG e dano que ignora escudo em rifle | Papéis por família respeitados; cura de suporte em Sniper e utilidades sem dano extra |
| Escopetas com soma de bagos acima de 200 | Propostas com dano máximo simultâneo abaixo de 200 |

O documento antigo de arsenal foi transformado em entrada para o catálogo completo, evitando duas tabelas contraditórias. A lore e o `Core_gameplay.txt` originais foram preservados. A lore contém caracteres já substituídos por `?`; recuperar esses caracteres exige uma fonte íntegra. O roteiro usa codificação Windows-1252, que deve ser respeitada ao abri-lo, e não foi convertido nesta entrega. As novas fichas foram escritas em UTF-8.

## Cobertura e próximos passos

Trabalho feito sequencialmente, sem time de agentes. Foram lidos os scripts próprios, lore, gameplay, manifesto de pacotes, configurações relevantes, ambas as cenas serializadas, OBJ, importadores e Animator. FBX foram inventariados e comparados por hash, mas não renderizados nem avaliados por retargeting. Pacotes de terceiros e malhas ProBuilder não foram tratados como código próprio a corrigir.

Antes de declarar o protótipo jogável: importar com Unity, corrigir tag e atribuição de abate, consertar recarga e cadência dual, implementar a família de controladores especiais, executar o S.A.A. e ligar a rodada do Dragão. Depois, playtest integrado de arma, magia, colete e mapa. A lista de retomada está no `STATUS.json` da raiz.
