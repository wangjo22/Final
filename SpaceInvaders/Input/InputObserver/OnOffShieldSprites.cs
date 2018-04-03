using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class OnOffShieldSprites : InputObserver
    {
        public override void Notify()
        {
            SpriteBatch pSB_Box = SpriteBatchManager.Find(SpriteBatch.Name.ShieldSprites);
            pSB_Box.GetSBNodeManager().ToggleDraw();
        }
    }
}
