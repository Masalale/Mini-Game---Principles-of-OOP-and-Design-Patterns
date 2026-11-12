# Crypt Crawler

A one-screen 2D dungeon maze in Unity: grab every potion before the ghosts catch you, then try to beat your best time.

## Features

- **Ghosts that hunt you**: the green ghost always takes the shortest route to you; the white ones wander the corridors.
- **It gets harder**: a new wandering ghost appears every 3 potions.
- **Potion tracker**: a row of potion icons lights up as you collect them, next to a running timer.
- **Best times**: your 3 fastest escapes are saved between plays and shown on the end screen.
- **Smooth corridors**: the knight lines up with gaps, so he never snags on wall corners.

## Quick Start

1. Clone the repository:
   ```bash
   git clone https://github.com/Masalale/Mini-Game---Principles-of-OOP-and-Design-Patterns.git
   ```
2. Open the folder in Unity 6000.3.23f1.
3. Open `Assets/Scenes/Game.unity`, press **Play**, and move with `WASD` or the arrow keys. Collect all 10 potions without touching a ghost.

## Structure

- `Assets/Scripts/GameManager.cs` : potions, timer, win and loss
- `Assets/Scripts/EnemyFactory.cs` : spawns the ghosts
- `Assets/Scripts/Enemy.cs`, `ChaserEnemy.cs`, `WandererEnemy.cs` : how the ghosts move
- `Assets/Scripts/GridPathfinder.cs` : shortest route through the maze
- `Assets/Scripts/Leaderboard.cs` : sorts and saves the best times
- `Assets/Scripts/PlayerController.cs` : knight movement
- `Assets/Scripts/ITouchable.cs`, `Potion.cs` : things the knight can touch
- `Assets/Scripts/HUD.cs` : potion icons, timer and end screen
- `Assets/Scenes/Game.unity` : the maze, player and UI

## Credits

- Sprites: [Tiny Dungeon](https://kenney.nl/assets/tiny-dungeon) by Kenney (CC0)
- Font: Kenney Mini from [Kenney Fonts](https://kenney.nl/assets/kenney-fonts) by Kenney (CC0)
