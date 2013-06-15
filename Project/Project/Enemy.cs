using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Media;

namespace Project
{
    public class Enemy
    {

        public Texture2D enemyTexture;
        public Vector2 enemyPosition;
        public Rectangle Rectenemy;
        public Rectangle enemyRectangle;
        int screenWidth = 1366, screenHeight = 768;

        int vitesse = 1;

        public int ligne = 1, colonne = 1, mapnumber = 5, strength;

        public string Direction;
        int timer = 0, timerEnemy = 0;
        public int health, healthMax;
        public bool col;

        public Enemy(Texture2D newTexture, Vector2 newPosition, Rectangle newRectangle, Rectangle newsprite, int newHealth, int newStrength)
        {
            enemyTexture = newTexture;
            enemyPosition = newPosition;
            enemyRectangle = newRectangle;
            Rectenemy = newsprite;
            health = newHealth;
            healthMax = newHealth;
            strength = newStrength;
        }


        public void Update(GameTime gametime, Vector2 position1, Vector2 position2)
        {
            KeyboardState KState = Keyboard.GetState();

            if ((position1.X - 45 - enemyPosition.X >= -150 && position1.X - enemyPosition.X - 45 < 0 && position1.Y - enemyPosition.Y > -100 && position1.Y - enemyPosition.Y < 100)||(position2.X - 45 - enemyPosition.X >= -150 && position2.X - enemyPosition.X - 45 < 0 && position2.Y - enemyPosition.Y > -100 && position2.Y - enemyPosition.Y < 100))
            {
                Direction = "left";
                timer++;
                col = false;
                if (timer == 15)
                {
                    timer = 0;
                    if (colonne == 3)
                    {
                        colonne = 1;
                    }
                    else
                    {
                        colonne++;
                    }
                }
                else
                {
                    if (!col)
                    {
                        enemyPosition += (new Vector2((-vitesse), 0));
                        enemyRectangle.X -= vitesse;
                    }
                }
            }

            else if ((position1.X - 45 - enemyPosition.X <= 150 && position1.X - enemyPosition.X - 45 > 0 && position1.Y - enemyPosition.Y > -100 && position1.Y - enemyPosition.Y < 100)||(position2.X - 45 - enemyPosition.X <= 150 && position2.X - enemyPosition.X - 45 > 0 && position2.Y - enemyPosition.Y > -100 && position2.Y - enemyPosition.Y < 100))
            {
                Direction = "right";
                timer++;
                col = false;
                if (timer == 15)
                {
                    timer = 0;
                    if (colonne == 3)
                    {
                        colonne = 1;
                    }
                    else
                    {
                        colonne++;
                    }
                }
                if (enemyPosition.X >= (screenWidth - enemyTexture.Width / 6))
                {
                    mapnumber += 1;
                    enemyPosition.X = 0;
                }
                else
                {
                    if (!col)
                    {
                        enemyPosition += new Vector2(vitesse, 0);
                        enemyRectangle.X += vitesse;
                    }
                }

            }

            else if ((enemyPosition.Y - position1.Y > 0 && enemyPosition.Y - position1.Y < 200 && ((position1.X - enemyPosition.X >= 0 && position1.X - enemyPosition.X <= 100) || (position1.X - enemyPosition.X >= -100 && position1.X - enemyPosition.X <= 0)))||(enemyPosition.Y - position2.Y > 0 && enemyPosition.Y - position2.Y < 200 && ((position2.X - enemyPosition.X >= 0 && position2.X - enemyPosition.X <= 100) || (position2.X - enemyPosition.X >= -100 && position2.X - enemyPosition.X <= 0))))
            {

                Direction = "up";
                timer++;
                col = false;
                if (timer == 15)
                {
                    timer = 0;
                    if (colonne == 3)
                    {
                        colonne = 1;
                    }
                    else
                    {
                        colonne++;
                    }
                }
                else
                {
                    if (!col)
                    {
                        enemyPosition += new Vector2(0, -vitesse);
                        enemyRectangle.Y -= vitesse;
                    }
                }

            }
            else if ((enemyPosition.Y - position1.Y <= 0 && enemyPosition.Y - position1.Y >= -200 && ((position1.X - enemyPosition.X >= 0 && position1.X - enemyPosition.X < 100) || (position1.X - enemyPosition.X >= -100 && position1.X - enemyPosition.X <= 0)))||(enemyPosition.Y - position2.Y <= 0 && enemyPosition.Y - position2.Y >= -200 && ((position2.X - enemyPosition.X >= 0 && position2.X - enemyPosition.X < 100) || (position2.X - enemyPosition.X >= -100 && position2.X - enemyPosition.X <= 0))))
            {
                Direction = "down";
                timer++;
                col = false;
                if (timer == 15)
                {
                    timer = 0;
                    if (colonne == 3)
                    {
                        colonne = 1;
                    }
                    else
                    {
                        colonne++;
                    }
                }

                else
                {
                    if (!col)
                    {
                        enemyPosition += new Vector2(0, vitesse);
                        enemyRectangle.Y += vitesse;
                    }
                }

            }
            else //patrouille
            {
                timerEnemy++;
                if (timerEnemy <= 150)
                {
                    Direction = "left";
                    timer++;
                    col = false;
                    if (timer == 15)
                    {
                        timer = 0;
                        if (colonne == 3)
                        {
                            colonne = 1;
                        }
                        else
                        {
                            colonne++;
                        }
                    }
                    else
                    {
                        if (!col)
                        {
                            enemyPosition += (new Vector2((-vitesse), 0));
                            enemyRectangle.X -= vitesse;
                        }
                    }
                }
                else if (timerEnemy > 150 && timerEnemy <= 200)
                {
                    Direction = "down";
                    timer++;
                    col = false;
                    if (timer == 15)
                    {
                        timer = 0;
                        if (colonne == 3)
                        {
                            colonne = 1;
                        }
                        else
                        {
                            colonne++;
                        }
                    }
                    else
                    {
                        if (!col)
                        {
                            enemyPosition += new Vector2(0, vitesse);
                            enemyRectangle.Y += vitesse;
                        }
                    }

                }

                else if (timerEnemy > 200 && timerEnemy <= 350)
                {
                    Direction = "right";
                    timer++;
                    col = false;
                    if (timer == 15)
                    {
                        timer = 0;
                        if (colonne == 3)
                        {
                            colonne = 1;
                        }
                        else
                        {
                            colonne++;
                        }
                    }
                    else
                    {
                        if (!col)
                        {
                            enemyPosition += new Vector2(vitesse, 0);
                            enemyRectangle.X += vitesse;
                        }
                    }
                }
                else if (timerEnemy > 350 && timerEnemy <= 400)
                {

                    Direction = "up";
                    timer++;
                    col = false;
                    if (timer == 15)
                    {
                        timer = 0;
                        if (colonne == 3)
                        {
                            colonne = 1;
                        }
                        else
                        {
                            colonne++;
                        }
                    }

                    else
                    {
                        if (!col)
                        {
                            enemyPosition += new Vector2(0, -vitesse);

                            enemyRectangle.Y -= vitesse;
                        }
                    }

                }
                else
                {
                    timerEnemy = 0;
                }

            }
            switch (Direction)
            {
                case "up":

                    ligne = 4;
                    Rectenemy = new Rectangle((colonne - 1) * Rectenemy.Width, (ligne - 1) * Rectenemy.Height, Rectenemy.Width, Rectenemy.Height);
                    break;

                case "down":

                    ligne = 1;
                    Rectenemy = new Rectangle((colonne - 1) * Rectenemy.Width, (ligne - 1) * Rectenemy.Height, Rectenemy.Width, Rectenemy.Height);
                    break;

                case "left":

                    ligne = 2;
                    Rectenemy = new Rectangle((colonne - 1) * Rectenemy.Width, (ligne - 1) * Rectenemy.Height, Rectenemy.Width, Rectenemy.Height);
                    break;

                case "right":

                    ligne = 3;
                    Rectenemy = new Rectangle((colonne - 1) * Rectenemy.Width, (ligne - 1) * Rectenemy.Height, Rectenemy.Width, Rectenemy.Height);
                    break;

            }
        }

