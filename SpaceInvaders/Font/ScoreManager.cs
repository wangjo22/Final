using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class ScoreManager
    {
        private ScoreManager()
        {
            this.Player1_Score = 0;
            this.Player2_Score = 0;
            this.High_Score = 0;
        }

        static public void Create()
        {
            if (pInstance == null)
            {
                pInstance = new ScoreManager();
            }
            Debug.Assert(pInstance != null);
        }

        static public void AddScoreToPlayer1(int score)
        {
            ScoreManager pMan = ScoreManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.Player1_Score += score;
            pMan.CheckHighScore();
            ScoreManager.UpdateAllScore();
        }

        static public void AddScoreToPlayer2(int score)
        {
            ScoreManager pMan = ScoreManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.Player2_Score += score;
            pMan.CheckHighScore();
        }

        static public int GetPlayer1Score()
        {
            ScoreManager pMan = ScoreManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.Player1_Score;
        }

        static public int GetPlayer2Score()
        {
            ScoreManager pMan = ScoreManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.Player2_Score;
        }
        static public int GetHighScore()
        {
            ScoreManager pMan = ScoreManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            return pMan.High_Score;
        }

        static public void UpdateAllScore()
        {
            ScoreManager pMan = ScoreManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            Font pTestMessage = FontManager.Find(Font.Name.Player1_Score);
            Debug.Assert(pTestMessage != null);
            pTestMessage.UpdateMessage(pMan.Player1_Score);

            pTestMessage = FontManager.Find(Font.Name.Player2_Score);
            Debug.Assert(pTestMessage != null);
            pTestMessage.UpdateMessage(pMan.Player2_Score);

            pTestMessage = FontManager.Find(Font.Name.High_Score);
            Debug.Assert(pTestMessage != null);
            pTestMessage.UpdateMessage(pMan.High_Score);

            pTestMessage = FontManager.Find(Font.Name.Player1_Lives);
            Debug.Assert(pTestMessage != null);
            pTestMessage.UpdateMessage("Lives " + GameScene.Get1PlayerLive());

            pTestMessage = FontManager.Find(Font.Name.Player2_Lives);
            Debug.Assert(pTestMessage != null);
            pTestMessage.UpdateMessage("Lives " + GameScene.Get2PlayerLives());
        }

        static public void InitializePlayer1Score()
        {
            ScoreManager pMan = ScoreManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.Player1_Score = 0;
        }

        static public void InitializePlayer2Score()
        {
            ScoreManager pMan = ScoreManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            pMan.Player2_Score = 0;
        }

        private void CheckHighScore()
        {
            if (this.High_Score < Player1_Score)
            {
                this.High_Score = Player1_Score;
            }
            if (this.High_Score < Player2_Score)
            {
                this.High_Score = Player2_Score;
            }
        }

        private static ScoreManager PrivGetInstance()
        {
            Debug.Assert(pInstance != null);
            return pInstance;
        }

        // This method should be called 
        // after PlayerGroup and MissileGroup are inserted Game Object Manager
       

        private static ScoreManager pInstance;
        private int Player1_Score;
        private int Player2_Score;
        private int High_Score;
    }
}
