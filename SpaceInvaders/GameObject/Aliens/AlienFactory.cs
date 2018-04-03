using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class AlienFactory
    {
        private AlienFactory()
        {
            this.pGameSpriteBatch = SpriteBatchManager.Find(SpriteBatch.Name.GameSprites);
            Debug.Assert(this.pGameSpriteBatch != null);

            this.pBoxSpriteBatch = SpriteBatchManager.Find(SpriteBatch.Name.BoxSprites);
            Debug.Assert(this.pBoxSpriteBatch != null);
        }

        public static void InitializeAlienFactory()
        {
            if(pInstance == null)
            {
                pInstance = new AlienFactory();
            }
            Debug.Assert(pInstance != null);
        }

        ~AlienFactory()
        {
            Debug.WriteLine("~FactoryAlien():");
            this.pBoxSpriteBatch = null;
            this.pGameSpriteBatch = null;
        }

        public static GameObject Create(AlienCategory.Type type, int _Index, float posX = 0.0f, float posY = 0.0f)
        {
            AlienFactory pFactory = AlienFactory.PrivGetInstance();
            GameObject pGameObj = null;
            switch (type)
            {
                case AlienCategory.Type.Crab:
                    pGameObj = new AlienCrab(GameObject.Name.Crab, GameSprite.Name.Crab, _Index, posX, posY);
                    break;
                case AlienCategory.Type.Octopus:
                    pGameObj = new AlienOctopus(GameObject.Name.Octopus, GameSprite.Name.Octopus, _Index, posX, posY);
                    break;
                case AlienCategory.Type.Squid:
                    pGameObj = new AlienSquid(GameObject.Name.Squid, GameSprite.Name.Squid, _Index, posX, posY);
                    break;
                case AlienCategory.Type.Column:
                    pGameObj = new AlienColumn(GameObject.Name.AlienColumn, GameSprite.Name.NullObject, _Index, 0.0f, 0.0f);
                    break;
                case AlienCategory.Type.Group:
                    pGameObj = new AlienGroup(GameObject.Name.AlienGroup, GameSprite.Name.NullObject, _Index, 0.0f, 0.0f);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }

            Debug.Assert(pGameObj != null);

            // Attach to SpriteBatch
            pGameObj.ActivateGameSprite(pFactory.pGameSpriteBatch);
            pGameObj.ActivateCollisionSprite(pFactory.pBoxSpriteBatch);
            return pGameObj;
        }


        private static AlienFactory PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        // Data: ---------------------
        private static AlienFactory pInstance;
        private SpriteBatch pGameSpriteBatch;
        private SpriteBatch pBoxSpriteBatch;
    }
}