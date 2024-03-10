using Microsoft.Xna.Framework.Graphics;

namespace BombRunner;

internal class Global {
    public static int score = 0;
    public static bool dead = false;
    
    public const int WIDTH = 500;
    public const int HEIGHT = 500;

    public static string TITLE = "Bomb Runner";
    public static bool showDebug = false;
    
    public static Texture2D pixels;
    public static SpriteBatch spriteBatch;
}
