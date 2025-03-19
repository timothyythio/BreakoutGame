## 🎯 Summary
This lab, we took a base prototype of a classic **Breakout** game and enhanced it with various gameplay improvements. The goal was to collaborate using **GitHub**, implement unique features, and refine the overall game feel with **"game juice"** techniques. Each team member contributed a distinct upgrade, ensuring smoother visuals, better audio feedback, and improved player interactions.

---

## 🎮 Features & Improvements

### 🔹 **Score Display**
- ✅ **Real-time Score Updates**: Displays the number of bricks destroyed.
- ✅ **Smooth Score Transition**: Uses UI animations for score updates.

### 🔹 **Lives & Game Over Screen**
- ✅ **HUD Life Counter**: Displays remaining lives.
- ✅ **Game Over Animation**: Freezes time and displays a game-over screen.
- ✅ **Seamless Transition**: After 1.5 seconds, returns to the main menu via `SceneHandler.Instance.LoadMenuScene()`.

### 🔹 **Audio Manager & SFX**
- ✅ **Dynamic Sound Effects**: Adds sounds for brick destruction, paddle movement, and ball collisions.
- ✅ **Background Music Support**: Ensures smooth looping of background music.
- ✅ **Easy Audio Access**: Centralized system for managing sound effects.


### 🔹 **Camera Shake**
- ✅ **DoTween Integration**: Implements camera shake for impactful moments.
- ✅ **Smooth Shake Animation**: Enhances game feel with position and rotation shake.
- ✅ **Easy Triggering**: Call `CameraShake.Instance.Shake(duration, strength)` to activate.

```csharp
using DG.Tweening;

public class CameraShake : SingletonMonoBehavior<CameraShake>
{
    public static void Shake(float duration, float strength)
    {
        Instance.OnShake(duration, strength);
    }

    private void OnShake(float duration, float strength)
    {
        transform.DOShakePosition(duration, strength);
        transform.DOShakeRotation(duration, strength);
    }
}
```

---
## **📹 Video Demo**

[![Watch the video](https://img.youtube.com/vi/4d43VhhUOY8/maxresdefault.jpg)](https://youtu.be/4d43VhhUOY8)

### [Click here to watch the game demo](https://youtu.be/4d43VhhUOY8)
