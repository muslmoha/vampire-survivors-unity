# Unity Vampire Survivors Starter

This repository is a starter kit for a Vampire Survivors-style game in Unity. It includes:

- Q/W/E combo spell input and R cast execution
- two spell slots for queued casting
- enemy pooling for large numbers of enemies
- simple melee/ranged enemy movement toward the player
- XP and level-up scaffold
- a few sample spells, including a Stone Slab spell that matches your design

## Project layout

- `Assets/Scripts/Player/` - input, casting, player controller
- `Assets/Scripts/Spells/` - spell definitions and effects
- `Assets/Scripts/Enemies/` - pooling and spawning logic
- `Assets/Scripts/Combat/` - health and damage interfaces
- `Assets/Scripts/Systems/` - level system and supporting logic

## Core design

This starter is intentionally simple and optimized for a first playable prototype:

- Enemy count target: 500
- AI: direct walk toward the player (no pathfinding)
- Spell input: Q/W/E, then R to cast
- Bosses: handled separately with fewer active enemies
- Performance: object pooling and capped active enemy counts

## Quick start in Unity

1. Create a new Unity project or use this project as a reference.
2. Create an empty GameObject named `GameRoot`.
3. Attach the following scripts to it:
   - `EnemySpawner`
   - `SpellInputController`
   - `LevelSystem`
   - `PlayerController`
4. Create a simple player capsule or cube and assign it to the PlayerController reference.
5. Create a simple enemy prefab with a `SphereCollider` and `Enemy` component.
6. Assign the prefab to `EnemySpawner` and `EnemyPool`.
7. Press Play and use Q/W/E + R to cast spells.

## Example combos

- `QQQ` = Stone Slab
- `QWE` = Frost Nova
- `WWW` = Meteor Burst
- `WQE` = Gravity Well
- `EEE` = Chain Lightning

## Notes

This is a prototype foundation, not a full production-ready game. It is intentionally structured so you can expand the spell pool, boss logic, and progression without rewriting the core systems.
