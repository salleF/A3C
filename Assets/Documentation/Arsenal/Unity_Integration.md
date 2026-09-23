# Executar o protótipo local

O catálogo alimenta `WeaponController`, `WeaponState`, `ArcaneSystem` e `ArcaneWorld`. A cena `A3C_Training` monta uma sala de projeção com motivos de Crushle: ruínas suspensas, portal violeta, madeira, latão e runas. A `SampleScene` e os modelos de origem continuam preservados.

## Preparação no editor

1. Instalar o Unity indicado por `ProjectSettings/ProjectVersion.txt` e ativar a licença no Hub.
2. Abrir a raiz do projeto, aguardar a importação e usar **A3C > Configurar cena de treino**.
3. Abrir `Assets/Scenes/A3C_Training.unity` e iniciar Play. O menu de equipamento aparece primeiro.
4. Para gerar o executável, usar **A3C > Validar e gerar build Windows**. O destino é `Builds/WindowsRelease/A3C.exe`.

O build usa Windows x64 e Mono, sem Development Build ou conexão de diagnóstico. Não exige IL2CPP, Android, WebGL ou instalação de Visual Studio para executar o jogo. O editor adiciona o shader URP Unlit às inclusões do build para os visuais procedurais.

Automação equivalente, usando o caminho real do editor:

```powershell
./Tools/unity-verify.ps1 -EditorPath 'C:\Program Files\Unity\Hub\Editor\6000.6.2f1\Editor\Unity.exe'
```

O script primeiro importa, verifica e gera o build. Depois executa o teste de runtime no próprio executável. Logs e relatórios ficam em `Logs/`, fora do Git. Se houver timeout, o processo é preservado para diagnóstico; não iniciar outro editor no mesmo projeto antes de conferir o primeiro.

## Controles e treino

| Controle | Ação |
|---|---|
| WASD / Shift / Espaço | Mover, correr e saltar |
| Mouse / LMB | Mirar e disparar |
| RMB | Mira de sniper; segunda mão em Lust & Greed; disparo duplo em TriShot/MPX |
| R | Recarregar; Lust & Greed recarrega a mão menos carregada |
| Q / E / C | Armar ou desarmar o cartucho do slot para o próximo disparo |
| E no objetivo | Segurar para plantar ou despertar; prioridade sobre o cartucho |
| B / Esc | Menu de equipamento; pausa toda a simulação |
| F5 | Reiniciar exercício, munição, cargas, vida e efeitos |

No menu, escolha arma, slot e cartucho da família compatível. O modo equipamento livre permite experimentar todo o catálogo. Desmarcar a opção usa uma carteira de treino de 15.000 CR e os preços dos assets. Isso é um exercício de compras, sem economia de partida competitiva. Repor munição/cargas e reiniciar são comandos explícitos de treino.

Alvos vermelhos têm 100 HP + 100 de escudo, reaparecem após três segundos e podem se mover ou atirar. O alvo verde tem 40 HP e serve para testar cura e recusa de fogo amigo. A cabeça possui `Hitbox`, sem depender de uma tag criada manualmente.

Treino de ataque começa com o dragão transportado abstratamente pelo jogador. Nos círculos A/B, segure E por três segundos para plantar. Treino de defesa começa com o dragão plantado, a oito metros do jogador. Sustente E por quatro segundos em alcance para despertar. Soltar a tecla ou sair de alcance zera o progresso. A corrupção chega a 100% em 50 segundos e encerra a rodada uma única vez. Tempos de plantio e corrupção são propostas; quatro segundos de despertar vêm do roteiro.

## Armas

O estado de munição pertence ao jogador, não ao ScriptableObject. Trocar entre armas já equipadas conserva munição. Recarga é cancelada por troca, morte ou menu; não há reposição oculta na troca.

- Cadência é compartilhada entre botões. Dispersão cresce por disparo e recupera; movimento e supressão aumentam o cone.
- FAMAS executa três balas sequenciais. Um cartucho armado pertence à rajada inteira.
- TriShot usa 2+2 cargas e três bagos por mão. RMB usa uma mão disponível se a outra estiver vazia. MPX usa 20+20 e duplo com dispersão adicional de 35%.
- Lust & Greed usa mãos independentes, reserva de 21 e recarga da mão menos carregada. Doze tiros sem recarga liberam Punishment. O trunfo retorna a 6+6 sem debitar reserva; R usa reserva.
- Choke concentra ao segurar LMB e dispara ao soltar. Minigun exige aquecimento contínuo e perde aquecimento ao soltar.
- Espada modifica a corrida, foice alcança múltiplos alvos do arco com um golpe por alvo, martelo multiplica dano somente contra barreiras.
- Projéteis avançam por tempo e consultam todo o segmento percorrido no frame, respeitando cobertura e alcance. Ghost não renderiza o projétil; armas silenciadas usam som procedural reduzido.
- Snipers usam RMB para reduzir o campo de visão. Não há ainda animação de luneta ou ADS final.

