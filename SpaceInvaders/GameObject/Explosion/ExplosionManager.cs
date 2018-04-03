using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ExplosionManager
    {

        private ExplosionManager()
        {
            this.pSprite = SpriteBatchManager.Find(SpriteBatch.Name.ExplosionEffectSprite);
            this.pPlayerExpld = new PlayerExplosion(GameObject.Name.PlayerExplision, GameSprite.Name.Player_Explosion, -10.0f, -10.0f);
        }

        static public void Create(ExplosionGroup pGroup)
        {
            Debug.Assert(pGroup != null);

            if (pInstance == null)
            {
                pInstance = new ExplosionManager();
            }
            Debug.Assert(pInstance != null);

            pInstance.pGrid = pGroup;
            pInstance.pGrid.Add(pInstance.pPlayerExpld);
        }

       
        static public void GetAlienExplosion(GameObject pObj)
        {
            Debug.Assert(pObj != null);

            ExplosionManager pMan = ExplosionManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            AlienExplosion pExplosion = new AlienExplosion(GameObject.Name.AlienExplosion, GameSprite.Name.Alien_Explosion, pObj.x, pObj.y);
            pMan.pGrid.Add(pExplosion);
            pMan.pSprite.Attach(pExplosion.pProxySprite);

            RemoveEffect pRemove = new RemoveEffect(pExplosion);
            TimerManager.Add(TimeEvent.Name.AnimationSprite, pRemove, 1.0f);
        }


        static public void GetBombExplosion(GameObject pObj)
        {
            Debug.Assert(pObj != null);

            ExplosionManager pMan = ExplosionManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            BombExplosion pExplosion = new BombExplosion(GameObject.Name.BombExplosion, GameSprite.Name.Bomb_Explosion, pObj.x, pObj.y - 10.0f);
            pMan.pGrid.Add(pExplosion);
            pMan.pSprite.Attach(pExplosion.pProxySprite);

            RemoveEffect pRemove = new RemoveEffect(pExplosion);
            TimerManager.Add(TimeEvent.Name.AnimationSprite, pRemove, 1.0f);
        }


        static public void GetUFOExplosion(GameObject pObj)
        {
            Debug.Assert(pObj != null);

            ExplosionManager pMan = ExplosionManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            UFOExplosion pExplosion = new UFOExplosion(GameObject.Name.UFOExplosion, GameSprite.Name.UFO_Explosion, pObj.x, pObj.y - 10.0f);
            pMan.pGrid.Add(pExplosion);
            pMan.pSprite.Attach(pExplosion.pProxySprite);

            RemoveEffect pRemove = new RemoveEffect(pExplosion);
            TimerManager.Add(TimeEvent.Name.AnimationSprite, pRemove, 1.0f);
        }

        static public void GetPlayerExplosion(GameObject pObj)
        {
            Debug.Assert(pObj != null);

            ExplosionManager pMan = ExplosionManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.pPlayerExpld.x = pObj.x;
            pMan.pPlayerExpld.y = pObj.y;
            pMan.pSprite.Attach(pMan.pPlayerExpld.pProxySprite);
        }

        static public GameObject GetPlayerExplosion()
        {
            ExplosionManager pMan = ExplosionManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.pPlayerExpld;
        }

        static public void DeactiveExplosion(GameObject pObj)
        {
            Debug.Assert(pObj != null);

            ExplosionManager pMan = ExplosionManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pObj.RemoveFromSpriteBatch();
            pMan.pGrid.RemoveFromHeadAndLast(pObj);
        }

        static private ExplosionManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        


        private static ExplosionManager pInstance;
        private SpriteBatch pSprite;
        private ExplosionGroup pGrid;
        private PlayerExplosion pPlayerExpld;
    }
}
