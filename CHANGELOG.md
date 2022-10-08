# Changelog

 - Added a better UI
 - Added laser beam turret
    - Deals damage over time to enemies
    - The laser also slows them while being damaged
 - Added world UI in the map borders
 - Added color and value animation effects to money UI and watch UI
 - Added sound effects to standard and missile launcher turrets
    - Laser beam will have sound effects added in the next update
 - Enemies now have health! 💥
 - Refactored code from original source
    - Splited Movement things from main class things, e.g: Enemy and EnemyMovement
    - Splited Wave spawing events from Game Manager to a individual object 
    - Properly delegated events like damaging player to a Game Manager
