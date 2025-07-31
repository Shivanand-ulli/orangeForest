# Random Project
This is a 2D Unity game developed as a part of coding assessment.

Project Details
  **Engine:**     Unity 2021.3.45f1 LTS
  **Language:**         C#   
  **Platform:**         PC/Anroind/iOS
  **Genre:**            Puzzle/Casual
  **Render Pipeline:**  Built-in (2D)  
  **Version Control:**  GitHub

##  Project Overview

The game challenges players to flip and match pairs of cards. Each successful match increases the score, promoting engagement and memory-based gameplay. The design follows a minimal UI style with smooth transitions and visual feedback.

## Features

- Dynamic grid generation based on screen size  
- Card flip logic and match detection  
- Scoring, timer, and move tracking  
- Save/load functionality using `PlayerPrefs`  
- UI transitions and button interactions  
- Background music, SFX, and particle effects  
- Responsive design for multiple platforms  

## Folder Structure

- `Scripts/` – Core game logic (Grid, Card, GameManager, UIManager, etc.)  
- `Prefabs/` – Cards, UI elements, and reusable components  
- `Sprites/` – Card icons and flat UI visuals  
- `Scenes/` – Menu, Game, and End screen scenes  
- `Animations/` – Card and UI animations  
- `Materials/` – Basic visual materials  

## Technical Implementation

- **C# scripting** with clean OOP structure  
- **Coroutines** for smooth delays (e.g., unmatched flip-back)  
- **Events & Delegates** to separate UI and gameplay logic  
- **Singletons** for game-wide managers  
- **PlayerPrefs** used for saving game data  
- Optimized scripts with inline comments for clarity and maintainability  

## UI & Visuals

- Wireframes created to outline screen layout and UX flow  
- Flat and minimalistic UI design using solid colors and clean icons  
- Visual consistency across all game elements  
- Feedback elements like sound and particle effects for engagement  

## Audio & Effects

- Card flip and match animations  
- Background music loop  
- SFX for interactions and matches  
- Particle effect for successful matches  

## Summary

This project showcases core Unity 2D skills, clean code practices, and a focus on usability and performance. It demonstrates the ability to plan, build, and polish a complete game loop with save/load functionality and responsive visuals.

---

*Note: This project is for educational and assessment purposes.*