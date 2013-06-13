using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project
{
    public class Character
    {
        public Texture2D persoTexture;
        public Vector2 persoPosition;
        Rectangle Rectsprite;
        public Rectangle persoRectangle;
        public string type, type2;

        int vitesse = 2; //test git hub
        public int mapnumber = 5, health, ligne = 1, colonne = 1, mana, healthMax, manaMax, Experience, Strenght, Intelligence, Degat, Armor, Lvl, ExperienceNext, Gold;
        public string Direction;
        int timer = 0, timerRun = 0, i = 0, timerMenu;
        public bool fight = false, lvlup;
        public Map map, map4, map5;

        public Character(Texture2D newTexture, Vector2 newPosition, Rectangle newRectangle, Rectangle newsprite, int newHealth, int newMana, int newExperience, int newStrenght, int newIntelligence, int newDegat, int newArmor, int newGold)
        {
            persoTexture = newTexture;
            persoPosition = newPosition;
            persoRectangle = newRectangle;
            Rectsprite = newsprite;
            health = newHealth;
            mana = newMana;
            manaMax = newMana;
            healthMax = newHealth;
            Experience = newExperience;
            Strenght = newStrenght;
            Intelligence = newIntelligence;
            Degat = newDegat;
            Armor = newArmor;
            Gold = newGold;
            Lvl = 1;

        }

        public void Update(GameTime gametime, Game1.GameState gameState)
        {
            GamePadState pad1 = GamePad.GetState(PlayerIndex.Two);
            KeyboardState KState = Keyboard.GetState();
            lvlup = (Experience >= 150 * Lvl);

            if (lvlup)
            {
                ExperienceNext = Experience - (150 * Lvl);
                Experience = ExperienceNext;
                healthMax += 500;
                manaMax += 100;
                health = healthMax;
                mana = manaMax;
                Strenght += 20;
                Intelligence += 50;
                Lvl += 1;
            }
            if (gameState == Game1.GameState.MainMenu)
            {
                timerMenu++;
                if (timerMenu < 100 || (timerMenu >= 315 && timerMenu < 415)) //gauche
                {
                    ligne = 1;
                    timer++;
                    if (timer == 15)
                    {
                        timer = 0;
                        if (colonne == 5)
                        {
                            colonne = 2;
                        }
                        else
                        {
                            colonne++;
                        }
                    }
                    Game1.playerMenu.Rectsprite = new Rectangle((colonne) * 32, (ligne) * 63, 30, 63);
                    Game1.playerMenu.persoPosition += (new Vector2((-vitesse), 0));
                }
                else if ((timerMenu >= 100 && timerMenu < 155) || (timerMenu >= 255 && timerMenu < 315) || (timerMenu >= 415 && timerMenu < 468)) //bas
                {
                    timer++;
                    ligne = 0;
                    if (timer == 15)
                    {
                        timer = 0;
                        if (colonne == 5)
                        {
                            colonne = 1;
                        }
                        else
                        {
                            colonne++;
                        }
                    }

                    Game1.playerMenu.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 62);
                    Game1.playerMenu.persoPosition += new Vector2(0, vitesse);
                }
                else if ((timerMenu >= 155 && timerMenu < 255) || (timerMenu >= 468 && timerMenu < 563)) //droite
                {
                    timer++;
                    ligne = 3;
                    if (timer == 15)
                    {
                        timer = 0;
                        if (colonne == 5)
                        {
                            colonne = 2;
                        }
                        else
                        {
                            colonne++;
                        }
                    }

                    Game1.playerMenu.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 63);
                    Game1.playerMenu.persoPosition += new Vector2(vitesse, 0);
                }
                else if (timerMenu >=563 && timerMenu < 728) // haut
                {
                    ligne = 2;
                    timer++;
                    if (timer == 15)
                    {
                        timer = 0;
                        if (colonne == 5)
                        {
                            colonne = 1;
                        }
                        else
                        {
                            colonne++;
                        }
                    }
                    Game1.playerMenu.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 63);
                    Game1.playerMenu.persoPosition += new Vector2(0, -vitesse);
                }
                else
                {
                    timerMenu = 0;
                    Game1.playerMenu.persoPosition = new Vector2(788, 230);
                }
            }
            if (gameState == Game1.GameState.Playing || gameState == Game1.GameState.Playing)
            {
                if (!fight)
                {
                    //Left
                    if ((KState.IsKeyDown(Keys.Q)&& KState.IsKeyUp(Keys.LeftShift) || (pad1.DPad.Left == ButtonState.Pressed && pad1.Buttons.RightShoulder == ButtonState.Released)))
                    {
                        ligne = 1;
                        timer++;
                        if (timer == 15)
                        {
                            timer = 0;
                            if (colonne == 5)
                            {
                                colonne = 2;
                            }
                            else
                            {
                                colonne++;
                            }
                        }
                        if (KState.IsKeyDown(Keys.Q) && KState.IsKeyUp(Keys.LeftShift))
                        {
                            Game1.player.Rectsprite = new Rectangle((colonne) * 32, (ligne) * 63, 30, 63);
                            Game1.player.persoPosition += (new Vector2((-vitesse), 0));
                        }
                        if (Playing.nbjoueurs == 2)
                        {
                            if (pad1.DPad.Left == ButtonState.Pressed && pad1.Buttons.RightShoulder == ButtonState.Released)
                            {
                                Game1.player2.Rectsprite = new Rectangle((colonne) * 32, (ligne) * 63, 30, 63);
                                Game1.player2.persoPosition += (new Vector2((-vitesse), 0));
                            }
                        }
                    }

                    else if (KState.IsKeyDown(Keys.LeftShift) && (KState.IsKeyDown(Keys.Q))) //|| (pad1.DPad.Left == ButtonState.Pressed && pad1.Buttons.RightShoulder == ButtonState.Pressed)))
                    {
                        ligne = 6;
                        timerRun++;
                        if (timerRun == 8)
                        {
                            timerRun = 0;
                            if (colonne == 5)
                            {
                                colonne = 1;
                            }
                            if (colonne == 3)
                            {
                                colonne = 1;
                            }
                            else
                            {
                                colonne++;
                            }
                        }
                        if (KState.IsKeyDown(Keys.LeftShift) && KState.IsKeyDown(Keys.Q))
                        {
                            Game1.player.Rectsprite = new Rectangle((colonne) * 32, (ligne) * 63, 30, 63);
                            Game1.player.persoPosition += (new Vector2((-2 * vitesse), 0));
                            if (Playing.nbjoueurs == 2)
                            {/* }
                        if (pad1.DPad.Left == ButtonState.Pressed && pad1.Buttons.RightShoulder == ButtonState.Pressed)
                        {*/
                                Game1.player2.Rectsprite = new Rectangle((colonne) * 32, (ligne) * 63, 30, 63);
                                Game1.player2.persoPosition += (new Vector2((-2 * vitesse), 0));
                            }
                        }
                    }

                    //Up
                    else if ((KState.IsKeyDown(Keys.Z) && KState.IsKeyUp(Keys.LeftShift) || (pad1.DPad.Up == ButtonState.Pressed && pad1.Buttons.RightShoulder == ButtonState.Released)))
                    {
                        ligne = 2;
                        timer++;
                        if (timer == 15)
                        {
                            timer = 0;
                            if (colonne == 5)
                            {
                                colonne = 1;
                            }
                            else
                            {
                                colonne++;
                            }
                        }
                        if (KState.IsKeyDown(Keys.Z) && KState.IsKeyUp(Keys.LeftShift))
                        {
                            Game1.player.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 63);
                            Game1.player.persoPosition += new Vector2(0, -vitesse);

                        }
                        if (Playing.nbjoueurs == 2)
                        {
                            if (pad1.DPad.Up == ButtonState.Pressed && pad1.Buttons.RightShoulder == ButtonState.Released)
                            {
                                Game1.player2.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 63);
                                Game1.player2.persoPosition += new Vector2(0, -vitesse);

                            }
                        }
                    }

                    else if (KState.IsKeyDown(Keys.LeftShift) && (KState.IsKeyDown(Keys.Z) ||(pad1.DPad.Up == ButtonState.Pressed && pad1.Buttons.RightShoulder == ButtonState.Pressed)))
                    {
                        ligne = 5;
                        timerRun++;
                        if (timerRun == 8)
                        {
                            timerRun = 0;
                            if (colonne == 5)
                            {
                                colonne = 1;
                            }
                            else
                            {
                                colonne++;
                            }
                        }
                        if (KState.IsKeyDown(Keys.LeftShift) && KState.IsKeyDown(Keys.Z))
                        {
                            Game1.player.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 63);
                            Game1.player.persoPosition += new Vector2(0, -2 * vitesse);

                        }
                        if (Playing.nbjoueurs == 2)
                        {
                            if (pad1.DPad.Up == ButtonState.Pressed && pad1.Buttons.RightShoulder == ButtonState.Pressed)
                            {
                                Game1.player2.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 63);
                                Game1.player2.persoPosition += new Vector2(0, -2 * vitesse);

                            }
                        }
                    }

                    //Down
                    else if ((KState.IsKeyDown(Keys.S) && KState.IsKeyUp(Keys.LeftShift) || (pad1.DPad.Down == ButtonState.Pressed && pad1.Buttons.RightShoulder == ButtonState.Released)))
                    {
                        timer++;
                        ligne = 0;
                        if (timer == 15)
                        {
                            timer = 0;
                            if (colonne == 5)
                            {
                                colonne = 1;
                            }
                            else
                            {
                                colonne++;
                            }
                        }
                        if (KState.IsKeyDown(Keys.S) && KState.IsKeyUp(Keys.LeftShift))
                        {
                            Game1.player.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 62);
                            Game1.player.persoPosition += new Vector2(0, vitesse);

                        }
                        if (Playing.nbjoueurs == 2)
                        {
                            if (pad1.DPad.Down == ButtonState.Pressed && pad1.Buttons.RightShoulder == ButtonState.Released)
                            {
                                Game1.player2.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 62);
                                Game1.player2.persoPosition += new Vector2(0, vitesse);

                            }
                        }
                    }

                    else if (KState.IsKeyDown(Keys.LeftShift) && (KState.IsKeyDown(Keys.S) || (pad1.DPad.Down == ButtonState.Pressed && pad1.Buttons.RightShoulder == ButtonState.Pressed)))
                    {
                        timerRun++;
                        ligne = 4;
                        if (timerRun == 8)
                        {
                            timerRun = 0;
                            if (colonne == 5)
                            {
                                colonne = 1;
                            }
                            else
                            {
                                colonne++;
                            }

                        }
                        if (KState.IsKeyDown(Keys.LeftShift) && KState.IsKeyDown(Keys.S))
                        {
                            Game1.player.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 62);
                            Game1.player.persoPosition += new Vector2(0, 2 * vitesse);

                        }
                        if (Playing.nbjoueurs == 2)
                        {
                            if (pad1.DPad.Down == ButtonState.Pressed && pad1.Buttons.RightShoulder == ButtonState.Pressed)
                            {
                                Game1.player2.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 62);
                                Game1.player2.persoPosition += new Vector2(0, 2 * vitesse);

                            }
                        }

                    }
                    //right
                    else if ((KState.IsKeyDown(Keys.D) && KState.IsKeyUp(Keys.LeftShift) ||(pad1.DPad.Right == ButtonState.Pressed && pad1.Buttons.RightShoulder == ButtonState.Released)))
                    {
                        timer++;
                        ligne = 3;
                        if (timer == 15)
                        {
                            timer = 0;
                            if (colonne == 5)
                            {
                                colonne = 2;
                            }
                            else
                            {
                                colonne++;
                            }
                        }
                        if (KState.IsKeyDown(Keys.D) && KState.IsKeyUp(Keys.LeftShift))
                        {
                            Game1.player.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 63);
                            Game1.player.persoPosition += new Vector2(vitesse, 0);

                        }
                        if (Playing.nbjoueurs == 2)
                        {
                            if (pad1.DPad.Right == ButtonState.Pressed && pad1.Buttons.RightShoulder == ButtonState.Released)
                            {
                                Game1.player2.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 63);
                                Game1.player2.persoPosition += new Vector2(vitesse, 0);

                            }
                        }
                    }

                    else if (KState.IsKeyDown(Keys.LeftShift) && (KState.IsKeyDown(Keys.D) || (pad1.DPad.Right == ButtonState.Pressed && pad1.Buttons.RightShoulder == ButtonState.Pressed)))
                    {
                        timerRun++;
                        ligne = 7;

                        if (timerRun == 8)
                        {
                            timerRun = 0;

                            if (colonne == 5)
                            {
                                colonne = 1;
                            }
                            if (colonne == 3)
                            {
                                colonne = 1;
                            }
                            else
                            {
                                colonne++;
                            }
                        }
                        if (KState.IsKeyDown(Keys.LeftShift) && KState.IsKeyDown(Keys.D))
                        {
                            Game1.player.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 63);
                            Game1.player.persoPosition += new Vector2(2 * vitesse, 0);

                        }
                        if (Playing.nbjoueurs == 2)
                        {
                            if (pad1.DPad.Right == ButtonState.Pressed && pad1.Buttons.RightShoulder == ButtonState.Pressed)
                            {
                                Game1.player2.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 63);
                                Game1.player2.persoPosition += new Vector2(2 * vitesse, 0);

                            }
                        }
                    }
                }
            }
            Game1.player.persoRectangle = new Rectangle((int)Game1.player.persoPosition.X, (int)Game1.player.persoPosition.Y + Game1.player.persoTexture.Height / 16, Game1.player.persoTexture.Width / 12, Game1.player.persoTexture.Height / 16);
            if (Playing.nbjoueurs == 2) Game1.player2.persoRectangle = new Rectangle((int)Game1.player2.persoPosition.X, (int)Game1.player2.persoPosition.Y + Game1.player2.persoTexture.Height / 16, Game1.player2.persoTexture.Width / 12, Game1.player2.persoTexture.Height / 16);

        }

        public void Draw(SpriteBatch spritBatch, Game1.GameState gameState)
        {
            if (gameState == Game1.GameState.MainMenu)
            {
                spritBatch.Draw(Game1.playerMenu.persoTexture, Game1.playerMenu.persoPosition, Game1.playerMenu.Rectsprite, Color.White);
            }
            else
            {
                spritBatch.Draw(Game1.player.persoTexture, Game1.player.persoPosition, Game1.player.Rectsprite, Color.White);
                if (Playing.nbjoueurs == 2) spritBatch.Draw(Game1.player2.persoTexture, Game1.player2.persoPosition, Game1.player2.Rectsprite, Color.White);
            }
        }

        public void Collision(Rectangle newRectangle)
        {
            if (persoRectangle.TouchTopOf(newRectangle))
            {
                persoPosition.Y = newRectangle.Y - persoRectangle.Height - persoTexture.Height / 16;
            }

            if (persoRectangle.TouchLeftOf(newRectangle))
            {
                persoPosition.X = newRectangle.X - persoRectangle.Width - 4;
            }
            if (persoRectangle.TouchRightOf(newRectangle))
            {
                persoPosition.X = newRectangle.X + newRectangle.Width + 4;
            }
            if (persoRectangle.TouchBottomOf(newRectangle))
                persoPosition.Y = newRectangle.Y + newRectangle.Height + 5 - persoTexture.Height / 16;

        }
    }
}
