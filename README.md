<p align="center"><img img width="900" height="564" src="https://github.com/user-attachments/assets/af32106d-38c3-415a-9b44-81e0c9be9213" xalign="mid"></p>
<!---
https://github.com/user-attachments/assets/af32106d-38c3-415a-9b44-81e0c9be9213
https://github.com/user-attachments/assets/ceceaae4-070b-4ca7-8d82-cf24c2b5ca50
--->

<br>

## About
Magic Magistry is a spellcasting survival game. Combine different spell components to fight off the horde of enemies and survive for as long as you can.

## Controls

WASD/Arrows to move

L = Left Click

R = Right Click

Space = Roll

Combine up to 3 elements together, then do RL to cast

RR to cancel spell

##  Scripts

|  Scripts | Description |
| --- | --- |
| `PlayerMovement.cs` | Player WASD movement|
| `PlayerDash.cs` | Player Dash script|
| `SpellChecker.cs`| Detects player's left and right click and show the components that are currently made|
| `SpellCaster.cs` | Takes calls from `SpellChecker.cs` for executing all the different spells and their combination|
| `Spells`| The spells folder has all the scripts for all the different spells. It usually had data about damage, effects (like slowing down enemies), timer function, OnHit, etc|
|`Enemy.cs`| This script is the base for all the other enemies. It has functions about movement, taking damage, etc. 
|`Enemy`| The folder has all the different enemy scripts that inherits `Enemy.cs`. Each enemy has each their own ways to deal damage to the player and making it inherit the enemy class is easier to implement for additional enemies in the future|
|`FollowPlayer.cs`| Script for camera to follow player|

## Contributors
Steven Wijaya (Me) - Game Programmer

Assets Used:

https://bdragon1727.itch.io/free-effect-bullet-impact-explosion-32x32

https://enjl.itch.io/tileset-top-down-dungeon

https://www.dafont.com/pixelsix.font 

https://xenophero.itch.io/rock-sprites 

https://pixabay.com/sound-effects/swish-swoosh-woosh-sfx-17-357183/

https://pixabay.com/sound-effects/trinkets-69217/ 
