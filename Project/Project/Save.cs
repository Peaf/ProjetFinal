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
    public class Save
    {
        static int line;
        public static int Line
        {
            get { return line; }
        }
        public static void Update()
        {

            FileStream stream;
            StreamWriter sw;
            line = 0;

            try
            {
                stream = new FileStream("save2.txt", FileMode.Create);
                sw = new StreamWriter(stream);
                sw.WriteLine(Playing.nbjoueurs);
                sw.WriteLine(Game1.questState);
                line++;
                if (Playing.map == Playing.map2)
                    sw.WriteLine(2);
                else if (Playing.map == Playing.map4)
                    sw.WriteLine(4);
                else if (Playing.map == Playing.map5)
                    sw.WriteLine(5);
                else if (Playing.map == Playing.map6)
                    sw.WriteLine(6);
                else if (Playing.map == Playing.map8)
                    sw.WriteLine(8);
                line++;
                switch (Playing.nbjoueurs)
                {
                    case 1:
                        sw.WriteLine(Game1.player.persoPosition.X);
                        line++;
                        sw.WriteLine(Game1.player.persoPosition.Y);
                        line++;
                        sw.WriteLine(Game1.player.persoRectangle.X);
                        line++;
                        sw.WriteLine(Game1.player.persoRectangle.Y);
                        line++;
                        sw.WriteLine(Game1.player.persoRectangle.Width);
                        line++;
                        sw.WriteLine(Game1.player.persoRectangle.Height);
                        line++;
                        sw.WriteLine(Game1.player.health);
                        line++;
                        sw.WriteLine(Game1.player.mana);
                        line++;
                        sw.WriteLine(Game1.player.manaMax);
                        line++;
                        sw.WriteLine(Game1.player.healthMax);
                        line++;
                        sw.WriteLine(Game1.player.Experience);
                        line++;
                        sw.WriteLine(Game1.player.Strenght);
                        line++;
                        sw.WriteLine(Game1.player.Intelligence);
                        line++;
                        sw.WriteLine(Game1.player.Degat);
                        line++;
                        sw.WriteLine(Game1.player.Armor);
                        line++;
                        sw.WriteLine(Game1.player.Gold);
                        line++;
                        sw.WriteLine(Game1.player.Lvl);
                        line++;
                        for (int i = 0; i < Game1.invent1.tablEquiped.Length; i++)
                        {

                            if (Game1.invent1.tablEquiped[i].name != "rien")
                            {
                                sw.WriteLine("Equip");
                                sw.WriteLine(Game1.invent1.tablEquiped[i].name);
                                line++;
                                sw.WriteLine(Game1.invent1.tablEquiped[i].total);
                                line++;
                            }
                        }
                        for (int i = 0; i < Game1.invent1.tablObjects.Length; i++)
                        {
                            if (Game1.invent1.tablObjects[i].name != "rien")
                            {
                                sw.WriteLine("Obj");
                                sw.WriteLine(Game1.invent1.tablObjects[i].name);
                                line++;
                                sw.WriteLine(Game1.invent1.tablObjects[i].total);
                                line++;
                            }
                        }
                        break;

                    case 2:
                        sw.WriteLine(Game1.player.persoPosition.X);
                        line++;
                        sw.WriteLine(Game1.player2.persoPosition.X);
                        line++;
                        sw.WriteLine(Game1.player.persoPosition.Y);
                        line++;
                        sw.WriteLine(Game1.player2.persoPosition.Y);
                        line++;
                        sw.WriteLine(Game1.player.persoRectangle.X);
                        line++;
                        sw.WriteLine(Game1.player2.persoRectangle.X);
                        line++;
                        sw.WriteLine(Game1.player.persoRectangle.Y);
                        line++;
                        sw.WriteLine(Game1.player2.persoRectangle.Y);
                        line++;
                        sw.WriteLine(Game1.player.persoRectangle.Width);
                        line++;
                        sw.WriteLine(Game1.player2.persoRectangle.Width);
                        line++;
                        sw.WriteLine(Game1.player.persoRectangle.Height);
                        line++;
                        sw.WriteLine(Game1.player2.persoRectangle.Height);
                        line++;
                        sw.WriteLine(Game1.player.health);
                        line++;
                        sw.WriteLine(Game1.player2.health);
                        line++;
                        sw.WriteLine(Game1.player.mana);
                        line++;
                        sw.WriteLine(Game1.player2.mana);
                        line++;
                        sw.WriteLine(Game1.player.manaMax);
                        line++;
                        sw.WriteLine(Game1.player2.manaMax);
                        line++;
                        sw.WriteLine(Game1.player.healthMax);
                        line++;
                        sw.WriteLine(Game1.player2.healthMax);
                        line++;
                        sw.WriteLine(Game1.player.Experience);
                        line++;
                        sw.WriteLine(Game1.player2.Experience);
                        line++;
                        sw.WriteLine(Game1.player.Strenght);
                        line++;
                        sw.WriteLine(Game1.player2.Strenght);
                        line++;
                        sw.WriteLine(Game1.player.Intelligence);
                        line++;
                        sw.WriteLine(Game1.player2.Intelligence);
                        line++;
                        sw.WriteLine(Game1.player.Degat);
                        line++;
                        sw.WriteLine(Game1.player2.Degat);
                        line++;
                        sw.WriteLine(Game1.player.Armor);
                        line++;
                        sw.WriteLine(Game1.player2.Armor);
                        line++;
                        sw.WriteLine(Game1.player.Gold);
                        line++;
                        sw.WriteLine(Game1.player2.Gold);
                        line++;
                        sw.WriteLine(Game1.player.Lvl);
                        line++;
                        sw.WriteLine(Game1.player2.Lvl);
                        line++;
                        for (int i = 0; i < Game1.invent1.tablEquiped.Length; i++)
                        {

                            if (Game1.invent1.tablEquiped[i].name != "rien")
                            {
                                sw.WriteLine("Equip1");
                                sw.WriteLine(Game1.invent1.tablEquiped[i].name);
                                line++;
                                sw.WriteLine(Game1.invent1.tablEquiped[i].total);
                                line++;
                                if (Game1.invent2.tablEquiped[i].name != "rien")
                                {
                                    sw.WriteLine("Equip2");
                                    sw.WriteLine(Game1.invent2.tablEquiped[i].name);
                                    line++;
                                    sw.WriteLine(Game1.invent2.tablEquiped[i].total);
                                    line++;
                                }
                            }
                        }
                        for (int i = 0; i < Game1.invent1.tablObjects.Length; i++)
                        {
                            if (Game1.invent1.tablObjects[i].name != "rien")
                            {
                                sw.WriteLine("Obj1");
                                sw.WriteLine(Game1.invent1.tablObjects[i].name);
                                line++;
                                sw.WriteLine(Game1.invent1.tablObjects[i].total);
                                line++;
                                if (Game1.invent2.tablObjects[i].name != "rien")
                                {
                                    sw.WriteLine("Obj2");
                                    sw.WriteLine(Game1.invent2.tablObjects[i].name);
                                    line++;
                                    sw.WriteLine(Game1.invent2.tablObjects[i].total);
                                    line++;
                                }
                            }
                        }
                        break; 
                }
                sw.WriteLine("Stop");
                sw.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }


}
