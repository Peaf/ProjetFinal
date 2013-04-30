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
        int screenWidth = 1366, screenHeight = 768;

        int vitesse = 2; //test git hub
        public int mapnumber = 5, health, ligne = 1, colonne = 1, mana, healthMax, manaMax, Experience, Strenght, Intelligence, Degat, Armor, Lvl, ExperienceNext;
        public string Direction;
        int timer = 0, timerRun = 0, i = 0;
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
            lvlup = (Experience >= 100 * Lvl);

            if (lvlup)
            {
                ExperienceNext = Experience - (100 * Lvl);
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
                if (KState.IsKeyDown(Keys.Q) && KState.IsKeyUp(Keys.LeftShift))
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
                    Rectsprite = new Rectangle((colonne) * 32, (ligne) * 63, 30, 63);
                    if (persoPosition.X <= 0)
                    {
                        map = map4;
                        persoPosition.X = (screenWidth - persoTexture.Width / 6);
                    }
                    else
                    {
                        persoPosition += (new Vector2((-vitesse), 0));
                    }

                }
                
                else if (KState.IsKeyDown(Keys.LeftShift) && KState.IsKeyDown(Keys.Q))
                {
                    ligne = 6;
                    timerRun++;
                    if (timerRun == 20)
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
                    Rectsprite = new Rectangle((colonne) * 32, (ligne) * 63, 30, 63);
                    if (persoPosition.X <= 0)
                    {
                        map = map4;
                        persoPosition.X = (screenWidth - persoTexture.Width / 6);
                    }
                    else
                    {
                        persoPosition += (new Vector2((-2 * vitesse), 0));
                    }
                }

                //Up
                else if (KState.IsKeyDown(Keys.Z) && KState.IsKeyUp(Keys.LeftShift))
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
                    Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 63);
                    if (persoPosition.Y <= 0)
                    {
                        mapnumber += 3;
                        persoPosition.Y = screenHeight - persoTexture.Height / 8;
                    }
                    else
                    {
                        persoPosition += new Vector2(0, -vitesse);
                    }
                }
                
                else if (KState.IsKeyDown(Keys.LeftShift) && KState.IsKeyDown(Keys.Z))
                {
                    ligne = 5;
                    timerRun++;
                    if (timerRun == 20)
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
                    Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 63);
                    if (persoPosition.Y <= 0)
                    {
                        mapnumber += 3;
                        persoPosition.Y = screenHeight - persoTexture.Height / 8;
                    }
                    else
                    {
                        persoPosition += new Vector2(0, -2 * vitesse);
                    }
                }

                //Down
                else if (KState.IsKeyDown(Keys.S) && KState.IsKeyUp(Keys.LeftShift))
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
                    Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 62);
                    if (persoPosition.Y >= (screenHeight - persoTexture.Height / 8))
                    {
                        mapnumber -= 3;
                        persoPosition.Y = 0;
                    }
                    else
                    {
                        persoPosition += new Vector2(0, vitesse);
                    }
                }

                else if (KState.IsKeyDown(Keys.LeftShift) && KState.IsKeyDown(Keys.S))
                {
                    timerRun++;
                    ligne = 4;
                    if (timerRun == 20)
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
                    Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 62);
                    if (persoPosition.Y >= (screenHeight - persoTexture.Height / 8))
                    {
                        mapnumber -= 3;
                        persoPosition.Y = 0;
                    }
                    else
                    {
                        persoPosition += new Vector2(0, 2 * vitesse);
                    }
                }
                //right
                else if (KState.IsKeyDown(Keys.D) && KState.IsKeyUp(Keys.LeftShift))
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
                    Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 63);
                    if (persoPosition.X >= (screenWidth - persoTexture.Width / 6))
                    {
                        mapnumber += 1;
                        persoPosition.X = 0;
                    }
                    else
                    {
                        persoPosition += new Vector2(vitesse, 0);
                    }
                }

                else if (KState.IsKeyDown(Keys.LeftShift) && KState.IsKeyDown(Keys.D))
                {
                    timerRun++;
                    ligne = 7;

                    if (timerRun == 20)
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
                    Rectsprite = new Rectangle(colonne * 32, ligne * 63, 30, 63);
                    if (persoPosition.X >= (screenWidth - persoTexture.Width / 6))
                    {
                        mapnumber += 1;
                        persoPosition.X = 0;
                    }
                    else
                    {
                        persoPosition += new Vector2(2 * vitesse, 0);
                    }
                }

            }

            persoRectangle = new Rectangle((int)persoPosition.X, (int)persoPosition.Y + persoTexture.Height / 16, persoTexture.Width / 12, persoTexture.Height / 16);
        }

        public void Draw(SpriteBatch spritBatch)
        {
            spritBatch.Draw(persoTexture, persoPosition, Rectsprite, Color.White);
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
