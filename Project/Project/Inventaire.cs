using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project
{
    public class Inventaire
    {
        public Item[] tablObjects, tablEquiped;
        public int place_object;
        public bool added = false;

        public Inventaire()
        {
            this.tablObjects = new Item[60];
            this.tablEquiped = new Item[60];
        }
        public void Initialize()
        {
            for (int i = 0; i < tablObjects.Length; i++)
            {
                tablObjects[i] = new Item("", "rien", "", 0, 1, "");
                tablEquiped[i] = new Item("", "rien", "", 0, 1, "notequiped");
                tablObjects[i].place = i;
                tablEquiped[i].place = i;
            }
        }

        public Item getItem(string s)
        {
            int i = 0;
            while (i < tablObjects.Length && tablObjects[i].name != s)
            {
                i++;
            }
            return tablObjects[i];
        }


        public void addItem(Item toAdd)
        {
            int i = 0;
            
            while (i < tablObjects.Length && tablObjects[i].name != "rien" && tablObjects[i].name != toAdd.name)
            {
                i++;
            }
            if (tablObjects[i].name == toAdd.name)
                tablObjects[i].total += toAdd.total;

            else if (i < tablObjects.Length)
            {
                toAdd.place = i;
                tablObjects[i].isEquiped = "notequiped";
                tablObjects[i] = toAdd;
                
            }

        }

        public void addItemtoequip(Item toAdd)
        {
            int i = 0;
            while (i < tablEquiped.Length && tablEquiped[i].name != "rien" && tablEquiped[i].name != toAdd.name)
            {
                i++;
            }

            if (i < tablEquiped.Length)
            {
                toAdd.place = i;
                tablEquiped[i].name = toAdd.name;
                tablEquiped[i].isEquiped = "equiped";
                added = true;
            }

        }


        public void useItem(Item item)
        {
            switch (item.type)
            {
                case "Potion":
                    if (item.effect == "health" && Game1.player.health < Game1.player.healthMax)
                        Game1.player.health += item.stat;
                    if (item.effect == "mana" && Game1.player.mana < Game1.player.manaMax)
                        Game1.player.mana += item.stat;
                    break;

                case "Weapon":
                    Game1.player.Degat += item.stat;
                    if (item.isEquiped == "notequiped")
                        addItemtoequip(item);
                    if (item.isEquiped == "equiped")
                        addItem(item);
                    break;
                case "Armor":
                    Game1.player.Armor += item.stat;
                    if (item.isEquiped == "notequiped")
                        addItemtoequip(item);
                    if (item.isEquiped == "equiped")
                        addItem(item);
                    break;
            }
            if (item.total > 1)
                item.total--;
            else
            {
                if (item.isEquiped == "notequiped") 
                    removeItem(item);
                else
            removeItemE(item);
            }

        }



        public void removeItem(Item toRemove)
        {
            int i = 0;
            while (i < tablObjects.Length && tablObjects[i].name != toRemove.name)
            {
                i++;
            }
            if (i < tablObjects.Length)
            {
                tablObjects[i].type = "";
                tablObjects[i].name = "rien";
            }
        }
        public void removeItemE(Item toRemove)
        {
            int i = 0;
            while (i < tablEquiped.Length && tablEquiped[i].name != toRemove.name)
            {
                i++;
            }
            if (i < tablEquiped.Length)
            {
                tablEquiped[i].type = "";
                tablEquiped[i].name = "rien";
            }
        }
    }
}