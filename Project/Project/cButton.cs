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

    public class cButton
    {
        Texture2D texture;
        Vector2 position;
        Rectangle rectangle;

        MouseState lastMouse;

        Color colour = new Color(255, 255, 255, 255);

        public Vector2 size;

        public cButton(Texture2D newTexture, int a, int b)
        {
            texture = newTexture;

            size = new Vector2(a, b);

        }
        bool down;
        public bool isClicked;

        public void Update(GameTime gametime)
        {
            MouseState currentMouse = Mouse.GetState();


            KeyboardState KState = Keyboard.GetState();
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle) || KState.IsKeyDown(Keys.Down))
            {
                if (colour.A == 255)
                    down = false;
                if (colour.A == 50)
                    down = true;
                if (down)
                    colour.A += 3;
                else
                    colour.A -= 3;
                if (currentMouse.LeftButton == ButtonState.Pressed && lastMouse.LeftButton == ButtonState.Released) isClicked = true;

            }
            else if (colour.A < 255)
            {
                colour.A = 255;
                isClicked = false;

            } lastMouse = currentMouse;
        }
        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public int getPositionX()
        {
            return (int)position.X;
        }

        public int getPositionY()
        {
            return (int)position.Y;
        }

        public int getSizeX()
        {
            return (int)size.X;
        }

        public int getSizeY()
        {
            return (int)size.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, colour);
        }


    }
}
