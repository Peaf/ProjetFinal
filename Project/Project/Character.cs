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
        public Texture2D persoTexture, persoTexture2;
        public Vector2 persoPosition, persoPosition2;
        Rectangle Rectsprite, Rectsprite2;
        public Rectangle persoRectangle, persoRectangle2;
        public string type, type2;

        int vitesse = 1; //test git hub
        public int mapnumber = 5, health, ligne = 1, colonne = 1, mana, healthMax, manaMax, Experience, Strenght, Intelligence, Degat, Armor, Lvl, ExperienceNext;
        public string Direction;
        int timer = 0, timerRun = 0, i = 0, screenWidth = 1366, screenHeight = 768;
        public bool fight = false, lvlup;
        public Map map, map4, map5;

        public Character(Texture2D newTexture, Vector2 newPosition, Rectangle newRectangle, Rectangle newsprite, int newHealth, int newMana, int newExperience, int newStrenght, int newIntelligence, int newDegat, int newArmor)
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
            Lvl = 1;

        }

        public void Update(GameTime gametime)
        {
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
            if (!fight)
            {
                //Left
                if ((KState.IsKeyDown(Keys.Q) || KState.IsKeyDown(Keys.F)) && KState.IsKeyUp(Keys.LeftShift))
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

                    if (KState.IsKeyDown(Keys.F) && KState.IsKeyUp(Keys.LeftShift))
                    {
                        Game1.player2.Rectsprite = new Rectangle((colonne) * 32, (ligne) * 63, 30, 63); 
                        Game1.player2.persoPosition += (new Vector2((-vitesse), 0));

                    }


                }

                else if (KState.IsKeyDown(Keys.LeftShift) && (KState.IsKeyDown(Keys.Q) || KState.IsKeyDown(Keys.F)))
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
                    }
                    if (KState.IsKeyDown(Keys.LeftShift) && KState.IsKeyDown(Keys.F))
                    {
                        Game1.player2.Rectsprite = new Rectangle((colonne) * 32, (ligne) * 63, 30, 63);
                        Game1.player2.persoPosition += (new Vector2((-2 * vitesse), 0));
                    }
                }

                //Up
                else if ((KState.IsKeyDown(Keys.Z) || KState.IsKeyDown(Keys.T)) && KState.IsKeyUp(Keys.LeftShift))
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
                    if (KState.IsKeyDown(Keys.T) && KState.IsKeyUp(Keys.LeftShift))
                    {
                        Game1.player2.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 63);
                        Game1.player2.persoPosition += new Vector2(0, -vitesse);

                    }

                }

                else if (KState.IsKeyDown(Keys.LeftShift) && (KState.IsKeyDown(Keys.Z) || KState.IsKeyDown(Keys.T)))
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
                    if (KState.IsKeyDown(Keys.LeftShift) && KState.IsKeyDown(Keys.T))
                    {
                        Game1.player2.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 63);
                        Game1.player2.persoPosition += new Vector2(0, -2 * vitesse);

                    }

                }

                //Down
                else if ((KState.IsKeyDown(Keys.S) || KState.IsKeyDown(Keys.G)) && KState.IsKeyUp(Keys.LeftShift))
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
                    if (KState.IsKeyDown(Keys.G) && KState.IsKeyUp(Keys.LeftShift))
                    {
                        Game1.player2.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 62);
                        Game1.player2.persoPosition += new Vector2(0, vitesse);

                    }

                }

                else if (KState.IsKeyDown(Keys.LeftShift) && (KState.IsKeyDown(Keys.S) || KState.IsKeyDown(Keys.G)))
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
                    if (KState.IsKeyDown(Keys.LeftShift) && KState.IsKeyDown(Keys.G))
                    {
                        Game1.player2.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 62);
                        Game1.player2.persoPosition += new Vector2(0, 2 * vitesse);

                    }

                }
                //right
                else if ((KState.IsKeyDown(Keys.D) || KState.IsKeyDown(Keys.H)) && KState.IsKeyUp(Keys.LeftShift))
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
                    if (KState.IsKeyDown(Keys.H) && KState.IsKeyUp(Keys.LeftShift))
                    {
                        Game1.player2.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 63);
                        Game1.player2.persoPosition += new Vector2(vitesse, 0);

                    }

                }

                else if (KState.IsKeyDown(Keys.LeftShift) && (KState.IsKeyDown(Keys.D) || KState.IsKeyDown(Keys.H)))
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
                    if (KState.IsKeyDown(Keys.LeftShift) && KState.IsKeyDown(Keys.H))
                    {
                        Game1.player2.Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 63);
                        Game1.player2.persoPosition += new Vector2(2 * vitesse, 0);

                    }

                }

            }


            Game1.player.persoRectangle = new Rectangle((int)Game1.player.persoPosition.X, (int)Game1.player.persoPosition.Y + Game1.player.persoTexture.Height / 16, Game1.player.persoTexture.Width / 12, Game1.player.persoTexture.Height / 16);
            Game1.player2.persoRectangle = new Rectangle((int)Game1.player2.persoPosition.X, (int)Game1.player2.persoPosition.Y + Game1.player2.persoTexture.Height / 16, Game1.player2.persoTexture.Width / 12, Game1.player2.persoTexture.Height / 16);

        }

        public void Draw(SpriteBatch spritBatch)
        {
            spritBatch.Draw(Game1.player.persoTexture, Game1.player.persoPosition, Game1.player.Rectsprite, Color.White);
            spritBatch.Draw(Game1.player2.persoTexture, Game1.player2.persoPosition, Game1.player2.Rectsprite, Color.White);
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
