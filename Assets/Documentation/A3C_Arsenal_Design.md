# A3C — Design de Arsenal: Armas & Sistema Arcano (S.A.A.)

> Documento de design vivo. Última atualização: Set/2026.

---

## 1. Economia de Combate

| Recurso | Valor |
|---|---|
| Vida base | 100 HP |
| Escudo (Colete Leve) | +50 → total 150 HP |
| Escudo (Colete Energético) | +50 regenerável → total 150 HP |
| Escudo (Colete Pesado) | +100 → total 200 HP |
| Escudo (Colete Construtivo) | 0 inicial, +20 por abate → máx ~100 |

**Regra de ouro:** Escudo absorve primeiro, vida depois. Dano nunca mistura as duas barras.

**Hitkills possíveis (0 → 200 HP instantâneo):**
1. HS de AWP (350+ dmg)
2. HS da Lust & Greed — trunfo *Punishment* (666 dmg)

---

## 2. Tabela de Armas

### 2.1 Armas Iniciais (Gratuitas)

| Nome | Categoria | Dano Corpo | Dano HS | RPM | Pente | CR | Mecânica Especial |
|---|---|---|---|---|---|---|---|
| Classic | Pistola | 26 | 78 | 160 | 12+36 | **GRÁTIS** | Semi-auto, versão Magitech |
| Varinha Rústica | Projétil Arcano | 26 | 78 | 160 | 12+36 | **GRÁTIS** | Disparo ativado por estalar de dedos; visual mágico |

> Simetria intencional: Classic = tecnologia, Varinha = magia. Mesmo kit de estatísticas, identidade visual oposta.

---

### 2.2 Pistolas Secundárias

| Nome | Categoria | Dano Corpo | Dano HS | RPM | Pente | CR | Mecânica Especial |
|---|---|---|---|---|---|---|---|
| Ghost | Pistola | 26 | 78 | 160 | 15+45 | 500 | Silenciada, sem traçante visível |
| TriShot Arcano | Pistola Akimbo | 23×2 | 55×2 | 200 | 4+4 (×2) | 800 | LMB alterna pistola E/D; RMB dispara duplo simultâneo |
| Lust & Greed | Dual Pistols | 34 | 102 | 120 | 6+6 | 900 | **Trunfo Punishment:** após 6 acertos consecutivos → próximo HS = 666 dmg (hitkill) |

#### Detalhe — TriShot Arcano
- Sem sprint para manter precisão akimbo
- LMB (botão esq): alterna tiro esq→dir→esq…
- RMB (botão dir): dispara ambas ao mesmo tempo (–1 bala de cada)
- Spread aumenta ao usar RMB consecutivo

#### Detalhe — Lust & Greed
- **Lust** (esq): dano 6 balas acumulativo, incrementa contador *Punishment*
- **Greed** (dir): usa o contador — se chegou a 6 → next HS = 666 (Punishment reset)
- Falhar HS com Punishment ativo **não** reseta o trunfo; só um tiro sem HS reseta
- *(TODO: decidir se Punishment tem cooldown entre usos ou apenas por pente)*

---

### 2.3 Melee

| Nome | Categoria | Dano | CR | Mecânica Especial |
|---|---|---|---|---|
| Espada Rúnica | Espada | 50 corpo / 120 HS | 400 | +10% velocidade de movimento enquanto equipada |
| Foice de Ceifador | Foice | 45 corpo / 110 HS | 550 | Arco horizontal amplo; pode acertar múltiplos alvos |
| Martelo de Forja | Martelo | 60 corpo / 130 HS | 700 | +50% dano a barreiras e objetos de cenário |

> *(TODO: Melee usa animação de swing, não projétil. WeaponController precisará de modo Melee com RaycastSphere ao invés de SpawnProjectile)*

---

### 2.4 SMGs (Submetralhadoras)

| Nome | Categoria | Dano Corpo | Dano HS | RPM | Pente | CR | Mecânica Especial |
|---|---|---|---|---|---|---|---|
| P90 Rúnica | SMG | 22 | 38 | 800 | 50+100 | 1050 | Pente cilíndrico de cristal, cadência mais alta |
| Thompson Arcana | SMG | 27 | 47 | 545 | 30+90 | 850 | Estilo anos 40, som grave mágico |
| Dual Hand Sig MPX | SMG Akimbo | 18×2 | 36×2 | 600 | 20+20 | 1100 | Duas MPX; LMB alterna, RMB duplo (alto recoil) |

---

### 2.5 Shotguns (Escopetas)

| Nome | Categoria | Dano/Pellet | Pellets | RPM | Pente | CR | Mecânica Especial |
|---|---|---|---|---|---|---|---|
| Sawed-Off Rúnica | Shotgun | 24 | 8 | 200 | 5+10 | 850 | Dois canos, disparo duplo com RMB |
| Lever-Action Arcana | Shotgun | 34 | 6 | 140 | 5+15 | 1050 | **Choke:** segurar LMB concentra pellets (+dmg, –spread); soltar dispara |

