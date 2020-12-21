# MarsExpedition

This project simulates a basic Mars Expedition with Grid Map and Rovers

### Map System
Map has only max X, Y coordinates(e.g. 5, 5; 10, 5) and it's a **Grid** system.

Map creates Rovers for tracking them and generates a **GUID** for every rover created.

Map Checks for Collisions With **CheckForCollisionAndLog** and if there is a collision between Rovers it logs, when Map gets disposed it also disposes Rovers


### Rover System
Rovers do not use compass in themselves only for Human input

Rovers can only Turn to the sides(Right or Left) and can Move Forward to facing direction

#### Move
When rover is going to move if it goes outside the Map then Rover does not move!

If Rover moves then calls **Map**'s UpdateRover for headsup

#### TurnRight and TurnLeft
Calculates the rover's next direction when rover Turns Right or Left

#### ExecuteCommands
Accepts primitive string and loops inside and it only accepts 'L','R','M' inputs if input char is outside of these characters then does nothing.