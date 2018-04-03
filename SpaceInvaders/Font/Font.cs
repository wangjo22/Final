using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class Font : DLink
    {
        public enum Name
        {
            Score,
            Player1_Score,
            Player2_Score,
            High_Score,
            Player1_Lives,
            Player2_Lives,

            Player1_Stage,


            Select_Scene_text,

            GameOver,
            HowToRestart,

            NullObject,
            Uninitialized
        };

        public Font()
            : base()
        {
            this.name = Name.Uninitialized;
            this.pFontSprite = new FontSprite();
        }

        ~Font()
        {
#if (TRACK_DESTRUCTOR)
            Debug.WriteLine("~Font():{0} ", this.GetHashCode());
#endif
            this.name = Name.Uninitialized;
            this.pFontSprite = null;
        }

        public void UpdateMessage(String pMessage)
        {
            Debug.Assert(pMessage != null);
            Debug.Assert(this.pFontSprite != null);
            this.pFontSprite.UpdateMessage(pMessage);
        }

        public void UpdateMessage(int score)
        {
            Debug.Assert(this.pFontSprite != null);
            string pMessage = "";
            if (score == 0)
            {
                pMessage = "0000";
            }
            else if(score < 10)
            {
                pMessage = "000" + score;
            }
            else if(score < 100)
            {
                pMessage = "00" + score;
            }
            else if(score < 1000)
            {
                pMessage = "0" + score;
            }
            else
            {
                pMessage = score.ToString();
            }
            this.pFontSprite.UpdateMessage(pMessage);
        }

        public void Set(Font.Name name, String pMessage, Glyph.Name glyphName, float xStart, float yStart)
        {
            Debug.Assert(pMessage != null);

            this.name = name;
            this.pFontSprite.Set(name, pMessage, glyphName, xStart, yStart);
        }

        public void Wash()
        {
            this.name = Name.Uninitialized;
            this.pFontSprite.Set(Font.Name.NullObject, pNullString, Glyph.Name.NullObject, 0.0f, 0.0f);
        }

        public void Dump()
        {
        }


        // ----------------------------------------------------------------
        // Data 
        // ----------------------------------------------------------------
        public Name name;
        public FontSprite pFontSprite;
        static private String pNullString = "null";
    }
}
