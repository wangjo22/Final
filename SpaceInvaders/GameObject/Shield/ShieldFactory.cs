using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class ShieldFactory
    {
        private ShieldFactory()
        {
            this.pShieldSpriteBatch = SpriteBatchManager.Find(SpriteBatch.Name.ShieldSprites);
            Debug.Assert(this.pShieldSpriteBatch != null);

            this.pBoxSpriteBatch = SpriteBatchManager.Find(SpriteBatch.Name.BoxSprites);
            Debug.Assert(this.pBoxSpriteBatch != null);

            this.pParent = null;
        }

        public static void InitializeAlienFactory()
        {
            if(pInstance == null)
            {
                pInstance = new ShieldFactory();
            }
            Debug.Assert(pInstance != null);
        }

        ~ShieldFactory()
        {
            Debug.WriteLine("~ShieldFactory():");
            this.pBoxSpriteBatch = null;
            this.pShieldSpriteBatch = null;
        }

        public static void SetParent(GameObject pComp)
        {
            Debug.Assert(pComp != null);

            ShieldFactory pFactory = ShieldFactory.PrivGetInstance();
            Debug.Assert(pFactory != null);

            pFactory.pParent = pComp;
        }

        public static GameObject Create(ShieldCategory.Type type, int _Index, float posX = 0.0f, float posY = 0.0f)
        {
            ShieldFactory pFactory = ShieldFactory.PrivGetInstance();
            Debug.Assert(pFactory != null);

            GameObject pGameObj = null;
            switch (type)
            {
                case ShieldCategory.Type.Brick:
                    pGameObj = new ShieldBrick(GameObject.Name.ShieldBrick, GameSprite.Name.Brick, _Index, posX, posY);
                    break;
                case ShieldCategory.Type.LeftBottom:
                    pGameObj = new ShieldBrick(GameObject.Name.ShieldBrick, GameSprite.Name.BrickLeft_Bottom, _Index, posX, posY);
                    break;
                case ShieldCategory.Type.LeftTop0:
                    pGameObj = new ShieldBrick(GameObject.Name.ShieldBrick, GameSprite.Name.BrickLeft_Top0, _Index, posX, posY);
                    break;
                case ShieldCategory.Type.LeftTop1:
                    pGameObj = new ShieldBrick(GameObject.Name.ShieldBrick, GameSprite.Name.BrickLeft_Top1, _Index, posX, posY);
                    break;
                case ShieldCategory.Type.RightBottom:
                    pGameObj = new ShieldBrick(GameObject.Name.ShieldBrick, GameSprite.Name.BrickRight_Bottom, _Index, posX, posY);
                    break;
                case ShieldCategory.Type.RightTop0:
                    pGameObj = new ShieldBrick(GameObject.Name.ShieldBrick, GameSprite.Name.BrickRight_Top0, _Index, posX, posY);
                    break;
                case ShieldCategory.Type.RightTop1:
                    pGameObj = new ShieldBrick(GameObject.Name.ShieldBrick, GameSprite.Name.BrickRight_Top1, _Index, posX, posY);
                    break;
                case ShieldCategory.Type.Column:
                    pGameObj = new ShieldColumn(GameObject.Name.ShieldColumn, GameSprite.Name.NullObject, _Index, posX, posY);
                    break;
                case ShieldCategory.Type.Group:
                    pGameObj = new ShieldGroup(GameObject.Name.ShieldRoot, GameSprite.Name.NullObject, _Index, posX, posY);
                    break;
                default:
                    Debug.Assert(false);
                    break;
            }
            Debug.Assert(pGameObj != null);


            // Attach to SpriteBatch
            if (pGameObj.type == Component.Container.LEAF)
            {
                pFactory.pParent.Add(pGameObj);
                pGameObj.ActivateGameSprite(pFactory.pShieldSpriteBatch);
            }
            pGameObj.ActivateCollisionSprite(pFactory.pBoxSpriteBatch);
            return pGameObj;
        }


        private static ShieldFactory PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        // Data: ---------------------
        private static ShieldFactory pInstance;
        private SpriteBatch pShieldSpriteBatch;
        private SpriteBatch pBoxSpriteBatch;
        private GameObject pParent;
    }
}