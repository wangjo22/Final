using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class OnOffCollisionBox : InputObserver
    {
        public override void Notify()
        {
            SpriteBatch pSB_Box = SpriteBatchManager.Find(SpriteBatch.Name.BoxSprites);
            pSB_Box.GetSBNodeManager().ToggleDraw();
        }
    }
}
