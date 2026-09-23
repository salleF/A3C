# A3C: Academia de Combate Contra Calamidades

Protótipo local de FPS magitech inspirado em Crushle. O catálogo contém 20 armas equipáveis, o trunfo Punishment e 25 cartuchos arcanos configuráveis.

## Jogar

Abra o projeto no Unity indicado em `ProjectSettings/ProjectVersion.txt`. Use **A3C > Configurar cena de treino** e inicie Play em `Assets/Scenes/A3C_Training.unity`.

O menu permite escolher arma, cartuchos Q/E/C e colete, além de iniciar os exercícios de ataque ou defesa. B/Esc abre o equipamento e pausa. WASD move, Shift corre, Espaço salta, LMB dispara, RMB usa mira ou a segunda mão, R recarrega. Segure E perto do objetivo para plantar/despertar. F5 reinicia o exercício.

Para criar o executável Windows, use **A3C > Validar e gerar build Windows**. Saída: `Builds/WindowsRelease/A3C.exe`.

## Documentação e validação

- [Guia de execução, controles, contratos e limites](Assets/Documentation/Arsenal/Unity_Integration.md)
- [Armas, magias e fichas individuais](Assets/Documentation/Arsenal/README.md)
- [Revisão do código, mapa e modelos originais](Assets/Documentation/A3C_Project_Review.md)
- [Lore de Crushle](Assets/Documentation/A3C_Lore_Crushle.md)
- [Regras originais](Core_gameplay.txt)
- [Estado do trabalho e evidências](STATUS.json)

Validação estática: `python Tools/validate_arsenal.py`. Importação, testes de regras, build e testes do executável: `Tools/unity-verify.ps1 -EditorPath <caminho-do-Unity.exe>`.

Esta versão fornece treino local com alvos e visuais procedurais. Rede 5v5, bots táticos, economia de partida, modelos/animações finais e a passagem de arte do mapa original ainda não estão concluídos.
