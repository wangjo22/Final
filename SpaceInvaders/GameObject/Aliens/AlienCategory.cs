using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class AlienCategory : Leaf
    {
        public enum Type
        {
            Squid,
            Octopus,
            Crab,
            Column,
            Group,
            Uninitialized
        }

        protected AlienCategory(GameObject.Name gameObjectName, GameSprite.Name gameSpriteName, int _Index, float _ori_X, float _ori_Y)
             : base(gameObjectName, gameSpriteName)
        {
            this.index = _Index;
            this.originX = _ori_X;
            this.originY = _ori_Y;
        }
        public float originX;
        public float originY;
    }
}
