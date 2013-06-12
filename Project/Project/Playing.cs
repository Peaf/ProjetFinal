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
    static class Playing
    {
        public static Texture2D maison, speechBoxTexture, speechBoxTexture2, bookTexture, inventaireTexture, healthPotionTexture, manaPotionTexture, swordTexture, armorTexture, QuestBookTexture, cactusTexture;
        public static int mapNumber, timerInventaire, nbjoueurs;
        static string line;
        static int[,] tab_map8;
        static int[,] tab_map5;
        static int[,] tab_map4;
        static int[,] tab_map2;
        static int[,] tab_map6;
        public static Map map;
        public static Map map5, map8, map4, map2, map6;
        static StreamReader streamMap8;
        static StreamReader streamMap5;
        static StreamReader streamMap4;
        static StreamReader streamMap2;
        static StreamReader streamMap6;
        static bool Isfighting, talking, lvlUp, talkOnce;
        static public bool inventaire;
        static int turn, lvlBefore, j,cactus;
        static Song song3;
        public static Rectangle speechBoxRectangle, speechBoxRectangle2, bookRectangle, inventaireRectangle, cactusRectangle, cactusRectangle2, cactusRectangle3, cactusRectangle4, cactusRectangle5;
        static string attackChoisi;
        static KeyboardState presentKey, pastKey;

        public static void Initialize()
        {
            mapNumber = 5;
            timerInventaire = 0;

            tab_map8 = new int[26, 44];
            tab_map5 = new int[26, 44];
            tab_map4 = new int[26, 44];
            tab_map2 = new int[26, 44];
            tab_map6 = new int[26, 44];
            map = new Map();
            map5 = new Map();
            map8 = new Map();
            map4 = new Map();
            map2 = new Map();
            map6 = new Map();
            streamMap8 = new StreamReader("map8.txt");
            streamMap5 = new StreamReader("map5.txt");
            streamMap4 = new StreamReader("map4.txt");
            streamMap2 = new StreamReader("map2.txt");
            streamMap6 = new StreamReader("map6.txt");
            Isfighting = false;
            talking = false;
            lvlUp = false;
            talkOnce = false;
            inventaire = false;
            turn = -1;
            lvlBefore = 1;
            j = 0;

            attackChoisi = "";

        }
        public static void LoadContent(ContentManager Content, int screenWidth, int screenHeight)
        {

            Tiles.Content = Content;

            Game1.btnNext = new cButton(Content.Load<Texture2D>("Button/Next"), 75, 44);
            Game1.btnNext.setPosition(new Vector2(1200, 700));

            Game1.btnEndFight = new cButton(Content.Load<Texture2D>("Button/EndFight"), 75, 44);
            Game1.btnEndFight.setPosition(new Vector2(1190, 685));

            song3 = Content.Load<Song>("Song/SongFight");
            maison = Content.Load<Texture2D>("Tile/Maison");
            //inventaire
            inventaireTexture = Content.Load<Texture2D>("Menu/inventaire");
            inventaireRectangle = new Rectangle(0, 0, inventaireTexture.Width, Game1.screenHeight);

            //speech 
            speechBoxTexture = Content.Load<Texture2D>("SpeechBox");
            speechBoxRectangle = new Rectangle(0, 675, speechBoxTexture.Width, speechBoxTexture.Height);

            speechBoxTexture2 = Content.Load<Texture2D>("SpeechBox");
            speechBoxRectangle2 = new Rectangle(0, 0, speechBoxTexture.Width, speechBoxTexture.Height + 30);

            //Book
            bookTexture = Content.Load<Texture2D>("Book");
            bookRectangle = new Rectangle(200, 330, bookTexture.Width, bookTexture.Height);

            //cactus
            cactusTexture = Content.Load<Texture2D>("Tile/Tile102");
            cactusRectangle = new Rectangle(200, 315, cactusTexture.Width, cactusTexture.Height);
            cactusRectangle2 = new Rectangle(370, 460, cactusTexture.Width, cactusTexture.Height);
            cactusRectangle3 = new Rectangle(430, 130, cactusTexture.Width, cactusTexture.Height);
            cactusRectangle4 = new Rectangle(950, 370, cactusTexture.Width, cactusTexture.Height);
            cactusRectangle5 = new Rectangle(820, 615, cactusTexture.Width, cactusTexture.Height);


            //Potion
            healthPotionTexture = Content.Load<Texture2D>("healthPotion");
            manaPotionTexture = Content.Load<Texture2D>("manaPotion");

            //Sword
            swordTexture = Content.Load<Texture2D>("Sword");

            //Armor
            armorTexture = Content.Load<Texture2D>("Armor");

            //QuestBook
            QuestBookTexture = Content.Load<Texture2D>("QuestBook");

            //Moteur à particules
            Game1.snow = new ParticleGenerator(Content.Load<Texture2D>("snow"), screenWidth, 50); // verifier le 2 nd arg
            Game1.sand = new ParticleGenerator1(Content.Load<Texture2D>("sand"), screenWidth, 50); // verifier le 2 nd arg


            while ((line = streamMap8.ReadLine()) != null)
            {
                char[] splitchar = { ',' };
                line = line.TrimEnd(splitchar); // enleve tout les caracteres "splichar" de la fin
                string[] tiles = line.Split(splitchar);

                for (int i = 0; i < tab_map8.GetUpperBound(1); i++) //Upperbound donne le nbres d'elts d'un tab suivant cette dimension 1 par exemple represente lenbre de colonne par ligne
                {

                    tab_map8[j, i] = int.Parse(tiles[i]);
                }
                j++;
            }

            map8.Generate(tab_map8, 32);
            streamMap8.Close();

            j = 0;

            while ((line = streamMap5.ReadLine()) != null)
            {
                char[] splitchar = { ',' };
                line = line.TrimEnd(splitchar); // enleve tout les caracteres "splichar" de la fin
                string[] tiles = line.Split(splitchar);

                for (int i = 0; i < tab_map5.GetUpperBound(1); i++) //Upperbound donne le nbres d'elts d'un tab suivant cette dimension 1 par exemple represente lenbre de colonne par ligne
                {

                    tab_map5[j, i] = int.Parse(tiles[i]);
                }
                j++;
            }

            map5.Generate(tab_map5, 32);
            streamMap5.Close();

            j = 0;
            while ((line = streamMap4.ReadLine()) != null)
            {
                char[] splitchar = { ',' };
                line = line.TrimEnd(splitchar); // enleve tout les caracteres "splichar" de la fin
                string[] tiles = line.Split(splitchar);

                for (int i = 0; i < tab_map4.GetUpperBound(1); i++) //Upperbound donne le nbres d'elts d'un tab suivant cette dimension 1 par exemple represente lenbre de colonne par ligne
                {

                    tab_map4[j, i] = int.Parse(tiles[i]);
                }
                j++;
            }

            map4.Generate(tab_map4, 32);
            streamMap4.Close();

            j = 0;
            while ((line = streamMap2.ReadLine()) != null)
            {
                char[] splitchar = { ',' };
                line = line.TrimEnd(splitchar); // enleve tout les caracteres "splichar" de la fin
                string[] tiles = line.Split(splitchar);

                for (int i = 0; i < tab_map2.GetUpperBound(1); i++) //Upperbound donne le nbres d'elts d'un tab suivant cette dimension 1 par exemple represente lenbre de colonne par ligne
                {

                    tab_map2[j, i] = int.Parse(tiles[i]);
                }
                j++;
            }

            map2.Generate(tab_map2, 32);
            streamMap2.Close();

            j = 0;
            while ((line = streamMap6.ReadLine()) != null)
            {
                char[] splitchar = { ',' };
                line = line.TrimEnd(splitchar); // enleve tout les caracteres "splichar" de la fin
                string[] tiles = line.Split(splitchar);

                for (int i = 0; i < tab_map6.GetUpperBound(1); i++) //Upperbound donne le nbres d'elts d'un tab suivant cette dimension 1 par exemple represente lenbre de colonne par ligne
                {

                    tab_map6[j, i] = int.Parse(tiles[i]);
                }
                j++;
            }

            map6.Generate(tab_map6, 32);
            streamMap6.Close();

            map = map5;
        }

        public static Game1.GameState Update(GameTime gameTime, int screenWidth, int screenHeight, GraphicsDeviceManager graphics, int newNbjoueurs)
        {
            nbjoueurs = newNbjoueurs;
            //Item
            Item book = new Item("QuestItem", "Book", "", 0, 1, "");
            Isfighting = false;
            MouseState mouse = Mouse.GetState();
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
            KeyboardState KState = Keyboard.GetState();
            Game1.GameState CurrentGameState = Game1.GameState.Playing1;

            if (!inventaire)
            {
                foreach (CollisionTiles tile in map.CollisionTiles)
                {
                    if ((tile.num >= 7 && tile.num < 20) || tile.num >= 100)
                    {
                        Game1.player.Collision(tile.Rectangle);
                        if (nbjoueurs == 2) Game1.player2.Collision(tile.Rectangle);
                    }
                }

                if (map == map2)
                {

                    //Pnj
                    if (!Game1.healer.Collision(Game1.healer))
                    {
                        Game1.healer.Update(gameTime, 0, "map2");
                    }

                    if (Game1.healer.Collision(Game1.healer))
                    {
                        Game1.healer.Update(gameTime, 1, "map2");
                        if (Game1.player.health < Game1.player.healthMax)
                        {

                            Game1.player.Gold -= 5;
                        }
                        if (Game1.player.health == Game1.player.healthMax && Game1.questState == 3)
                        {
                            Game1.questState = 4;
                        }
                        if (Game1.questState == 5)
                        {
                            Game1.player.Gold += 30;
                            Game1.player.Experience += 70;
                            Game1.questState++;
                        }
                        Game1.player.health = Game1.player.healthMax;

                    }
                }
                if (map == map4)
                {
                    Game1.sand.update(gameTime, graphics.GraphicsDevice);
                    if (Game1.questState == 4)
                    {
                        if (Game1.player.persoRectangle.Intersects(cactusRectangle) && KState.IsKeyDown(Keys.F))
                        {
                            cactusRectangle = new Rectangle(0, 0, 0, 0);
                            Game1.invent.addItem(book);
                            cactus++;
                        }
                        if (Game1.player.persoRectangle.Intersects(cactusRectangle2) && KState.IsKeyDown(Keys.F))
                        {
                            cactusRectangle2 = new Rectangle(0, 0, 0, 0);
                            Game1.invent.addItem(book);
                            cactus++;
                        }
                        if (Game1.player.persoRectangle.Intersects(cactusRectangle3) && KState.IsKeyDown(Keys.F))
                        {
                            cactusRectangle3 = new Rectangle(0, 0, 0, 0);
                            Game1.invent.addItem(book);
                            cactus++;
                        }
                        if (Game1.player.persoRectangle.Intersects(cactusRectangle4) && KState.IsKeyDown(Keys.F))
                        {
                            cactusRectangle4 = new Rectangle(0, 0, 0, 0);
                            Game1.invent.addItem(book);
                            cactus++;
                        }
                        if (Game1.player.persoRectangle.Intersects(cactusRectangle5) && KState.IsKeyDown(Keys.F))
                        {
                            cactusRectangle5 = new Rectangle(0, 0, 0, 0);
                            Game1.invent.addItem(book);
                            cactus++;
                        }
                        if (cactus == 5)
                        {
                            Game1.questState = 5;
                        }

                    }

                }

                if (map == map5)
                {

                    //Enemy
                    if (Game1.enemy1.health > 0)
                        Game1.enemy1.Update(gameTime, Game1.player.persoPosition);

                    if (Game1.enemy2.health > 0)
                        Game1.enemy2.Update(gameTime, Game1.player.persoPosition);

                    if (Game1.enemy1.Collision())
                    {
                        Game1.previousPosX = Game1.player.persoPosition.X;
                        Game1.previousPosY = Game1.player.persoPosition.Y;
                        turn = -1;
                        Game1.btnEndFight.isClicked = false;
                        Game1.btnStartFight.isClicked = false;
                        Isfighting = true;
                        MediaPlayer.Play(song3);
                        Game1.enemy = Game1.enemy1;
                        attackChoisi = "";
                    }
                    else if (Game1.enemy2.Collision())
                    {
                        Game1.previousPosX = Game1.player.persoPosition.X;
                        Game1.previousPosY = Game1.player.persoPosition.Y;
                        turn = -1;
                        Isfighting = true;
                        MediaPlayer.Play(song3);
                        Game1.btnEndFight.isClicked = false;
                        Game1.btnStartFight.isClicked = false;
                        Game1.enemy = Game1.enemy2;
                        attackChoisi = "";
                    }


                }

                if (map == map6)
                {
                    if (Game1.enemy3.health > 0)
                        Game1.enemy3.Update(gameTime, Game1.player.persoPosition);
                    if (Game1.enemy3.Collision())
                    {
                        Game1.previousPosX = Game1.player.persoPosition.X;
                        Game1.previousPosY = Game1.player.persoPosition.Y;
                        turn = -1;
                        Game1.btnEndFight.isClicked = false;
                        Game1.btnStartFight.isClicked = false;
                        Isfighting = true;
                        MediaPlayer.Play(song3);
                        Game1.enemy = Game1.enemy3;
                        attackChoisi = "";
                    }


                }
                if (map == map8)
                {

                    if (Game1.enemy4.health > 0)
                        Game1.enemy4.Update(gameTime, Game1.player.persoPosition);

                    if (Game1.enemy4.Collision())
                    {
                        Game1.previousPosX = Game1.player.persoPosition.X;
                        Game1.previousPosY = Game1.player.persoPosition.Y;
                        turn = -1;
                        Isfighting = true;
                        MediaPlayer.Play(song3);
                        Game1.btnEndFight.isClicked = false;
                        Game1.btnStartFight.isClicked = false;
                        Game1.enemy = Game1.enemy4;
                        attackChoisi = "";
                    }
                    //Moteur à particules
                    Game1.snow.update(gameTime, graphics.GraphicsDevice);

                    if (Game1.player.persoRectangle.Intersects(bookRectangle) && KState.IsKeyDown(Keys.F))
                    {
                        Game1.questState = 1;
                        Game1.invent.addItem(book);
                        bookRectangle = new Rectangle(0, 0, 0, 0);
                    }
                }
                if (Game1.player.persoPosition.Y <= 0)
                {
                    if (map == map5)
                    {
                        map = map8;
                        Game1.player.persoPosition.Y = (screenHeight - Game1.player.persoTexture.Height / 8);

                    }
                    else if (map == map2)
                    {
                        map = map5;
                        Game1.player.persoPosition.Y = (screenHeight - Game1.player.persoTexture.Height / 8);
                    }
                    else
                    {
                        Game1.player.persoPosition.Y = 2;

                    }
                    if (nbjoueurs == 2) Game1.player2.persoPosition = Game1.player.persoPosition;

                }
                else if (Game1.player.persoPosition.Y >= screenHeight - Game1.player.persoTexture.Height / 8)
                {
                    if (map == map8)
                    {
                        map = map5;
                        Game1.player.persoPosition.Y = Game1.player.persoTexture.Height / 8 - 40;
                    }
                    else if (map == map5)
                    {
                        map = map2;
                        Game1.player.persoPosition.Y = Game1.player.persoTexture.Height / 8 - 40;
                    }
                    else
                    {
                        Game1.player.persoPosition.Y = screenHeight - Game1.player.persoTexture.Height / 8 - 1;

                    }
                    if (nbjoueurs == 2) Game1.player2.persoPosition = Game1.player.persoPosition;
                }
                else if (Game1.player.persoPosition.X <= 0)
                {
                    if (map == map5)
                    {
                        map = map4;
                        Game1.player.persoPosition.X = screenWidth - Game1.player.persoTexture.Width / 4;

                    }
                    else if (map == map6)
                    {
                        map = map5;;
                        Game1.player.persoPosition.X = screenWidth - Game1.player.persoTexture.Width / 4;
                    }
                    else
                    {
                        Game1.player.persoPosition.X = 2;
                    }
                    if (nbjoueurs == 2) Game1.player2.persoPosition = Game1.player.persoPosition;

                }
                else if (Game1.player.persoPosition.X >= screenWidth - Game1.player.persoTexture.Width / 4)
                {
                    if (map == map4)
                    {
                        map = map5;
                        Game1.player.persoPosition.X = 0;

                    }
                    else if (map == map5)
                    {
                        map = map6;
                        Game1.player.persoPosition.X = 0;
                    }
                    else
                    {
                        Game1.player.persoPosition.X = screenWidth - Game1.player.persoTexture.Width / 4;

                    }
                    if (nbjoueurs == 2) Game1.player2.persoPosition = Game1.player.persoPosition;
                }

                if (nbjoueurs == 2)
                {
                    if (Game1.player2.persoPosition.Y <= 0)
                    {
                        Game1.player2.persoPosition.Y = 2;
                    }
                    else if (Game1.player2.persoPosition.Y >= screenHeight - Game1.player2.persoTexture.Height / 8)
                    {
                        Game1.player2.persoPosition.Y = screenHeight - Game1.player2.persoTexture.Height / 8 - 1;

                    }
                    else if (Game1.player2.persoPosition.X <= 0)
                    {
                        Game1.player2.persoPosition.X = 2;

                    }
                    else if (Game1.player2.persoPosition.X >= screenWidth - Game1.player2.persoTexture.Width / 4)
                    {

                        Game1.player2.persoPosition.X = screenWidth - Game1.player2.persoTexture.Width / 4;

                    }
                }

                if (Isfighting)
                {
                    CurrentGameState = Game1.GameState.Fight;
                }
                if (lvlBefore != Game1.player.Lvl)
                {
                    lvlUp = true;
                }
                timerInventaire++;

                //Perso        
                Game1.player.Update(gameTime, Game1.GameState.Playing1);
                if (nbjoueurs == 2) Game1.player2.Update(gameTime, Game1.GameState.Playing2);

                //PNJ
                Game1.player.Collision(Game1.healer.taille);
                Game1.player.Collision(Game1.pnj1.taille);

                if (nbjoueurs == 2) Game1.player2.Collision(Game1.healer.taille);
                if (nbjoueurs == 2) Game1.player2.Collision(Game1.pnj1.taille);

                if (Game1.pnj1.Collision(Game1.pnj1))
                {
                    talking = true;
                }
                if (Game1.questState == 0)
                {
                    Game1.pnj1.Update(gameTime, 0, "map8");
                }
                if (talking)
                {
                    Game1.btnNext.Update(mouse, gameTime);
                    if (Game1.questState == 1)
                    {
                        Game1.pnj1.Update(gameTime, 1, "map8");
                    }
                    if (Game1.btnNext.isClicked && Game1.questState == 1)
                    {
                        Game1.questState = 2;
                        Game1.player.Experience += 60;
                        Game1.player.Gold += 20;
                        Game1.btnNext.isClicked = false;
                    }
                    if (Game1.questState == 2)
                    {
                        Game1.btnNext.isClicked = false;
                        Game1.invent.removeItem(book);
                        Game1.pnj1.Update(gameTime, 2, "map8");
                    }
                    if (Game1.questState == 2 && Game1.enemy1.health == 0 && Game1.enemy2.health == 0 && Game1.enemy3.health == 0)
                    {
                        Game1.pnj1.Update(gameTime, 2, "map8");
                        Game1.player.Gold += 25;
                        Game1.questState = 3;
                    }

                    if (!Game1.pnj1.Collision(Game1.pnj1))
                        talking = false;
                }


                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    CurrentGameState = Game1.GameState.Pause;
                }

                if (timerInventaire > 15)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.I))
                    {
                        inventaire = true;
                        timerInventaire = 0;
                    }
                }
            }
            else if (inventaire)
            {
                timerInventaire++;
                if (timerInventaire > 15)
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.I) || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    {
                        inventaire = false;
                        timerInventaire = 0;
                    }
                }
                foreach (Item item in Game1.invent.tablObjects)
                {
                    if (mouseRectangle.Intersects(new Rectangle((item.place % 6) * 68 + 25, 482 + 68 * (item.place / 6), 39, 64)) && (mouse.LeftButton == ButtonState.Pressed) && Game1.pastMouse.LeftButton == ButtonState.Released)
                    {
                        Game1.invent.useItem(item);

                    }
                }
                foreach (Item item in Game1.invent.tablEquiped)
                {
                    if (item.type == "Weapon")
                    {
                        if (mouseRectangle.Intersects(new Rectangle(30, 320, swordTexture.Width / 7, swordTexture.Height / 7)) && (mouse.LeftButton == ButtonState.Pressed) && Game1.pastMouse.LeftButton == ButtonState.Released)
                        {
                            /* Game1.invent.removeItemE(item);
                             Game1.invent.addItem((new Item("Weapon","Sword","dmg", 30, 1,"notequiped")));*/

                            Game1.invent.deUseItem(item);
                        }
                    }
                    else
                    {
                        if (mouseRectangle.Intersects(new Rectangle(120, 125, armorTexture.Width, armorTexture.Height)) && (mouse.LeftButton == ButtonState.Pressed) && Game1.pastMouse.LeftButton == ButtonState.Released)
                        {

                            Game1.invent.deUseItem(item);
                        }
                    }
                }
            }
            Game1.pastMouse = mouse;

            return (CurrentGameState);
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch, int screenWidth, int screenHeight, int nbjoueur)
        {
            presentKey = Keyboard.GetState();
            map.Draw(spriteBatch);
            if (map == map5)
            {
                spriteBatch.Draw(maison, new Rectangle(32 * 11, 0, 96, 128), Color.White);
                if (Game1.enemy1.health > 0)
                    Game1.enemy1.Draw(spriteBatch);
                if (Game1.enemy2.health > 0)
                    Game1.enemy2.Draw(spriteBatch);
            }

            if (lvlUp)
            {
                spriteBatch.Draw(speechBoxTexture2, speechBoxRectangle2, Color.White);
                spriteBatch.DrawString(Game1.spriteFont, "You leveled up !!!", new Vector2(10, 10), Color.Black);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "Level : " + Game1.player.Lvl, new Vector2(10, 35), Color.Black);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "Health : " + Game1.player.health + "/" + Game1.player.healthMax, new Vector2(10, 60), Color.Black);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "Experience " + Game1.player.Experience + "/" + (Game1.player.Lvl * 100), new Vector2(10, 95), Color.Black);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "Press Enter to continue", new Vector2(1100, 95), Color.Black);

                if (presentKey.IsKeyDown(Keys.Enter) && pastKey.IsKeyUp(Keys.Enter))
                {
                    lvlUp = false;
                    lvlBefore = Game1.player.Lvl;
                }
                pastKey = presentKey;
            }
            if (map == map2)
            {
                Game1.healer.Draw(spriteBatch, 0, "map2");
                if (Game1.questState == 4 && Game1.healer.Collision(Game1.healer))
                {
                    spriteBatch.Draw(speechBoxTexture, speechBoxRectangle, Color.White);
                    spriteBatch.DrawString(Game1.spriteFont, "Zuras: Here you go little warrior. Can I Ask you something? I need plants to heal people.", new Vector2(10, 700), Color.Blue);
                    spriteBatch.DrawString(Game1.spriteFont, "Zuras : But now I'm old and it's so far away. Can you go in the desert and pick 5 cactus for me? ", new Vector2(10, 725), Color.Blue);
                    spriteBatch.DrawString(Game1.spriteFont, "Claudius : Sure. I'm on my way ", new Vector2(10, 750), Color.Black);
                }
                if (Game1.questState == 5 && Game1.healer.Collision(Game1.healer))
                {
                    spriteBatch.Draw(speechBoxTexture, speechBoxRectangle, Color.White);
                    spriteBatch.DrawString(Game1.spriteFont, "Zuras: Thank you little human. Here's you reward", new Vector2(10, 700), Color.Blue);
                    spriteBatch.DrawString(Game1.spriteFont, "+ 30 Gold", new Vector2(1100, 695), Color.Gold);
                    spriteBatch.DrawString(Game1.spriteFont, "+ 70 Xp", new Vector2(1110, 725), Color.Red);
                }

            }


            //GraphicsDevice.Clear(Color.CornflowerBlue);
            Game1.player.Draw(spriteBatch, Game1.GameState.Playing1);
            if (nbjoueur == 2)
                Game1.player2.Draw(spriteBatch, Game1.GameState.Playing2);

            if (map == map4)
            {
                Game1.sand.Draw(spriteBatch);
                spriteBatch.Draw(cactusTexture, cactusRectangle, Color.Blue);
                spriteBatch.Draw(cactusTexture, cactusRectangle2, Color.Blue);
                spriteBatch.Draw(cactusTexture, cactusRectangle3, Color.Blue);
                spriteBatch.Draw(cactusTexture, cactusRectangle4, Color.Blue);
                spriteBatch.Draw(cactusTexture, cactusRectangle5, Color.Blue);
            }

            if (map == map6)
            {
                if (Game1.enemy3.health > 0)
                    Game1.enemy3.Draw(spriteBatch);
            }
            if (map == map8)
            {
                Game1.snow.Draw(spriteBatch);
                if (Game1.questState != 2 && !talking)
                {
                    Game1.pnj1.Draw(spriteBatch, 0, "map8");
                }
                if (Game1.questState == 2)
                {
                    Game1.pnj1.Draw(spriteBatch, 2, "map8");
                }
                if (Game1.questState == 0)
                {
                    spriteBatch.Draw(bookTexture, bookRectangle, Color.White);
                    if (Game1.player.persoRectangle.Intersects(bookRectangle))
                    {
                        spriteBatch.Draw(speechBoxTexture, speechBoxRectangle, Color.White);
                        spriteBatch.DrawString(Game1.spriteFont, "Press F to pick up the book", new Vector2(10, 700), Color.Blue);
                    }
                }
                if (talking && Game1.questState == 0)
                {
                    Game1.pnj1.Draw(spriteBatch, 0, "map8");
                    spriteBatch.Draw(speechBoxTexture, speechBoxRectangle, Color.White);
                    spriteBatch.DrawString(Game1.spriteFont, "Arha: I'm Arha can you help me? I'm freezing and I've lost my spell book can you find it for me? I can't leave this place without it", new Vector2(10, 700), Color.Blue);
                    talkOnce = true;
                }
                if (talking && Game1.questState == 1)
                {
                    spriteBatch.Draw(speechBoxTexture, speechBoxRectangle, Color.White);
                    Game1.btnNext.Draw(spriteBatch);
                    Game1.pnj1.Draw(spriteBatch, 1, "map8");
                    spriteBatch.DrawString(Game1.spriteFont, "Arha: Thanks a lot. Here take this gold as a reward.", new Vector2(10, 700), Color.Blue);
                    spriteBatch.DrawString(Game1.spriteFont, "Arha: Can you do something else for me? I've seen some ennemies in the south can you kill them I want to go home?", new Vector2(10, 725), Color.Blue);
                    spriteBatch.DrawString(Game1.spriteFont, "+ 20 Gold", new Vector2(1100, 695), Color.Gold);
                    spriteBatch.DrawString(Game1.spriteFont, "+ 60 Xp", new Vector2(1110, 725), Color.Red);
                }
                if (talking && Game1.questState == 2)
                {
                    Game1.pnj1.Draw(spriteBatch, 2, "map8");
                    spriteBatch.Draw(speechBoxTexture, speechBoxRectangle, Color.White);
                    spriteBatch.DrawString(Game1.spriteFont, "Arha: Can you kill these ennemies for me? I hope they are not too strong for you", new Vector2(10, 700), Color.Blue);
                    spriteBatch.DrawString(Game1.spriteFont, "Claudius: You underestimate my power", new Vector2(10, 725), Color.Black);
                }
                if (talking && Game1.questState == 3)
                {
                    Game1.pnj1.Draw(spriteBatch, 2, "map8");
                    spriteBatch.Draw(speechBoxTexture, speechBoxRectangle, Color.White);
                    spriteBatch.DrawString(Game1.spriteFont, "Arha: Thanks a lot , but ... but ... you're hurt! Go south you'll find a healer there", new Vector2(10, 700), Color.Blue);
                    spriteBatch.DrawString(Game1.spriteFont, "Arha: Oh and you will need that. He is really greedy he wont heal you for free", new Vector2(10, 725), Color.Blue);
                    spriteBatch.DrawString(Game1.spriteFont, "+ 25 Gold", new Vector2(1100, 725), Color.Gold);
                }

                if (Game1.questState == 6 && talking)
                {
                    Game1.pnj1.Draw(spriteBatch, 2, "map8");
                    spriteBatch.Draw(speechBoxTexture, speechBoxRectangle, Color.White);
                    spriteBatch.DrawString(Game1.spriteFont, "Arha: Good job Claudius your journey ends here you're a hero now ", new Vector2(10, 700), Color.Blue);
                }

                if (Game1.enemy4.health > 0)
                    Game1.enemy4.Draw(spriteBatch);
            }

            if (presentKey.IsKeyDown(Keys.L))
            {
                spriteBatch.Draw(QuestBookTexture, new Rectangle(0, 0, QuestBookTexture.Width, QuestBookTexture.Height), Color.White);
                if (Game1.questState == 0 && talkOnce)
                {
                    spriteBatch.DrawString(Game1.spriteFont, "Find Arha's book in the East part of the map", new Vector2(95, 50), Color.Red);
                }
                if (Game1.questState == 1)
                {
                    spriteBatch.DrawString(Game1.spriteFont, "Find Arha's book in the East part of the map", new Vector2(95, 50), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Bring the book back to Arha", new Vector2(95, 80), Color.Red);
                }
                if (Game1.questState == 2)
                {
                    spriteBatch.DrawString(Game1.spriteFont, "Find Arha's book in the East part of the map", new Vector2(95, 50), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Bring the book back to Arha", new Vector2(95, 80), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Kill the enemies in the south", new Vector2(95, 110), Color.Red);
                }
                if (Game1.questState == 2 && Game1.enemy1.health == 0 && Game1.enemy2.health == 0 && Game1.enemy3.health == 0)
                {
                    spriteBatch.DrawString(Game1.spriteFont, "Find Arha's book in the East part of the map", new Vector2(95, 50), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Bring the book back to Arha", new Vector2(95, 80), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Kill the enemies in the south", new Vector2(95, 110), Color.Green);
                }
                if (Game1.questState == 3)
                {
                    spriteBatch.DrawString(Game1.spriteFont, "Find Arha's book in the East part of the map", new Vector2(95, 50), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Bring the book back to Arha", new Vector2(95, 80), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Kill the enemies in the south", new Vector2(95, 110), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Find the healer", new Vector2(95, 140), Color.Red);
                }
                if (Game1.questState == 4)
                {
                    spriteBatch.DrawString(Game1.spriteFont, "Find Arha's book in the East part of the map", new Vector2(95, 50), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Bring the book back to Arha", new Vector2(95, 80), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Kill the enemies in the south", new Vector2(95, 110), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Find the healer", new Vector2(95, 140), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Find 5 cactus in the desert and bring them back to Zuras", new Vector2(95, 170), Color.Red);
                }
                if (Game1.questState == 5)
                {
                    spriteBatch.DrawString(Game1.spriteFont, "Find Arha's book in the East part of the map", new Vector2(95, 50), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Bring the book back to Arha", new Vector2(95, 80), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Kill the enemies in the south", new Vector2(95, 110), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Find the healer", new Vector2(95, 140), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Find 5 cactus in the desert and bring them back to Zuras", new Vector2(95, 170), Color.Green);
                }
                if (Game1.questState == 6)
                {
                    spriteBatch.DrawString(Game1.spriteFont, "Find Arha's book in the East part of the map", new Vector2(95, 50), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Bring the book back to Arha", new Vector2(95, 80), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Kill the enemies in the south", new Vector2(95, 110), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Find the healer", new Vector2(95, 140), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Find 5 cactus in the desert and bring them back to Zuras", new Vector2(95, 170), Color.Green);
                    spriteBatch.DrawString(Game1.spriteFont, "Go back to Ahra", new Vector2(95, 200), Color.Red);
                }

            }
            if (inventaire)
            {
                spriteBatch.Draw(inventaireTexture, inventaireRectangle, Color.White);
                /*if (Game1.bookState == 1)
                {
                    spriteBatch.Draw(bookTexture, bookRectangle2, Color.White);
                }*/
                Game1.spriteBatch.DrawString(Game1.spriteFont, "Claudius", new Vector2(520, 18), Color.Black);

                Game1.spriteBatch.DrawString(Game1.spriteFont, "Level : ", new Vector2(445, 45), Color.White);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "" + Game1.player.Lvl, new Vector2(515, 45), Color.White);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "Experience : ", new Vector2(445, 70), Color.White);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "" + Game1.player.Experience + "/" + (Game1.player.Lvl * 150), new Vector2(570, 70), Color.White);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "Health : ", new Vector2(445, 225), Color.Red);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "" + Game1.player.health + "/" + Game1.player.healthMax, new Vector2(530, 225), Color.Red);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "Mana  : ", new Vector2(445, 250), Color.Blue);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "" + Game1.player.mana + "/" + Game1.player.manaMax, new Vector2(530, 250), Color.Blue);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "Intelligence : ", new Vector2(445, 290), Color.Black);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "" + Game1.player.Intelligence, new Vector2(575, 290), Color.Black);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "Armor : ", new Vector2(445, 315), Color.Black);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "" + Game1.player.Armor, new Vector2(575, 315), Color.Black);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "Damage : ", new Vector2(445, 340), Color.Black);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "" + Game1.player.Degat, new Vector2(575, 340), Color.Black);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "Strength : ", new Vector2(445, 365), Color.Black);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "" + Game1.player.Strenght, new Vector2(575, 365), Color.Black);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "Gold : ", new Vector2(445, 400), Color.Gold);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "" + Game1.player.Gold, new Vector2(575, 400), Color.Gold);



                foreach (Item item in Game1.invent.tablObjects)
                {
                    if (item.name != "rien")
                    {
                        Game1.spriteBatch.DrawString(Game1.spriteFont, "" + item.total, new Vector2((item.place % 6) * 68 + 15, 525 + 68 * (item.place / 6)), Color.White);
                        switch (item.name)
                        {
                            case "healthPotion":
                                spriteBatch.Draw(healthPotionTexture, new Rectangle((item.place % 6) * 68 + 25, 482 + 68 * (item.place / 6), 39, 64), Color.White);
                                break;

                            case "manaPotion":
                                spriteBatch.Draw(manaPotionTexture, new Rectangle((item.place % 6) * 68 + 25, 482 + 68 * (item.place / 6), 39, 64), Color.White);
                                break;

                            case "Sword":
                                spriteBatch.Draw(swordTexture, new Rectangle((item.place % 6) * 68 + 25, 482 + 68 * (item.place / 6), 39, 64), Color.White);
                                break;

                            case "Armor":
                                spriteBatch.Draw(armorTexture, new Rectangle((item.place % 6) * 68 + 25, 482 + 68 * (item.place / 6), 39, 64), Color.White);

                                break;

                            case "Book":
                                spriteBatch.Draw(bookTexture, new Rectangle((item.place % 6) * 68 + 25, 482 + 68 * (item.place / 6), 39, 64), Color.White);
                                break;

                        }
                    }
                }
                foreach (Item item in Game1.invent.tablEquiped)
                {
                    switch (item.name)
                    {
                        case "Sword":
                            spriteBatch.Draw(swordTexture, new Rectangle(30, 320, swordTexture.Width / 7, swordTexture.Height / 7), Color.White);
                            break;

                        case "Armor":
                            spriteBatch.Draw(armorTexture, new Rectangle(120, 125, armorTexture.Width, armorTexture.Height), Color.White);
                            break;
                    }
                }
            }
        }
    }
}
