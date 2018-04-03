using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class ExplosionCategory : Leaf
    {
        protected ExplosionCategory(GameObject.Name gameObjectName, GameSprite.Name gameSpriteName)
             : base(gameObjectName, gameSpriteName)
        { }
    }

    
}
