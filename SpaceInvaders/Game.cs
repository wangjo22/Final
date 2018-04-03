using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    class SpaceInvaders : Azul.Game
    {
        //-----------------------------------------------------------------------------
        // Game::Initialize()
        //		Allows the engine to perform any initialization it needs to before 
        //      starting to run.  This is where it can query for any required services 
        //      and load any non-graphic related content. 
        //-----------------------------------------------------------------------------
        public override void Initialize()
        {
            // Game Window Device setup
            this.SetWindowName("SE450 - Space Invader PA4");
            this.SetWidthHeight(Constant.WINDOW_WIDTH, Constant.WINDOW_HEIGHT);
            this.SetClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }

        //-----------------------------------------------------------------------------
        // Game::LoadContent()
        //		Allows you to load all content needed for your engine,
        //	    such as objects, graphics, etc.
        //-----------------------------------------------------------------------------
        public override void LoadContent()
        {
            //---------------------------------------------------------------------------------------------------------
            // Manager initialization
            //---------------------------------------------------------------------------------------------------------
            TextureManager.Create(1, 1);
            ImageManager.Create(5, 2);
            GameSpriteManager.Create(4, 2);
            BoxSpriteManager.Create(3, 1);
            SpriteBatchManager.Create(3, 1);
            ProxySpriteManager.Create(10, 1);
            TimerManager.Create(3, 1);
            GameObjectManager.Create(3, 1);
            CollisionPairManager.Create(3, 1);
            InputManager.Create();
            SoundManager.Create();

            GlyphManager.Create(3, 1);
            FontManager.Create(3, 1);
            ScoreManager.Create();

            


            //---------------------------------------------------------------------------------------------------------
            // Load the Textures
            //---------------------------------------------------------------------------------------------------------
            TextureManager.Add(Texture.Name.Aliens, "aliens14x14.tga");
            TextureManager.Add(Texture.Name.Shield, "birds_N_shield.tga");
            TextureManager.Add(Texture.Name.Consolas36pt, "consolas36pt.tga");
            FontManager.AddXml(Glyph.Name.Consolas36pt, "Consolas36pt.xml", Texture.Name.Consolas36pt);

            //---------------------------------------------------------------------------------------------------------
            // Create Images
            //---------------------------------------------------------------------------------------------------------
            ImageManager.Add(Image.Name.Octopus0, Texture.Name.Aliens, 56, 28, 168, 112);
            ImageManager.Add(Image.Name.Octopus1, Texture.Name.Aliens, 56, 182, 168, 112);
            ImageManager.Add(Image.Name.Crab1, Texture.Name.Aliens, 322, 182, 154, 112);
            ImageManager.Add(Image.Name.Crab0, Texture.Name.Aliens, 322, 28, 154, 112);
            ImageManager.Add(Image.Name.Squid0, Texture.Name.Aliens, 616, 28, 112, 112);
            ImageManager.Add(Image.Name.Squid1, Texture.Name.Aliens, 616, 182, 112, 112);
            ImageManager.Add(Image.Name.UFO, Texture.Name.Aliens, 84, 504, 224, 98);
            ImageManager.Add(Image.Name.Missile, Texture.Name.Aliens, 378, 798, 14, 98);
            ImageManager.Add(Image.Name.Player, Texture.Name.Aliens, 57, 336, 182, 112);
            ImageManager.Add(Image.Name.BombCross, Texture.Name.Aliens, 196, 798, 42, 84);
            ImageManager.Add(Image.Name.BombStraight, Texture.Name.Aliens, 630, 798, 14, 98);
            ImageManager.Add(Image.Name.BombZigZag, Texture.Name.Aliens, 574, 644, 42, 98);

            ImageManager.Add(Image.Name.UFO_Explosion, Texture.Name.Aliens, 42, 643, 294, 112);
            ImageManager.Add(Image.Name.Player_Explosion1, Texture.Name.Aliens, 308, 336, 210, 112);
            ImageManager.Add(Image.Name.Player_Explosion2, Texture.Name.Aliens, 560, 336, 224, 112);
            ImageManager.Add(Image.Name.Missile_Explosion, Texture.Name.Aliens, 406, 490, 112, 112);
            ImageManager.Add(Image.Name.Alien_Explosion2, Texture.Name.Aliens, 574, 490, 182, 112);
            ImageManager.Add(Image.Name.Bomb_Explosion, Texture.Name.Aliens, 700, 798, 84, 112);

            ImageManager.Add(Image.Name.Brick, Texture.Name.Shield, 20, 210, 10, 5);
            ImageManager.Add(Image.Name.BrickLeft_Top0, Texture.Name.Shield, 15, 180, 10, 5);
            ImageManager.Add(Image.Name.BrickLeft_Top1, Texture.Name.Shield, 15, 185, 10, 5);
            ImageManager.Add(Image.Name.BrickLeft_Bottom, Texture.Name.Shield, 35, 215, 10, 5);
            ImageManager.Add(Image.Name.BrickRight_Top0, Texture.Name.Shield, 75, 180, 10, 5);
            ImageManager.Add(Image.Name.BrickRight_Top1, Texture.Name.Shield, 75, 185, 10, 5);
            ImageManager.Add(Image.Name.BrickRight_Bottom, Texture.Name.Shield, 55, 215, 10, 5);



            //---------------------------------------------------------------------------------------------------------
            // Create Sprites
            //---------------------------------------------------------------------------------------------------------
            GameSpriteManager.Add(GameSprite.Name.Squid, Image.Name.Squid0, 400, 200, Constant.ALIEN_WIDTH - 10, Constant.ALIEN_HEIGHT);
            GameSpriteManager.Add(GameSprite.Name.Octopus, Image.Name.Octopus0, 50, 500, Constant.ALIEN_WIDTH, Constant.ALIEN_HEIGHT);
            GameSpriteManager.Add(GameSprite.Name.Crab, Image.Name.Crab0, 100, 300, Constant.ALIEN_WIDTH, Constant.ALIEN_HEIGHT);
            GameSpriteManager.Add(GameSprite.Name.Missile, Image.Name.Missile, Constant.WINDOW_WIDTH / 2, 0, 4, 20);
            GameSpriteManager.Add(GameSprite.Name.Player, Image.Name.Player, Constant.WINDOW_WIDTH / 2, 30, 40.0f, 25.0f);
            GameSpriteManager.Add(GameSprite.Name.BombStraight, Image.Name.BombStraight, Constant.WINDOW_WIDTH / 2, Constant.WINDOW_HEIGHT, 6, 25.0f);
            GameSpriteManager.Add(GameSprite.Name.BombCross, Image.Name.BombCross, Constant.WINDOW_WIDTH / 2, Constant.WINDOW_HEIGHT, 6, 25.0f);
            GameSpriteManager.Add(GameSprite.Name.BombZigZag, Image.Name.BombZigZag, Constant.WINDOW_WIDTH / 2, Constant.WINDOW_HEIGHT, 6, 25.0f);
            GameSpriteManager.Add(GameSprite.Name.UFO, Image.Name.UFO, Constant.WINDOW_WIDTH / 2, Constant.WINDOW_HEIGHT, 40, 20);
            GameSpriteManager.Add(GameSprite.Name.Missile_Explosion, Image.Name.Missile_Explosion, 0, 0, 10, 10);

            GameSpriteManager.Add(GameSprite.Name.UFO_Explosion, Image.Name.UFO_Explosion, Constant.WINDOW_WIDTH / 2, Constant.WINDOW_HEIGHT, 40, 20);
            GameSpriteManager.Add(GameSprite.Name.Player_Explosion, Image.Name.Player_Explosion1, Constant.WINDOW_WIDTH / 2, Constant.WINDOW_HEIGHT, 40, 25.0f);
            GameSpriteManager.Add(GameSprite.Name.Bomb_Explosion, Image.Name.Bomb_Explosion, Constant.WINDOW_WIDTH / 2, Constant.WINDOW_HEIGHT, 10, 10);
            GameSpriteManager.Add(GameSprite.Name.Alien_Explosion, Image.Name.Alien_Explosion2, Constant.WINDOW_WIDTH / 2, Constant.WINDOW_HEIGHT, Constant.ALIEN_WIDTH, Constant.ALIEN_HEIGHT);


            GameSpriteManager.Add(GameSprite.Name.Brick, Image.Name.Brick, 50, 25, Constant.SHIELD_BRICK_WIDTH, Constant.SHIELD_BRICK_HEIGHT);
            GameSpriteManager.Add(GameSprite.Name.BrickLeft_Top0, Image.Name.BrickLeft_Top0, 50, 25, Constant.SHIELD_BRICK_WIDTH, Constant.SHIELD_BRICK_HEIGHT);
            GameSpriteManager.Add(GameSprite.Name.BrickLeft_Top1, Image.Name.BrickLeft_Top1, 50, 25, Constant.SHIELD_BRICK_WIDTH, Constant.SHIELD_BRICK_HEIGHT);
            GameSpriteManager.Add(GameSprite.Name.BrickLeft_Bottom, Image.Name.BrickLeft_Bottom, 50, 25, Constant.SHIELD_BRICK_WIDTH, Constant.SHIELD_BRICK_HEIGHT);
            GameSpriteManager.Add(GameSprite.Name.BrickRight_Top0, Image.Name.BrickRight_Top0, 50, 25, Constant.SHIELD_BRICK_WIDTH, Constant.SHIELD_BRICK_HEIGHT);
            GameSpriteManager.Add(GameSprite.Name.BrickRight_Top1, Image.Name.BrickRight_Top1, 50, 25, Constant.SHIELD_BRICK_WIDTH, Constant.SHIELD_BRICK_HEIGHT);
            GameSpriteManager.Add(GameSprite.Name.BrickRight_Bottom, Image.Name.BrickRight_Bottom, 50, 25, Constant.SHIELD_BRICK_WIDTH, Constant.SHIELD_BRICK_HEIGHT);



            //---------------------------------------------------------------------------------------------------------
            // Create SpriteBatch
            //---------------------------------------------------------------------------------------------------------
            SpriteBatch pSB_Aliens = SpriteBatchManager.Add(SpriteBatch.Name.GameSprites);
            SpriteBatch pSB_Boxes = SpriteBatchManager.Add(SpriteBatch.Name.BoxSprites);
            SpriteBatch pSB_Shields = SpriteBatchManager.Add(SpriteBatch.Name.ShieldSprites);
            SpriteBatch pSB_Bombs = SpriteBatchManager.Add(SpriteBatch.Name.BombSprites);
            SpriteBatch pSB_Texts = SpriteBatchManager.Add(SpriteBatch.Name.TextSprite);
            SpriteBatch pSB_Effect = SpriteBatchManager.Add(SpriteBatch.Name.ExplosionEffectSprite);

            pSB_Boxes.GetSBNodeManager().ToggleDraw();

            AlienGroup pAlienGroup = new AlienGroup(GameObject.Name.AlienGroup, GameSprite.Name.NullObject, -1, 0.0f, 0.0f);
            pSB_Boxes.Attach(pAlienGroup.pColObject.pBoxSprite);
            GameObjectManager.Attach(pAlienGroup);

            ShieldGroup pShieldGroup = new ShieldGroup(GameObject.Name.ShieldRoot, GameSprite.Name.NullObject, -1, 0.0f, 0.0f);
            pSB_Boxes.Attach(pShieldGroup.pColObject.pBoxSprite);
            GameObjectManager.Attach(pShieldGroup);

            AlienManager.Create();
            ShieldNodeManager.Create();

            GameSceneManager.Create();
            GameScene.Create();
            GameScene.SetGameScene(GameState.SceneName.Select);

        }

        //-----------------------------------------------------------------------------
        // Game::Update()
        //      Called once per frame, update data, tranformations, etc
        //      Use this function to control process order
        //      Input, AI, Physics, Animation, and Graphics
        //-----------------------------------------------------------------------------
        public override void Update()
        {
            GameScene.Update(this.GetTime());
        }

        //-----------------------------------------------------------------------------
        // Game::Draw()
        //		This function is called once per frame
        //	    Use this for draw graphics to the screen.
        //      Only do rendering here
        //-----------------------------------------------------------------------------
        public override void Draw()
        {
            GameScene.Draw();
        }

        //-----------------------------------------------------------------------------
        // Game::UnLoadContent()
        //       unload content (resources loaded above)
        //       unload all content that was loaded before the Engine Loop started
        //-----------------------------------------------------------------------------
        public override void UnLoadContent()
        {
            //SpriteBatchManager.Destroy();
            //BoxSpriteManager.Destroy();
            //GameSpriteManager.Destroy();
            //ImageManager.Destroy();
            //TextureManager.Destroy();
        }
    }
}

