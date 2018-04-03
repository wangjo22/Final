using System.Diagnostics;

namespace SpaceInvaders
{

    public abstract class SpriteBatch_DLink : DLink
    {
        // This class do nothing. It exists only for UML Diagram.
    }

    public class SpriteBatch : SpriteBatch_DLink
    {
        //------------------------------------------------------------------
        // Enum
        //------------------------------------------------------------------
        public enum Name
        {
            GameSprites,
            BoxSprites,
            ShieldSprites,
            BombSprites,
            TextSprite,
            ExplosionEffectSprite,
            Uninitialized
        }

        //------------------------------------------------------------------
        // Constructor
        //------------------------------------------------------------------
        public SpriteBatch()
            : base()
        {
            this.name = SpriteBatch.Name.Uninitialized;
            this.poSBNodeMan = new SBNodeManager();
            Debug.Assert(poSBNodeMan != null);
        }

        //------------------------------------------------------------------
        // Destructor
        //------------------------------------------------------------------
        ~SpriteBatch()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~SpriteBatch():{0} ", this.GetHashCode());
#endif
            this.name = Name.Uninitialized;
            this.poSBNodeMan = null;
        }

        public void Destroy()
        {
            Debug.Assert(this.poSBNodeMan != null);
            this.poSBNodeMan.Destroy();
        }


        public void Set(SpriteBatch.Name name, int reserveNum = 3, int reserveGrow = 1)
        {
            this.name = name;
            this.poSBNodeMan.Set(name, reserveNum, reserveGrow);
        }

       

        public SBNode Attach(SpriteBase pNode)
        {
            Debug.Assert(pNode != null);
            SBNode pSBNode = this.poSBNodeMan.Attach(pNode);

            pSBNode.Set(pNode, this.poSBNodeMan);

            // Back pointer
           // this.poSBNodeMan.SetBackToSpriteBatch(this);
            return pSBNode;
        }



        public void Wash()
        { }

        public void Dump()
        {
            Debug.WriteLine("\t\t {0}", this.name);
            this.poSBNodeMan.Dump();
        }

        public void SetName(SpriteBatch.Name name)
        {
            this.name = name;
        }

        public SpriteBatch.Name GetName()
        {
            return this.name;
        }

        public SBNodeManager GetSBNodeManager()
        {
            return this.poSBNodeMan;
        }
        //--------------------------------------------------------------
        // Data
        //--------------------------------------------------------------
        private SpriteBatch.Name name;
        private SBNodeManager poSBNodeMan;
    }
}
