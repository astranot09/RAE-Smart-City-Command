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
| Buoy Simulator | Gameplay Scene | Logic for making graph when normal and alarmed. |
| Gas Sensor Simulator | Gameplay Scene | Logic for making graph when normal and alarmed. |
| Seismograph Simulator | Gameplay Scene | Logic for making graph when normal and alarmed. |
| Tide Gauge Simulator | Gameplay Scene | Logic for making numbers when normal and alarmed. |
| UI Graph Line | Gameplay Scene | Logic for draw line renderer in canvas. |
| Upgrade Manager | Gameplay Scene | Logic for upgrade like UI, Insert if there is something to upgrade, etc. |
| Evacuate Manager | Gameplay Scene | Logic for setUp UI for choosing evacuate. |
| Day Report Manager | Gameplay Scene | Summarizes disaster responses, population survival rates, and outpost progression. |
| Gameplay Manager | Gameplay Scene | Logic for changing graph when changing outpost. |

## Game Flow

## Unity Asset
- Free Quick Effects Vol. 1