        public void Update(GameTime gametime, Vector2 position1)
        {
            KeyboardState KState = Keyboard.GetState();

            if ((position1.X - 45 - enemyPosition.X >= -150 && position1.X - enemyPosition.X - 45 < 0 && position1.Y - enemyPosition.Y > -100 && position1.Y - enemyPosition.Y < 100)) 
            {
                Direction = "left";
                timer++;
                col = false;
                if (timer == 15)
                {
                    timer = 0;
                    if (colonne == 3)
                    {
                        colonne = 1;
                    }
                    else
                    {
                        colonne++;
                    }
                }
                else
                {
                    if (!col)
                    {
                        enemyPosition += (new Vector2((-vitesse), 0));
                        enemyRectangle.X -= vitesse;
                    }
                }
            }

            else if ((position1.X - 45 - enemyPosition.X <= 150 && position1.X - enemyPosition.X - 45 > 0 && position1.Y - enemyPosition.Y > -100 && position1.Y - enemyPosition.Y < 100))
            {
                Direction = "right";
                timer++;
                col = false;
                if (timer == 15)
                {
                    timer = 0;
                    if (colonne == 3)
                    {
                        colonne = 1;
                    }
                    else
                    {
                        colonne++;
                    }
                }
                if (enemyPosition.X >= (screenWidth - enemyTexture.Width / 6))
                {
                    mapnumber += 1;
                    enemyPosition.X = 0;
                }
                else
                {
                    if (!col)
                    {
                        enemyPosition += new Vector2(vitesse, 0);
                        enemyRectangle.X += vitesse;
                    }
                }

            }

            else if ((enemyPosition.Y - position1.Y > 0 && enemyPosition.Y - position1.Y < 200 && ((position1.X - enemyPosition.X >= 0 && position1.X - enemyPosition.X <= 100) || (position1.X - enemyPosition.X >= -100 && position1.X - enemyPosition.X <= 0))))
            {

                Direction = "up";
                timer++;
                col = false;
                if (timer == 15)
                {
                    timer = 0;
                    if (colonne == 3)
                    {
                        colonne = 1;
                    }
                    else
                    {
                        colonne++;
                    }
                }
                else
                {
                    if (!col)
                    {
                        enemyPosition += new Vector2(0, -vitesse);
                        enemyRectangle.Y -= vitesse;
                    }
                }

            }
            else if ((enemyPosition.Y - position1.Y <= 0 && enemyPosition.Y - position1.Y >= -200 && ((position1.X - enemyPosition.X >= 0 && position1.X - enemyPosition.X < 100) || (position1.X - enemyPosition.X >= -100 && position1.X - enemyPosition.X <= 0))))
            {
                Direction = "down";
                timer++;
                col = false;
                if (timer == 15)
                {
                    timer = 0;
                    if (colonne == 3)
                    {
                        colonne = 1;
                    }
                    else
                    {
                        colonne++;
                    }
                }

                else
                {
                    if (!col)
                    {
                        enemyPosition += new Vector2(0, vitesse);
                        enemyRectangle.Y += vitesse;
                    }
                }

            }
            else //patrouille
            {
                timerEnemy++;
                if (timerEnemy <= 150)
                {
                    Direction = "left";
                    timer++;
                    col = false;
                    if (timer == 15)
                    {
                        timer = 0;
                        if (colonne == 3)
                        {
                            colonne = 1;
                        }
                        else
                        {
                            colonne++;
                        }
                    }
                    else
                    {
                        if (!col)
                        {
                            enemyPosition += (new Vector2((-vitesse), 0));
                            enemyRectangle.X -= vitesse;
                        }
                    }
                }
                else if (timerEnemy > 150 && timerEnemy <= 200)
                {
                    Direction = "down";
                    timer++;
                    col = false;
                    if (timer == 15)
                    {
                        timer = 0;
                        if (colonne == 3)
                        {
                            colonne = 1;
                        }
                        else
                        {
                            colonne++;
                        }
                    }
                    else
                    {
                        if (!col)
                        {
                            enemyPosition += new Vector2(0, vitesse);
                            enemyRectangle.Y += vitesse;
                        }
                    }

                }

                else if (timerEnemy > 200 && timerEnemy <= 350)
                {
                    Direction = "right";
                    timer++;
                    col = false;
                    if (timer == 15)
                    {
                        timer = 0;
                        if (colonne == 3)
                        {
                            colonne = 1;
                        }
                        else
                        {
                            colonne++;
                        }
                    }
                    else
                    {
                        if (!col)
                        {
                            enemyPosition += new Vector2(vitesse, 0);
                            enemyRectangle.X += vitesse;
                        }
                    }
                }
                else if (timerEnemy > 350 && timerEnemy <= 400)
                {

                    Direction = "up";
                    timer++;
                    col = false;
                    if (timer == 15)
                    {
                        timer = 0;
                        if (colonne == 3)
                        {
                            colonne = 1;
                        }
                        else
                        {
                            colonne++;
                        }
                    }

                    else
                    {
                        if (!col)
                        {
                            enemyPosition += new Vector2(0, -vitesse);

                            enemyRectangle.Y -= vitesse;
                        }
                    }

                }
                else
                {
                    timerEnemy = 0;
                }

            }
            switch (Direction)
            {
                case "up":

                    ligne = 4;
                    Rectenemy = new Rectangle((colonne - 1) * Rectenemy.Width, (ligne - 1) * Rectenemy.Height, Rectenemy.Width, Rectenemy.Height);
                    break;

                case "down":

                    ligne = 1;
                    Rectenemy = new Rectangle((colonne - 1) * Rectenemy.Width, (ligne - 1) * Rectenemy.Height, Rectenemy.Width, Rectenemy.Height);
                    break;

                case "left":

                    ligne = 2;
                    Rectenemy = new Rectangle((colonne - 1) * Rectenemy.Width, (ligne - 1) * Rectenemy.Height, Rectenemy.Width, Rectenemy.Height);
                    break;

                case "right":

                    ligne = 3;
                    Rectenemy = new Rectangle((colonne - 1) * Rectenemy.Width, (ligne - 1) * Rectenemy.Height, Rectenemy.Width, Rectenemy.Height);
                    break;

            }
        }

