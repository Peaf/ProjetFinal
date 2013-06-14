using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Project
{
    static class Fight
    {
        static cButton btnAttack1, btnSpell, btnObjects;
        static Texture2D speechBoxTexture, healthBoxTexture, manaTexture, enemyHealthTexture, fightBackTexture, healthTexture, persoFight, fireTexture, lightTexture;
        static int turn = -1, disablePlayer1End, disablePlayer2End, degat, degatEnemy, timerinventaire = 0, manaPerdu, timerAnimation = 0, colonne = 0, ligne = 0, timerAnimationDegat = 0, colonneFire = 0, nbreAnimationFire = 0;
        static Rectangle healthBoxRectangle, healthBoxRectangle2, healthRectangle, healthRectangle2, manaRectangle, manaRectangle2, enemyHealthRectangle, fightBackRectangle, persoFightRectangle, fireRectangle, lightRectangle, speechBoxRectangle;
        static MouseState pastMouse;
        static string attackChoisi = "";
        static Random rand = new Random();
        static KeyboardState presentKey, pastKey;
        static Song songGameOver, songVictory, song2;
        static bool Isfighting = false, stop = false, disablePlayer1 = false, disablePlayer2, turnPlayer2 = false, turnPlayer = true, turnEnemy = false, attaquePlayer1 = false;
        static Vector2 persoFightPosition, firePosition, lightPosition, origin, lightPosition2;



        public static void LoadContent(ContentManager Content, SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            speechBoxTexture = Content.Load<Texture2D>("SpeechBox");
            speechBoxRectangle = new Rectangle(0, 675, speechBoxTexture.Width, speechBoxTexture.Height);

            fightBackTexture = Content.Load<Texture2D>("Menu/FightBack");
            fightBackRectangle = new Rectangle(0, 0, screenWidth, screenHeight);

            Game1.btnStartFight = new cButton(Content.Load<Texture2D>("Button/StartFight"), 150, 70);
            Game1.btnStartFight.setPosition(new Vector2(screenWidth / 2 - 100, screenHeight / 2));

            btnAttack1 = new cButton(Content.Load<Texture2D>("Button/AttackButton"), 120, 45);
            btnAttack1.setPosition(new Vector2(50, screenHeight / 2 + 200));

            btnSpell = new cButton(Content.Load<Texture2D>("Button/SpellButton"), 120, 45);
            btnSpell.setPosition(new Vector2(170, screenHeight / 2 + 200));

            btnObjects = new cButton(Content.Load<Texture2D>("Button/ObjectsButton"), 120, 45);
            btnObjects.setPosition(new Vector2(100, screenHeight / 2 + 250));

            songVictory = Content.Load<Song>("Song/Victory");
            songGameOver = Content.Load<Song>("Song/songGameOver");
            song2 = Content.Load<Song>("Song/Song2");

            healthTexture = Content.Load<Texture2D>("Barres/Health");
            manaTexture = Content.Load<Texture2D>("Barres/Mana");
            healthBoxTexture = Content.Load<Texture2D>("Barres/HealthBox");

            enemyHealthTexture = Content.Load<Texture2D>("Barres/HealthEnemy");

            //animation 

            persoFight = Content.Load<Texture2D>("AnimationFight");
            persoFightRectangle = new Rectangle(200, screenHeight / 2 + 100, 320, 847);
            persoFightPosition = new Vector2(200, screenHeight / 2 + 320);
            origin = new Vector2(100, (screenHeight / 2 + 100) / 2);

            lightTexture = Content.Load<Texture2D>("eclairs");
            lightRectangle = new Rectangle(120, screenHeight / 2 + 30, 114, 65);
            lightPosition = new Vector2(120, screenHeight / 2 + 30);
            lightPosition2 = new Vector2(200, screenHeight / 2 + 140);

            fireTexture = Content.Load<Texture2D>("Fire");
            fireRectangle = new Rectangle(1030, screenHeight / 2 + 100, 200, 64);
            firePosition = new Vector2(1015, screenHeight / 2 + 150);
        }

        public static Game1.GameState Update(GameTime gameTime, int screenWidth, int screenHeight)
        {
            presentKey = Keyboard.GetState();
            Game1.GameState CurrentGameState = Game1.GameState.Fight;
            MouseState mouse = Mouse.GetState();
            Rectangle mouseRectangle = new Rectangle(mouse.X, mouse.Y, 1, 1);
            persoFightRectangle = new Rectangle(colonne * 80, ligne * 77, 80, 77);

            if (Playing.nbjoueurs == 1)
            {
                Game1.player.persoPosition.X = 200;
                Game1.player.persoPosition.Y = screenHeight / 2 + 100;
                Game1.player.fight = true;
                Game1.player.colonne = 2;
                // Game1.player.Direction = "right";
                Game1.player.Update(gameTime, Game1.GameState.Playing);
                healthBoxRectangle = new Rectangle(10, 10, healthBoxTexture.Width, healthBoxTexture.Height);
                healthRectangle = new Rectangle(16, 14, (Game1.player.health * 379) / Game1.player.healthMax, 35);
                manaRectangle = new Rectangle(115, 62, (Game1.player.mana * 280) / Game1.player.manaMax, manaTexture.Height);
                enemyHealthRectangle = new Rectangle((1030 - Game1.enemy.health / 2), (screenHeight / 2 - enemyHealthTexture.Height / 2 + 60), Game1.enemy.health, enemyHealthTexture.Height);

                if (attackChoisi == "")
                {
                    ligne = 0;
                    if (timerAnimation == 15)
                    {
                        timerAnimation = 0;
                        if (colonne == 3)
                        {
                            colonne = 0;
                        }
                        else
                        {
                            colonne++;
                        }
                    }
                }
                if (!Playing.inventaire)
                {
                    timerAnimation++;
                    timerinventaire++;
                    if (turn == -1 && Game1.btnStartFight.isClicked)
                    {
                        turn = 0;
                        Game1.enemy.healthMax = Game1.enemy.health;
                    }
                    if (turn % 2 == 0 && Game1.player.health > 0 && Game1.enemy.health > 0)
                    {
                        if (timerinventaire > 15)
                        {
                            if (btnObjects.isClicked && pastMouse.LeftButton == ButtonState.Released)
                            {
                                Playing.inventaire = true;
                                timerinventaire = 0;
                                btnObjects.isClicked = false;
                            }
                        }

                        if (btnAttack1.isClicked && pastMouse.LeftButton == ButtonState.Released)
                        {
                            btnAttack1.isClicked = false;
                            attackChoisi = "Basic attack";
                            btnAttack1.Update(mouse, gameTime);
                            timerAnimation = 0;
                            degat = Game1.player.Degat + rand.Next(0, 30) + Game1.player.Strenght / 2;
                            manaPerdu = 0;
                            stop = false;
                            colonne = 0;
                        }
                        if (Game1.player.Lvl >= 2)
                        {
                            if (btnSpell.isClicked && pastMouse.LeftButton == ButtonState.Released)
                            {
                                btnSpell.isClicked = false;
                                attackChoisi = "Fire Ball";
                                btnSpell.Update(mouse, gameTime);
                                timerAnimation = 0;
                                degat = rand.Next(80, 120) + Game1.player.Intelligence + Game1.player.Degat;
                                manaPerdu = 20;
                                stop = false;
                                colonne = 0;
                                colonneFire = 0;
                                nbreAnimationFire = 0;
                            }
                        }
                        if (attackChoisi == "Basic attack" && !stop)
                        {
                            ligne = 7;
                            if (timerAnimation % 12 == 0)
                            {
                                if (colonne == 3)
                                {
                                    colonne = 0;
                                    ligne = 0;
                                    stop = true;
                                }
                                else
                                {
                                    colonne++;
                                }
                            }
                            persoFightRectangle = new Rectangle(colonne * 80, ligne * 77, 80, 77);
                        }
                        if (Game1.player.Lvl >= 2)
                        {
                            if (attackChoisi == "Fire Ball")
                            {
                                if (timerAnimation % 12 == 0)
                                {
                                    if (!stop)
                                    {
                                        ligne = 8;
                                        if (colonne == 3)
                                        {
                                            colonne = 0;
                                            ligne = 0;
                                            stop = true;
                                        }
                                        else
                                        {
                                            colonne++;
                                        }
                                        persoFightRectangle = new Rectangle(colonne * 80, ligne * 77, 80, 77);
                                    }
                                    if (nbreAnimationFire <= 4)
                                    {
                                        if (colonneFire == 4)
                                        {
                                            colonneFire = 0;
                                            nbreAnimationFire++;
                                        }
                                        else
                                        {
                                            colonneFire++;
                                            nbreAnimationFire++;
                                        }
                                        fireRectangle = new Rectangle(colonneFire * 30, 0, 30, 64);
                                    }
                                }
                            }
                        }
                        if (presentKey.IsKeyDown(Keys.Enter) && pastKey.IsKeyUp(Keys.Enter) && (attackChoisi == "Basic attack" || attackChoisi == "Fire Ball"))
                        {
                            if (degat >= Game1.enemy.health)
                            {
                                MediaPlayer.Play(songVictory);
                            }
                            Game1.enemy.health -= degat;
                            Game1.player.mana -= manaPerdu;
                            turn++;
                            timerAnimationDegat = 0;
                            attackChoisi = "";
                        }
                    }

                    else if (presentKey.IsKeyDown(Keys.Enter) && pastKey.IsKeyUp(Keys.Enter) && turn % 2 == 1 && Game1.player.health > 0 && Game1.enemy.health > 0) //enemy 
                    {
                        degatEnemy = rand.Next(100, 120) + Game1.enemy.strength / 2 + 15 * Game1.player.Lvl;
                        if (disablePlayer1)
                        {
                            Game1.player.health = Game1.player.health + Game1.player.Armor - degatEnemy;
                            if (turn > disablePlayer1End)
                            {
                                disablePlayer1 = false;
                                turn++;
                            }
                            else
                            {
                                turn += 2;
                            }
                        }
                        else
                        {
                            disablePlayer1 = (rand.Next(1, 5) == 1); // on peut disable que si le player ne l'est pas deja
                            if (disablePlayer1)
                            {
                                disablePlayer1End = turn + 5;
                                turn += 2;
                            }
                            else
                            {
                                Game1.player.health = Game1.player.health + Game1.player.Armor - degatEnemy;
                                turn++;
                            }

                        }
                        timerAnimationDegat = 0;

                    }
                    if (Game1.player.health <= 0)
                    {
                        CurrentGameState = Game1.GameState.GameOver;
                        MediaPlayer.Play(songGameOver);
                    }
                    if (Game1.enemy.health <= 0)
                    {
                        Game1.enemy.health = 0;
                        if (Game1.btnEndFight.isClicked)
                        {
                            Game1.player.Experience += Game1.enemy.healthMax;
                            Game1.player.persoPosition.X = Game1.previousPosX;
                            Game1.player.persoPosition.Y = Game1.previousPosY;
                            Game1.player.persoRectangle = new Rectangle((int)Game1.previousPosX, (int)Game1.previousPosY, Game1.player.persoRectangle.Width, Game1.player.persoRectangle.Height);
                            Game1.player.fight = false;
                            Game1.player.Gold += Game1.enemy.healthMax / 10;
                            CurrentGameState = Game1.GameState.Playing;
                            Isfighting = false;
                            Game1.enemy.enemyPosition.X = -100;
                            Game1.enemy.enemyPosition.Y = -100;
                            turn = -1;
                            Game1.enemy.enemyRectangle = new Rectangle(0, 0, 0, 0);
                            MediaPlayer.Play(song2);
                            attackChoisi = "";
                        }
                    }

                    Game1.btnStartFight.Update(mouse, gameTime);
                    btnAttack1.Update(mouse, gameTime);
                    btnObjects.Update(mouse, gameTime);
                    btnSpell.Update(mouse, gameTime);
                    Game1.btnEndFight.Update(mouse, gameTime);
                    pastKey = presentKey;
                    pastMouse = mouse;
                    return (CurrentGameState);
                }
                if (Playing.inventaire)
                {

                    timerinventaire++;
                    if (Playing.timerInventaire > 15)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter) || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        {
                            Playing.inventaire = false;
                            timerinventaire = 0;
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
                            if (mouseRectangle.Intersects(new Rectangle(30, 320, Playing.swordTexture.Width / 7, Playing.swordTexture.Height / 7)) && (mouse.LeftButton == ButtonState.Pressed) && Game1.pastMouse.LeftButton == ButtonState.Released)
                            {
                                /* Game1.invent.removeItemE(item);
                                 Game1.invent.addItem((new Item("Weapon","Sword","dmg", 30, 1,"notequiped")));*/
                                Game1.invent.deUseItem(item);
                            }
                        }
                        else
                        {
                            if (mouseRectangle.Intersects(new Rectangle(120, 125, Playing.armorTexture.Width, Playing.armorTexture.Height)) && (mouse.LeftButton == ButtonState.Pressed) && Game1.pastMouse.LeftButton == ButtonState.Released)
                            {
                                Game1.invent.deUseItem(item);
                            }
                        }
                    }
                }
                Game1.pastMouse = mouse;

                return (CurrentGameState);
            }
            else // ici 2 players
            {
                Game1.player.persoPosition.X = 200;
                Game1.player.persoPosition.Y = screenHeight / 2 + 100;
                Game1.player.fight = true;
                Game1.player.colonne = 2;
                Game1.player.Update(gameTime, Game1.GameState.Playing);

                Game1.player2.persoPosition.X = 200;
                Game1.player2.persoPosition.Y = Game1.player.persoPosition.Y + 100;
                Game1.player2.fight = true;

                healthBoxRectangle = new Rectangle(10, 10, healthBoxTexture.Width, healthBoxTexture.Height);
                healthRectangle = new Rectangle(16, 14, (Game1.player.health * 379) / Game1.player.healthMax, 35);
                manaRectangle = new Rectangle(115, 62, (Game1.player.mana * 280) / Game1.player.manaMax, manaTexture.Height);
                healthBoxRectangle2 = new Rectangle(150, 120, healthBoxTexture.Width, healthBoxTexture.Height);
                healthRectangle2 = new Rectangle(156, 124, (Game1.player2.health * 379) / Game1.player2.healthMax, 35);
                manaRectangle2 = new Rectangle(255, 172, (Game1.player2.mana * 280) / Game1.player2.manaMax, manaTexture.Height);

                enemyHealthRectangle = new Rectangle((1030 - Game1.enemy.health / 2), (screenHeight / 2 - enemyHealthTexture.Height / 2 + 60), Game1.enemy.health, enemyHealthTexture.Height);
                if (attackChoisi == "")
                {
                    ligne = 0;
                    if (timerAnimation == 15)
                    {
                        timerAnimation = 0;
                        if (colonne == 3)
                        {
                            colonne = 0;
                        }
                        else
                        {
                            colonne++;
                        }
                    }
                }
                if (!Playing.inventaire)
                {
                    timerAnimation++;
                    timerinventaire++;
                    if (turn == -1 && Game1.btnStartFight.isClicked)
                    {
                        turn = 0;
                        Game1.enemy.healthMax = Game1.enemy.health;
                    }

                    if (turnPlayer && turn % 2 == 0 && Game1.player.health > 0 && Game1.enemy.health > 0)
                    {
                        if (timerinventaire > 15)
                        {
                            if (btnObjects.isClicked && pastMouse.LeftButton == ButtonState.Released)
                            {
                                Playing.inventaire = true;
                                timerinventaire = 0;
                                btnObjects.isClicked = false;
                            }
                        }
                        if (btnAttack1.isClicked && pastMouse.LeftButton == ButtonState.Released)
                        {
                            btnAttack1.isClicked = false;
                            attackChoisi = "Basic attack";
                            btnAttack1.Update(mouse, gameTime);
                            timerAnimation = 0;
                            degat = Game1.player.Degat + rand.Next(0, 30) + Game1.player.Strenght / 2;
                            manaPerdu = 0;
                            stop = false;
                            colonne = 0;
                        }
                        if (Game1.player.Lvl >= 2)
                        {
                            if (btnSpell.isClicked && pastMouse.LeftButton == ButtonState.Released)
                            {
                                btnSpell.isClicked = false;
                                attackChoisi = "Fire Ball";
                                btnSpell.Update(mouse, gameTime);
                                timerAnimation = 0;
                                degat = rand.Next(80, 120) + Game1.player.Intelligence + Game1.player.Degat;
                                manaPerdu = 20;
                                stop = false;
                                colonne = 0;
                                colonneFire = 0;
                                nbreAnimationFire = 0;
                            }
                        }
                        if (attackChoisi == "Basic attack" && !stop)
                        {
                            ligne = 7;
                            if (timerAnimation % 12 == 0)
                            {
                                if (colonne == 3)
                                {
                                    colonne = 0;
                                    ligne = 0;
                                    stop = true;
                                }
                                else
                                {
                                    colonne++;
                                }
                            }
                            persoFightRectangle = new Rectangle(colonne * 80, ligne * 77, 80, 77);
                        }
                        if (Game1.player.Lvl >= 2)
                        {
                            if (attackChoisi == "Fire Ball")
                            {
                                if (timerAnimation % 12 == 0)
                                {
                                    if (!stop)
                                    {
                                        ligne = 8;
                                        if (colonne == 3)
                                        {
                                            colonne = 0;
                                            ligne = 0;
                                            stop = true;
                                        }
                                        else
                                        {
                                            colonne++;
                                        }
                                        persoFightRectangle = new Rectangle(colonne * 80, ligne * 77, 80, 77);
                                    }
                                    if (nbreAnimationFire <= 4)
                                    {
                                        if (colonneFire == 4)
                                        {
                                            colonneFire = 0;
                                            nbreAnimationFire++;
                                        }
                                        else
                                        {
                                            colonneFire++;
                                            nbreAnimationFire++;
                                        }
                                        fireRectangle = new Rectangle(colonneFire * 30, 0, 30, 64);
                                    }
                                }
                            }
                        }
                        if (presentKey.IsKeyDown(Keys.Enter) && pastKey.IsKeyUp(Keys.Enter) && (attackChoisi == "Basic attack" || attackChoisi == "Fire Ball"))
                        {
                            if (degat >= Game1.enemy.health)
                            {
                                MediaPlayer.Play(songVictory);
                            }
                            Game1.enemy.health -= degat;
                            Game1.player.mana -= manaPerdu;
                            turnPlayer = false;
                            if (disablePlayer2 || Game1.player2.health <= 0)
                            {
                                turn++;
                            }
                            else
                            {
                                turnPlayer2 = true;
                            }
                            timerAnimationDegat = 0;
                            attackChoisi = "";
                        }

                    }

                    else if (turnPlayer2 && Game1.player2.health > 0)
                    {
                        if (btnAttack1.isClicked && pastMouse.LeftButton == ButtonState.Released)
                        {
                            btnAttack1.isClicked = false;
                            attackChoisi = "Basic attack";
                            btnAttack1.Update(mouse, gameTime);
                            timerAnimation = 0;
                            degat = Game1.player2.Degat + rand.Next(0, 30) + Game1.player2.Strenght / 2;
                            manaPerdu = 0;
                            stop = false;
                            colonne = 0;
                        }
                        if (presentKey.IsKeyDown(Keys.Enter) && pastKey.IsKeyUp(Keys.Enter) && (attackChoisi == "Basic attack" || attackChoisi == "Fire Ball"))
                        {
                            if (degat >= Game1.enemy.health)
                            {
                                MediaPlayer.Play(songVictory);
                            }
                            Game1.enemy.health -= degat;
                            Game1.player2.mana -= manaPerdu;
                            turn++;
                            turnPlayer = false;
                            turnPlayer2 = false;
                            timerAnimationDegat = 0;
                            attackChoisi = "";
                        }
                    }
                    else if (presentKey.IsKeyDown(Keys.Enter) && pastKey.IsKeyUp(Keys.Enter) && turn % 2 == 1 && Game1.enemy.health > 0) //enemy 
                    {

                        if (disablePlayer1 && turn > disablePlayer1End)
                        {
                            disablePlayer1 = false;
                        }
                        if (disablePlayer2 && turn > disablePlayer2End)
                        {
                            disablePlayer2 = false;
                        }
                        if ((rand.Next(1, 3) == 1 && Game1.player.health > 0) || (Game1.player2.health <= 0))  // on attaque le player1 si on tombe sur lui en random ou si l'autre est dead
                        {
                            degatEnemy = rand.Next(100, 120) + Game1.enemy.strength / 2 + 15 * Game1.player.Lvl;
                            attaquePlayer1 = true;
                            if (!disablePlayer1)
                            {
                                disablePlayer1 = (rand.Next(1, 80) == 1);
                                if (disablePlayer1) //si on disable
                                {
                                    disablePlayer1End = turn + 5;
                                }
                                else //sinon on fait des degats sur le P1
                                {
                                    degatEnemy -= Game1.player.Armor;
                                    Game1.player.health = Game1.player.health - degatEnemy;
                                }
                            }
                            else //sinon on fait des degats sur le P1
                            {
                                degatEnemy -= Game1.player.Armor;
                                Game1.player.health = Game1.player.health - degatEnemy;
                            }
                            if (Game1.player.health <= 0)
                            {
                                Game1.player.health = 0;
                            }
                            if (disablePlayer1 || Game1.player.health <= 0)
                            {
                                turnPlayer = false;
                                turnPlayer2 = true;
                                turn++;
                            }
                            else if ((disablePlayer1 || Game1.player.health <= 0) && (disablePlayer2 || Game1.player.health <= 0))
                            {
                                turn += 2;
                            }
                            else
                            {
                                turnPlayer = true;
                                turn++;
                            }
                        }
                        else //on attaque le player2
                        {
                            degatEnemy = rand.Next(100, 120) + Game1.enemy.strength / 2 + 15 * Game1.player2.Lvl;
                            attaquePlayer1 = false;
                            if (!disablePlayer2)
                            {
                                disablePlayer2 = (rand.Next(1, 8) == 1);
                                if (disablePlayer2) //si on disable
                                {
                                    disablePlayer2End = turn + 5;
                                }
                                else //sinon on fait des degats sur le P2
                                {
                                    degatEnemy -= Game1.player2.Armor;
                                    Game1.player2.health = Game1.player2.health - degatEnemy;
                                }
                            }
                            else //sinon on fait des degats sur le P2
                            {
                                degatEnemy -= Game1.player2.Armor;
                                Game1.player2.health = Game1.player2.health - degatEnemy;
                            }
                            if (Game1.player2.health <= 0)
                            {
                                Game1.player2.health = 0;
                            }
                            if (disablePlayer1 || Game1.player.health <= 0)
                            {
                                turnPlayer = false;
                                turnPlayer2 = true;
                                turn++;
                            }
                            else if ((disablePlayer1 || Game1.player.health <= 0) && (disablePlayer2 || Game1.player.health <= 0))
                            {
                                turn += 2;
                            }
                            else
                            {
                                turnPlayer = true;
                                turn++;
                            }
                        }

                        timerAnimationDegat = 0;
                    }
                    if ((Game1.player.health <= 0 && Playing.nbjoueurs == 1) || (Playing.nbjoueurs == 2 && Game1.player.health <= 0 && Game1.player2.health <= 0))
                    {
                        CurrentGameState = Game1.GameState.GameOver;
                        MediaPlayer.Play(songGameOver);
                    }

                    if (Game1.enemy.health <= 0)
                    {
                        Game1.enemy.health = 0;
                        if (Game1.btnEndFight.isClicked)
                        {
                            Game1.player.Experience += Game1.enemy.healthMax;
                            Game1.player2.Experience += Game1.enemy.healthMax;
                            Game1.player.persoPosition.X = Game1.previousPosX;
                            Game1.player.persoPosition.Y = Game1.previousPosY;
                            Game1.player2.persoPosition.X = Game1.previousPosX;
                            Game1.player2.persoPosition.Y = Game1.previousPosY;
                            Game1.player.persoRectangle = new Rectangle((int)Game1.previousPosX, (int)Game1.previousPosY, Game1.player.persoRectangle.Width, Game1.player.persoRectangle.Height);
                            Game1.player2.persoRectangle = new Rectangle((int)Game1.previousPosX, (int)Game1.previousPosY, Game1.player2.persoRectangle.Width, Game1.player2.persoRectangle.Height);
                            Game1.player.fight = false;
                            Game1.player2.fight = false;
                            Game1.player.Gold += Game1.enemy.healthMax / 10;
                            Game1.player2.Gold += Game1.enemy.healthMax / 10;
                            CurrentGameState = Game1.GameState.Playing;
                            Isfighting = false;
                            Game1.enemy.enemyPosition.X = -100;
                            Game1.enemy.enemyPosition.Y = -100;
                            turn = -1;
                            Game1.enemy.enemyRectangle = new Rectangle(0, 0, 0, 0);
                            MediaPlayer.Play(song2);
                            attackChoisi = "";
                        }
                    }
                    Game1.btnStartFight.Update(mouse, gameTime);
                    btnObjects.Update(mouse, gameTime);
                    btnSpell.Update(mouse, gameTime);
                    btnAttack1.Update(mouse, gameTime);
                    Game1.btnNext.Update(mouse, gameTime);
                    pastKey = presentKey;
                    pastMouse = mouse;
                }
                if (Playing.inventaire)
                {

                    timerinventaire++;
                    if (Playing.timerInventaire > 15)
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter) || Keyboard.GetState().IsKeyDown(Keys.Escape))
                        {
                            Playing.inventaire = false;
                            timerinventaire = 0;
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
                            if (mouseRectangle.Intersects(new Rectangle(30, 320, Playing.swordTexture.Width / 7, Playing.swordTexture.Height / 7)) && (mouse.LeftButton == ButtonState.Pressed) && Game1.pastMouse.LeftButton == ButtonState.Released)
                            {
                                //Game1.invent.removeItemE(item);
                                //Game1.invent.addItem((new Item("Weapon","Sword","dmg", 30, 1,"notequiped")));
                                Game1.invent.deUseItem(item);
                            }
                        }
                        else
                        {
                            if (mouseRectangle.Intersects(new Rectangle(120, 125, Playing.armorTexture.Width, Playing.armorTexture.Height)) && (mouse.LeftButton == ButtonState.Pressed) && Game1.pastMouse.LeftButton == ButtonState.Released)
                            {
                                Game1.invent.deUseItem(item);
                            }
                        }
                    }
                }
                Game1.pastMouse = mouse;
                return (CurrentGameState);
            }
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch, int screenWidth, int screenHeight)
        {
            spriteBatch.Draw(fightBackTexture, fightBackRectangle, Color.White);
            //Game1.player.Draw(spriteBatch);
            if (Playing.map == Playing.map8)
            {
                spriteBatch.Draw(Game1.enemy.enemyTexture, new Vector2(990, screenHeight / 2 + 80), new Rectangle(2 * Game1.enemy.Rectenemy.Width, 1 * Game1.enemy.Rectenemy.Height, Game1.enemy.Rectenemy.Width, Game1.enemy.Rectenemy.Height), Color.White);
            }
            if (Playing.map == Playing.map6)
            {
                spriteBatch.Draw(Game1.enemy.enemyTexture, new Vector2(990, screenHeight / 2 + 80), new Rectangle(2 * Game1.enemy.Rectenemy.Width, 1 * Game1.enemy.Rectenemy.Height, Game1.enemy.Rectenemy.Width, Game1.enemy.Rectenemy.Height), Color.White);
            }
            if (Playing.map == Playing.map5)
            {
                spriteBatch.Draw(Game1.enemy.enemyTexture, new Vector2(970, screenHeight / 2 + 120), new Rectangle(2 * Game1.enemy.Rectenemy.Width, 1 * Game1.enemy.Rectenemy.Height, Game1.enemy.Rectenemy.Width, Game1.enemy.Rectenemy.Height), Color.White);
            }

            if (Playing.nbjoueurs == 1)
            {
                spriteBatch.Draw(healthBoxTexture, healthBoxRectangle, Color.White);
                spriteBatch.Draw(manaTexture, manaRectangle, Color.White);
                spriteBatch.Draw(healthTexture, healthRectangle, Color.White);
                spriteBatch.DrawString(Game1.spriteFont, Game1.player.health + "/" + Game1.player.healthMax, new Vector2(healthTexture.Width / 2 - 16, 21), Color.White);
                spriteBatch.DrawString(Game1.spriteFont, Game1.player.mana + "/" + Game1.player.manaMax, new Vector2(manaTexture.Width / 2 + 88, 60), Color.White);
                spriteBatch.Draw(enemyHealthTexture, enemyHealthRectangle, Color.White);
                spriteBatch.DrawString(Game1.spriteFont, Game1.enemy.health + "/" + Game1.enemy.healthMax, new Vector2(995, screenHeight / 2 + 49), Color.Black);
                spriteBatch.Draw(speechBoxTexture, speechBoxRectangle, Color.White);
                spriteBatch.Draw(persoFight, persoFightPosition, persoFightRectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.FlipHorizontally, 0);
                spriteBatch.Draw(fireTexture, firePosition, fireRectangle, Color.White);
            }

            if (Playing.nbjoueurs == 2)
            {

                //palyer1
                spriteBatch.Draw(persoFight, persoFightPosition, persoFightRectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.FlipHorizontally, 0);
                spriteBatch.Draw(healthBoxTexture, healthBoxRectangle, Color.White);
                spriteBatch.Draw(manaTexture, manaRectangle, Color.White);
                spriteBatch.Draw(healthTexture, healthRectangle, Color.White);
                spriteBatch.DrawString(Game1.spriteFont, Game1.player.health + "/" + Game1.player.healthMax, new Vector2(healthTexture.Width / 2 - 16, 21), Color.White);
                spriteBatch.DrawString(Game1.spriteFont, Game1.player.mana + "/" + Game1.player.manaMax, new Vector2(manaTexture.Width / 2 + 88, 60), Color.White);
                //player2
                Game1.player2.Rectsprite = new Rectangle(2 * 32, 3 * 63, 30, 63);
                spriteBatch.Draw(Game1.player2.persoTexture, Game1.player2.persoPosition, Game1.player2.Rectsprite, Color.White);
                spriteBatch.Draw(healthBoxTexture, healthBoxRectangle2, Color.White);
                spriteBatch.Draw(healthTexture, healthRectangle2, Color.White);
                spriteBatch.Draw(manaTexture, manaRectangle2, Color.White);
                spriteBatch.DrawString(Game1.spriteFont, Game1.player2.health + "/" + Game1.player2.healthMax, new Vector2(healthTexture.Width / 2 + 124, 131), Color.White);
                spriteBatch.DrawString(Game1.spriteFont, Game1.player2.mana + "/" + Game1.player2.manaMax, new Vector2(manaTexture.Width / 2 + 228, 170), Color.White);
                //enemy
                spriteBatch.Draw(enemyHealthTexture, enemyHealthRectangle, Color.White);
                spriteBatch.DrawString(Game1.spriteFont, Game1.enemy.health + "/" + Game1.enemy.healthMax, new Vector2(995, screenHeight / 2 + 49), Color.Black);
                //Speech
                spriteBatch.Draw(speechBoxTexture, speechBoxRectangle, Color.White);
                spriteBatch.Draw(fireTexture, firePosition, fireRectangle, Color.White);

            }
            if (Game1.player.health <= 0)
            {
                spriteBatch.DrawString(Game1.spriteFont, "DEAD", new Vector2(100, 390), Color.Red);
            }
            if (Game1.player2.health <= 0)
            {
                spriteBatch.DrawString(Game1.spriteFont, "DEAD", new Vector2(195, 500), Color.Red);
            }
            if (turn == -1)
            {
                spriteBatch.DrawString(Game1.spriteFont, "You're being attacked !!!", new Vector2(10, 700), Color.Black);
                Game1.btnStartFight.Draw(spriteBatch);
            }
            if (((turn % 2 == 1) && Game1.enemy.health > 0) || (Playing.nbjoueurs == 2 && turnPlayer2)) // on fait l'animation des degats au début du tour suivant
            {
                timerAnimationDegat++;
                if (disablePlayer1 && Playing.nbjoueurs == 1)
                {
                    if (timerAnimationDegat <= 20)
                    {
                        spriteBatch.DrawString(Game1.spriteFont, "disable", new Vector2(100, 390), Color.Orange);
                        lightRectangle = new Rectangle(0, 0, 38, 65);
                        spriteBatch.Draw(lightTexture, lightPosition, lightRectangle, Color.White);
                    }

                    if (timerAnimationDegat >= 20 && timerAnimationDegat < 40)
                    {
                        spriteBatch.DrawString(Game1.spriteFont, "disable", new Vector2(100, 370), Color.OrangeRed);
                        lightRectangle = new Rectangle(38, 0, 38, 65);
                        spriteBatch.Draw(lightTexture, lightPosition, lightRectangle, Color.White);
                    }
                    if (timerAnimationDegat >= 40 && timerAnimationDegat < 60)
                    {
                        lightRectangle = new Rectangle(76, 0, 38, 65);
                        spriteBatch.Draw(lightTexture, lightPosition, lightRectangle, Color.White);
                        spriteBatch.DrawString(Game1.spriteFont, degatEnemy + "", new Vector2(110, 390), Color.Black);
                    }
                    if (timerAnimationDegat >= 60 && timerAnimationDegat < 80)
                    {
                        spriteBatch.DrawString(Game1.spriteFont, degatEnemy + "", new Vector2(110, 370), Color.Gray);
                    }
                    spriteBatch.DrawString(Game1.spriteFont, "Player1 : You're disable you can't attack for now", new Vector2(500, 700), Color.Black);
                }
                else if (Game1.player.health <= 0 && turnPlayer2)
                {

                    if (!attaquePlayer1)
                    {
                        if (timerAnimationDegat <= 20)
                        {
                            spriteBatch.Draw(lightTexture, lightPosition2, lightRectangle, Color.White);
                        }
                        if (timerAnimationDegat >= 20 && timerAnimationDegat < 40)
                        {
                            lightRectangle = new Rectangle(38, 0, 38, 65);
                            spriteBatch.Draw(lightTexture, lightPosition2, lightRectangle, Color.White);
                        }
                        if (timerAnimationDegat >= 40 && timerAnimationDegat < 60)
                        {
                            lightRectangle = new Rectangle(76, 0, 38, 65);
                            spriteBatch.Draw(lightTexture, lightPosition2, lightRectangle, Color.White);
                            spriteBatch.DrawString(Game1.spriteFont, degatEnemy + "", new Vector2(195, 500), Color.Black);
                        }
                        if (timerAnimationDegat >= 60 && timerAnimationDegat < 80)
                        {
                            spriteBatch.DrawString(Game1.spriteFont, degatEnemy + "", new Vector2(195, 480), Color.Gray);
                        }
                    }

                }
                /*else if (Game1.player2.health <= 0 )
                {
                    
                    if (timerAnimationDegat <= 20)
                    {
                        spriteBatch.Draw(lightTexture, lightPosition, lightRectangle, Color.White);
                    }
                    if (timerAnimationDegat >= 20 && timerAnimationDegat < 40)
                    {
                        lightRectangle = new Rectangle(38, 0, 38, 65);
                        spriteBatch.Draw(lightTexture, lightPosition, lightRectangle, Color.White);
                    }
                    if (timerAnimationDegat >= 40 && timerAnimationDegat < 60)
                    {
                        lightRectangle = new Rectangle(76, 0, 38, 65);
                        spriteBatch.Draw(lightTexture, lightPosition, lightRectangle, Color.White);
                        spriteBatch.DrawString(Game1.spriteFont, degatEnemy + "", new Vector2(110, 390), Color.Black);
                    }
                    if (timerAnimationDegat >= 60 && timerAnimationDegat < 80)
                    {
                        spriteBatch.DrawString(Game1.spriteFont, degatEnemy + "", new Vector2(110, 370), Color.Gray);
                    }
                }*/
                else if (disablePlayer1 && Playing.nbjoueurs == 2 && turnPlayer2)  //si 2players et player1 disable
                {

                    if (timerAnimationDegat <= 20)
                    {
                        spriteBatch.DrawString(Game1.spriteFont, "disable", new Vector2(100, 390), Color.Orange);
                        lightRectangle = new Rectangle(0, 0, 38, 65);
                        if (attaquePlayer1 && turn != disablePlayer1End - 4)
                        {
                            spriteBatch.Draw(lightTexture, lightPosition, lightRectangle, Color.White);
                        }
                        else if (!attaquePlayer1)
                        {
                            spriteBatch.Draw(lightTexture, lightPosition2, lightRectangle, Color.White);
                        }
                    }

                    if (timerAnimationDegat >= 20 && timerAnimationDegat < 40)
                    {
                        spriteBatch.DrawString(Game1.spriteFont, "disable", new Vector2(100, 370), Color.OrangeRed);
                        lightRectangle = new Rectangle(38, 0, 38, 65);
                        if (attaquePlayer1 && turn != disablePlayer1End - 4)
                        {
                            spriteBatch.Draw(lightTexture, lightPosition, lightRectangle, Color.White);
                        }
                        else if (!attaquePlayer1)
                        {
                            spriteBatch.Draw(lightTexture, lightPosition2, lightRectangle, Color.White);
                        }
                    }
                    if (timerAnimationDegat >= 40 && timerAnimationDegat < 60)
                    {
                        lightRectangle = new Rectangle(76, 0, 38, 65);
                        if (attaquePlayer1 && turn != disablePlayer1End - 4)
                        {
                            spriteBatch.Draw(lightTexture, lightPosition, lightRectangle, Color.White);
                            spriteBatch.DrawString(Game1.spriteFont, degatEnemy + "", new Vector2(110, 390), Color.Black);
                        }
                        else if (!attaquePlayer1)
                        {
                            spriteBatch.Draw(lightTexture, lightPosition2, lightRectangle, Color.White);
                            spriteBatch.DrawString(Game1.spriteFont, degatEnemy + "", new Vector2(195, 500), Color.Black);
                        }

                    }
                    if (timerAnimationDegat >= 60 && timerAnimationDegat < 80)
                    {
                        if (attaquePlayer1 && turn != disablePlayer1End - 4)
                        {
                            spriteBatch.DrawString(Game1.spriteFont, degatEnemy + "", new Vector2(110, 370), Color.Gray);
                        }
                        else if (!attaquePlayer1)
                        {
                            spriteBatch.DrawString(Game1.spriteFont, degatEnemy + "", new Vector2(195, 480), Color.Gray);
                        }
                    }
                    spriteBatch.DrawString(Game1.spriteFont, "Player1 : You're disable you can't attack for now", new Vector2(500, 700), Color.Black);
                }

                else
                {
                    if (timerAnimationDegat <= 20)
                    {
                        spriteBatch.DrawString(Game1.spriteFont, degat + "", new Vector2(1020, 350), Color.Black);
                    }
                    if (timerAnimationDegat >= 20 && timerAnimationDegat < 40)
                    {
                        spriteBatch.DrawString(Game1.spriteFont, degat + "", new Vector2(1020, 330), Color.Gray);
                    }
                    if (!turnPlayer2 && !turnPlayer)
                    {
                        spriteBatch.DrawString(Game1.spriteFont, "The ennemy attacks you", new Vector2(10, 700), Color.Black);
                    }

                }
                Game1.spriteBatch.DrawString(Game1.spriteFont, "Press Enter to continue", new Vector2(1100, 725), Color.Black);
            }

            if (turn % 2 == 0 && attackChoisi == "" && (Game1.player.health >= 0 || Game1.player2.health >= 0) && Game1.enemy.health > 0)
            {
                btnAttack1.Draw(spriteBatch);
                btnObjects.Draw(spriteBatch);

                if (Game1.player.Lvl >= 2)
                    btnSpell.Draw(spriteBatch);
                else if (Playing.nbjoueurs == 2 && disablePlayer2) //2player et player2 disable
                {
                    timerAnimationDegat++;
                    if (timerAnimationDegat <= 20)
                    {
                        spriteBatch.DrawString(Game1.spriteFont, "disable", new Vector2(195, 480), Color.Orange);
                        lightRectangle = new Rectangle(0, 0, 38, 65);
                        if (!attaquePlayer1 && turn != disablePlayer2End - 4)
                        {
                            spriteBatch.Draw(lightTexture, lightPosition2, lightRectangle, Color.White);
                        }
                        else if (attaquePlayer1)
                        {
                            spriteBatch.Draw(lightTexture, lightPosition, lightRectangle, Color.White);
                        }
                    }

                    if (timerAnimationDegat >= 20 && timerAnimationDegat < 40)
                    {
                        spriteBatch.DrawString(Game1.spriteFont, "disable", new Vector2(195, 460), Color.OrangeRed);
                        lightRectangle = new Rectangle(38, 0, 38, 65);
                        if (!attaquePlayer1 && turn != disablePlayer2End - 4)
                        {
                            spriteBatch.Draw(lightTexture, lightPosition2, lightRectangle, Color.White);
                        }
                        else if (attaquePlayer1)
                        {
                            spriteBatch.Draw(lightTexture, lightPosition, lightRectangle, Color.White);
                        }
                    }
                    if (timerAnimationDegat >= 40 && timerAnimationDegat < 60)
                    {
                        lightRectangle = new Rectangle(76, 0, 38, 65);
                        if (!attaquePlayer1 && turn != disablePlayer2End - 4)
                        {
                            spriteBatch.Draw(lightTexture, lightPosition2, lightRectangle, Color.White);
                            spriteBatch.DrawString(Game1.spriteFont, degatEnemy + "", new Vector2(195, 500), Color.Black);
                        }
                        else if (attaquePlayer1)
                        {
                            spriteBatch.Draw(lightTexture, lightPosition, lightRectangle, Color.White);
                            spriteBatch.DrawString(Game1.spriteFont, degatEnemy + "", new Vector2(110, 390), Color.Black);
                        }

                    }
                    if (timerAnimationDegat >= 60 && timerAnimationDegat < 80)
                    {
                        if (!attaquePlayer1 && turn != disablePlayer2End - 4)
                        {
                            spriteBatch.DrawString(Game1.spriteFont, degatEnemy + "", new Vector2(195, 480), Color.Gray);
                        }
                        else if (attaquePlayer1)
                        {
                            spriteBatch.DrawString(Game1.spriteFont, degatEnemy + "", new Vector2(110, 370), Color.Gray);
                        }
                    }
                    spriteBatch.DrawString(Game1.spriteFont, "Player2 : You're disable you can't attack for now", new Vector2(500, 700), Color.Black);
                }
                if (turn != 0 && turnPlayer2 == false && !disablePlayer1 && !disablePlayer2)
                {
                    if (Playing.nbjoueurs == 1 || attaquePlayer1)
                    {
                        timerAnimationDegat++;
                        if (timerAnimationDegat <= 20)
                        {
                            lightRectangle = new Rectangle(0, 0, 38, 65);
                            spriteBatch.Draw(lightTexture, lightPosition, lightRectangle, Color.White);
                        }
                        if (timerAnimationDegat >= 20 && timerAnimationDegat < 40)
                        {
                            lightRectangle = new Rectangle(38, 0, 38, 65);
                            spriteBatch.Draw(lightTexture, lightPosition, lightRectangle, Color.White);
                        }
                        if (timerAnimationDegat >= 40 && timerAnimationDegat < 60)
                        {
                            lightRectangle = new Rectangle(76, 0, 38, 65);
                            spriteBatch.Draw(lightTexture, lightPosition, lightRectangle, Color.White);
                            spriteBatch.DrawString(Game1.spriteFont, degatEnemy + "", new Vector2(110, 390), Color.Black);
                        }
                        if (timerAnimationDegat >= 60 && timerAnimationDegat < 80)
                        {
                            spriteBatch.DrawString(Game1.spriteFont, degatEnemy + "", new Vector2(110, 370), Color.Gray);
                        }
                    }
                    else //animation sur le player2
                    {
                        timerAnimationDegat++;
                        if (timerAnimationDegat <= 20)
                        {
                            lightRectangle = new Rectangle(0, 0, 38, 65);
                            spriteBatch.Draw(lightTexture, lightPosition2, lightRectangle, Color.White);
                        }
                        if (timerAnimationDegat >= 20 && timerAnimationDegat < 40)
                        {
                            lightRectangle = new Rectangle(38, 0, 38, 65);
                            spriteBatch.Draw(lightTexture, lightPosition2, lightRectangle, Color.White);
                        }
                        if (timerAnimationDegat >= 40 && timerAnimationDegat < 60)
                        {

                            lightRectangle = new Rectangle(76, 0, 38, 65);
                            spriteBatch.Draw(lightTexture, lightPosition2, lightRectangle, Color.White);
                            spriteBatch.DrawString(Game1.spriteFont, degatEnemy + "", new Vector2(195, 500), Color.Black);
                        }
                        if (timerAnimationDegat >= 60 && timerAnimationDegat < 80)
                        {
                            spriteBatch.DrawString(Game1.spriteFont, degatEnemy + "", new Vector2(195, 480), Color.Gray);
                        }
                    }
                }
                if (!turnPlayer2)
                {
                    spriteBatch.DrawString(Game1.spriteFont, "Player1: It's your turn choose your fate", new Vector2(10, 700), Color.Black);
                }


            }
            if (turn % 2 == 0 && (attackChoisi != "") || (Playing.nbjoueurs == 2 && turnPlayer2))
            {
                if (turnPlayer2)
                {
                    spriteBatch.DrawString(Game1.spriteFont, "Player2 : You use the attack: " + attackChoisi, new Vector2(10, 700), Color.Black);
                    Game1.spriteBatch.DrawString(Game1.spriteFont, "Press Enter to continue", new Vector2(1100, 725), Color.Black);
                }
                else
                {
                    spriteBatch.DrawString(Game1.spriteFont, "Player1 : You use the attack: " + attackChoisi, new Vector2(10, 700), Color.Black);
                    Game1.spriteBatch.DrawString(Game1.spriteFont, "Press Enter to continue", new Vector2(1100, 725), Color.Black);
                }

            }

            else if (Game1.enemy.health <= 0)
            {
                timerAnimationDegat++;
                if (timerAnimationDegat <= 20)
                {
                    spriteBatch.DrawString(Game1.spriteFont, degat + "", new Vector2(1020, 350), Color.Black);
                }
                if (timerAnimationDegat >= 20 && timerAnimationDegat < 40)
                {
                    spriteBatch.DrawString(Game1.spriteFont, degat + "", new Vector2(1020, 330), Color.Gray);
                }
                Game1.btnEndFight.Draw(spriteBatch);
                spriteBatch.DrawString(Game1.spriteFont, "You win !!!", new Vector2(10, 700), Color.Black);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "Clik the arrow to continue", new Vector2(1100, 730), Color.Black);

            }

            if (Playing.inventaire)
            {
                spriteBatch.Draw(Playing.inventaireTexture, Playing.inventaireRectangle, Color.White);
                /*if (Game1.bookState == 1)
                {
                    spriteBatch.Draw(bookTexture, bookRectangle2, Color.White);
                }*/
                Game1.spriteBatch.DrawString(Game1.spriteFont, "Claudius", new Vector2(520, 15), Color.Black);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "" + Game1.player.Lvl, new Vector2(495, 45), Color.White);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "" + Game1.player.Experience + "/" + (Game1.player.Lvl * 100), new Vector2(495, 65), Color.White);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "" + Game1.player.health + "/" + Game1.player.healthMax, new Vector2(520, 225), Color.Red);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "" + Game1.player.mana + "/" + Game1.player.manaMax, new Vector2(520, 250), Color.Blue);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "" + Game1.player.Intelligence, new Vector2(545, 270), Color.Black);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "" + Game1.player.Armor, new Vector2(515, 290), Color.Black);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "" + Game1.player.Degat, new Vector2(535, 308), Color.Black);
                Game1.spriteBatch.DrawString(Game1.spriteFont, "" + Game1.player.Strenght, new Vector2(525, 325), Color.Black);

                foreach (Item item in Game1.invent.tablObjects)
                {
                    if (item.name != "rien")
                    {
                        Game1.spriteBatch.DrawString(Game1.spriteFont, "" + item.total, new Vector2((item.place % 6) * 68 + 15, 525 + 68 * (item.place / 6)), Color.White);
                        switch (item.name)
                        {
                            case "healthPotion":
                                spriteBatch.Draw(Playing.healthPotionTexture, new Rectangle((item.place % 6) * 68 + 25, 482 + 68 * (item.place / 6), 39, 64), Color.White);
                                break;

                            case "manaPotion":
                                spriteBatch.Draw(Playing.manaPotionTexture, new Rectangle((item.place % 6) * 68 + 25, 482 + 68 * (item.place / 6), 39, 64), Color.White);
                                break;

                            case "Sword":
                                spriteBatch.Draw(Playing.swordTexture, new Rectangle((item.place % 6) * 68 + 25, 482 + 68 * (item.place / 6), 39, 64), Color.White);
                                break;

                            case "Armor":
                                spriteBatch.Draw(Playing.armorTexture, new Rectangle((item.place % 6) * 68 + 25, 482 + 68 * (item.place / 6), 39, 64), Color.White);

                                break;

                            case "Book":
                                spriteBatch.Draw(Playing.bookTexture, new Rectangle((item.place % 6) * 68 + 25, 482 + 68 * (item.place / 6), 39, 64), Color.White);
                                break;

                        }
                    }
                }
                foreach (Item item in Game1.invent.tablEquiped)
                {
                    switch (item.name)
                    {
                        case "Sword":
                            spriteBatch.Draw(Playing.swordTexture, new Rectangle(30, 320, Playing.swordTexture.Width / 7, Playing.swordTexture.Height / 7), Color.White);
                            break;

                        case "Armor":
                            spriteBatch.Draw(Playing.armorTexture, new Rectangle(120, 125, Playing.armorTexture.Width, Playing.armorTexture.Height), Color.White);
                            break;
                    }
                }
            }

        }
    }
}
