﻿using System;
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
    static class Options
    {
        static Texture2D optionsImage;
        static cButton btnVolume0, btnVolume25, btnVolume50, btnVolume75, btnVolume100, btnBack;
        public static int timerOptions = 0;

        public static void LoadContent(ContentManager Content, int screenWidth, int screenHeight)
        {
            optionsImage = Content.Load<Texture2D>("Menu/Options");

            btnVolume0 = new cButton(Content.Load<Texture2D>("Volume/volume0"), 100, 75);
            btnVolume0.setPosition(new Vector2(screenWidth / 8 - btnVolume0.size.X / 2 + 100, screenHeight / 2));

            btnVolume25 = new cButton(Content.Load<Texture2D>("Volume/volume25"), 100, 75);
            btnVolume25.setPosition(new Vector2(screenWidth / 8 - btnVolume25.size.X / 2 + 300, screenHeight / 2));

            btnVolume50 = new cButton(Content.Load<Texture2D>("Volume/volume50"), 100, 75);
            btnVolume50.setPosition(new Vector2(screenWidth / 8 - btnVolume50.size.X / 2 + 500, screenHeight / 2));

            btnVolume75 = new cButton(Content.Load<Texture2D>("Volume/volume75"), 100, 75);
            btnVolume75.setPosition(new Vector2(screenWidth / 8 - btnVolume75.size.X / 2 + 700, screenHeight / 2));

            btnVolume100 = new cButton(Content.Load<Texture2D>("Volume/volume100"), 100, 75);
            btnVolume100.setPosition(new Vector2(screenWidth / 8 - btnVolume100.size.X / 2 + 900, screenHeight / 2));

            btnBack = new cButton(Content.Load<Texture2D>("Button/BackButton"), 250, 100);
            btnBack.setPosition(new Vector2(screenWidth / 2 - btnBack.size.X / 2, screenHeight / 4));

            
        }

        public static Game1.GameState Update(GameTime gameTime)
        {

            MouseState mouse = Mouse.GetState();
            Game1.GameState CurrentGameState = Game1.GameState.Options;
            timerOptions++;
            btnBack.isClicked = false;
            btnBack.Update( gameTime);
            if (timerOptions > 5)
            {
                if (btnBack.isClicked)
                {
                    timerOptions = 0;
                    //Game1.paused = false;
                    if (Game1.optionBackfromMenu)
                        CurrentGameState = Game1.GameState.MainMenu;
                    else
                        CurrentGameState = Game1.GameState.Playing;
                    
                }
                if (btnVolume0.isClicked)
                {
                    MediaPlayer.Volume = 0.0f;
                    btnVolume25.isClicked = false;
                    btnVolume50.isClicked = false;
                    btnVolume75.isClicked = false;
                    btnVolume100.isClicked = false;
                    btnVolume0.Update( gameTime);
                    btnVolume25.Update( gameTime);
                    btnVolume50.Update( gameTime);
                    btnVolume75.Update( gameTime);
                    btnVolume100.Update( gameTime);
                }

                if (btnVolume25.isClicked)
                {
                    MediaPlayer.Volume = 0.25f;
                    btnVolume0.isClicked = false;
                    btnVolume50.isClicked = false;
                    btnVolume75.isClicked = false;
                    btnVolume100.isClicked = false;
                    btnVolume0.Update( gameTime);
                    btnVolume25.Update( gameTime);
                    btnVolume50.Update( gameTime);
                    btnVolume75.Update( gameTime);
                    btnVolume100.Update( gameTime);
                }

                if (btnVolume50.isClicked)
                {
                    MediaPlayer.Volume = 0.5f;
                    btnVolume0.isClicked = false;
                    btnVolume25.isClicked = false;
                    btnVolume75.isClicked = false;
                    btnVolume100.isClicked = false;
                    btnVolume0.Update(gameTime);
                    btnVolume25.Update( gameTime);
                    btnVolume50.Update( gameTime);
                    btnVolume75.Update( gameTime);
                    btnVolume100.Update( gameTime);
                }
                if (btnVolume75.isClicked)
                {
                    MediaPlayer.Volume = 0.75f;
                    btnVolume0.isClicked = false;
                    btnVolume25.isClicked = false;
                    btnVolume50.isClicked = false;

                    btnVolume100.isClicked = false;
                    btnVolume0.Update( gameTime);
                    btnVolume25.Update( gameTime);
                    btnVolume50.Update( gameTime);
                    btnVolume75.Update( gameTime);
                    btnVolume100.Update( gameTime);
                }
                if (btnVolume100.isClicked)
                {
                    MediaPlayer.Volume = 1.0f;
                    btnVolume0.isClicked = false;
                    btnVolume25.isClicked = false;
                    btnVolume50.isClicked = false;
                    btnVolume75.isClicked = false;
                    btnVolume0.Update( gameTime);
                    btnVolume25.Update( gameTime);
                    btnVolume50.Update( gameTime);
                    btnVolume75.Update( gameTime);
                    btnVolume100.Update( gameTime);
                }

                btnVolume0.Update(gameTime);
                btnVolume25.Update( gameTime);
                btnVolume50.Update( gameTime);
                btnVolume75.Update( gameTime);
                btnVolume100.Update( gameTime);
                btnBack.Update( gameTime);
            }

            return (CurrentGameState);
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            spriteBatch.Draw(optionsImage, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            btnVolume0.Draw(spriteBatch);
            btnVolume25.Draw(spriteBatch);
            btnVolume50.Draw(spriteBatch);
            btnVolume75.Draw(spriteBatch);
            btnVolume100.Draw(spriteBatch);
            btnBack.Draw(spriteBatch);
        }



    }
}
