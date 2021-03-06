﻿using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Media;
using Microsoft.Xna.Framework.Content;

namespace Project
{
    public class PNJ
    {
        public Vector2 pnjPosition, origine;
        public Texture2D pnjTexture;
        public Rectangle pnjRectangle, taille;
        int ligne = 0, colonne = 0, timerAnimation = 0, timerPnj2 = 0, colonneHealer = 0;
        string directionPnj2 = "";
        public static bool arrived;
        int animHeal = 0;

        public PNJ(Texture2D newTexture, Vector2 newPosition, Rectangle newRectangle, Vector2 newOrigine, Rectangle newtaille)
        {
            pnjTexture = newTexture;
            pnjPosition = newPosition;
            pnjRectangle = newRectangle;
            taille = newtaille;

        }

        public bool Collision(PNJ pnj)
        {
            if (Playing.nbjoueurs == 2)
                return ((Game1.player.persoPosition.X > pnj.pnjPosition.X - 50 && Game1.player.persoPosition.Y > pnj.pnjPosition.Y - 50 && Game1.player.persoPosition.Y < pnj.pnjPosition.Y + 50 && Game1.player.persoPosition.X < pnj.pnjPosition.X + 50) || (Game1.player2.persoPosition.X > pnj.pnjPosition.X - 50 && Game1.player2.persoPosition.Y > pnj.pnjPosition.Y - 50 && Game1.player2.persoPosition.Y < pnj.pnjPosition.Y + 50 && Game1.player2.persoPosition.X < pnj.pnjPosition.X + 50));
            else return (Game1.player.persoPosition.X > pnj.pnjPosition.X - 50 && Game1.player.persoPosition.Y > pnj.pnjPosition.Y - 50 && Game1.player.persoPosition.Y < pnj.pnjPosition.Y + 50 && Game1.player.persoPosition.X < pnj.pnjPosition.X + 50);
        }


