using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.GamerServices;

namespace Project
{
    public class Load
    {
        int nblignes;

        public static void Update()
        {
            StreamReader sr;
            string name1 = "lol";
            string name2 = "lol";
            int amount1;
            int amount2;
            string type = "lol";
            string type2 = "lol";
            try
            {
                sr = File.OpenText("save2.txt");
                Playing.nbjoueurs = int.Parse(sr.ReadLine());
                Game1.questState = int.Parse(sr.ReadLine());
                switch (int.Parse(sr.ReadLine()))
                {
                    case 2:
                        Playing.map = Playing.map2;
                        break;
                    case 4:
                        Playing.map = Playing.map4;
                        break;
                    case 5:
                        Playing.map = Playing.map5;
                        break;
                    case 6:
                        Playing.map = Playing.map6;
                        break;
                    case 8:
                        Playing.map = Playing.map8;
                        break;
                    case 9:
                        Playing.map = Playing.mapShop;
                        break;
                    case 10:
                        Playing.map = Playing.mapChateauExt;
                        break;
                    case 11:
                        Playing.map = Playing.mapChateauInt;
                        break;
                }
                switch (Playing.nbjoueurs)
                {
                    case 1:
                        Game1.player.persoPosition.X = int.Parse(sr.ReadLine());
                        Game1.player.persoPosition.Y = int.Parse(sr.ReadLine());
                        Game1.player.persoRectangle.X = int.Parse(sr.ReadLine());
                        Game1.player.persoRectangle.Y = int.Parse(sr.ReadLine());
                        Game1.player.persoRectangle.Width = int.Parse(sr.ReadLine());
                        Game1.player.persoRectangle.Height = int.Parse(sr.ReadLine());
                        Game1.player.health = int.Parse(sr.ReadLine());
                        Game1.player.mana = int.Parse(sr.ReadLine());
                        Game1.player.manaMax = int.Parse(sr.ReadLine());
                        Game1.player.healthMax = int.Parse(sr.ReadLine());
                        Game1.player.Experience = int.Parse(sr.ReadLine());
                        Game1.player.Strenght = int.Parse(sr.ReadLine());
                        Game1.player.Intelligence = int.Parse(sr.ReadLine());
                        Game1.player.Degat = int.Parse(sr.ReadLine());
                        Game1.player.Armor = int.Parse(sr.ReadLine());
                        Game1.player.Gold = int.Parse(sr.ReadLine());
                        Game1.player.Lvl = int.Parse(sr.ReadLine());
                        Game1.invent1.Initialize();
                        type = sr.ReadLine();
                        while (type != "Stop")
                        {
                            if (type == "Equip")
                            {
                                name1 = sr.ReadLine();
                                amount1 = int.Parse(sr.ReadLine());
                                Game1.invent1.addItemtoequip(new Item(name1, amount1));
                            }
                            else if (type == "Obj")
                            {
                                name1 = sr.ReadLine();
                                amount1 = int.Parse(sr.ReadLine());
                                Game1.invent1.addItem(new Item(name1, amount1));
                            }
                            type = sr.ReadLine();
                        }

                        sr.Close();
                        break;


                    case 2:
                        Game1.player.persoPosition.X = int.Parse(sr.ReadLine());
                        Game1.player2.persoPosition.X = int.Parse(sr.ReadLine());
                        Game1.player.persoPosition.Y = int.Parse(sr.ReadLine());
                        Game1.player2.persoPosition.Y = int.Parse(sr.ReadLine());
                        Game1.player.persoRectangle.X = int.Parse(sr.ReadLine());
                        Game1.player2.persoRectangle.X = int.Parse(sr.ReadLine());
                        Game1.player.persoRectangle.Y = int.Parse(sr.ReadLine());
                        Game1.player2.persoRectangle.Y = int.Parse(sr.ReadLine());
                        Game1.player.persoRectangle.Width = int.Parse(sr.ReadLine());
                        Game1.player2.persoRectangle.Width = int.Parse(sr.ReadLine());
                        Game1.player.persoRectangle.Height = int.Parse(sr.ReadLine());
                        Game1.player2.persoRectangle.Height = int.Parse(sr.ReadLine());
                        Game1.player.health = int.Parse(sr.ReadLine());
                        Game1.player2.health = int.Parse(sr.ReadLine());
                        Game1.player.mana = int.Parse(sr.ReadLine());
                        Game1.player2.mana = int.Parse(sr.ReadLine());
                        Game1.player.manaMax = int.Parse(sr.ReadLine());
                        Game1.player2.manaMax = int.Parse(sr.ReadLine());
                        Game1.player.healthMax = int.Parse(sr.ReadLine());
                        Game1.player2.healthMax = int.Parse(sr.ReadLine());
                        Game1.player.Experience = int.Parse(sr.ReadLine());
                        Game1.player2.Experience = int.Parse(sr.ReadLine());
                        Game1.player.Strenght = int.Parse(sr.ReadLine());
                        Game1.player2.Strenght = int.Parse(sr.ReadLine());
                        Game1.player.Intelligence = int.Parse(sr.ReadLine());
                        Game1.player2.Intelligence = int.Parse(sr.ReadLine());
                        Game1.player.Degat = int.Parse(sr.ReadLine());
                        Game1.player2.Degat = int.Parse(sr.ReadLine());
                        Game1.player.Armor = int.Parse(sr.ReadLine());
                        Game1.player2.Armor = int.Parse(sr.ReadLine());
                        Game1.player.Gold = int.Parse(sr.ReadLine());
                        Game1.player2.Gold = int.Parse(sr.ReadLine());
                        Game1.player.Lvl = int.Parse(sr.ReadLine());
                        Game1.player2.Lvl = int.Parse(sr.ReadLine());
                        Game1.invent1.Initialize();
                        Game1.invent2.Initialize();
                        type = sr.ReadLine();
                        while (type != "Stop")
                        {
                            if (type == "Equip1")
                            {
                                name1 = sr.ReadLine();
                                amount1 = int.Parse(sr.ReadLine());
                                Game1.invent1.addItemtoequip(new Item(name1, amount1));

                            }

                            else if (type == "Equip2")
                            {
                                name2 = sr.ReadLine();
                                amount2 = int.Parse(sr.ReadLine());
                                Game1.invent2.addItemtoequip(new Item(name2, amount2));
                            }
                            else if (type == "Obj2")
                            {
                                name2 = sr.ReadLine();
                                amount2 = int.Parse(sr.ReadLine());
                                Game1.invent2.addItem(new Item(name2, amount2));
                            }


                            else if (type == "Obj1")
                            {
                                name1 = sr.ReadLine();
                                amount1 = int.Parse(sr.ReadLine());
                                Game1.invent1.addItem(new Item(name1, amount1));

                            }
                            type = sr.ReadLine();          
                        }
                        break;
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}

