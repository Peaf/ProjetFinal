using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace Project
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static Character player, player2, playerMenu;

        public static PNJ pnj1, healer, pnjShop1, pnjShop2;
        public static Enemy enemy, enemy1, enemy2, enemy3, enemy4;
        public static Song song1, song2;

        Random rand = new Random();
        Video video;
        VideoPlayer playerVideo;
        Texture2D videoTexture;
        //Pause
        public static cButton btnNext, btnEndFight, btnStartFight, btnArmors, btnPotions, btnWeapons,btnBuyArmor,btnSellArmor, btnBuyWeapon, btnSellWeapon,btnBuyPot, btnSellPot,btnDoneArmor,btnDoneWeapon,btnDonePot ;
        public static ParticleGenerator snow;
        public static ParticleGenerator1 sand;

        public static MouseState pastMouse;
        KeyboardState presentKey;
        KeyboardState pastKey;

        public static bool optionBackfromMenu = true;


        public static SpriteFont spriteFont;

        bool Isfighting = false, inventaire = false, talking = false, playOnce = true;
        public static int screenWidth, screenHeight; //taille de l'ecran
        public static float previousPosX, previousPosY;
        public static int questState = 0;

        int[,] tab_map8 = new int[26, 44];
        int[,] tab_map5 = new int[26, 44];
        int[,] tab_map4 = new int[26, 44];


        public static Inventaire invent1 = new Inventaire();
        public static Inventaire invent2 = new Inventaire();
        public static Inventaire inventPnjArmor = new Inventaire();
        public static Inventaire inventPnjWeapon = new Inventaire();
        public static Inventaire inventPnjPot = new Inventaire();

        public enum GameState
        {
            Video,
            Title,
            MainMenu,
            Options,
            Playing,
            Fight,
            GameOver,
            Pause
        }
        public GameState CurrentGameState = GameState.Playing;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Playing.Initialize();
            player = new Character(Content.Load<Texture2D>("Sprites/Player"), new Vector2(388, 130), new Rectangle(260 - 30, 438, 30, 59), new Rectangle(0, 0, 30, 59), 500, 200, 0,50, 10, 50, 50, 0);
            player2 = new Character(Content.Load<Texture2D>("Sprites/Player2"), new Vector2(388, 230), new Rectangle(196, 507, 32, 65), new Rectangle(0, 0, 32, 65), 400, 300, 0, 10, 50, 10, 20, 0);
            playerMenu = new Character(Content.Load<Texture2D>("Sprites/Player"), new Vector2(788, 230), new Rectangle(260 - 30, 438, 30, 59), new Rectangle(0, 0, 30, 59), 500, 200, 0, 50, 10, 15, 50, 0);
            /*  this.graphics.IsFullScreen = true;
              this.graphics.ApplyChanges();
           
            screenWidth = 1366;
              this.graphics.ApplyChanges();*/
            screenHeight = 768;
            screenWidth = 1366;
            invent1.Initialize();
            invent2.Initialize();
            inventPnjArmor.Initialize();
            inventPnjWeapon.Initialize();
            inventPnjPot.Initialize();
            invent1.addItem(new Item("Potion", "healthPotion", "health", 50, 1, "",10,"une pot"));
            invent1.addItem(new Item("Potion", "healthPotion", "health", 50, 1, "",10,"une pot"));
            invent1.addItem(new Item("Potion", "manaPotion", "mana", 20, 1, "",15,""));
            invent1.addItem(new Item("Weapon", "Sword", "dmg", 30, 1, "notequiped",30,"arme"));
           // invent1.addItem(new Item("Armor", "Armor", "", 30, 1, "notequiped",50));

            invent2.addItem(new Item("Armor", "Armor", "", 30, 1, "notequiped",50,"armure"));

            inventPnjArmor.addItem(new Item("Armor", "Armor", "", 30, 1, "notequiped",50,"armure"));

            inventPnjWeapon.addItem(new Item("Weapon", "Sword", "dmg", 30, 1, "notequiped",30,"arme"));

            inventPnjPot.addItem(new Item("Potion", "healthPotion", "health", 50, 1, "",10,"pot"));
            inventPnjPot.addItem(new Item("Potion", "manaPotion", "mana", 20, 1, "",15,"pot"));



            base.Initialize();
        }

        protected override void LoadContent()
        {

            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;

            graphics.ApplyChanges();

            IsMouseVisible = true;
            spriteBatch = new SpriteBatch(GraphicsDevice);
            song1 = Content.Load<Song>("Song/Song1");
            song2 = Content.Load<Song>("Song/Song2");

            //Video
            video = Content.Load<Video>("Intro");
            playerVideo = new VideoPlayer();

            //Menu
            Menu.LoadContent(Content, screenWidth, screenHeight);

            //Playing
            Playing.LoadContent(Content, screenWidth, screenHeight);

            //Fight
            Fight.LoadContent(Content, spriteBatch, screenWidth, screenHeight);

            //Enemy
            enemy1 = new Enemy(Content.Load<Texture2D>("Sprites/enemy"), new Vector2(800, 600), new Rectangle(815, 600, 50, 62), new Rectangle(0, 0, 111, 62), 200, 50);
            enemy2 = new Enemy(Content.Load<Texture2D>("Sprites/enemy"), new Vector2(350, 500), new Rectangle(365, 500, 50, 62), new Rectangle(0, 0, 111, 62), 800, 50 );
            enemy3 = new Enemy(Content.Load<Texture2D>("Sprites/enemy3"), new Vector2(500, 570), new Rectangle(515, 585, 55,90), new Rectangle(0, 0, 78, 105), 300, 100);
            enemy4 = new Enemy(Content.Load<Texture2D>("Sprites/enemy4"), new Vector2(550, 150), new Rectangle(550, 165, 55, 90), new Rectangle(0, 0, 78, 105), 100, 20);

            //PNJ
            pnj1 = new PNJ(Content.Load<Texture2D>("Sprites/PnjAnimation"), new Vector2(1110, 290), new Rectangle(1110, 290, 276, 378), new Vector2(1110, 290), new Rectangle(1100, 290, 69, 150));
            healer = new PNJ(Content.Load<Texture2D>("Sprites/Healer"), new Vector2(1250, 550), new Rectangle(1250, 550, 658, 696), new Vector2(1250, 550), new Rectangle(1270, 550, 85, 80));
            pnjShop1 = new PNJ(Content.Load<Texture2D>("Tile/Tile195"), new Vector2(705,288), new Rectangle(0,0 ,32,32),new Vector2(300,300),new Rectangle(300,300,32,32));
            pnjShop2 = new PNJ(Content.Load<Texture2D>("Sprites/pnjShop2"), new Vector2(705, 288), new Rectangle(0, 0, 32,46), new Vector2(300, 300), new Rectangle(300, 300, 38, 50));
            //Pause
            Pause.LoadContent(Content, screenWidth, screenHeight);

            //Options
            Options.LoadContent(Content, screenWidth, screenHeight);

            //TexteHealth
            spriteFont = Content.Load<SpriteFont>("SpriteFont1");

            //btnshop
            btnArmors = new cButton(Content.Load<Texture2D>("Button/btnArmors"),170,45);
            btnArmors.setPosition(new Vector2(525,280));
            btnWeapons = new cButton(Content.Load<Texture2D>("Button/btnWeapons"), 170, 45);
            btnWeapons.setPosition(new Vector2(620, 230));
            btnPotions = new cButton(Content.Load<Texture2D>("Button/btnPotions"), 170, 45);
            btnPotions.setPosition(new Vector2(735, 280));

            btnBuyArmor = new cButton(Content.Load<Texture2D>("Button/Buy"), 170, 45);
            btnBuyArmor.setPosition(new Vector2(10, screenHeight / 2 - 125));
            btnSellArmor = new cButton(Content.Load<Texture2D>("Button/Sell"), 170, 45);
            btnSellArmor.setPosition(new Vector2(10, screenHeight / 2-35));

            btnBuyWeapon = new cButton(Content.Load<Texture2D>("Button/Buy"), 170, 45);
            btnBuyWeapon.setPosition(new Vector2(screenWidth-370, 95));
            btnSellWeapon = new cButton(Content.Load<Texture2D>("Button/Sell"), 170, 45);
            btnSellWeapon.setPosition(new Vector2(screenWidth-370, 180));

            btnBuyPot = new cButton(Content.Load<Texture2D>("Button/Buy"), 170, 45);
            btnBuyPot.setPosition(new Vector2(screenWidth - 430, screenHeight - 155));
            btnSellPot = new cButton(Content.Load<Texture2D>("Button/Sell"), 170, 45);
            btnSellPot.setPosition(new Vector2(screenWidth - 430, screenHeight - 60));

            btnDoneArmor = new cButton(Content.Load<Texture2D>("Button/Done"), 170, 45);
            btnDoneArmor.setPosition(new Vector2(60, screenHeight / 2 - 80));

            btnDoneWeapon = new cButton(Content.Load<Texture2D>("Button/Done"), 170, 45);
            btnDoneWeapon.setPosition(new Vector2(screenWidth - 350,135));

            btnDonePot = new cButton(Content.Load<Texture2D>("Button/Done"), 170, 45);
            btnDonePot.setPosition(new Vector2(screenWidth - 430, screenHeight - 110));
            
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            presentKey = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);

            /* if (CurrentGameState == GameState.Playing && !inventaire && !talking)
             {
                 IsMouseVisible = false;
             }
             else
             {
                 IsMouseVisible = true;
             }*/

            switch (CurrentGameState)
            {
                case GameState.Video:

                    if (playOnce)
                    {
                        playOnce = false;
                    }

                    if (presentKey.IsKeyDown(Keys.Enter) && pastKey.IsKeyUp(Keys.Enter))
                    {
                        CurrentGameState = GameState.MainMenu;
                        MediaPlayer.Play(song1);
                    }
                    break;

                case GameState.Title:

                    if (presentKey.IsKeyDown(Keys.Enter))
                    {
                        CurrentGameState = GameState.Video;
                        playerVideo.Play(video);
                    }
                    break;

                case GameState.MainMenu:

                    CurrentGameState = Menu.Update(gameTime);
                    break;

                case GameState.Playing:

                    CurrentGameState = Playing.Update(gameTime, screenWidth, screenHeight, graphics);
                    break;

                case GameState.Pause:

                    CurrentGameState = Pause.Update(gameTime);

                    break;

                case GameState.Options:

                    CurrentGameState = Options.Update(gameTime);

                    break;

                case GameState.Fight:

                    CurrentGameState = Fight.Update(gameTime, screenWidth, screenHeight);

                    break;

                case GameState.GameOver:
                    if (presentKey.IsKeyDown(Keys.Escape))
                    {
                        this.Exit();
                    }

                    break;
            }

            pastKey = presentKey;
            pastMouse = mouse;
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            switch (CurrentGameState)
            {
                case GameState.Video:
                    Texture2D videoTexture = playerVideo.GetTexture();
                    spriteBatch.Draw(videoTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    break;
                case GameState.Title:
                    {
                        spriteBatch.Draw(Content.Load<Texture2D>("Menu/Title"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    }

                    break;

                case GameState.MainMenu:

                    Menu.Draw(gameTime, spriteBatch, screenWidth, screenHeight);

                    break;

                case GameState.Playing:

                    Playing.Draw(gameTime, spriteBatch, screenWidth, screenHeight);
                    break;

                case GameState.Pause:

                    Playing.Draw(gameTime, spriteBatch, screenWidth, screenHeight);
                    Pause.Draw(gameTime, spriteBatch, screenWidth, screenHeight);

                    break;

                case GameState.Options:

                    Options.Draw(gameTime, spriteBatch, screenWidth, screenHeight);

                    break;

                case GameState.Fight:
                    Fight.Draw(gameTime, spriteBatch, screenWidth, screenHeight);
                    break;

                case GameState.GameOver:

                    spriteBatch.Draw(Content.Load<Texture2D>("Menu/GameOver"), new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
                    break;

            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
