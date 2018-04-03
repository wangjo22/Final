using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class GameObject : CollisionVisitor
    {
        //--------------------------------------------------------------------------------------------
        // Enum
        //--------------------------------------------------------------------------------------------    
        public enum Name
        {
            Squid,
            Octopus,
            Crab,

            WallLeft,
            WallRight,
            WallTop,
            WallBottom,
            WallGroup,

            BumperLeft,
            BumperRight,

            AlienGroup,
            AlienColumn,

            Missile,
            MissileGroup,

            Player,
            PlayerGroup,

            BombGroup,
            BombStraight,
            BombZigZag,
            BombCross,

            ShieldRoot,
            ShieldColumn,
            ShieldBrick,

            UFO,
            UFOGroup,

            AlienExplosion,
            PlayerExplision,
            BombExplosion,
            UFOExplosion,
            ExplosionGroup,

            Null_Object,
            Uninitialized
        }

        //--------------------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------------------
        protected GameObject(GameObject.Name gameObjectName, GameSprite.Name gameSpriteName)
        {
            this.name = gameObjectName;
            this.x = 0.0f;
            this.y = 0.0f;
            this.sx = 1.0f;
            this.sy = 1.0f;
            this.pProxySprite = ProxySpriteManager.Add(gameSpriteName);
            Debug.Assert(this.pProxySprite != null);
            this.pColObject = new CollisionObject(this.pProxySprite);
            Debug.Assert(this.pColObject != null);
            this.isDead = false;
        }


        //--------------------------------------------------------------------------------------------
        // Destructor
        //--------------------------------------------------------------------------------------------
        ~GameObject()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("   ~GameObject():{0}. this.GetHashCode());
#endif
            this.name = GameObject.Name.Uninitialized;
            this.pProxySprite = null;
        }

        //--------------------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------------------
        public virtual void Update()
        {
            Debug.Assert(this.pProxySprite != null);
            this.pProxySprite.x = this.x;
            this.pProxySprite.y = this.y;

            Debug.Assert(this.pColObject != null);
            this.pColObject.UpdatePos(this.x, this.y);

            Debug.Assert(this.pColObject.pBoxSprite != null);
            this.pColObject.pBoxSprite.Update();
        }

        protected void BaseUpdateBoundingBox()
        {
            GameObject pNode = (GameObject)ForwardIterator.GetChild(this);
            CollisionRect pBigRect = this.pColObject.pColRect;
            if (pNode != null)
            {
                pBigRect.Set(pNode.pColObject.pColRect);
            }
            while (pNode != null)
            {
                pBigRect.Union(pNode.pColObject.pColRect);
                pNode = (GameObject)ForwardIterator.GetSibling(pNode);
            }

            this.x = this.pColObject.pColRect.x;
            this.y = this.pColObject.pColRect.y;
        }

        public void ActivateCollisionSprite(SpriteBatch spriteBatch)
        {
            Debug.Assert(spriteBatch != null);
            Debug.Assert(this.pColObject != null);
            spriteBatch.Attach(this.pColObject.pBoxSprite);
        }

        public void ActivateGameSprite(SpriteBatch spriteBatch)
        {
            Debug.Assert(spriteBatch != null);
            spriteBatch.Attach(this.pProxySprite);
        }

        public void RemoveFromSpriteBatch()
        {
            Debug.Assert(this.pProxySprite != null);
            Debug.Assert(this.pColObject != null);

            SBNode pSBNode;
            if(this.type == Composite.Container.LEAF)
            {
                if(!(this is ExplosionCategory))
                {
                    pSBNode = this.pColObject.pBoxSprite.GetBackToSBNode();

                    // Some of Leaf object don't have COllision box.
                    if (pSBNode != null)
                    {
                        SpriteBatchManager.Remove(pSBNode);
                    }
                }
                pSBNode = this.pProxySprite.GetBackToSBNode();
                SpriteBatchManager.Remove(pSBNode); 
            }
            else if(this.type == Composite.Container.COMPOSITE)
            {
                pSBNode = this.pColObject.pBoxSprite.GetBackToSBNode();
               
                SpriteBatchManager.Remove(pSBNode); 
            }
        }

        public void Dump()
        {
            // Data:
            Debug.WriteLine("\t\t\t       name: {0} ({1})", this.name, this.GetHashCode());

            if (this.pProxySprite != null)
            {
                Debug.WriteLine("\t\t   pProxySprite: {0}", this.pProxySprite.name);
                Debug.WriteLine("\t\t    pRealSprite: {0}", this.pProxySprite.pSprite.GetName());
            }
            else
            {
                Debug.WriteLine("\t\t   pProxySprite: null");
                Debug.WriteLine("\t\t    pRealSprite: null");
            }
            Debug.WriteLine("\t\t\t      (x,y): {0}, {1}", this.x, this.y);

        }

        public GameObject.Name GetName()
        {
            return this.name;
        }

        public float GetX()
        {
            return this.x;
        }
        public float GetY()
        {
            return this.y;
        }

        public void SetScaleX(float sx)
        {
            this.pProxySprite.sx *= sx;
        }

        public void SetScaleY(float sy)
        {
            this.pProxySprite.sy *= sy;
        }

        //--------------------------------------------------------------------------------------------
        // Data
        //--------------------------------------------------------------------------------------------
        public GameObject.Name name;
        public float sx;
        public float sy;
        public float x;
        public float y;
        public ProxySprite pProxySprite;
        public CollisionObject pColObject;
        public int index;
        public bool isDead;
    }
}
