using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    public class SelectScene: GameState
    {
        public SelectScene()
        {
            this.name = SceneName.Select;

            this.pSquid = new AlienSquid(GameObject.Name.Squid, GameSprite.Name.Squid, 9999, 200.0f, 500.0f);
            this.pCrab = new AlienCrab(GameObject.Name.Crab, GameSprite.Name.Crab, 9999, 200.0f, 450.0f);
            this.pOcto = new AlienOctopus(GameObject.Name.Octopus, GameSprite.Name.Octopus, 9999, 200.0f, 400.0f);
            this.pUFO = new UFO(GameObject.Name.UFO, GameSprite.Name.UFO, 200.0f, 350.0f);
            this.selectSceneSpriteBatch = new SpriteBatch();
            this.didLoadContent = false;

            this.pTitle = new Font();
            this.pSquidDescription = new Font();
            this.pCrabDescription = new Font();
            this.pOctopusDescription = new Font();
            this.pUFODescription = new Font();
            this.pSinglePlayDescription = new Font();
            this.pDoublePlayDescription = new Font();
            this.pSilliDescription = new Font();
        }

        public override void Handle()
        {
            GameScene.SetGameScene(SceneName.Player1);
        }

        public override void LoadContent()
        {
            this.selectSceneSpriteBatch.Attach(this.pSquid.pProxySprite);
            this.selectSceneSpriteBatch.Attach(this.pCrab.pProxySprite);
            this.selectSceneSpriteBatch.Attach(this.pOcto.pProxySprite);
            this.selectSceneSpriteBatch.Attach(this.pUFO.pProxySprite);

            this.pTitle.Set(Font.Name.Select_Scene_text, "SPACE INVADER", Glyph.Name.Consolas36pt, 200, 600);
            this.selectSceneSpriteBatch.Attach(this.pTitle.pFontSprite);

            this.pSquidDescription.Set(Font.Name.Select_Scene_text, " = 30 POINTS", Glyph.Name.Consolas36pt, 250, 500);
            this.selectSceneSpriteBatch.Attach(this.pSquidDescription.pFontSprite);

            this.pCrabDescription.Set(Font.Name.Select_Scene_text, " = 20 POINTS", Glyph.Name.Consolas36pt, 250,450);
            this.selectSceneSpriteBatch.Attach(this.pCrabDescription.pFontSprite);

            this.pOctopusDescription.Set(Font.Name.Select_Scene_text, " = 10 POINTS", Glyph.Name.Consolas36pt, 250, 400);
            this.selectSceneSpriteBatch.Attach(this.pOctopusDescription.pFontSprite);

            this.pUFODescription.Set(Font.Name.Select_Scene_text, " = 200 POINTS", Glyph.Name.Consolas36pt, 250, 350);
            this.selectSceneSpriteBatch.Attach(this.pUFODescription.pFontSprite);

            this.pSinglePlayDescription.Set(Font.Name.Select_Scene_text, "1 PLAYER MODE : PRESS 1", Glyph.Name.Consolas36pt, 40, 250);
            this.selectSceneSpriteBatch.Attach(this.pSinglePlayDescription.pFontSprite);

            this.pDoublePlayDescription.Set(Font.Name.Select_Scene_text, "2 PLAYERS MODE : COMING SOON", Glyph.Name.Consolas36pt, 40, 200);
            this.selectSceneSpriteBatch.Attach(this.pDoublePlayDescription.pFontSprite);

            //this.pSilliDescription.Set(Font.Name.Select_Scene_text, "This scene is very attractive", Glyph.Name.Consolas36pt, 20, 100);
            //this.selectSceneSpriteBatch.Attach(this.pSilliDescription.pFontSprite);
        }

        public override void Update(float getTime)
        {
            if(!this.didLoadContent)
            {
                this.LoadContent();
                this.didLoadContent = true;
            }
            this.pSquid.Update();
            this.pCrab.Update();
            this.pOcto.Update();
            this.pUFO.UpdateOnlySprite();
        }

        public override void Draw()
        {
            SBNodeManager pSB_Man = this.selectSceneSpriteBatch.GetSBNodeManager();
            pSB_Man.Draw();
            InputManager.Update();
        }

        public override void MoveToNextStage()
        {
            
        }

        private AlienSquid pSquid;
        private AlienCrab pCrab;
        private AlienOctopus pOcto;
        private UFO pUFO;
        private SpriteBatch selectSceneSpriteBatch;
        private bool didLoadContent;


        private Font pTitle;
        private Font pSquidDescription;
        private Font pCrabDescription;
        private Font pOctopusDescription;
        private Font pUFODescription;
        private Font pSinglePlayDescription;
        private Font pDoublePlayDescription;
        private Font pSilliDescription;
    }
}
