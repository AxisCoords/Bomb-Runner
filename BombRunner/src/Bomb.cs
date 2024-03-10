using System;
using System.Net.Http.Headers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombRunner;

internal class Bomb {
    float timer;
    const byte SCALE = 3;
    const int GRAVITY = 400;
    public Vector2 position;
    Texture2D texture;
    Rectangle sourceRect = new Rectangle(16, 0, 16, 16);
    Rectangle collisionRect = new Rectangle(16, 0, 16 / 2 * SCALE, 16 * SCALE);
    Random randomTimer = new Random();
    Random randomPosX = new Random();
    
    public Bomb(Texture2D texture) {
        this.texture = texture;

        position.Y = -16 * SCALE;
        position.X = randomPosX.Next(sourceRect.Width / 2 - 4 * SCALE, Global.WIDTH - sourceRect.Width / 2 - 16 * SCALE);

        GenNewTimer();
    }

    private float GenNewTimer() {
        return timer = randomTimer.Next(2, 7);
    }

    public bool CheckCollisionsRect(Player player) {
        if (collisionRect.Intersects(player.collisionRect)) {
            return true;
        } else {
            return false;
        }
    }
    
    public void Update(GameTime gameTime) {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        collisionRect.X = (int)position.X + sourceRect.Width / 2 + 4;

        timer -= 1 * delta;
        
        if (timer <= 1)
            position.Y += GRAVITY * delta;
            collisionRect.Y = (int)position.Y;

        if (position.Y >= 500 - 16 * 3) {
            position.Y = -16 * SCALE;
            position.X = randomPosX.Next(sourceRect.Width / 2 - 4 * SCALE, Global.WIDTH - sourceRect.Width / 2 - 4 * SCALE);
            Global.score++;
            GenNewTimer();
        }
    }

    public void Draw() {
        Global.spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, sourceRect.Width * SCALE, sourceRect.Height * SCALE), 
                                                                                                                    sourceRect, Color.White);

        if (Global.showDebug) {
            Global.spriteBatch.Draw(Global.pixels, collisionRect, new Color(Color.Red, 100));
        }
    }
}
