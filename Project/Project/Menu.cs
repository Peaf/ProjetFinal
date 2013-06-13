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

    static class Menu
    {
        static Texture2D ImageMenu;
        static cButton NewGameButton, CoopButton, LoadButton, QuitButton, OptionsButton;
        static Song song2;
        //public GameState CurrentGameState;

        public static void LoadContent(ContentManager Content, int screenWidth, int screenHeight)
        {
            ImageMenu = Content.Load<Texture2D>("Menu/MainMenu");

            NewGameButton = new cButton(Content.Load<Texture2D>("Button/Player1Button"), 250, 100);
            NewGameButton.setPosition(new Vector2(screenWidth / 2 - NewGameButton.size.X / 2, screenHeight / 2 - 200));

            CoopButton = new cButton(Content.Load<Texture2D>("Button/Player2Button"), 250, 100);
            CoopButton.setPosition(new Vector2(screenWidth / 2 - CoopButton.size.X / 2, screenHeight / 2 - 100));

            LoadButton = new cButton(Content.Load<Texture2D>("Button/LoadButton"), 250, 100);
            LoadButton.setPosition(new Vector2(screenWidth / 2 - LoadButton.size.X / 2, screenHeight / 2 ));

            OptionsButton = new cButton(Content.Load<Texture2D>("Button/OptionsButton"), 250, 100);
            OptionsButton.setPosition(new Vector2(screenWidth / 2 - OptionsButton.size.X / 2, screenHeight / 2+100));

            QuitButton = new cButton(Content.Load<Texture2D>("Button/QuitButton"), 250, 100);
            QuitButton.setPosition(new Vector2(screenWidth / 2 - QuitButton.size.X / 2, screenHeight / 2 +200));




        }

        public static Game1.GameState Update(GameTime gameTime)
        {
            MouseState mouse = Mouse.GetState();
            Game1.GameState CurrentGameState = Game1.GameState.MainMenu;
            Game1.playerMenu.Update(gameTime, Game1.GameState.MainMenu);
<<<<<<< HEAD
            if (NewGameButton.isClicked)
            {
                MediaPlayer.Stop();
                Playing.nbjoueurs = 1;
                CurrentGameState = Game1.GameState.Playing;
                MediaPlayer.Play(Game1.song2);
=======
            if (PlayButton.isClicked == true)
            {                
              MediaPlayer.Stop();
              Playing.nbjoueurs = 1;
              CurrentGameState = Game1.GameState.Playing;
              MediaPlayer.Play(song2);
>>>>>>> 554b75c0e0aca83b41e52373258acea180f9144c
            }

            if (CoopButton.isClicked)
            {
                Playing.nbjoueurs = 2;
                CurrentGameState = Game1.GameState.Playing;
                MediaPlayer.Play(Game1.song2);
            }

            if (LoadButton.isClicked)
            {
                Load.Update();
                CurrentGameState = Game1.GameState.Playing;
                MediaPlayer.Play(Game1.song2);

            }

            if (OptionsButton.isClicked)
                CurrentGameState = Game1.GameState.Options;

            if (QuitButton.isClicked)
                Environment.Exit(0);

            OptionsButton.Update(mouse, gameTime);
            QuitButton.Update(mouse, gameTime);
            NewGameButton.Update(mouse, gameTime);
            CoopButton.Update(mouse, gameTime);
            LoadButton.Update(mouse, gameTime);
            return (CurrentGameState);
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            spriteBatch.Draw(ImageMenu, new Rectangle(0, 0, screenWidth, screenHeight), Color.White);
            NewGameButton.Draw(spriteBatch);
            OptionsButton.Draw(spriteBatch);
            QuitButton.Draw(spriteBatch);
            CoopButton.Draw(spriteBatch);
            LoadButton.Draw(spriteBatch);
            Game1.playerMenu.Draw(spriteBatch, Game1.GameState.MainMenu);
        }
    }
}
