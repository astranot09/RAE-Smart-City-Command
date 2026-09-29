# RAE : Smart City Command (WIP) 

## About Game
A serious game, where player play as RAE, monitor CCTV feeds and sensor data, and determine emergency levels across multiple island regions. Upgrade your equipment to boost sensor accuracy and spot false alarms with ease.

This Game I made for MAGE 12 <br>
Game Engine = Unity 6000.5.5f1

## Team Contribution
| Name | Roles | Duration |
| :---: | :---: | :---: |
| astranot09 | Game Programmer | 14 |
| Stopit-m8 | Game Designer | 7 |
| raymondbenedict2802405245’s | Game Artist | ... |

## My Contribution (astranot09)
- Outpost Logic (When Disaster will hit the outpost, When the time in that outpost to make graph alert, How much people left in that outpost, etc)
- NPC State (When the state wandering around, when the state run to evactuation zone, when the state coming baack from zone, what happend when they died)
- Change Camera Logic
- Create all the code for game systems and features
- Implement Animation Character in Unity, Animation UI Dotween
- Create Game Logic
- etc

## Key Features

### Real-time Telemetry & Graph Analysis
Monitor live feeds from Seismographs, Buoys, Gas Sensors, and Tide Gauges to spot disaster anomalies.

### Evacuation Protocol Management
Trigger region-wide evacuation orders to route civilians to safety before disasters strike.

### Dynamic Civilian AI
Population react dynamically to disaster events, seeking shelter and repopulating zones once threats clear.

### Equipment & Sensor Upgrades
Reinvest command currency to upgrade sensor precision and filter out false alarms.

### Multi-Outpost Surveillance
Switch camera feeds between various island regions to manage multiple outposts simultaneously.

### After Disaster Day Reports
Evaluate disaster responses, civilian survival rates, and unlocked outposts.

## Layer / Module Design

<img width="1222" height="1099" alt="RAEModule drawio" src="https://github.com/user-attachments/assets/1928e0a9-2254-40e4-8913-b689519558b7" />


## Modules and Features

| Name | Scene | Responsibility |
| :---: | :---: | :---: |
| Scene Controller | All Scene | Scene transitions, transition animation, exit game. |
| Audio Manager | All Scene | Plays BGM/SFX globally via audio database. |
| UI Manager | All Scene | Controls panel visibility, canvas layers, and overlay states. |
| Dotween Animation Collections | All Scene | Library of reusable DOTween animations for UI hover. |
| Main Menu Manager | Main Menu | Manages menu UI navigation, options, and game startup routines. |
| Currency Manager | Gameplay Scene | Managed currency changes. |
| VCam Manager | Gameplay Scene | Controls Cinemachine virtual camera switches between various outposts. |
| Lose Manager | Gameplay Scene | Evaluates defeat conditions when populations reach 0. |
| NPC Script | Gameplay Scene | Implements Finite State Machine (FSM) for civilian movement, evacuation, and death. |
| Outpost Manager | Gameplay Scene | Core manager handling disaster countdowns, outpost population, graph alerts, and spawns. |
| Buoy Simulator | Gameplay Scene | Logic for making ocean telemetry graph when normal and alarmed. |
| Gas Sensor Simulator | Gameplay Scene | Logic for making graph for gas levels in volcano when normal and alarmed. |
| Seismograph Simulator | Gameplay Scene | Logic for making earthquake or volcano tremor when normal and alarmed. |
| Tide Gauge Simulator | Gameplay Scene | Logic for making numerical water level data when normal and alarmed. |
| UI Graph Line | Gameplay Scene | Handles real-time line rendering on UI canvas elements. |
| Upgrade Manager | Gameplay Scene | Manages upgrade for sensor accuracy, noise filtering, etc |
| Evacuate Manager | Gameplay Scene | Handles evacuation UI setup, and Select the type of disaster. |
| Day Report Manager | Gameplay Scene | Summarizes disaster responses, population survival rates, and outpost progression. |
| Gameplay Manager | Gameplay Scene | Logic for changing graph when changing outpost. |

## Game Flow

<img width="1082" height="1487" alt="RAEGameFlow drawio" src="https://github.com/user-attachments/assets/f5c1417c-ec2a-412b-86b9-2c2281f34c17" />


## Unity Asset
- Free Quick Effects Vol. 1
