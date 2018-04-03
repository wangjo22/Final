using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class WallCategory: Leaf
    {
        //public enum Type
        //{
        //    LeftWall,
        //    RightWall,
        //    TopWall,
        //    BottomWall,
        //    WallGroup,
        //    Uninitialized
        //}
        protected WallCategory(GameObject.Name gameObjectName, GameSprite.Name gameSpriteName)
             : base(gameObjectName, gameSpriteName)
        {
     
        }
    }
}
