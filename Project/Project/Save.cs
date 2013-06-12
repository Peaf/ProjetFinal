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
       
        public static void Update()
        {
         
            FileStream stream;
            StreamWriter sw;

            try
            {
                stream = new FileStream("save.txt",FileMode.Create);
                sw = new StreamWriter(stream);
                sw.WriteLine(Game1.questState);
                sw.WriteLine(Game1.player.persoPosition.X);
                sw.WriteLine(Game1.player.persoPosition.Y);
                sw.WriteLine(Game1.player.persoRectangle.X);
                sw.WriteLine(Game1.player.persoRectangle.Y);
                sw.WriteLine(Game1.player.persoRectangle.Width);
                sw.WriteLine(Game1.player.persoRectangle.Height);
                sw.WriteLine(Game1.player.health);
                sw.WriteLine(Game1.player.mana);
                sw.WriteLine(Game1.player.manaMax);
                sw.WriteLine(Game1.player.healthMax);
                sw.WriteLine(Game1.player.Experience);
                sw.WriteLine(Game1.player.Strenght);
                sw.WriteLine(Game1.player.Intelligence);
                sw.WriteLine(Game1.player.Degat);
                sw.WriteLine(Game1.player.Armor);
                sw.WriteLine(Game1.player.Gold);
                sw.WriteLine(Game1.player.Lvl);
                sw.Close();
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }


}
