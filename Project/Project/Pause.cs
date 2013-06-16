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
    static class Pause
    {
        static Texture2D pausedTexture;
        static cButton btnBack, btnOptions, btnQuit, btnSave,btnLoad;

        public static void LoadContent(ContentManager Content, int screenWidth, int screenHeight)
        {
            pausedTexture = Content.Load<Texture2D>("Menu/Paused");

            btnBack = new cButton(Content.Load<Texture2D>("Button/BackButton"), 250, 100);
            btnBack.setPosition(new Vector2(screenWidth / 2 - btnBack.size.X / 2, screenHeight / 2));

            btnOptions = new cButton(Content.Load<Texture2D>("Button/OptionsButton"), 250, 100);
            btnOptions.setPosition(new Vector2(screenWidth / 2 - btnOptions.size.X / 2, screenHeight / 2 +125));

            btnQuit = new cButton(Content.Load<Texture2D>("Button/QuitButton"), 250, 100);
            btnQuit.setPosition(new Vector2(screenWidth / 2 - btnQuit.size.X / 2, screenHeight / 2 + 250));

            btnSave = new cButton(Content.Load<Texture2D>("Button/SaveButton"), 250, 100);
            btnSave.setPosition(new Vector2(screenWidth / 2 - btnSave.size.X / 2, screenHeight / 2 -250));

            btnLoad = new cButton(Content.Load<Texture2D>("Button/LoadButton"), 250, 100);
            btnLoad.setPosition(new Vector2(screenWidth / 2 - btnLoad.size.X / 2, screenHeight / 2 - 125));
        }

        public static Game1.GameState Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            Game1.GameState CurrentGameState = Game1.GameState.Pause;

            MediaPlayer.Pause();
            btnBack.isClicked = false;
            btnBack.Update( gameTime);
            btnOptions.isClicked = false;
            btnOptions.Update( gameTime);
            btnLoad.isClicked = false;
            btnLoad.Update(gameTime);
            if (btnBack.isClicked)
            {
                CurrentGameState = Game1.GameState.Playing;
                MediaPlayer.Resume();
            }
            else if (btnOptions.isClicked)
            {
                CurrentGameState = Game1.GameState.Options;
                MediaPlayer.Resume();
            }
            if (btnQuit.isClicked)
            {
                
                Environment.Exit(0);
            }
            if(btnSave.isClicked)
            {
                Save.Update();
            }
            if (btnLoad.isClicked)
            {
                Load.Update();
                CurrentGameState = Game1.GameState.Playing;
                MediaPlayer.Resume();
            }
           
            btnBack.Update( gameTime);
            btnOptions.Update( gameTime);
            btnQuit.Update( gameTime);
            btnSave.Update( gameTime);
            btnLoad.Update( gameTime);
            return (CurrentGameState);
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            spriteBatch.Draw(pausedTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            btnBack.Draw(spriteBatch);
            btnOptions.Draw(spriteBatch);
            btnQuit.Draw(spriteBatch);
            btnSave.Draw(spriteBatch);
            btnLoad.Draw(spriteBatch);
        }

    }
}
