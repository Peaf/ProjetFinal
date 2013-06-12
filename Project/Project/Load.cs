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
        public static void Update()
        {
            StreamReader sr;

            try
            {
                sr = File.OpenText("save2.txt");
                Game1.questState = int.Parse(sr.ReadLine());
                switch (int.Parse(sr.ReadLine()))
                {
                    case 2 :
                        Playing.map = Playing.map2;
                        break;
                    case 4 :
                        Playing.map = Playing.map4;
                        break;
                    case 5 :
                        Playing.map = Playing.map5;
                        break;
                    case 6 :
                        Playing.map = Playing.map6;
                        break;
                    case 8 :
                        Playing.map = Playing.map8;
                        break;
                }
                sr.ReadLine();
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
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}

