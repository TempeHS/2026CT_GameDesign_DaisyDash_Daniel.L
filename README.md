# Unity Game Development Summary

| Field | Detail |
|---|---|
| **Game Title** | Daisy Dash |
| **Student Name(s)** | Daniel L |
| **Class / Course** | Computer Technology |
| **Repository** | https://github.com/TempeHS/2026CT_GameDesign_DaisyDash_Daniel.L |
| **Unity Version** | 6000.0.58f1 |
| **Document Version** | 0.2 |
| **Date** | 27/08/2026 |

---

## Table of Contents
1. [Game Overview](#1-game-overview)
2. [Video Walkthrough](#2-video-walkthrough)
3. [Game Mechanics](#3-game-mechanics)
4. [Visual Features](#4-visual-features)
5. [Audio Design](#5-audio-design)
6. [User Interface & HUD](#6-user-interface--hud)
7. [Scene & Level Design](#7-scene--level-design)
8. [Scripts & Programming](#8-scripts--programming)
9. [Development Techniques & Tutorials Acknowledged](#9-development-techniques--tutorials-acknowledged)
10. [Third-Party Content Acknowledgements](#10-third-party-content-acknowledgements)
11. [Challenges & Solutions](#11-challenges--solutions)
12. [Branch Development Summary](#12-branch-development-summary)

---

## 1. Game Overview

### 1.1 Genre
2D platformer

### 1.2 Target Audience
Players who enjoy fast-paced 2D platforming, hidden routes, and movement challenges.

### 1.3 Game Summary
Daisy Dash is a 2D platformer in which the player navigates platforming levels using running, jumping, wall sliding, wall jumping, and dashing. A mouse-controlled flashlight reveals hidden blocks and routes, while checkpoints prevent the player from being sent back to the start. Players must avoid hazard blocks and reach the finish line as quickly as possible.

### 1.4 Win / Loss Conditions
| Condition | Description |
|---|---|
| Win | Reach the finish line. The timer stops and the next scene loads when another level is available. |
| Loss | Touch a hazard or fall into the void. The player respawns at the latest checkpoint. |

### 1.5 Platform & Build Settings
| Setting | Detail |
|---|---|
| Target Platform | PC |
| Resolution | 1980x1080 |
| Build Type | Windows x64 |

---

## 2. Video Walkthrough

### 2.1 Full Gameplay Walkthrough

<!--
  Embed a YouTube/Vimeo video or link to a file in the repository.
  YouTube embed syntax:
  [![Video Title](https://img.youtube.com/vi/VIDEO_ID/0.jpg)](https://www.youtube.com/watch?v=VIDEO_ID)

  OR link to a local file:
  [Watch Walkthrough Video](./docs/video/walkthrough.mp4)
-->

| Field | Detail |
|---|---|
| **Video Title** | Daisy Dash: Tutorial and Gameplay Walkthrough |
| **Link / Embed** | [Watch the gameplay walkthrough](docs/videos/Game%20playthrough.mp4) |
| **Duration** | 1:38 |
| **Description** | This walkthrough begins at the main menu and demonstrates the tutorial level, including movement, jumping, flashlight-revealed blocks, wall sliding, wall jumping, dashing, one-way platforms, and the pause menu. It also shows the hazard and respawn system before finishing the tutorial by reaching the green triangle. |

### 2.2 Feature Highlight Clips

| Clip | Description | Link |
|---|---|---|
| Flashlight reveal | Shows the flashlight revealing hidden blocks and routes. | [Watch flashlight clip](docs/videos/flashlight.mp4) |
| Dash and one-way platforms | Demonstrates dashing and using one-way platforms. | [Watch dash and platform clip](docs/videos/Dashing%20and%20one%20way%20platform.mp4) |
| Pause menu | Shows the pause menu and its controls. | [Watch pause menu clip](docs/videos/Pause%20menu.mp4) |

---

## 3. Game Mechanics

### 3.1 Core Mechanics
| ID | Mechanic | Description | Implemented In (Script/Object) |
|---|---|---|---|
| M-1 | Player movement | Controls running, jumping, wall movement, and dashing through each level. | `PlayerMovement.cs` |
| M-2 | Flashlight | The flashlight can reveal hidden blocks and paths | `FlashlightControls.cs` & `FlashlightReveal.cs` |
| M-3 | Checkpoints system | Automatically puts a player back at their last checkpoint after dying. | `Checkpoint.cs`|
| M-4 | Level timer | Starts a timer when starting a level and stops timer at end | `TimerManager.cs` |
| M-5 | Finish line/Level completion | Reaching finish line completes level and loads next scene | `FinishLine.cs` |
| M-6 | Wall movement | Lets the player slide down walls and jump away from them. | `PlayerMovement.cs` |
| M-7 | Dashing | Gives the player a short burst of movement in a chosen direction. | `PlayerMovement.cs` |
| M-8 | Hazard and respawn system | Plays a death sound and returns the player to the latest checkpoint. | `HazardBlock.cs` & `RespawnManager.cs` |
| M-9 | Pause menu | Pauses gameplay and provides Resume, Restart, and Main Menu options. | `PauseMenu.cs` |
| M-10 | Tutorial prompts | Displays context-specific instructions during the tutorial. | `TutorialPrompt.cs` |
| M-11 | One-way platforms | Allows the player to jump through platforms from below and land on them from above. | `One way.prefab` |

### 3.2 Player Controls
| Action | Input (Keyboard / Controller) | Description |
|---|---|---|
| Walk | A & D | Left and right movement |
| Jump | Space | Allows player to jump |
| Dash | Shift | Gives player a quick speed boost |
| Wall slide / wall jump | Automatic / Space | Slowly slides down walls and jumps away from them |
| Flashlight | Left mouse button | Reveals hidden blocks and routes |
| Pause menu | Escape | Opens or closes the pause menu |

### 3.3 Physics & Collision
| Feature | Description |
|---|---|
| Rigidbody Physics | Uses `Rigidbody 2D` and `Box Collider 2D` for player's movement, gravity calculation and collision. |
| Platform Collisions | Uses `Box Collider 2D` to prevent player from falling through floor. |
| Hazard Collisions | Uses `Box Collider 2D` with `Is Trigger` enabled to detect player overlap. |

### 3.4 Game Loop
| Stage | Description |
|---|---|
| Start / Initialisation | After clicking Play on the main menu, the first level is loaded. |
| Core Loop | The player moves through the level and avoids hazards while the timer runs. |
| Win / End State | Reaching end stops timer and loads the next scene. |
| Restart | After touching a hazard, the player respawns at the latest checkpoint. The restart script does not currently work. |

### 3.5 Scoring & Progression
| Element | Description |
|---|---|
| Scoring System | No points are awarded and the timer measures the player’s performance. |
| Difficulty Progression | Each level becomes progressively more difficult. |
| Unlockables / Levels | No unlock system is currently implemented. Completing a level loads the next scene when another level is available. |

---

## 4. Visual Features

### 4.1 Particle Effects

| Effect Name | Purpose | Screenshot |
|---|---|---|
| None currently implemented | N/A | N/A |

> Add screenshot images using: `![Effect Name](./docs/screenshots/effect_name.png)`

---

### 4.2 Cut Scenes & Cinematics

| Cut Scene | Trigger | Description | Screenshot / Still |
|---|---|---|---|
| None currently implemented | N/A | N/A | N/A |

> Add screenshot images using: `![Cut Scene Name](./docs/screenshots/cutscene_name.png)`

---

### 4.3 Animations

| Animation | Object / Character | Description | Screenshot |
|---|---|---|---|
| Idle animation | Player | Plays while the player is standing still. | [View idle animation screenshot](docs/screenshots/player%20idle.png) |

> Add screenshot images using: `![Animation Name](./docs/screenshots/animation_name.png)`

---

### 4.4 Lighting & Post-Processing

| Feature | Description | Screenshot |
|---|---|---|
| Flashlight | Reveals hidden blocks and routes. | [View flashlight screenshot](docs/screenshots/flashlight.png) |

> Add screenshot images using: `![Feature Name](./docs/screenshots/lighting_name.png)`

---

### 4.5 Shaders & Materials

| Shader / Material | Applied To | Description | Screenshot |
|---|---|---|---|
| None currently implemented | N/A | N/A | N/A |

> Add screenshot images using: `![Shader Name](./docs/screenshots/shader_name.png)`

---

### 4.6 Additional Visual Screenshots

<!--
  Add any other notable screenshots here.
  Syntax: ![Description](./docs/screenshots/filename.png)
-->

| Description | Screenshot |
|---|---|
| Main menu | [View menu screenshot](docs/screenshots/menu.png) |

---

## 5. Audio Design

### 5.1 Music
| Track | Scene / Trigger | Source / Composer |
|---|---|---|
| None currently implemented | N/A | N/A |

### 5.2 Sound Effects
| Sound Effect | Trigger | Source |
|---|---|---|
| Jump sounds | Jumping and wall jumping | [OpenGameArt platformer jumping sounds](https://opengameart.org/content/platformer-jumping-sounds) |
| Dash whoosh | Dashing | [Pixabay woosh sound effect](https://pixabay.com/sound-effects/film-special-effects-woosh-230554/) |
| Hurt sound | Player death before respawning | [OpenGameArt 8-bit sound effects](https://opengameart.org/content/8-bit-sound-effects-2) |

### 5.3 Audio Implementation
| Feature | Description |
|---|---|
| Audio Mixer / Groups | No custom audio mixer groups are currently used. |
| Spatial / 3D Audio | Sound effects play at the player’s position using `AudioSource.PlayClipAtPoint`. |
| Dynamic Audio | Jump sounds are selected randomly from the available jump clips, while dash and hurt sounds play during their related actions. |

---

## 6. User Interface & HUD

### 6.1 HUD Elements
| Element | Purpose | Screenshot |
|---|---|---|
| Level timer | Displays the elapsed time during gameplay. | [View HUD screenshot](docs/screenshots/gameplay%20hud.png) |
| Tutorial popup | Gives the player instructions for learning the game mechanics. | [View tutorial popup screenshot](docs/screenshots/tutorial%20popup.png) |

> Add screenshot images using: `![HUD Element](./docs/screenshots/hud_name.png)`

### 6.2 Menus
| Menu | Purpose | Screenshot |
|---|---|---|
| Main Menu | Allows the player to start or exit the game. | [View menu screenshot](docs/screenshots/menu.png) |
| Pause Menu | Allows the player to resume, restart, or return to the main menu. | [View pause menu screenshot](docs/screenshots/pause%20menu.png) |

> Add screenshot images using: `![Menu Name](./docs/screenshots/menu_name.png)`

---

## 7. Scene & Level Design

### 7.1 Scene List
| Scene Name | Purpose | Description |
|---|---|---|
| Menu | Starting screen | Allows the player to start the game or exit. |
| Level 1 | Tutorial level | Introduces the player to the game’s movement and mechanics. |
| Level 2 | Second gameplay level | Work in progress. |

### 7.2 Level / Environment Screenshots
| Level / Area | Description | Screenshot |
|---|---|---|
| Level 1 tutorial | Introduces movement, jumping, flashlight blocks, wall movement, and dashing. | [View Level 1 screenshot](docs/screenshots/level%201.png) |
| Level 2 | Second gameplay level. Work in progress. | No screenshot since WIP |

> Add screenshot images using: `![Level Name](./docs/screenshots/level_name.png)`

### 7.3 Scene Management
| Feature | Description |
|---|---|
| Scene Loading Method | Uses `SceneManager.LoadScene` to load levels and return to the main menu. |
| Persistent Data Between Scenes | No persistent data is carried between scenes. |
| Scene Transition Effects | The start menu loads Level 1, and the finish line loads the next level without a transition animation. |

---

## 8. Scripts & Programming

### 8.1 Script Summary
| Script Name | Attached To | Responsibility |
| --- | --- | --- |
| **Checkpoint.cs** | Checkpoints | Stores & updates player respawn position when touched. |
| **FinishLine.cs** | Finish line | Detects level completion; stops timer and triggers next scene. |
| **FlashlightControls.cs** | Flashlight | Rotates flashlight toward mouse, keeps flashlight on player, handles on/off toggle. |
| **FlashlightReveal.cs** | Hidden blocks | Shows/hides hidden blocks when shone by flashlight. |
| **HazardBlock.cs** | Hazard blocks | Detects player collision with hazards and triggers respawn. |
| **PlayerMovement.cs** | Player | Handles player input, movement, jumping and physics. |
| **PauseMenu.cs** | Pause menu controller | Handles pausing, resuming, restarting, and returning to the main menu. |
| **RespawnManager.cs** | Player | Tracks current checkpoint and respawns player there after death or hazard collision. |
| **Restart.cs** | Not functional | Intended to restart level; currently does not work. |
| **StartMenuController.cs** | Start menu controller object | Controls start menu UI, button actions play/quit, and scene transitions. |
| **TimerManager.cs** | Timer object | Tracks and displays time passed; used for level timing and stops when reaching end. |
| **TutorialPrompt.cs** | Tutorial trigger areas | Displays tutorial messages when the player enters trigger areas. |

### 8.2 Key Algorithms / Logic
| Feature | Script | Description |
|---|---|---|
| Player movement and wall jumping | `PlayerMovement.cs` | Uses Rigidbody2D velocity, acceleration, collision contacts, and movement states to handle jumping, wall sliding, wall jumping, and dashing. |
| Flashlight block reveal | `FlashlightControls.cs` & `FlashlightReveal.cs` | Uses the flashlight direction, distance, angle, and raycasts to reveal block renderers and colliders. |
| Checkpoint respawning | `Checkpoint.cs` & `RespawnManager.cs` | Stores the latest checkpoint and returns the player there after a hazard or death sequence. |

### 8.3 Design Patterns Used
| Pattern | Where Applied | Justification |
|---|---|---|
| Reusable components | `TutorialPrompt.cs` and `FlashlightReveal.cs` | The same script can be attached to multiple objects, with different Inspector settings for each one. |

---

## 9. Development Techniques & Tutorials Acknowledged

> List every tutorial, course, video, or article that informed or guided your implementation. Include what you used it for and what you changed or adapted.

| # | Title | Author / Creator | URL / Source | What You Used It For | What You Changed / Adapted |
|---|---|---|---|---|---|
| 1 | Build Any 2D Top Down Game in Unity, Modular Template Tutorial (45+ Features) | Game Code Library | https://www.youtube.com/watch?v=HAVp6Z8b4xA | Original plan but was replaced, however animations from this video are still used. | Changed the actual game to a platformer after it got too complicated. |
| 2 | Time.timeScale | Unity | https://docs.unity3d.com/ScriptReference/Time-timeScale.html | Used to pause and resume gameplay by setting the time scale to 0 or 1. | Adapted it into a custom PauseMenu controller with Escape and UI buttons. |
| 3 | Pause Menu Tutorial | Brackeys | https://www.youtube.com/watch?v=JivuXdrIHK0 | Used as reference for the pause menu script and button setup. | Adapted the approach for this project’s Resume, restart, and main menu buttons. |
| 4 | How To Wall Slide & Wall Jump In Unity | bendux | https://www.youtube.com/watch?v=O6VX6Ro7EtA | Used as a reference for wall sliding and wall jumping. | Adapted the controls and physics for Daisy Dash. |
| 5 | Unity 2D CHECKPOINTS Tutorial | Rehope Games | https://www.youtube.com/watch?v=VE_bkPrrZdE | Used as a reference for checkpoint triggers and respawn positions. | Adapted the system to use RespawnManager and added delayed respawning with a death sound. |

---

## 10. Third-Party Content Acknowledgements

> All third-party assets (art, audio, fonts, scripts, packages) must be listed here with their licence. Using an asset without acknowledgement may constitute academic misconduct.

### 10.1 Visual Assets
| Asset Name | Type | Creator / Source | Licence | URL | Used For |
|---|---|---|---|---|---|
| None  | N/A | N/A | N/A | N/A | N/A |

### 10.2 Audio Assets
| Asset Name | Type | Creator / Source | Licence | URL | Used For |
|---|---|---|---|---|---|
| Platformer jumping sounds | Sound effects | dklon | CC BY 3.0 | https://opengameart.org/content/platformer-jumping-sounds | Jumping and wall jumping |
| Film special effects woosh | Sound effect | RibhavAgrawal | Pixabay Content License | https://pixabay.com/sound-effects/film-special-effects-woosh-230554/ | Dashing |
| 8-bit sound effects | Sound effects | hosch | CC BY-SA 4.0 | https://opengameart.org/content/8-bit-sound-effects-2 | Player death sound |

### 10.3 Scripts & Code Snippets
| Script / Snippet | Source | Licence | URL | Used For | Changes Made |
|---|---|---|---|---|---|
| None  | N/A | N/A | N/A | N/A | N/A |

### 10.4 Unity Packages & Plugins
| Package Name | Version | Source | Licence | URL | Purpose |
|---|---|---|---|---|---|
| Cinemachine | 3.1.7 | Unity Technologies | Unity Companion License | https://docs.unity3d.com/Packages/com.unity.cinemachine@3.1/manual/index.html | Camera tools |
| Input System | 1.14.2 | Unity Technologies | Unity Companion License | https://docs.unity3d.com/Packages/com.unity.inputsystem@1.14/manual/index.html | Input actions and controls |
| Universal Render Pipeline | 17.0.4 | Unity Technologies | Unity Companion License | https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@17.0/manual/index.html | 2D rendering and lighting |
| Unity UI | 2.0.0 | Unity Technologies | Unity Companion License | https://docs.unity3d.com/Packages/com.unity.ugui@2.0/manual/index.html | Menus and interface elements |

### 10.5 Fonts
| Font Name | Creator / Source | Licence | URL |
|---|---|---|---|
| TextMesh Pro default resources | Unity Technologies | Unity Companion License | https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.0/manual/index.html |

---

## 11. Challenges & Solutions

| # | Challenge Encountered | How It Was Solved |
|---|---|---|
| 1 | The built-in `IsGrounded` check was not resetting correctly | Replaced it with collision code for the 2D Rigidbody, grounding the player on floors and ungrounding them after jumping or leaving the floor. |
| 2 | The flashlight only checked the centre of hidden objects, so blocks were not revealed reliably. | Changed the reveal system to sample the object’s full bounds and check multiple points before enabling its renderer and collider. |
| 3 | Wall jumps sometimes became normal jumps because coyote-time jumping took priority. | Changed the jump logic so airborne wall contact is checked before normal coyote time jumping. |
| 4 | Revealing a block while the player was inside it could trap the player. | Just killed the player and respawned the player through the existing checkpoint system. |

---

## 12. Branch Development Summary

> One section per feature branch. Add or remove sections to match your repository. Branches should be named for the feature they implement e.g. `feature/player-movement`. Link each branch name directly to the branch in your GitHub repository.

---

### Branch 1 — `main`

| Field | Detail |
|---|---|
| **Branch Name** | `main` |
| **Purpose** | Stable, releasable version of the game |
| **Merged From** | N/A |
| **Final Commit** | N/A |

---

### Branch 2 — `feature/camera`

| Field | Detail |
|---|---|
| **Branch Name** | `origin/Camera` |
| **Feature Developed** | Camera movement and lighting |
| **Merged Into** | `main` |
| **Date Started** | Unknown |
| **Date Merged** | 2026-06-11 |

#### What Was Built
Added camera movement and lighting.

#### Key Commits
| Commit Message | What Changed |
|---|---|
| `Added camera movement lighting` | Final branch commit on 2026-06-11. |
| | |
| | |

#### Problems Encountered & Resolved
| Problem | Resolution |
|---|---|
| | |
| | |

#### Screenshot / Evidence
<!-- Add a screenshot of the feature working -->
> `![Feature Name](./docs/screenshots/branch_feature_name.png)`

---

### Branch 3 — `feature/levels`

| Field | Detail |
|---|---|
| **Branch Name** | `origin/Levels` |
| **Feature Developed** | Level development |
| **Merged Into** | `main` |
| **Date Started** | Unknown |
| **Date Merged** | 2026-08-24 |

#### What Was Built
Developed the level layout, with further level work still in progress.


#### Key Commits
| Commit Message | What Changed |
|---|---|
| `Wip levels` | Final branch commit on 2026-08-24. |
| | |
| | |

#### Problems Encountered & Resolved
| Problem | Resolution |
|---|---|
|  | |

#### Screenshot / Evidence
> [View Level 1 screenshot](docs/screenshots/level%201.png)

---

### Branch 4 — `feature/animations`

| Field | Detail |
|---|---|
| **Branch Name** | `origin/animations` |
| **Feature Developed** | Animations |
| **Merged Into** | `main` |
| **Date Started** | Unknown |
| **Date Merged** | 2026-06-18 |

#### What Was Built
Developed the player idle animation, with further animation work still in progress.


#### Key Commits
| Commit Message | What Changed |
|---|---|
| `Wip animations` | Final branch commit on 2026-06-18. |
| | |
| | |

#### Problems Encountered & Resolved
| Problem | Resolution |
|---|---|
| N/A | N/A |
| | |

#### Screenshot / Evidence
> [View idle animation screenshot](docs/screenshots/player%20idle.png)

---

### Branch 5 — `feature/lighting`

| Field | Detail |
|---|---|
| **Branch Name** | `origin/lighting` |
| **Feature Developed** | Lighting |
| **Merged Into** | `main` |
| **Date Started** | Unknown |
| **Date Merged** | 2026-06-17 |

#### What Was Built
Switched to a spot light to improve the flashlight effect and level visibility.


#### Key Commits
| Commit Message | What Changed |
|---|---|
| `Fixed lighting` | Final branch commit on 2026-06-17. |
| | |
| | |

#### Problems Encountered & Resolved
| Problem | Resolution |
|---|---|
| The original lighting setup did not illuminate the level correctly. | Switched to a spot light to improve the lighting and flashlight effect. |
| | |

#### Screenshot / Evidence
> [View lighting screenshot](docs/screenshots/flashlight.png)

---

### Branch 6 — `feature/movement`

| Field | Detail |
|---|---|
| **Branch Name** | `origin/movement` |
| **Feature Developed** | Level and player movement development |
| **Merged Into** | `main` |
| **Date Started** | Unknown |
| **Date Merged** | 2026-08-10 |

#### What Was Built
Continued work on the level layout and player movement systems.


#### Key Commits
| Commit Message | What Changed |
|---|---|
| `MAde level` | Final branch commit on 2026-08-10. |
| | |
| | |

#### Problems Encountered & Resolved
| Problem | Resolution |
|---|---|
| | |
| | |

#### Screenshot / Evidence
> [View Level 1 screenshot](docs/screenshots/level%201.png)

---

### Branch Development Overview

> Complete this summary table once all branches are finished.

| Branch Name | Feature | Date Started | Date Merged | Status |
|---|---|---|---|---|
| `main` | Stable release | N/A | Not applicable | Active |
| `origin/Camera` | Camera movement and lighting | Unknown | 2026-06-11 | Merged into main |
| `origin/Levels` | Level development | Unknown | 2026-08-24 | Merged into main |
| `origin/animations` | Animations | Unknown | 2026-06-18 | Merged into main |
| `origin/lighting` | Lighting | Unknown | 2026-06-17 | Merged into main |
| `origin/movement` | Level and movement development | Unknown | 2026-08-10 | Merged into main |

---

> **Student Declaration:** All work submitted is my own except where explicitly acknowledged above.