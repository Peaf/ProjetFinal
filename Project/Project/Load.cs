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
                sr = File.OpenText("save.txt");
                Game1.questState = int.Parse(sr.ReadLine());
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