> *(Lever-Action é inspirado no Peacekeeper de Apex. Choke é mecânica de alta skill-floor.)*

---

### 2.6 Rifles de Assalto

| Nome | Categoria | Dano Corpo | Dano HS | RPM | Pente | CR | Mecânica Especial |
|---|---|---|---|---|---|---|---|
| FAMAS Arcano | Rifle (Burst) | 35 | 86 | 500 burst (3 balas) | 25+75 | 1650 | Rajada de 3; terceira bala do burst tem –5 dano |
| Vandal Rúnico (AK) | Rifle Full-Auto | 40 | 160 | 495 | 25+75 | 2900 | Alto recoil; **160 HS** não hitkill com colete cheio |
| Phantom Arcano (M4) | Rifle Full-Auto | 39 | 156 | 550 | 30+90 | 2900 | Silenciado; –10% spread em movimento |
| Guardian Arcano | Rifle Semi-Auto | 65 | 195 | 195 | 12+36 | 2250 | Semi-auto preciso; funciona como DMR |

> **Regra do HS de Rifle (160 dmg):** Jogador com Colete Pesado (200 HP total) sobrevive com 40 HP. Jogador sem colete (100 HP) morre.

---

### 2.7 Snipers

| Nome | Categoria | Dano Corpo | Dano HS | RPM | Pente | CR | Mecânica Especial |
|---|---|---|---|---|---|---|---|
| Scout Leve | Sniper Semi-Auto | 80 | 160 | 100 | 8+24 | 2050 | Leve, pode mover com scope; HS = 160 não-hitkill |
| AWP Pesada | Sniper Bolt-Action | 190 | 350+ | 40 | 5+15 | 4700 | **190 corpo** deixa colete cheio com 10 HP; **HS = hitkill** |

> AWP é a arma mais cara do jogo. Proibida de entrar no S.A.A. para manter equilíbrio *(TODO: confirmar esta decisão)*.

---

### 2.8 Arma Pesada

| Nome | Categoria | Dano Corpo | Dano HS | RPM | Pente | CR | Mecânica Especial |
|---|---|---|---|---|---|---|---|
| Minigun a Vapor Arcana | LMG | 18 | 28 | 1200 (warm-up 1s) | 100+0 | 3500 | Precisa de 1s para atingir velocidade máxima; –70% velocidade ao segurar |

---

## 3. Coletes (Revisão)

| Colete | Shield | Especial | CR |
|---|---|---|---|
| Leve | 50 estático | Nada | 400 |
| Energético | 50 | Regenera após 6s fora de combate | 750 |
| Pesado | 100 imediatos | Nada | 1000 |
| Construtivo | 0 inicial | +20 shield por abate (máx ~100) | 650 |

---

## 4. S.A.A. — Sistema de Arsenal Arcano

### 4.1 Conceito

Cada arma pode receber **cartuchos arcanos** que adicionam um efeito elemental ao projétil. Os cartuchos são equipados antes da partida nos **3 slots de habilidade** (teclas Q, E, C). Ao usar um slot, o efeito é aplicado no próximo grupo de tiros até o cartucho acabar.

**Regra:** Os cartuchos NÃO mudam o dano base da arma. Eles adicionam efeito de status, área ou utility por cima.

---

### 4.2 Os 5 Elementos

| Ícone | Elemento | Cor | Identidade |
|---|---|---|---|
| 🔥 | **Fogo** | Vermelho/Laranja | Dano ao longo do tempo, zona de controle |
| 💧 | **Água** | Azul/Ciano | Lentidão, câmara de gelo |
| 🌱 | **Terra** | Verde/Marrom | Barreiras, bloqueio de passagem |
| ⚡ | **Relâmpago** | Amarelo/Roxo | Velocidade, stun, chain damage |
| ☯️ | **Luz & Trevas** | Branco/Preto | Cura aliados / enfraquece inimigos |

---

### 4.3 Classificação de Arma × Papel Tático

| Categoria de Arma | Papel Primário | Papel Secundário |
|---|---|---|
| SMG | Avanço | Controle |
| Shotgun | Controle | Bloqueio |
| Rifle de Assalto | Bloqueio | Observação |
| Sniper | Observação | Suporte |
| LMG / Minigun | Controle | Bloqueio |

---

### 4.4 Mapa de Combinações (25 combinações — TODO completo)

> Cada célula descreve o efeito do cartucho elemental naquele tipo de arma.