`effectiveRange` é um limite de viagem, sem queda gradual de dano. Danos são por bala/bago/golpe. `magazineSize` soma as duas mãos; `magazinePerHand` é a capacidade individual. `TimeBetweenShots` vem do RPM. Os GUIDs e campos originais foram preservados para os assets existentes.

## S.A.A. e efeitos

Primárias aceitam três slots compatíveis com sua família. Cada compra repõe três cargas no slot escolhido. Trocar de arma mantém os slots e suas cargas, mas impede o uso dos incompatíveis. Troca, morte, recarga e menu cancelam o preparo sem gastar carga.

Gatilho vazio ou minigun fria não gasta magia. Um disparo válido consome uma carga, inclusive quando erra. Efeitos em `Caster` ocorrem no disparo; `Impact` usa um objeto `ArcaneCast` compartilhado por todos os bagos/mãos/balas, aplicando somente no primeiro impacto em alcance. Impacto além do alcance do cartucho não gera efeito.

| Efeito | Execução local |
|---|---|
| Smoke | Volume opaco sem colisão; overlay quando a câmera está dentro e bloqueio de visão dos alvos que atiram |
| Slow | Multiplicador de movimento; `persistentArea` diferencia zona de pulso, independentemente da duração |
| Barrier | Parede destrutível com vida, largura, altura, profundidade e expiração do asset; recusa sobreposição e proximidade dos pontos protegidos |
| Reveal | Ping da última posição observada, visível ao time do conjurador; não acompanha o alvo |
| SpeedBoost | Bônus temporário de movimento no alvo configurado |
| Heal | Vida real até 100, somente vivos; não recupera escudo |
| Blind | Overlay temporário no jogador voltado para o pulso; alvos automatizados interrompem fogo |
| Suppression | Aumenta dispersão enquanto ativa; `persistentArea` seleciona zona ou pulso |

Pulsos e zonas validam time, raio, vida e cobertura sólida. Intensidades do mesmo tipo não somam: prevalece a maior fonte ainda ativa. O fim da rodada limpa efeitos, projéteis e pings. Cura é instantânea; não é revertida ao terminar a rodada. Os 25 cartuchos usam combinações desses oito efeitos, com dano elemental adicional zero.

As barreiras da sala de treino são recusadas perto do spawn e de A/B, além de sobreposições com atores/geometria. Não há análise topológica de rotas em mapas arbitrários. Integrar uma cena diferente exige preencher `ArcaneWorld.ProtectedPositions` e validar suas rotas. A arena aberta de treino não depende de uma única porta de saída.

## Verificações

`Tools/validate_arsenal.py` confere catálogo, fichas, schemas, referências, simetria e limites numéricos. `A3CVerification` executa as regras de munição, coletes, times, cura e objetivo dentro do editor. `RuntimeVerification` usa o executável e verifica projéteis entre frames, hitboxes, cobertura, rajada, cancelamento de recarga, pellets, cargas dos 25 cartuchos, barreiras, cura e expiração de fumaça.

Relatórios esperados: `Logs/a3c-editor-verification.json` e `Logs/a3c-runtime-verification.json`. O estado real da última execução e eventuais falhas constam no `STATUS.json`; a existência de um teste no código não significa que ele passou.

## Limites desta versão

É um protótipo local de treino. Não implementa rede 5v5, servidor autoritativo, matchmaking, economia entre rounds, compra por região/fase nem bots táticos de equipe. O mapa original ainda precisa da passagem de arte, integração de rotas/objetivos e playtest descritos na revisão. A arena nova usa geometria, sons e efeitos procedurais para testar mecânicas; não substitui os modelos e animações finais das fichas.

Preservar arquivos `.meta` ao mover assets, conforme a [documentação Unity](https://docs.unity3d.com/6000.0/Documentation/Manual/AssetMetadata.html).
