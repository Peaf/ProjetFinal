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
        static cButton btnPlay2, btnOptions2, btnQuit2, btnSave,btnLoad;

        public static void LoadContent(ContentManager Content, int screenWidth, int screenHeight)
        {
            pausedTexture = Content.Load<Texture2D>("Menu/Paused");

            btnPlay2 = new cButton(Content.Load<Texture2D>("Button/PlayButton2"), 100, 75);
            btnPlay2.setPosition(new Vector2(screenWidth / 2 - btnPlay2.size.X / 2, screenHeight / 2));

            btnOptions2 = new cButton(Content.Load<Texture2D>("Button/OptionsButton2"), 250, 100);
            btnOptions2.setPosition(new Vector2(screenWidth / 2 - btnOptions2.size.X / 2, screenHeight / 2 +125));

            btnQuit2 = new cButton(Content.Load<Texture2D>("Button/QuitButton2"), 100, 75);
            btnQuit2.setPosition(new Vector2(screenWidth / 2 - btnQuit2.size.X / 2, screenHeight / 2 + 250));

            btnSave = new cButton(Content.Load<Texture2D>("Button/SaveButton"), 250, 100);
            btnSave.setPosition(new Vector2(screenWidth / 2 - btnSave.size.X / 2, screenHeight / 2 -250));

            btnLoad = new cButton(Content.Load<Texture2D>("Button/LoadButton"), 250, 100);
            btnSave.setPosition(new Vector2(screenWidth / 2 - btnSave.size.X / 2, screenHeight / 2 - 125));
        }

        public static Game1.GameState Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            Game1.GameState CurrentGameState = Game1.GameState.Pause;

            MediaPlayer.Pause();
            btnPlay2.isClicked = false;
            btnPlay2.Update(mouse, gameTime);
            btnOptions2.isClicked = false;
            btnOptions2.Update(mouse, gameTime);

            if (btnPlay2.isClicked)
            {
                CurrentGameState = Game1.GameState.Playing;

                MediaPlayer.Resume();
            }
            else if (btnOptions2.isClicked)
            {

                CurrentGameState = Game1.GameState.Options;
                MediaPlayer.Resume();
            }
            if (btnQuit2.isClicked)
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
            }

            btnPlay2.Update(mouse, gameTime);
            btnOptions2.Update(mouse, gameTime);
            btnQuit2.Update(mouse, gameTime);
            btnSave.Update(mouse, gameTime);
            btnLoad.Update(mouse, gameTime);
            return (CurrentGameState);
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            spriteBatch.Draw(pausedTexture, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            btnPlay2.Draw(spriteBatch);
            btnOptions2.Draw(spriteBatch);
            btnQuit2.Draw(spriteBatch);
            btnSave.Draw(spriteBatch);
            btnLoad.Draw(spriteBatch);
        }

    }
}
