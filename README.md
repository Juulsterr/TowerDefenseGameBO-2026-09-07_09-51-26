# 🛡️ Boomers vs. Memes — Tower Defense Game

> Een unieke arcade Tower Defense game gemaakt in Unity waarin boomers de heilige grond van Reddit verdedigen tegen oprukkende internetmemes!

---

## 🎮 Over het Spel

**Boomers vs. Memes** is een klassieke tower defense game met een eigenzinnige en humoristische insteek. 

In dit universum proberen de boomers de ultieme orde te bewaren: internetmemes mogen koste wat het kost **Reddit** (de eindstreep) niet bereiken! Als speler zet je strategisch verschillende torens neer op het speelveld om de golven van oprukkende memes te stoppen voordat ze de heilige grond van Reddit vervuilen.

---

## 💥 De Unieke Twist: Kamikaze Enemies

Waar traditionele tower defense games vaak erg statisch zijn, introduceert deze game een dynamische en destructieve mechanic: **Kamikaze-vijanden**.

* **Toren Target AI:** Sommige boomers negeren het normale loop-pad volledig en rennen rechtstreeks af op jouw dichtstbijzijnde toren.
* **Permanente Verwoesting:** Bij impact blazen ze zichzelf én de toren op. Dit laat een rokende puinhoop achter op de tegel.
* **Strategische Keuzes:** De verwoeste plek is permanent onbruikbaar, wat de speler dwingt om na te denken over slimme verdedigingslinies en het inzetten van goedkope "offer-torens".

---

## 🛠️ Technische Highlights & Uitdagingen

- 🧬 **Custom AI & Overerving (OOP):**  
  De kamikaze-vijanden maken gebruik van object-georiënteerd programmeren. Door overerving vanuit het basis `Enemy`-script zoeken ze zelfstandig torens op en schakelen ze zichzelf uit **zonder** dat de `WaveManager`-logica of Go-knop breekt.
  
- 🎨 **Sprite Transformatie & Effecten:**  
  Zodra een toren geëxplodeerd is, schakelt de logica om en verandert de toren direct in een zelfgemaakte puin-sprite (getekend in *LibreSprite*) gecombineerd met een visueel particle-effect.
  
- ⚡ **Minimalistisch & Direct:**  
  Om de focus puur op de core-gameplay en mechanics te houden, is de game opgeleverd als een snelle, direct speelbare `.exe` build.

---

## ⚠️ Bekende Punten (Tijdgebrek / Scope)

* **UI & Menu's:** Vanwege de strakke deadline ontbreken er momenteel win-/verliesschermen en een quit-menu (afsluiten kan via `Alt + F4`).
* **Boss Health Bar:** De eindbaas heeft een grote hoeveelheid HP, maar dit wordt momenteel niet visueel weergegeven via een UI-balk.

---

### 🕹️ Hoe te spelen
1. Download en pak het `.zip` bestand van de nieuwste release uit.
2. Start `TowerDefenseGameBO.exe`.
3. Plaats je torens en verdedig Reddit tegen de meme-invasie!