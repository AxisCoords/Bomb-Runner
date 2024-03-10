using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombRunner;

internal class SimpleFps {
    private double frames = 0;
    private double updates = 0;
    private double elapsed = 0;
    private double last = 0;
    private double now = 0;
    private double msgFrequency = 1.0f; 
    private string msg = "";

    public void Update(GameTime gameTime) {
        now = gameTime.TotalGameTime.TotalSeconds;
        elapsed = (double)(now - last);
        if (elapsed > msgFrequency) {
            msg = " FPS: " + Convert.ToInt32((frames / elapsed)).ToString();
            elapsed = 0;
            frames = 0;
            updates = 0;
            last = now;
        }
        updates++;
        frames++;
    }

    public void DrawFps(SpriteFont font, Vector2 fpsDisplayPosition, Color fpsTextColor) {
        Global.spriteBatch.DrawString(font, msg, fpsDisplayPosition, fpsTextColor);
    }
}
