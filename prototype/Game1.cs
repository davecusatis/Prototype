#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using IrrKlang;
using TiledSharp;
using prototype.Engine.MonoTinySpace;
#endregion

namespace prototype

{
    using Engine;
    public enum Direction
    {
        Up, Down, Left, Right, Buttz
    };
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        ParticleEngine particleEngine;
        GraphicsDeviceManager graphics;
        Vector3 camera;
        SpriteBatch spriteBatch;
        Player player;
        Region region;
        TCWorld world;
        KeyboardState currentKeyState;
        KeyboardState previousKeyState;
        MouseState currentMouseState;
        MouseState previousMouseState;
        float playerMoveSpeed;
        float dodgeSpeed;
        float projectileSpeed;
        ISoundEngine SoundEngine;
        Song song;
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            SoundEngine = new ISoundEngine();
            SoundEngine.Play2D("../../../Content/Boss_GENERIC.mp3");
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player();
            world = new TCWorld();
            //world.AddRect(player.playerRect);
            playerMoveSpeed = 80.0f;
            dodgeSpeed = 10 * playerMoveSpeed;
            projectileSpeed = 15.0f;
            //world = new World(new Vector2(0, 0));
            region = new Region("Demo", new TmxMap("C:\\Users\\David\\Desktop\\Projects\\moblife\\prototype\\prototype\\Content\\demo.tmx"), this.Content, new TCWorld());
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            List<Texture2D> particleTextures = new List<Texture2D>();
            particleTextures.Add(Content.Load<Texture2D>("red"));
            particleTextures.Add(Content.Load<Texture2D>("darkorange"));
            particleTextures.Add(Content.Load<Texture2D>("orange"));
            particleEngine = new ParticleEngine(particleTextures, new Vector2((float)((13.5f))*32, (float)((22.5f))*32));
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Vector2 playerPos = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            //player.Initialize(Content.Load<Texture2D>("mrspy.bmp"), Content.Load<Texture2D>("red"), playerPos, this.Content);
            player.Initialize(Content.Load<Texture2D>("mrspy1"), Content.Load<Texture2D>("red"), region.PlayerSpawn, this.Content);
            region.World.AddRect(player.playerRect);
            SoundEffect song = Content.Load<SoundEffect>("Boss_GENERIC.wav");
            SoundEffectInstance seInstance = song.CreateInstance();
            seInstance.IsLooped = true;
            seInstance.Play();
            
           // SongCollection c = new SongCollection();
           // c.Add(song);
           // c.Add(song);
           // c.Add(song);
           // // TODO: use this.Content to load your game content here
           //// MediaPlayer.IsRepeating = true; // why the fuck does this not work
           // //MediaPlayer.Stop();
           // MediaPlayer.Play(c);
        }

       

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
           
            // TODO: Add your update logic here
            previousKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();
            UpdatePlayer(gameTime);
            
            particleEngine.Update();
            if (currentKeyState.IsKeyDown(Keys.X) && !previousKeyState.IsKeyDown(Keys.X))
            {
                player.Shoot();
            }
            player.Update();
            UpdatePlayer(gameTime);
            UpdateEnemies(gameTime);
            base.Update(gameTime);
        }

        private void UpdateEnemies(GameTime gameTime)
        {
            // VERY BUDGET STYLE
            foreach(Enemy e in region.EnemyList)
            {
                if(e.EnemyState == State.Idle)
                {
                    region.MoveEnemy(e);
                    e.stepsTraveled++;
                }
            }
        }

        //TODO refactor
        private void UpdatePlayer(GameTime gameTime)
        {
            if (currentKeyState.IsKeyDown(Keys.Left))
            {
                region.MovePlayer(player,new Vector2(-playerMoveSpeed, 0));              
                if (!currentKeyState.IsKeyDown(Keys.Space))
                {
                    player.directionFacing = Direction.Left;
                }
            }

            if (currentKeyState.IsKeyDown(Keys.Right))
            {
                region.MovePlayer(player,new Vector2(playerMoveSpeed, 0));
                if (!currentKeyState.IsKeyDown(Keys.Space))
                {
                    player.directionFacing = Direction.Right;
                }
            }

            if (currentKeyState.IsKeyDown(Keys.Up))
            {
                region.MovePlayer(player, new Vector2(0, -playerMoveSpeed));
                if (!currentKeyState.IsKeyDown(Keys.Space))
                {
                    player.directionFacing = Direction.Up;
                }
            }
            if (currentKeyState.IsKeyDown(Keys.Down))
            {
                region.MovePlayer(player, new Vector2(0, playerMoveSpeed));
                if (!currentKeyState.IsKeyDown(Keys.Space))
                {
                    player.directionFacing = Direction.Down;
                }
            }

            // DODGE mechanic maybe. TODO: tweak
            if (currentKeyState.IsKeyDown(Keys.Z) && !previousKeyState.IsKeyDown(Keys.Z))
            {
                switch(player.directionFacing)
                {
                    case Direction.Left:
                        region.MovePlayer(player, new Vector2(-dodgeSpeed, 0));
                        break;
                    case Direction.Right:
                        region.MovePlayer(player, new Vector2(dodgeSpeed, 0));
                        break;
                    case Direction.Up:
                        region.MovePlayer(player, new Vector2(0, -dodgeSpeed));
                        break;
                    case Direction.Down:
                        region.MovePlayer(player, new Vector2(0, dodgeSpeed));
                        break;
                }
            }

            // TODO: translation function maybe
            camera.X = -player.Position.X + GraphicsDevice.Viewport.Bounds.Width / 2;
            camera.Y = -player.Position.Y + GraphicsDevice.Viewport.Bounds.Height / 2;
            camera.Z = 0;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Matrix.CreateTranslation(camera));
            

            region.Draw(spriteBatch);
            player.Draw(spriteBatch);

            foreach (var enemy in region.EnemyList)
            {
                enemy.Draw(spriteBatch);
            }

            spriteBatch.End();
            // bulletz
            player.DrawBullets(spriteBatch, camera);

            // fire in the middle of the map
            particleEngine.Draw(spriteBatch, camera);
            base.Draw(gameTime);
        }
    }
}
