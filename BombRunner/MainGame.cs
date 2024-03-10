using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BombRunner;

public class MainGame : Game {
    private GraphicsDeviceManager _graphics;
    
    const byte MAX_BOMB_COUNT = 8;
    const byte MAX_PLANE_COUNT = 6;
    const byte MAX_CLOUD_COUNT = 6;
    SpriteFont font;
    SimpleFps fps = new SimpleFps();
    Player player;
    Texture2D playerTexture;
    Texture2D groundTexture;
    Texture2D grassTexture;
    Texture2D planeTexture;
    Texture2D bombTexture;
    Texture2D cloudTexture;
    List<Bomb> bombs;
    List<Plane> planes;
    List<Cloud> clouds;

    public MainGame() {
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = Global.WIDTH,
            PreferredBackBufferHeight = Global.HEIGHT,
            SynchronizeWithVerticalRetrace = true
        };

        IsFixedTimeStep = false;
        TargetElapsedTime = TimeSpan.FromMilliseconds(30000d / 1000d);
        
        _graphics.ApplyChanges();
        
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize() {
        Window.Title = Global.TITLE;
        
        Global.pixels = new Texture2D(GraphicsDevice, 1, 1);
        Global.pixels.SetData<Color>(new Color[] {Color.White});
        
        base.Initialize();
    }

    protected override void LoadContent() {
        Global.spriteBatch = new SpriteBatch(GraphicsDevice);
        string tile = "tile_atlas";
        
        font = Content.Load<SpriteFont>("newFont");
        playerTexture = Content.Load<Texture2D>(tile);
        groundTexture = Content.Load<Texture2D>(tile);
        grassTexture = Content.Load<Texture2D>(tile);
        bombTexture = Content.Load<Texture2D>(tile);
        planeTexture = Content.Load<Texture2D>(tile);
        cloudTexture = Content.Load<Texture2D>(tile);
        
        player = new Player(playerTexture, new Vector2(Global.WIDTH / 2, 400));
        GenerateLevel();
    }

    protected override void Update(GameTime gameTime) {
        KeyInput.ListenKeyStates();
        if (KeyInput.IsKeyJustPressed(Keys.Escape)) Exit();
        // Reset level
        if (KeyInput.IsKeyJustPressed(Keys.Enter) && Global.dead) {
            Global.score = 0;
            Global.dead = false;
            Global.showDebug = false;
            bombs.Clear();
            clouds.Clear();
            planes.Clear();

            GenerateLevel();
        }

        if (!Global.dead) {
            if (KeyInput.IsKeyJustPressed(Keys.F3)) Global.showDebug = !Global.showDebug;

            player.Update(gameTime);
            fps.Update(gameTime);

            // Planes
            foreach (Plane plane in planes) {
                plane.Update(gameTime);
            }
            
            // Bombs
            foreach (Bomb bomb in bombs) {
                bomb.Update(gameTime);
                if (bomb.CheckCollisionsRect(player)) {
                    Global.dead = true;
                }
            }

            // Clouds
            foreach (Cloud cloud in clouds) {
                cloud.Update(gameTime);
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(new Color(238, 195, 154, 255));
        Global.spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        if (!Global.dead) {

            // Clouds
            foreach (Cloud cloud in clouds) {
                cloud.Draw();
            }

            // Planes
            foreach (Plane plane in planes) {
                plane.Draw();
            }
            
            // Ground
            for (int i = 0; i < Global.WIDTH; i++) {
                Global.spriteBatch.Draw(groundTexture, new Rectangle(i * 16 * 3, 424, 16 * 3, 32 * 3), new Rectangle(0, 16, 16, 16), 
                                                                                Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            }

            // Grass
            for (int i = 0; i < Global.WIDTH; i++) {
                Global.spriteBatch.Draw(grassTexture, new Rectangle(i * 16 * 3, 376, 16 * 3, 16 * 3), new Rectangle(16 * 2, 0, 16, 16), 
                                                                                    Color.White, 0, Vector2.Zero, SpriteEffects.None, 1);
            }
            
            // Player
            player.Draw();

            // Bombs
            foreach (Bomb bomb in bombs) {
                bomb.Draw();
            }
            
            if (Global.showDebug) {
                fps.DrawFps(font, new Vector2(3, 3), new Color(20, 20, 20, 180));
                fps.DrawFps(font, new Vector2(0, 0), Color.White);

                Global.spriteBatch.Draw(Global.pixels, new Rectangle(Global.WIDTH / 2, 0, 1, Global.HEIGHT), Color.Lime);
                Global.spriteBatch.Draw(Global.pixels, new Rectangle(0, Global.HEIGHT / 2, Global.WIDTH, 1), Color.Red);
            }

            Text.DrawTextCentered(font, "Score: " + Global.score, new Vector2(Global.WIDTH / 2, 14), Color.White);
        } else {
            // Death screen
            Global.spriteBatch.Draw(groundTexture, new Rectangle(0, 0, Global.WIDTH, Global.HEIGHT), new Rectangle(0, 16, 16, 16), Color.White);
            Text.DrawTextCentered(font, "SCORE: " + Global.score, new Vector2(Global.WIDTH / 2, Global.HEIGHT / 2 - 100), Color.White, false);
            Text.DrawTextCentered(font, "Press ENTER to play again", new Vector2(Global.WIDTH / 2, Global.HEIGHT / 2 + 100), Color.White);
            Text.DrawText(font, "dev Axis", new Vector2(4, Global.HEIGHT - 28), Color.White, false);
        }

        
        Global.spriteBatch.End();
        base.Draw(gameTime);
    }

    // Generate entites and all
    private void GenerateLevel() {
        player = new Player(playerTexture, new Vector2(Global.WIDTH / 2, 400));

        // Bomb
        bombs = new List<Bomb>();
        for (int i = 1; i < MAX_BOMB_COUNT; i++) {
            Bomb bomb = new Bomb(bombTexture);
            bombs.Add(bomb);
        }

        // Plane
        planes = new List<Plane>();
        for (int i = 1; i < MAX_PLANE_COUNT; i++) {
            Random rand = new Random();

            Plane plain = new Plane(planeTexture, Convert.ToBoolean(rand.Next(0, 2)));
            planes.Add(plain);
        }

        // Cloud
        clouds = new List<Cloud>();
        for (int i = 1; i < MAX_CLOUD_COUNT; i++) {
            Cloud cloud = new Cloud(cloudTexture);
            clouds.Add(cloud);
        }
    }
}
