# [Tojam2018](https://en.wikipedia.org/wiki/TOJam) Game

### Alone Together

**Theme:** Winners are Losers

**Story:** You see your friends aren't super happy, so you send them your happiness, but in that process become drained, lethargic and sad yourself. By winning, that is, giving away all your happiness, you lose.

**Mechanics:** 
- Players have a limited quantity of happiness, which they send out as projectiles to others. 
- It is absorbed and respectively alters the happiness levels in each player. 
- When you've given away all your happiness, you will be sad, and in your final act, you have to give away yourself.

**Authors:**

_Coders_
- Jessica Gold
- Sam Albon
- Ian Nastajus

_Sound_
- Liam Gallagher


### Target System Requirements

PC for Windows.
Xbox 360 controllers.
2 to 4 players.


### Gameplay images

![Game play 1](https://raw.githubusercontent.com/2PersonGames/Tojam2018/dev/Readme/sad-happy.png "Game play.") 
![Game play 2](https://raw.githubusercontent.com/2PersonGames/Tojam2018/dev/Readme/sad-happy2.png "Game play.") 
![Game play 3](https://raw.githubusercontent.com/2PersonGames/Tojam2018/dev/Readme/sad-happy3.png "Game play.") 



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


