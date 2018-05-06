# Tojam2018

### Target System Requirements

PC for Windows.
Xbox 360 controllers.
2 to 4 players.


### Player Documentation

| Controller Input          |  Keyboard & Mouse Input                                    | Action            | Developer Unity's InputManager Axes       |
|---------------------------|------------------------------------------------------------|-------------------|-------------------------------------------|
| Left Stick                |  ASDW keys (player 1), JKLI (player 2)                     | Movement          | #Horizontal, #Vertical                    |
| Right Stick               |  Arrow keys (player 1), 1235 (player 2)                    | Direction of fire | #FireDirectionH, #FireDirectionV          |
| A                         |  E (player 1), O (player 2)                                | Fire              | #Fire1 (joystick # button 0 \| type: joy) |
| RT - Right Trigger        |  Mouse click left (player 1), mouse click right (player 2) | Fire              | #Fire1 (10th axis \| type: joy)           |
| Start                     |  Space, Enter, Return                                      | Submit            | #Submit (joystick button 7 \| type key or mouse)  |

- # pound sign replaced joystick number.
- Note: all InputManager Axes replicated with preceding Player numbers 1 through 4, e.g. 1Horizontal, 2Horizontal... this applies for joystick-related inputs only. Keyboard related inputs will only exist for player 1.

### Developer Documentation

- Created in Unity 2017.1.0f3 

![controller layout](https://raw.githubusercontent.com/2PersonGames/Tojam2018/dev/Readme/600px-X360Controller2.png "Xbox 360 controller layout and identifiers.") [source](http://wiki.unity3d.com/index.php?title=Xbox360Controller)



