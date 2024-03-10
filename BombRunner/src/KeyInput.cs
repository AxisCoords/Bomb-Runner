using Microsoft.Xna.Framework.Input;

namespace BombRunner;

internal class KeyInput {
    static KeyboardState previousKeyState;
    static KeyboardState currentKeyState;

    public static KeyboardState ListenKeyStates() {
        previousKeyState = currentKeyState;
        currentKeyState = Keyboard.GetState();
        return currentKeyState;
    }

    public static bool IsKeyPressed(Keys key) {
        return currentKeyState.IsKeyDown(key);
    }

    public static bool IsKeyJustPressed(Keys key) {
        return currentKeyState.IsKeyDown(key) && !previousKeyState.IsKeyDown(key);
    }
}
