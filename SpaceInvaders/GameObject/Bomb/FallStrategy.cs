using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class FallStrategy
    {
        abstract public void BombFall(Bomb pBomb);
        abstract public void Reset(float posY);
    }


}
