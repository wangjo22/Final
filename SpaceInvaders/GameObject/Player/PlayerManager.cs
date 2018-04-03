using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class PlayerManager
    {
        public enum MoveState
        {
            PlayerHitLeftWall,
            PlayerHitRightWall,
            PlayerMoveNormal
        }

        public enum ShootState
        {
            MissileFlying,
            MissileReady,
            PlayerDead,
        }

        private PlayerManager()
        {
            Debug.Assert(pInstance == null);

            this.poReadyState = new PlayerReadyToShoot();
            Debug.Assert(this.poReadyState != null);
            this.poFlyingState = new PlayerMissileFlyingState();
            Debug.Assert(this.poFlyingState != null);
            this.poEndState = new PlayerMissileEndState();
            Debug.Assert(this.poEndState != null);
            this.poDeadState = new PlayerDeadState();
            Debug.Assert(this.poDeadState != null);

            this.poNormalState = new PlayerMoveNormal();
            Debug.Assert(this.poNormalState != null);
            this.poHitLeftState = new PlayerHitLeftWall();
            Debug.Assert(this.poHitLeftState != null);
            this.poHitRightState = new PlayerHitRightWall();
            Debug.Assert(this.poHitRightState != null);


            this.poPlayer = this.CreatePlayer();
            Debug.Assert(this.poPlayer != null);

            this.poMissile = this.CreateMissile();
            Debug.Assert(this.poMissile != null);

            this.isCurrentPlayerAlive = true;
        }

        static public void Create()
        {
            if (pInstance == null)
            {
                pInstance = new PlayerManager();
            }
            Debug.Assert(pInstance != null);

            pInstance.poPlayer.SetMoveState(PlayerManager.MoveState.PlayerMoveNormal);
            pInstance.poPlayer.SetShootingState(PlayerManager.ShootState.MissileReady);
        }

        public static PlayerMoveState GetMoveState(PlayerManager.MoveState pState)
        {
            PlayerManager pMan = PlayerManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            PlayerMoveState toReturn = null;
            switch(pState)
            {
                case MoveState.PlayerMoveNormal:
                    toReturn = pMan.poNormalState;
                    break;
                case MoveState.PlayerHitLeftWall:
                    toReturn = pMan.poHitLeftState;
                    break;
                case MoveState.PlayerHitRightWall:
                    toReturn = pMan.poHitRightState;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            Debug.Assert(toReturn != null);
            return toReturn;
        }

        public static PlayerShootState GetShootState(PlayerManager.ShootState pState)
        {
            PlayerManager pMan = PlayerManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            PlayerShootState toReturn = null;
            switch (pState)
            {
                case ShootState.MissileFlying:
                    toReturn = pMan.poFlyingState;
                    break;
                case ShootState.MissileReady:
                    toReturn = pMan.poReadyState;
                    break;
                case ShootState.PlayerDead:
                    toReturn = pMan.poDeadState;
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            Debug.Assert(toReturn != null);
            return toReturn;
        }

        public static Player GetPlayer()
        {
            PlayerManager pMan = PlayerManager.PrivGetInstance();
            Debug.Assert(pMan.poPlayer != null);

            return pMan.poPlayer;
        }

        public static void PlayerDead()
        {
            PlayerManager pMan = PlayerManager.PrivGetInstance();
            Debug.Assert(pMan.poPlayer != null);

            SoundManager.Play(Sound.Name.PlayerExplode);
            GameScene.Player1Dead();
            ScoreManager.UpdateAllScore();
            PlayerManager.SetPlayerDead();
            pMan.poPlayer.y = -50;
            pMan.poPlayer.SetShootingState(ShootState.PlayerDead);
        }

        public static Missile GetMissile()
        {
            PlayerManager pMan = PlayerManager.PrivGetInstance();
            Debug.Assert(pMan.poMissile != null);

            return pMan.poMissile;
        }

        public static void DeactiveMissile()
        {
            PlayerManager pMan = PlayerManager.PrivGetInstance();
            Debug.Assert(pMan.poMissile != null);

            pMan.poMissile.DeactiveMissile();
        }

        private static PlayerManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        // This method should be called 
        // after PlayerGroup and MissileGroup are inserted Game Object Manager
        private Player CreatePlayer()
        {
            Player pPlayer = new Player(GameObject.Name.Player, GameSprite.Name.Player, Constant.WINDOW_WIDTH / 2, 100.0f);
            Debug.Assert(pPlayer != null);

            SpriteBatch pSB_GameSprite = SpriteBatchManager.Find(SpriteBatch.Name.GameSprites);
            SpriteBatch pSB_BoxSprite = SpriteBatchManager.Find(SpriteBatch.Name.BoxSprites);

            pSB_GameSprite.Attach(pPlayer.pProxySprite);
            pSB_BoxSprite.Attach(pPlayer.pColObject.pBoxSprite);

            GameObject pGroup = GameObjectManager.Find(GameObject.Name.PlayerGroup);
            Debug.Assert(pGroup != null);
            pGroup.Add(pPlayer);

            return pPlayer;
        }

        private Missile CreateMissile()
        {

            Missile pMissile = new Missile(GameObject.Name.Missile, GameSprite.Name.Missile, -100, 0.0f);
            Debug.Assert(pMissile != null);

            SpriteBatch pSB_GameSprite = SpriteBatchManager.Find(SpriteBatch.Name.GameSprites);
            SpriteBatch pSB_BoxSprite = SpriteBatchManager.Find(SpriteBatch.Name.BoxSprites);
            pSB_GameSprite.Attach(pMissile.pProxySprite);
            pSB_BoxSprite.Attach(pMissile.pColObject.pBoxSprite);

            GameObject pGroup = GameObjectManager.Find(GameObject.Name.MissileGroup);
            Debug.Assert(pGroup != null);
            pGroup.Add(pMissile);

            return pMissile;
        }

        public static bool GetPlayerDeadOrAlive()
        {
            PlayerManager pMan = PlayerManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.isCurrentPlayerAlive;
        }

        public static void SetPlayerDead()
        {
            PlayerManager pMan = PlayerManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.isCurrentPlayerAlive = false ;
        }

        public static void SetPlayerAlive()
        {
            PlayerManager pMan = PlayerManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.isCurrentPlayerAlive = true;
        }


        private static PlayerManager pInstance;

        private PlayerReadyToShoot poReadyState;
        private PlayerMissileFlyingState poFlyingState;
        private PlayerMissileEndState poEndState;
        private PlayerDeadState poDeadState;

        private PlayerMoveNormal poNormalState;
        private PlayerHitLeftWall poHitLeftState;
        private PlayerHitRightWall poHitRightState;

        private Player poPlayer;
        private Missile poMissile;
        private bool isCurrentPlayerAlive;
    }
}
