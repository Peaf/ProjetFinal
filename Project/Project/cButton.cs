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

        public bool isClicked;

        public void Update( GameTime gametime)
        {
            MouseState currentMouse = Mouse.GetState();
            

            KeyboardState KState = Keyboard.GetState();
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);

            Rectangle mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            if (mouseRectangle.Intersects(rectangle) || KState.IsKeyDown(Keys.Down))
            {
                colour = Color.Lime;
                if (currentMouse.LeftButton == ButtonState.Pressed && lastMouse.LeftButton == ButtonState.Released) isClicked = true;

            }
            else
            {
                colour = new Color(255, 255, 255, 255);

            } lastMouse = currentMouse;
        }
        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, colour);
        }


    }
}
