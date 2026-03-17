# Bounce3D

Bounce3D is a 3D platformer developed in Unity. The player controls a rolling ball, navigates through obstacles, and aims to reach the finish point in the shortest possible time while achieving the highest star rating.

---

## Features

- Time-based gameplay system  
- Star rating based on completion time  
- Interactive obstacles (spikes, saws, etc.) with sound effects  
- Audio system with background music and positional sounds  
- Multiple control modes (joystick and button-based)  
- Leaderboard system using FastAPI and SQLite  
- Checkpoint and respawn mechanics  

---

## Controls

- Joystick mode: movement via on-screen joystick  
- Button mode: movement via directional buttons  

Control mode can be selected in the main menu.

---

## Game Logic

Star rating is determined by completion time:

- Less than 2 minutes: 3 stars  
- Less than 3 minutes: 2 stars  
- Less than 4 minutes: 1 star  
- 4 minutes or more: 0 stars  

Additional behavior:

- The player respawns at the last checkpoint after failure  
- Obstacles reset after respawn  
- Animations and audio are re-triggered on reset  

---

## Audio System

- Separate background music per scene  
- Positional sound effects based on player proximity  
- Audio triggers for:
  - Player death  
  - Landing  
  - Teleportation and respawn  

---

## Project Structure

```

Assets/
├── Scripts/
├── Prefabs/
├── Scenes/
├── Audio/
├── Materials/
├── UI/

````

---

## Leaderboard API

The backend is implemented using FastAPI and SQLite.

---

## Setup

1. Open the project in Unity
2. Load the main menu scene
3. Start the game

To enable the leaderboard:

1. Run the backend server
2. Ensure the game is configured to use the correct API endpoint

---

## Requirements

* Unity (recommended version 2021 or newer)
* Python 3.10 or newer
* FastAPI
* Uvicorn

---

## Author

Aleksandr Sokolov, FAMNIT, 2026.

---

## License

This project is intended for educational and personal use.
