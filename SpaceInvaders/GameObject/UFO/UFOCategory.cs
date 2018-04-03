using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class UFOCategory : Leaf
    {
        protected UFOCategory(GameObject.Name gameObjectName, GameSprite.Name gameSpriteName)
             : base(gameObjectName, gameSpriteName)
        { }
    }

    
}
