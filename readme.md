# Avoid the Orbs
![screenshot](https://i.imgur.com/H3AXd6f.png)
**Avoid the Orbs** is a 2D pixel-art based game written in Unity. The
goal of the game is to avoid the dangeorus red orbs emitted by the *emitters*,
and survive as long as possible. This repo is the home of the source code for
the game.

You can download the game over at the [Releases](https://github.com/christofferaakre/avoid-the-orbs/releases). Simply unzip the downloaded file and launch the game
by opening the `plat.exe` file.

## Known Bugs
* The player can accidentally clip into the ground when on sloped ground,
  causing them to get suck
* If the game runs for too long, it will eventually freeze. This is likely
  related to a performance issue wit rendering too many rigid bodies, and could
potentially be fixed by rewriting the projectiles to use raycasting instead of
rigid bodies
