10/29/30
Intial commit and build of Psyche Against The Universe card game.
Commit consist of initial project files and templates along with required dependicies. 
Created seperate branches for each team member with main being used to house finished products
Program not in WEBGL format currently but will be converted once workable assets are incorporated.

11/1/2
Create working Hand, Playing Card Placeholder and ability to move cards around hand.
11/10/25
Menu at 80%
Game setup is at 80%. Added mode toggle logic. Quit button will end editor mode and real gameplay. Added psyche spacecraft movable sprite to scene. Can be used for easter eggs later. Fixed SuddenWin flag issue in game manager.This 
is in addition to all updates from 11/6/25. These updates consisted of completing the game manager which provides a player object queue. Use the debug.log code to see how to access fields that are unique to the CPU players vice 
the human player. Still need to add popups for the about and Manual buttons. Also need to add a setup menu to game screen transition. 
Last items for CPU character generation are to make the personality fields a string array that are randomly generated with 4 unique personalities. This hierarchy is used by the strategy pattern to determine CPU play strategy.
Player and CPU characters use the AnswerCard object in thier hand list. 
Feel free to modify classes as needed to interface with the game logic, but maintain the MVC interface where required.
