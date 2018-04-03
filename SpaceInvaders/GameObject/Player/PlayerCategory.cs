using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class PlayerCategory : Leaf
    {
        public enum Type
        {
            Player,
            PlayerGroup
        }
        protected PlayerCategory(GameObject.Name gameObjectName, GameSprite.Name gameSpriteName)
             : base(gameObjectName, gameSpriteName)
        { }
    }

    
}
