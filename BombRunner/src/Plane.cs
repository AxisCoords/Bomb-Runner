using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombRunner;

internal class Plane {
    const byte SCALE = 3;
    int speed;
    float timer;
    bool flipped;
    Texture2D texture;
    Vector2 position;
    Rectangle sourceRect = new Rectangle(16, 16, 16, 16);
    SpriteEffects spriteEffects = SpriteEffects.None;
    Random randomTimer = new Random();
    Random randomPosY = new Random();
    Random randSpeed = new Random();
    
    public Plane(Texture2D texture, bool flipped) {
        this.texture = texture;
        this.flipped = flipped;

        position.X = Global.WIDTH + 16 * 3;

        timer = RandomizeTimer();
        RandomizeSettings();
        speed = RandomizeSpeed();
    }

    private void RandomizeSettings() {
        position.Y = randomPosY.Next(0, 150);
    }

    private int RandomizeTimer() {
        return randomTimer.Next(3, 8);
    }

    private int RandomizeSpeed() {
        const int MAX_SPEED = 500;
        const int MIN_SPEED = 200;
        return randSpeed.Next(MIN_SPEED, MAX_SPEED);
    }
    
    public void Update(GameTime gameTime) {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        timer -= 1 * delta;

        if (timer <= 1) {
            if (flipped) {
                position.X += speed * delta;
                spriteEffects = SpriteEffects.FlipHorizontally;
            } else {
                position.X -= speed * delta;
                spriteEffects = SpriteEffects.None;
            }
        }

        if (flipped) {
            if (position.X >= Global.WIDTH + sourceRect.Width * SCALE) {
                position.X = 0 - sourceRect.Width * SCALE;
                timer = RandomizeTimer();
                RandomizeSettings();
                speed = RandomizeSpeed();
            }
        } else {
            if (position.X <= 0 - sourceRect.Width * SCALE) {
                position.X = Global.WIDTH + 16 * 3;
                timer = RandomizeTimer();
                RandomizeSettings();
                speed = RandomizeSpeed();
            }
        }
    }

    public void Draw() {
        Global.spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, 
                                             sourceRect.Width * SCALE, sourceRect.Height * SCALE), 
                                             sourceRect, Color.White, 0, Vector2.Zero, spriteEffects, 0.2f);
    }
}
