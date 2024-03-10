using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BombRunner;

internal class Player {
    Vector2 position;
    Texture2D texture;
    SpriteEffects spriteEffect = SpriteEffects.None;
    Rectangle sourceRect = new Rectangle(0, 0, 16, 16);
    public Rectangle collisionRect;
    const byte SCALE = 3;
    const int SPEED = 400;
    
    public Player(Texture2D texture, Vector2 position) {
        this.texture = texture;
        this.position = position;
    }

    public void Update(GameTime gameTime) {
        float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        if (KeyInput.IsKeyPressed(Keys.Right) && KeyInput.IsKeyPressed(Keys.Left)) {}
        else if (KeyInput.IsKeyPressed(Keys.Right) && position.X < Global.WIDTH - sourceRect.Width / 4 * SCALE) {
            spriteEffect = SpriteEffects.None;
            position.X += SPEED * delta;
        } else if (KeyInput.IsKeyPressed(Keys.Left) && position.X > 0 + sourceRect.Width / 4 * SCALE) {
            spriteEffect = SpriteEffects.FlipHorizontally;
            position.X -= SPEED * delta;
        }

        collisionRect = new Rectangle((int)position.X - 16 / 2 - 4, (int)position.Y - 16 / 2 * SCALE, 16 / 2 * SCALE, 16 * SCALE);
    }

    public void Draw() {
        Global.spriteBatch.Draw(texture, new Vector2(position.X, position.Y), sourceRect, Color.White, 0, new Vector2(16 / 2, 16 / 2), SCALE, spriteEffect, 1);

        if (Global.showDebug)
            Global.spriteBatch.Draw(Global.pixels, collisionRect, new Color(Color.Red, 100));
    }
}
