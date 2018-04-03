using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class BombCategory : Leaf
    {
        protected BombCategory(GameObject.Name gameObjectName, GameSprite.Name gameSpriteName)
             : base(gameObjectName, gameSpriteName)
        { }

        ~BombCategory()
        {

        }

        protected float dropSpeed = Constant.BOMB_FALL_SPEED;
    }

    
}