| | **SMG** | **Shotgun** | **Rifle** | **Sniper** | **LMG** |
|---|---|---|---|---|---|
| 🔥 **Fogo** | Projéteis deixam rastro de fogo breve; aliados evitam a área | Disparo cria nuvem de fogo 1s no impacto | Bala incendeia alvo por 2s (+8 dmg/s) | Bala explode em chama na parede, bloqueando linha de visão | Rajada sustentada cria zona de fogo no chão |
| 💧 **Água** | Tiros molham o alvo; próximo tiro de qualquer aliado congela (slow 40%) | Cone d'água que empurra inimigos para trás | Bala slow 20% por 3s | Projétil deixa trilha de gelo no chão (slip + slow) | Corrente d'água contínua; inimigos empurrados continuamente |
| 🌱 **Terra** | Tiro finca cristal no chão; cria cobertura pequena temporária | Pellets criam murete de pedra de 1m por 3s | Projétil fixa inimigo no lugar por 0.5s (raiz) | Após impacto, coluna de pedra surge no ponto (+bloqueio) | Rajada cria parede de terra empurrável |
| ⚡ **Relâmpago** | Tiros carregam alvo; 3 hits = descarga que atinge aliados próximos (chain) | Descarga elétrica em cone; stun 0.3s | Bala ricocheia em até 2 superfícies antes de acertar | Projétil teleporta cópia em linha reta até 2° alvo atrás do 1° | Rajada com chain: cada inimigo atingido passa dano para o mais próximo |
| ☯️ **Luz & Trevas** | Tiros de Luz curam 5 HP em aliados; Trevas reduzem 10 shield do inimigo ao acertar | Luz: cone de cura em área; Trevas: inimigos na área ficam cegos 0.5s | Luz: bala revela inimigos através de parede (ping); Trevas: dano ignora escudo | Luz: revela todos os inimigos no mapa por 2s; Trevas: HS remove 50 shield extra | Luz: aura de regeneração para aliados próximos; Trevas: zona que drena shield inimigo |

---

### 4.5 Slots de Cartucho (Q / E / C)

| Slot | Tecla | Cargas por uso | Recarga |
|---|---|---|---|
| Slot 1 | Q | 3 tiros com efeito | Não regenera em partida — comprado antes |
| Slot 2 | E | 3 tiros com efeito | Não regenera em partida — comprado antes |
| Slot 3 | C | 3 tiros com efeito | Não regenera em partida — comprado antes |

**Regra de balanceamento:** Um slot só pode ter 1 elemento. Você pode equipar o mesmo elemento em dois slots (para ter 6 cargas). Cartuchos são recursos escassos — usá-los em momentos errados é um erro tático.

**Raridade / Custo sugerido (TODO: finalizar):**

| Raridade | Elementos | CR estimado |
|---|---|---|
| Comum | Fogo, Água | 200 CR |
| Incomum | Terra, Relâmpago | 350 CR |
| Raro | Luz & Trevas | 600 CR |

---

## 5. O que Falta Implementar (TODO)

### Design (documento)
- [ ] Confirmar se AWP proibida de receber cartucho S.A.A.
- [ ] Decidir comportamento de Punishment (Lust & Greed): reseta por pente ou por tiro sem HS?
- [ ] Definir custo de CR de cada arma (valores acima são estimativas)
- [ ] Revisar 25 combinações da tabela S.A.A. — equilibrar utilidade vs. dano
- [ ] Criar fichas individuais de cada arma (reference sheet para artistas)
- [ ] Definir quais armas iniciam disponíveis e quais são desbloqueadas

### Código Unity
- [ ] Adicionar `FireMode.Melee` no `WeaponData.cs` com `RaycastSphere` ao invés de projétil
- [ ] Criar `SAASystem.cs` — gerencia os 3 slots de cartucho e aplica efeito ao projétil
- [ ] Adicionar campo `ElementType` ao `Projectile.cs` para receber efeito do S.A.A.
- [ ] Implementar efeitos de status: `OnFire`, `Slowed`, `Rooted`, `Stunned`, `Revealed`
- [ ] Criar `StatusEffect.cs` — componente que aplica e remove efeitos de status em `HealthSystem`
- [ ] Implementar `BurstFire` corretamente no `WeaponController.cs` (FAMAS)
- [ ] Implementar `Choke` para a Lever-Action (hold LMB concentra spread)
- [ ] Implementar `PunishmentTracker` para Lust & Greed
- [ ] Testar melee hit detection com animação de swing

### Arte / Modelos
- [ ] Modelo 3D da Classic (pistola estilo magitech)
- [ ] Modelo 3D da Varinha Rústica
- [ ] Modelos das armas secundárias (Ghost, TriShot, Lust & Greed)
- [ ] Partículas de muzzle flash por elemento (fogo = laranja, água = azul, relâmpago = amarelo)
- [ ] Efeito visual de cartucho ativo no HUD (indicador Q/E/C)

---

*Documento criado por A3C Dev. Para lore do mundo e mapa Crushle, ver `A3C_Lore_Crushle.md`.*
