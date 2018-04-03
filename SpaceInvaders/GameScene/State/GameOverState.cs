using System;
using System.Diagnostics;

namespace SpaceInvaders
{

    public class GameOverState : GameState
    {
        public GameOverState()
        {
            this.name = SceneName.GameOver;
            this.pGameOver = new Font();
            this.pDescriptionToRestart = new Font();
            this.GameOverSceneSpriteBatch = new SpriteBatch();
            this.didLoadContent = false;
        }

       
        public override void Handle()
        {
            GameScene.SetGameScene(SceneName.Select);
        }


        public override void LoadContent()
        {
            this.pGameOver.Set(Font.Name.GameOver, "Game Over", Glyph.Name.Consolas36pt, 220, 500);
            this.pDescriptionToRestart.Set(Font.Name.HowToRestart, "Press \"R\" to restart", Glyph.Name.Consolas36pt, 120, 300);
            this.GameOverSceneSpriteBatch.Attach(pGameOver.pFontSprite);
            this.GameOverSceneSpriteBatch.Attach(pDescriptionToRestart.pFontSprite);
        }

        public override void MoveToNextStage()
        {
            throw new NotImplementedException();
        }

        public override void Update(float getTime)
        {
            if(!this.didLoadContent)
            {
                this.didLoadContent = true;
                this.LoadContent();
            }
        }

        public override void Draw()
        {
            SBNodeManager pSB_Man = this.GameOverSceneSpriteBatch.GetSBNodeManager();
            pSB_Man.Draw();
            InputManager.Update();
        }

        private Font pGameOver;
        private Font pDescriptionToRestart;
        private SpriteBatch GameOverSceneSpriteBatch;
        private bool didLoadContent;
    }
}
