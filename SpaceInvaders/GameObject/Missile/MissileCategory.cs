using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class MissileCategory : Leaf
    {
        //public enum Type
        //{
        //    Missile,
        //    MissileGroup
        //}
        protected MissileCategory(GameObject.Name gameObjectName, GameSprite.Name gameSpriteName)
             : base(gameObjectName, gameSpriteName)
        { }
    }

    
}
