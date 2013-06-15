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
                tablEquiped[i] = new Item("", "rien", "", 0, 1, "equiped");
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
                toAdd.isEquiped = "notequiped";
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
               
                
                tablEquiped[i].isEquiped = "equiped";
                tablEquiped[i] = toAdd;
                added = true;
            }

        }


        public void useItem(Item item,Character player)
        {
            switch (item.type)
            {
                case "Potion":
                    if (item.effect == "health" && player.health < player.healthMax)
                        player.health += item.stat;
                    if (item.effect == "mana" && player.mana < player.manaMax)
                        player.mana += item.stat;
                    break;

                case "Weapon":
                    player.Degat += item.stat;
                        addItemtoequip(item);
                    
                    break;
                case "Armor":
                    player.Armor += item.stat;
                        addItemtoequip(item);
                    break;
            }
            if (item.total > 1)
                item.total--;
            else removeItem(item);
            

        }
        public void deUseItem(Item item,Character player)
        {
            switch (item.type)
            {
                case "Weapon":
                    player.Degat -= item.stat;
                        addItem(item);
                    break;
                case "Armor":
                    player.Armor -= item.stat;
                    addItem(item);
                    break;

            }
            removeItemE(item);
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
                toRemove.isEquiped = "equiped";
                tablObjects[i] = new Item("", "rien", "", 0, 1, "equiped");
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
                toRemove.isEquiped = "notequiped";
                tablEquiped[i] = new Item("", "rien", "", 0, 1, "notequiped");
            }
        }
    }
}