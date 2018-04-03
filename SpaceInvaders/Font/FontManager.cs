﻿using System;
using System.Xml;
using System.Diagnostics;

namespace SpaceInvaders
{
    class FontManager : Manager
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private FontManager(int reserveNum = 3, int reserveGrow = 1)
            : base()
        {
            this.BaseInitialize(reserveNum, reserveGrow);
            this.pRefNode = (Font)this.DerivedCreateNode();
        }
        ~FontManager()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("~FontMan():{0}", this.GetHashCode());
#endif
            this.pRefNode = null;
            FontManager.pInstance = null;
        }

        //----------------------------------------------------------------------
        // Static Manager methods can be implemented with base methods 
        // Can implement/specialize more or less methods your choice
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            // make sure values are ressonable 
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new FontManager(reserveNum, reserveGrow);
            }
        }
        public static void Destroy()
        {
            // Get the instance
            FontManager pMan = FontManager.PrivGetInstance();
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("--->FontMan.Destroy()");
#endif
            pMan.BaseDestroy();
        }
        public static Font Add(Font.Name name, SpriteBatch.Name SB_Name, String pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            FontManager pMan = FontManager.PrivGetInstance();

            Font pNode = (Font)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name, pMessage, glyphName, xStart, yStart);

            // Add to sprite batch
            SpriteBatch pSB = SpriteBatchManager.Find(SB_Name);
            Debug.Assert(pSB != null);
            Debug.Assert(pNode.pFontSprite != null);
            pSB.Attach(pNode.pFontSprite);

            return pNode;
        }

        public static void AddXml(Glyph.Name glyphName, String assetName, Texture.Name textName)
        {
            GlyphManager.AddXml(glyphName, assetName, textName);
        }

        public static void Remove(Glyph pNode)
        {
            Debug.Assert(pNode != null);
            FontManager pMan = FontManager.PrivGetInstance();
            pMan.BaseRemove(pNode);
        }
        public static Font Find(Font.Name name)
        {
            FontManager pMan = FontManager.PrivGetInstance();

            // Compare functions only compares two Nodes
            pMan.pRefNode.name = name;

            Font pData = (Font)pMan.BaseFind(pMan.pRefNode);
            return pData;
        }


        public static void Dump()
        {
            FontManager pMan = FontManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.WriteLine("------ Font Manager ------");
            pMan.BaseDump();
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        override protected Boolean DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            // This is used in baseFind() 
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            Font pDataA = (Font)pLinkA;
            Font pDataB = (Font)pLinkB;

            Boolean status = false;

            if (pDataA.name == pDataB.name)
            {
                status = true;
            }

            return status;
        }
        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new Font();
            Debug.Assert(pNode != null);
            return pNode;
        }
        override protected void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Font pNode = (Font)pLink;
            pNode.Wash();
        }

        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Font pNode = (Font)pLink;

            Debug.Assert(pNode != null);
            pNode.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static FontManager PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Data
        //----------------------------------------------------------------------
        private static FontManager pInstance = null;
        private Font pRefNode;
    }
}
