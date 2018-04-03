﻿using System;
using System.Xml;
using System.Diagnostics;

namespace SpaceInvaders
{
    class GlyphManager : Manager
    {
        //----------------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------------
        private GlyphManager(int reserveNum = 3, int reserveGrow = 1)
            : base()
        {
            this.BaseInitialize(reserveNum, reserveGrow);
            this.poNodeToCompare = (Glyph)this.DerivedCreateNode();
        }


        ~GlyphManager()
        {
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("~GlyphMan():{0}", this.GetHashCode());
#endif
            this.poNodeToCompare = null;
            GlyphManager.pInstance = null;
        }


        //----------------------------------------------------------------------
        // Static Manager methods can be implemented with base methods 
        // Can implement/specialize more or less methods your choice
        //----------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum >= 0);
            Debug.Assert(reserveGrow > 0);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new GlyphManager(reserveNum, reserveGrow);
            }
        }


        public static void Destroy()
        {
            GlyphManager pMan = GlyphManager.PrivGetInstance();
#if(TRACK_DESTRUCTOR)
            Debug.WriteLine("--->GlyphMan.Destroy()");
#endif
            pMan.BaseDestroy();
        }

        public static Glyph Add(Glyph.Name name, int key, Texture.Name textName, float x, float y, float width, float height)
        {
            GlyphManager pMan = GlyphManager.PrivGetInstance();

            Glyph pNode = (Glyph)pMan.BaseAdd();
            Debug.Assert(pNode != null);

            pNode.Set(name, key, textName, x, y, width, height);
            return pNode;
        }

        public static void AddXml(Glyph.Name glyphName, String assetName, Texture.Name textName)
        {
            System.Xml.XmlTextReader reader = new XmlTextReader(assetName);

            int key = -1;
            int x = -1;
            int y = -1;
            int width = -1;
            int height = -1;

            // I'm sure there is a better way to do this... but this works for now
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        if (reader.GetAttribute("key") != null)
                        {
                            key = Convert.ToInt32(reader.GetAttribute("key"));
                        }
                        else if (reader.Name == "x")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    x = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "y")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    y = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "width")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    width = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "height")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    height = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        break;

                    case XmlNodeType.EndElement: //Display the end of the element 
                        if (reader.Name == "character")
                        {
                            // have all the data... so now create a glyph
                            // Debug.WriteLine("key:{0} x:{1} y:{2} w:{3} h:{4}", key, x, y, width, height);
                            GlyphManager.Add(glyphName, key, textName, x, y, width, height);
                        }
                        break;
                }
            }

            // Debug.Write("\n");
        }

        public static void Remove(Glyph pNode)
        {
            Debug.Assert(pNode != null);
            GlyphManager pMan = GlyphManager.PrivGetInstance();
            pMan.BaseRemove(pNode);
        }

        public static Glyph Find(Glyph.Name name, int key)
        {
            GlyphManager pMan = GlyphManager.PrivGetInstance();

            // Compare functions only compares two Nodes
            pMan.poNodeToCompare.name = name;
            pMan.poNodeToCompare.key = key;

            Glyph pData = (Glyph)pMan.BaseFind(pMan.poNodeToCompare);
            return pData;
        }

        public static void Dump()
        {
            GlyphManager pMan = GlyphManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Debug.WriteLine("------ Glyph Manager ------");
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

            Glyph pDataA = (Glyph)pLinkA;
            Glyph pDataB = (Glyph)pLinkB;

            Boolean status = false;

            if (pDataA.name == pDataB.name && pDataA.key == pDataB.key)
            {
                status = true;
            }

            return status;
        }

        override protected DLink DerivedCreateNode()
        {
            DLink pNode = new Glyph();
            Debug.Assert(pNode != null);
            return pNode;
        }

        override protected void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Glyph pNode = (Glyph)pLink;
            pNode.Wash();
        }

        override protected void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            Glyph pNode = (Glyph)pLink;

            Debug.Assert(pNode != null);
            pNode.Dump();
        }

        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static GlyphManager PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);

            return pInstance;
        }

        //----------------------------------------------------------------------
        // Data
        //----------------------------------------------------------------------
        private static GlyphManager pInstance = null;
        private Glyph poNodeToCompare;
    }
}
