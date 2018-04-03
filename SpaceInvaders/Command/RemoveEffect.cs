using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class RemoveEffect : Command
    {
        public RemoveEffect(GameObject pObj)
        {
            Debug.Assert(pObj != null);
            this.pObj = pObj;
        }

        public override void Execute(float deltaTime)
        {
            ExplosionManager.DeactiveExplosion(this.pObj);
        }


        public void Dump()
        {
            Debug.WriteLine("MovementSprite.Dump()... No Idea what to show now...");
        }
        // Data: ---------------
        private GameObject pObj;
    }
}