        public void Update(GameTime gameTime, int state, string map)
        {
            if (map == "mapShop")
            {
                if (state == 0)
                {
                    pnjRectangle = new Rectangle(0, 0, 0, 0); // au debut on ne charge pas le pnj2
                }
                if (state == 1) // il marche vers les armures
                {
                    timerAnimation++;
                    if (timerPnj2 == 0)
                    {
                        timerPnj2 = 1;
                    }
                    if (timerPnj2 > 0 && timerPnj2 < 120) // haut
                    {
                        directionPnj2 = "up";
                        timerPnj2++;
                    }
                    else if ((timerPnj2 >= 120 && timerPnj2 < 550) || (timerPnj2 >= 680 & timerPnj2 < 780)) // gauche
                    {
                        directionPnj2 = "left";
                        timerPnj2++;
                    }
                    else if (timerPnj2 >= 550 && timerPnj2 < 680) //bas
                    {
                        directionPnj2 = "down";
                        timerPnj2++;
                    }
                    else
                    {
                        timerPnj2 = -1;
                        directionPnj2 = "stop";
                        ligne = 2;
                        pnjRectangle = new Rectangle(32 * colonne, ligne * 48, 32, 48);
                    }
                    if (timerPnj2 >= 780)
                        arrived = true;
                }
                if (state == 4)
                {
                    arrived = false;
                    timerAnimation++;
                    if (timerPnj2 == -1)
                    {
                        timerPnj2 = 1;
                    }
                    if (timerPnj2 > 0 && timerPnj2 < 100 || (timerPnj2 >= 230 && timerPnj2 <= 660))
                    {
                        directionPnj2 = "right";
                        timerPnj2++;
                    }
                    else if (timerPnj2 >= 100 && timerPnj2 < 230)
                    {
                        directionPnj2 = "up";
                        timerPnj2++;
                    }
                    else if (timerPnj2 >= 660 && timerPnj2 < 770)
                    {
                        directionPnj2 = "down";
                        timerPnj2++;
                    }
                    else
                    {
                        directionPnj2 = "stop";
                        Playing.whatToBuy = 0;
                        timerPnj2 = 0;
                        Playing.returnPnj = false;
                    }
                }
                if (state == 2) // direction Weapons
                {
                    timerAnimation++;
                    if (timerPnj2 == 0)
                    {
                        timerPnj2 = 1;
                    }
                    if (timerPnj2>0 && timerPnj2 < 30 || (timerPnj2 >= 600 && timerPnj2 < 950)) // haut
                    {
                        directionPnj2 = "up";
                        timerPnj2++;
                    }
                    else if ((timerPnj2 >= 30 && timerPnj2 < 100) || (timerPnj2 >= 380 && timerPnj2 < 600))
                    {
                        directionPnj2 = "right";
                        timerPnj2++;
                    }
                    else if ((timerPnj2 >= 100 && timerPnj2 < 230) || (timerPnj2 >= 250 && timerPnj2 < 380))
                    {
                        directionPnj2 = "down";
                        timerPnj2++;
                    }
                    else if (timerPnj2 >= 230 && timerPnj2 < 250)
                    {
                        directionPnj2 = "left";
                        timerPnj2++;
                    }
                    else
                    {
                        timerPnj2 = -1;
                        ligne = 2;
                        directionPnj2 = "stop";
                        pnjRectangle = new Rectangle(32 * colonne, ligne * 48, 32, 48);
                    }
                    if (timerPnj2 >= 950)
                        arrived = true;
                }
                if (state == 5)
                {
                    arrived = true;
                    timerAnimation++;

                    if (timerPnj2 == -1)
                    {
                        timerPnj2 = 1;
                    }
                    if (timerPnj2 > 0 && timerPnj2 < 350 || (timerPnj2 >= 925 && timerPnj2 < 945))
                    {
                        directionPnj2 = "down";
                        timerPnj2++;
                    }
                    else if (timerPnj2 >= 350 && timerPnj2 < 570 || (timerPnj2 >= 850 && timerPnj2 < 925))
                    {
                        directionPnj2 = "left";
                        timerPnj2++;
                    }
                    else if ((timerPnj2 >= 570 && timerPnj2 < 700) || (timerPnj2 >= 730 && timerPnj2 < 850))
                    {
                        directionPnj2 = "up";
                        timerPnj2++;
                    }
                    else if (timerPnj2 >= 700 && timerPnj2 < 730)
                    {
                        directionPnj2 = "right";
                        timerPnj2++;
                    }
                    else
                    {
                        directionPnj2 = "stop";
                        Playing.whatToBuy = 0;
                        timerPnj2 = 0;
                        Playing.returnPnj = false;
                    }
                }
                if (state == 3)
                {
                    timerAnimation++;
                    if (timerPnj2 == 0)
                    {
                        timerPnj2 = 1;
                    }
                    if ( timerPnj2 > 0 &&timerPnj2 < 30) // haut
                    {
                        directionPnj2 = "up";
                        timerPnj2++;
                    }
                    else if ((timerPnj2 >= 30 && timerPnj2 < 100) || (timerPnj2 >= 380 && timerPnj2 < 580))
                    {
                        directionPnj2 = "right";
                        timerPnj2++;
                    }
                    else if ((timerPnj2 >= 100 && timerPnj2 < 230) || (timerPnj2 >= 250 && timerPnj2 < 380) || (timerPnj2 >= 580 & timerPnj2 < 635))
                    {
                        directionPnj2 = "down";
                        timerPnj2++;
                    }
                    else if (timerPnj2 >= 230 && timerPnj2 < 250)
                    {
                        directionPnj2 = "left";
                        timerPnj2++;
                    }
                    else
                    {
                        ligne = 1;
                        timerPnj2 = -1;
                        directionPnj2 = "stop";
                        pnjRectangle = new Rectangle(32 * colonne, ligne * 48, 32, 48);
                    }
                    if (timerPnj2 >= 635)
                        arrived = true;
                }
                if (state == 6)
                {
                    arrived = false;
                    timerAnimation++;
                    if (timerPnj2 == -1)
                    {
                        timerPnj2 = 1;
                    }
                    else if (timerPnj2 > 0 && timerPnj2 < 55 || timerPnj2 >= 255 && timerPnj2 < 385 || timerPnj2 >= 415 && timerPnj2 < 545)
                    {
                        directionPnj2 = "up";
                        timerPnj2++;
                    }
                    else if (timerPnj2 >= 618 && timerPnj2 < 630)
                    {
                        directionPnj2 = "down";
                        timerPnj2++;
                    }
                    else if (timerPnj2 >= 55 && timerPnj2 < 255 || timerPnj2 >= 545 && timerPnj2 < 618)
                    {
                        directionPnj2 = "left";
                        timerPnj2++;
                    }
                    else if (timerPnj2 >= 385 && timerPnj2 < 415)
                    {
                        directionPnj2 = "right";
                        timerPnj2++;
                    }
                    else
                    {
                        directionPnj2 = "stop";
                        Playing.whatToBuy = 0;
                        timerPnj2 = 0;
                        Playing.returnPnj = false;
                    }
                }
                switch (directionPnj2)
                {
                    case "up":
                        ligne = 3;
                        if (timerAnimation == 7)
                        {
                            if (colonne == 3)
                            {
                                colonne = 0;
                            }
                            else
                            {
                                colonne++;
                            }
                            pnjRectangle = new Rectangle(32 * colonne, ligne * 48, 32, 48);
                            Game1.pnjShop2.pnjPosition += (new Vector2(0, -9));
                            timerAnimation = 0;
                        }
                        break;
                    case "down":
                        ligne = 0;
                        if (timerAnimation == 7)
                        {
                            if (colonne == 3)
                            {
                                colonne = 0;
                            }
                            else
                            {
                                colonne++;
                            }
                            pnjRectangle = new Rectangle(32 * colonne, ligne * 48, 32, 48);
                            Game1.pnjShop2.pnjPosition += (new Vector2(0, 9));
                            timerAnimation = 0;
                        }
                        break;

                    case "right":
                        ligne = 2;
                        if (timerAnimation == 7)
                        {
                            if (colonne == 3)
                            {
                                colonne = 0;
                            }
                            else
                            {
                                colonne++;
                            }
                            pnjRectangle = new Rectangle(32 * colonne, ligne * 48, 32, 48);
                            Game1.pnjShop2.pnjPosition += (new Vector2(9, 0));
                            timerAnimation = 0;
                        }
                        break;

                    case "left":
                        ligne = 1;
                        if (timerAnimation == 7)
                        {
                            if (colonne == 3)
                            {
                                colonne = 0;
                            }
                            else
                            {
                                colonne++;
                            }
                            pnjRectangle = new Rectangle(32 * colonne, ligne * 48, 32, 48);
                            Game1.pnjShop2.pnjPosition += (new Vector2(-9, 0));
                            timerAnimation = 0;
                        }

                        break;

                    case "stop":
                        timerAnimation = 0;
                        pnjRectangle = new Rectangle(32 * colonne, ligne * 48, 32, 48);
                        break;

                }
            }
            if (map == "map8")
            {
                if (state == 0)
                {
                    ligne = 0;
                    colonne = 0;
                    pnjRectangle = new Rectangle(colonne * 48, ligne * 94, 48, 94);
                }
                if (state == 1)
                {
                    timerAnimation++;
                    ligne = 3;
                    if (timerAnimation == 15)
                    {
                        if (colonne == 0)
                        {
                            colonne = 1;
                        }
                        else
                        {
                            colonne = 0;
                        }
                        timerAnimation = 0;
                        pnjRectangle = new Rectangle(colonne * 48, ligne * 94, 48, 94);
                    }

                }
                if (state == 2)
                {
                    ligne = 2;
                    colonne = 0;
                    pnjRectangle = new Rectangle(colonne * 48, ligne * 94, 48, 94);
                }
            }
            if (map == "map2")
            {
                if (state == 0)
                {
                    colonne = 5;
                    pnjRectangle = new Rectangle(colonne * 85, 0, 85, 80);
                }

                if (state == 1)
                {
                    timerAnimation++;
                    if (timerAnimation == 15 && animHeal < 5)
                    {
                        animHeal++;
                        if (colonneHealer == 5)
                        {
                            colonneHealer = 0;
                        }
                        else
                        {
                            colonneHealer++;
                        }
                        timerAnimation= 0;
                        pnjRectangle = new Rectangle(colonneHealer * 85, 0, 85, 80);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spritBatch, int bookState, string map)
        {
            if (map == "map8")
            {
                
                if (bookState == 2 || bookState == 3 || bookState == 4)
                {
                    spritBatch.Draw(pnjTexture, pnjPosition, pnjRectangle, Color.White, 0f, origine, 1.0f, SpriteEffects.FlipHorizontally, 0);
                }
                else if (bookState == 7)
                {
                    pnjRectangle = new Rectangle(0 * 48, 2 * 94, 48, 94);
                    spritBatch.Draw(pnjTexture, pnjPosition, pnjRectangle, Color.White, 0f, origine, 1.0f, SpriteEffects.FlipHorizontally, 0);
                }
                else
                {
                    spritBatch.Draw(pnjTexture, pnjPosition, pnjRectangle, Color.White);
                }
            }
            if (map == "map2")
            {
                spritBatch.Draw(pnjTexture, pnjPosition, pnjRectangle, Color.White, 0f, origine, 1.0f, SpriteEffects.FlipHorizontally, 0);
            }
            if (map == "mapShop")
            {
                if (bookState == 1) // 1 on dessine sinon on dessine pas
                {
                    spritBatch.Draw(pnjTexture, pnjPosition, pnjRectangle, Color.White);
                }

            }

        }

    }
}
