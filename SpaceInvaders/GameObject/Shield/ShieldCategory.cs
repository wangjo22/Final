using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ShieldCategory : Leaf
    {
        public enum Type
        {
            Group,
            Column,
            Brick,

            LeftTop0,
            LeftTop1,
            LeftBottom,
            RightTop0,
            RightTop1,
            RightBottom,

            Unitialized
        }
        protected ShieldCategory(GameObject.Name gameObjectName, GameSprite.Name gameSpriteName)
             : base(gameObjectName, gameSpriteName)
        { }
    }

    
}