        public bool Collision()
        {
            if (Playing.nbjoueurs == 2)
                return (enemyRectangle.Intersects(Game1.player.persoRectangle)) || (enemyRectangle.Intersects(Game1.player2.persoRectangle));
            else return (enemyRectangle.Intersects(Game1.player.persoRectangle));
        }

        /* public void Collision(Rectangle newRectangle)
         {
             if (enemyRectangle.TouchTopOf(newRectangle))
             {
                 enemyPosition.Y = newRectangle.Y - enemyRectangle.Height ;
             }

             if (enemyRectangle.TouchLeftOf(newRectangle))
             {
                 enemyPosition.X = newRectangle.X - 4;
             }
             if (enemyRectangle.TouchRightOf(newRectangle))
             {
                 enemyPosition.X = newRectangle.X + 11;
             }
             if (enemyRectangle.TouchBottomOf(newRectangle))
                 enemyPosition.Y = newRectangle.Y + newRectangle.Height + 5 ;

         }*/

        public void Draw(SpriteBatch spritBatch)
        {
            spritBatch.Draw(enemyTexture, enemyPosition, Rectenemy, Color.White);
        }

        public void Collision1(Rectangle newRectangle)
        {
            if (enemyRectangle.TouchTopOf(newRectangle))
            {
                enemyPosition.Y = newRectangle.Y - enemyRectangle.Height ;
                enemyRectangle.Y = newRectangle.Y - enemyRectangle.Height ;
                col = true;
            }
            
            if (enemyRectangle.TouchLeftOf(newRectangle))
            {
                enemyPosition.X = newRectangle.X - enemyRectangle.Width -20;
                enemyRectangle.X = newRectangle.X - enemyRectangle.Width -5;

                col = true;
            }
            if (enemyRectangle.TouchRightOf(newRectangle))
            {
                enemyPosition.X = newRectangle.X + newRectangle.Width -27;
                enemyRectangle.X = newRectangle.X + enemyRectangle.Width-12 ;
                col = true;
            }
            if (enemyRectangle.TouchBottomOf(newRectangle))
            {
                enemyPosition.Y = newRectangle.Y + newRectangle.Height + 5 ;
                enemyRectangle.Y = newRectangle.Y + newRectangle.Height;
                col = true;
            }

        }

        public void Collision2(Rectangle newRectangle)
        {
            if (enemyRectangle.TouchTopOf(newRectangle))
            {
                enemyPosition.Y = newRectangle.Y - enemyRectangle.Height-15;
                enemyRectangle.Y = newRectangle.Y - enemyRectangle.Height;
                col = true;
            }

            if (enemyRectangle.TouchLeftOf(newRectangle))
            {
                enemyPosition.X = newRectangle.X - enemyRectangle.Width - 5;
                enemyRectangle.X = newRectangle.X - enemyRectangle.Width - 5;

                col = true;
            }
            if (enemyRectangle.TouchRightOf(newRectangle))
            {
                enemyPosition.X = newRectangle.X + newRectangle.Width ;
                enemyRectangle.X = newRectangle.X + newRectangle.Width +5;
                col = true;
            }
            if (enemyRectangle.TouchBottomOf(newRectangle))
            {
                enemyPosition.Y = newRectangle.Y + enemyRectangle.Height -70;
                enemyRectangle.Y = newRectangle.Y + enemyRectangle.Height -55;
                col = true;
            }

        }
    }
}