using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombRunner;

internal class Text {
    public static void DrawTextCentered(SpriteFont font, string text, Vector2 position, Color color, bool shadow = true) {
        Vector2 textSize = font.MeasureString(text);

        if (shadow)
            Global.spriteBatch.DrawString(font, text, 
                                        new Vector2(position.X - textSize.X / 2 + 3, position.Y - textSize.Y / 2 + 3), 
                                        new Color(20, 20, 20, 180));
        Global.spriteBatch.DrawString(font, text, new Vector2(position.X - textSize.X / 2, position.Y - textSize.Y / 2), color);
    }

    public static void DrawText(SpriteFont font, string text, Vector2 position, Color color, bool shadow = true) {
        if (shadow)
            Global.spriteBatch.DrawString(font, text, new Vector2(position.X + 3, position.Y + 3), new Color(20, 20, 20, 180));
        Global.spriteBatch.DrawString(font, text, position, color);
    }
}
