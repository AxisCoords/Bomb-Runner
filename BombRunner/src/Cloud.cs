using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombRunner;

class Cloud {
    float timer;
    const byte SCALE = 5;
    const int SPEED = 100;
    Texture2D texture;
    Rectangle sourceRect = new Rectangle(32, 16, 16, 16);
    Vector2 position;
    Random randomTimer = new Random();
    Random randomPosY = new Random();
    
    public Cloud(Texture2D texture) {
        this.texture = texture;

        position.X = Global.WIDTH + sourceRect.Width * SCALE;

        RandomizeTimer();
        RandomizePosY();
    }

    private void RandomizeTimer() {
        timer = randomTimer.Next(1, 8);
    }

    private void RandomizePosY() {
        position.Y = randomPosY.Next(5, 100);
    }

    public void Update(GameTime gameTime) {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

        timer -= 1 * delta;

        if (timer <= 1)
            position.X -= SPEED * delta;

        if (position.X <= 0 - sourceRect.Width * SCALE) {
            position.X = Global.WIDTH + sourceRect.Width * SCALE;
            RandomizeTimer();
            RandomizePosY();
        }
    }

    public void Draw() {
        Global.spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, sourceRect.Width * SCALE, sourceRect.Height * SCALE), 
                                                            sourceRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.1f);
    }
}
