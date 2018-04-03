using System;
using System.Diagnostics;

namespace SpaceInvaders
{

    public class Player1Scene : GameState
    {
        public Player1Scene()
        {
            this.name = SceneName.Player1;
            this.didLoadContent = false;
            this.didLoadAliens = false;
            this.haveYouPlayed = false;
            this.isGameOver = false;
            this.poAlienGroup = new AlienGroup(GameObject.Name.AlienGroup, GameSprite.Name.NullObject, -1, 0.0f, 0.0f);
            this.poShieldGroup = new ShieldGroup(GameObject.Name.ShieldRoot, GameSprite.Name.NullObject, -1, 0.0f, 0.0f);
            this.poBombGroup = new BombGroup(GameObject.Name.BombGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            this.poMissileGroup = new MissileGroup(GameObject.Name.MissileGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            this.poUFOGroup = new UFOGroup(GameObject.Name.UFOGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            this.offsetForNextState = 50.0f;
            this.numStage = 1;
        }
        public override void Handle()
        {
            if (!GameScene.CheckIs2PlayerMode())     // This is single play mode
            {
                if (GameScene.Get1PlayerLive() >= 0)   //  
                {
                    this.MoveToNextStage();
                }
                else
                {
                    this.EnterToGameOver();
                    GameScene.SetGameScene(SceneName.GameOver);
                    this.haveYouPlayed = true;
                }
            }                
        }

        public void EnterToGameOver()
        {
            UFOManager.DeactiveUFO();
            ScoreManager.InitializePlayer1Score();


            //AlienManager.RemoveAliens();
            BombManager.RemoveBombs();
            DelayRemoveManager.RemoveAll();


            // Remove all remaining shields
            // 
        }

        public void LoadShield()
        {
            ShieldFactory.InitializeAlienFactory();

            GameObject pShieldGroup = GameObjectManager.Find(GameObject.Name.ShieldRoot);

            int colIndex = 0;
            int brickIndex = 100;
            // load by column
            {
         

                GameObject pColumn;
                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                float start_x = 70.0f;
                float start_y = 150.0f;
                float off_x = 0;
                float brickWidth = Constant.SHIELD_BRICK_WIDTH;
                float brickHeight = Constant.SHIELD_BRICK_HEIGHT;

                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.LeftTop1, brickIndex++, start_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.LeftTop0, brickIndex++, start_x, start_y + 9 * brickHeight);

                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);


                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.LeftBottom, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);

                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);


                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);

                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.RightBottom, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);

                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 0 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 1 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);


                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 0 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 1 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.RightTop1, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.RightTop0, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);



                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                start_x = 220.0f;
                start_y = 150.0f;
                off_x = 0;
                brickWidth = Constant.SHIELD_BRICK_WIDTH;
                brickHeight = Constant.SHIELD_BRICK_HEIGHT;

                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.LeftTop1, brickIndex++, start_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.LeftTop0, brickIndex++, start_x, start_y + 9 * brickHeight);

                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);


                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.LeftBottom, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);

                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);


                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);

                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.RightBottom, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);


                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 0 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 1 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);


                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 0 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 1 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.RightTop1, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.RightTop0, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);



                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                start_x = 370.0f;
                start_y = 150.0f;
                off_x = 0;
                brickWidth = Constant.SHIELD_BRICK_WIDTH;
                brickHeight = Constant.SHIELD_BRICK_HEIGHT;

                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.LeftTop1, brickIndex++, start_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.LeftTop0, brickIndex++, start_x, start_y + 9 * brickHeight);

                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);


                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.LeftBottom, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);

                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);


                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);

                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.RightBottom, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);


                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 0 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 1 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);


                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 0 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 1 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.RightTop1, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.RightTop0, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);



                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                start_x = 510.0f;
                start_y = 150.0f;
                off_x = 0;
                brickWidth = Constant.SHIELD_BRICK_WIDTH;
                brickHeight = Constant.SHIELD_BRICK_HEIGHT;

                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.LeftTop1, brickIndex++, start_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.LeftTop0, brickIndex++, start_x, start_y + 9 * brickHeight);

                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);


                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.LeftBottom, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);

                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);


                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);

                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.RightBottom, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);


                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);

                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 0 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 1 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);


                pColumn = ShieldFactory.Create(ShieldCategory.Type.Column, colIndex++);
                pShieldGroup.Add(pColumn);
                ShieldFactory.SetParent(pColumn);


                off_x += brickWidth;
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 0 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 1 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 2 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 3 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 4 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 5 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 6 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.Brick, brickIndex++, start_x + off_x, start_y + 7 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.RightTop1, brickIndex++, start_x + off_x, start_y + 8 * brickHeight);
                ShieldFactory.Create(ShieldCategory.Type.RightTop0, brickIndex++, start_x + off_x, start_y + 9 * brickHeight);



                ForwardIterator fi = new ForwardIterator(pShieldGroup);
                Component c = fi.First();
                while (!fi.IsDone())
                {
                    fi.Next();
                }

                ReverseIterator ri = new ReverseIterator(pShieldGroup);
                Component d = ri.First();
                while (!ri.IsDone())
                {
                    ri.Next();
                }
            }
        }

        public void LoadAliens()
        {
            //Create Alien Factory.
            AlienFactory.InitializeAlienFactory();
            int colIndex = 0;
            int alienIndex = 100;
            AlienGroup pAlienGroup = (AlienGroup)GameObjectManager.Find(GameObject.Name.AlienGroup);

            GameObject tmpColObj;
            GameObject tmpGameObj;
            float offset = Constant.ALIEN_OFFSET_X;
            for (int i = 0; i < 11; ++i)
            {
                tmpColObj = AlienFactory.Create(AlienCategory.Type.Column, colIndex++);

                tmpGameObj = AlienFactory.Create(AlienCategory.Type.Squid, alienIndex++, 100.0f + offset * i, Constant.WINDOW_HEIGHT - 200.0f - this.offsetForNextState*(this.numStage - 1));
                tmpColObj.Add(tmpGameObj);

                tmpGameObj = AlienFactory.Create(AlienCategory.Type.Crab, alienIndex++, 100.0f + offset * i, Constant.WINDOW_HEIGHT - 240.0f - this.offsetForNextState * (this.numStage - 1));
                tmpColObj.Add(tmpGameObj);

                tmpGameObj = AlienFactory.Create(AlienCategory.Type.Crab, alienIndex++, 100.0f + offset * i, Constant.WINDOW_HEIGHT - 280.0f- this.offsetForNextState * (this.numStage - 1));
                tmpColObj.Add(tmpGameObj);

                tmpGameObj = AlienFactory.Create(AlienCategory.Type.Octopus, alienIndex++, 100.0f + offset * i, Constant.WINDOW_HEIGHT - 320.0f - this.offsetForNextState * (this.numStage - 1));
                tmpColObj.Add(tmpGameObj);

                tmpGameObj = AlienFactory.Create(AlienCategory.Type.Octopus, alienIndex++, 100.0f + offset * i, Constant.WINDOW_HEIGHT - 360.0f - this.offsetForNextState * (this.numStage - 1));
                tmpColObj.Add(tmpGameObj);

                pAlienGroup.Add(tmpColObj);
            }

            ForwardIterator fi1 = new ForwardIterator(pAlienGroup);
            Component pCom1 = fi1.First();
            while (!fi1.IsDone())
            {
                // pCom1.Print();
                pCom1 = fi1.Next();
            }

            ReverseIterator ri1 = new ReverseIterator(pAlienGroup);
            Component pc1o = ri1.First();
            while (!ri1.IsDone())
            {
                // pc1o.Print();
                pc1o = ri1.Next();
            }

            if (!this.didLoadAliens)
            {
                //Attach the Alien hierachy to GameObjectManager
                MovementSprite pMovementSprite = new MovementSprite(pAlienGroup);
                TimerManager.Add(TimeEvent.Name.MovementSprite, pMovementSprite, 0.1f);

                SpawnBomb pSwapnBomb = new SpawnBomb(pAlienGroup);
                TimerManager.Add(TimeEvent.Name.SpawnBomb, pSwapnBomb, 0.5f);
            }
            this.didLoadAliens = true;
        }

        public override void MoveToNextStage()
        {
            this.numStage++;
            AlienManager.ResetAllAlienGrid();
            AlienManager.ResetNumAliens();
            AlienManager.ResetAlienPoints((this.numStage - 1) * this.offsetForNextState);
            ShieldNodeManager.ResetAllShieldGrid();
            DelayRemoveManager.RemoveAll();
            //this.LoadAliens();
            Font pTestMessage = FontManager.Find(Font.Name.Player1_Stage);
            Debug.Assert(pTestMessage != null);
            pTestMessage.UpdateMessage("Stage " + this.numStage);

            AlienGroup pG = (AlienGroup)GameObjectManager.Find(GameObject.Name.AlienGroup);
            pG.ResetState();
        }

        public override void LoadContent()
        {

            this.LoadAliens();
            this.LoadShield();

            SpriteBatch pSB_Boxes = SpriteBatchManager.Find(SpriteBatch.Name.BoxSprites);
            SpriteBatch pSB_Aliens = SpriteBatchManager.Find(SpriteBatch.Name.GameSprites);
            //---------------------------------------------------------------------------------------------------------
            // Create an animation sprite
            //---------------------------------------------------------------------------------------------------------
            AnimationSprite pAnimatedOctopus = new AnimationSprite(GameSprite.Name.Octopus);
            AnimationSprite pAnimatedSquid = new AnimationSprite(GameSprite.Name.Squid);
            AnimationSprite pAnimatedCrab = new AnimationSprite(GameSprite.Name.Crab);
            AnimationSprite pAnimationPlayerExplosion = new AnimationSprite(GameSprite.Name.Player_Explosion);
            //AnimationSprite pAnimationAlienExplosion = new AnimationSprite(GameSprite.Name.Alien_Explosion);

            //---------------------------------------------------------------------------------------------------------
            // Attach several images to AnimationSprite. -> Images will be held in ImageHolders
            //---------------------------------------------------------------------------------------------------------
            pAnimatedOctopus.Attach(Image.Name.Octopus1);
            pAnimatedOctopus.Attach(Image.Name.Octopus0);
            pAnimatedSquid.Attach(Image.Name.Squid1);
            pAnimatedSquid.Attach(Image.Name.Squid0);
            pAnimatedCrab.Attach(Image.Name.Crab1);
            pAnimatedCrab.Attach(Image.Name.Crab0);
            pAnimationPlayerExplosion.Attach(Image.Name.Player_Explosion2);
            pAnimationPlayerExplosion.Attach(Image.Name.Player_Explosion1);
           // pAnimationAlienExplosion.Attach(Image.Name.Alien_Explosion2);
           // pAnimationAlienExplosion.Attach(Image.Name.Alien_Explosion1);


            // Add AnimationSprite to timer
            TimerManager.Add(TimeEvent.Name.AnimationSprite, pAnimatedSquid, TimerManager.GetAlienDeltatTime());
            TimerManager.Add(TimeEvent.Name.AnimationSprite, pAnimatedCrab, TimerManager.GetAlienDeltatTime());
            TimerManager.Add(TimeEvent.Name.AnimationSprite, pAnimatedOctopus, TimerManager.GetAlienDeltatTime());
            TimerManager.Add(TimeEvent.Name.Uninitialized, pAnimationPlayerExplosion, 0.1f);
         //   TimerManager.Add(TimeEvent.Name.AnimationSprite, pAnimationAlienExplosion, 0.2f);

            //---------------------------------------------------------------------------------------------------------
            // Create Bomb
            //---------------------------------------------------------------------------------------------------------
            BombGroup pBombGroup = new BombGroup(GameObject.Name.BombGroup, GameSprite.Name.NullObject, -1, -1);
            pBombGroup.ActivateCollisionSprite(pSB_Boxes);

            BombManager.Create(pBombGroup);
            GameObjectManager.Attach(pBombGroup);

            //---------------------------------------------------------------------------------------------------------
            // Create Walls
            //---------------------------------------------------------------------------------------------------------
            WallGroup pWallGroup = new WallGroup(GameObject.Name.WallGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pWallGroup.ActivateCollisionSprite(pSB_Boxes);
            pWallGroup.ActivateGameSprite(pSB_Aliens);

            WallRight pWallRight = new WallRight(GameObject.Name.WallRight, GameSprite.Name.NullObject,
                Constant.WINDOW_WIDTH - 10, Constant.WINDOW_HEIGHT / 2, 15, Constant.WINDOW_HEIGHT - 10);
            pWallRight.ActivateCollisionSprite(pSB_Boxes);

            WallLeft pWallLeft = new WallLeft(GameObject.Name.WallLeft, GameSprite.Name.NullObject,
                10, Constant.WINDOW_HEIGHT / 2, 15, Constant.WINDOW_HEIGHT - 10);
            pWallLeft.ActivateCollisionSprite(pSB_Boxes);

            WallTop pWallTop = new WallTop(GameObject.Name.WallTop, GameSprite.Name.NullObject,
                Constant.WINDOW_WIDTH / 2, Constant.WINDOW_HEIGHT - 10.0f, Constant.WINDOW_WIDTH - 50, 10.0f);
            pWallTop.ActivateCollisionSprite(pSB_Boxes);

            WallBottom pWallBottom = new WallBottom(GameObject.Name.WallBottom, GameSprite.Name.NullObject,
                Constant.WINDOW_WIDTH / 2, 10.0f, Constant.WINDOW_WIDTH - 50, 10.0f);
            pWallBottom.ActivateCollisionSprite(pSB_Boxes);


            BumperLeft pBumperLeft = new BumperLeft(GameObject.Name.BumperLeft, GameSprite.Name.NullObject, 40, 100, 15, 100);
            pBumperLeft.ActivateCollisionSprite(pSB_Boxes);

            BumperRight pBumperRight = new BumperRight(GameObject.Name.BumperRight, GameSprite.Name.NullObject,
                Constant.WINDOW_WIDTH - 40, 100, 15, 100);
            pBumperRight.ActivateCollisionSprite(pSB_Boxes);

            pWallGroup.Add(pWallRight);
            pWallGroup.Add(pWallLeft);
            pWallGroup.Add(pWallTop);
            pWallGroup.Add(pWallBottom);
            pWallGroup.Add(pBumperLeft);
            pWallGroup.Add(pBumperRight);

            GameObjectManager.Attach(pWallGroup);

            //---------------------------------------------------------------------------------------------------------
            // Create Missile Objects
            //---------------------------------------------------------------------------------------------------------

            // Create Missile group.
            MissileGroup pMissileGroup = new MissileGroup(GameObject.Name.MissileGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pMissileGroup.ActivateCollisionSprite(pSB_Boxes);


            // Attach the missile hierachy to Game Object Manager
            GameObjectManager.Attach(pMissileGroup);

            //---------------------------------------------------------------------------------------------------------
            // Create player spaceship
            //---------------------------------------------------------------------------------------------------------
            PlayerGroup pPlayerGroup = new PlayerGroup(GameObject.Name.PlayerGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pPlayerGroup.ActivateCollisionSprite(pSB_Boxes);
            GameObjectManager.Attach(pPlayerGroup);

            PlayerManager.Create();


            //-------------------------------------------------------------
            // Create UFO
            //-------------------------------------------------------------
            UFOGroup pUFOGroup = new UFOGroup(GameObject.Name.UFOGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            pSB_Boxes.Attach(pUFOGroup.pColObject.pBoxSprite);

            UFOManager.Create(pUFOGroup);

            SpawnUFO pSpawnUFO = new SpawnUFO();
            TimerManager.Add(TimeEvent.Name.SpawnBomb, pSpawnUFO, 10.0f);

            SpawnBombForUFO pSBFU = new SpawnBombForUFO(pUFOGroup);
            TimerManager.Add(TimeEvent.Name.SpawnBomb, pSBFU, 13.0f);

            GameObjectManager.Attach(pUFOGroup);


            //---------------------------------------------------------------------------------------------------------
            // ColPair 
            //---------------------------------------------------------------------------------------------------------
            GameObject pAlienGroup = GameObjectManager.Find(GameObject.Name.AlienGroup);
            GameObject pShieldGroup = GameObjectManager.Find(GameObject.Name.ShieldRoot);


            CollisionPair pColPair = CollisionPairManager.Add(CollisionPair.Name.Alien_VS_Missile, pMissileGroup, pAlienGroup);
            pColPair.AttachObserver(new PlayerReadyStateObserver());
            pColPair.AttachObserver(new RemoveMissileObserver());
            pColPair.AttachObserver(new RemoveAlienObserver());

            pColPair = CollisionPairManager.Add(CollisionPair.Name.Alien_VS_Shield, pAlienGroup, pShieldGroup);
            pColPair.AttachObserver(new RemoveBrickObserver());

            pColPair = CollisionPairManager.Add(CollisionPair.Name.Alien_VS_PlayerShip, pPlayerGroup, pAlienGroup);
            //Add GameOver Observer.

            pColPair = CollisionPairManager.Add(CollisionPair.Name.PlyaerShip_VS_Bomb, pPlayerGroup, pBombGroup);
            pColPair.AttachObserver(new RemoveBombObserver());
            pColPair.AttachObserver(new PlayerDieObserver());

            pColPair = CollisionPairManager.Add(CollisionPair.Name.Alien_VS_Wall, pAlienGroup, pWallGroup);
            pColPair.AttachObserver(new AlienGridObserver());

            pColPair = CollisionPairManager.Add(CollisionPair.Name.Missile_VS_Wall, pMissileGroup, pWallGroup);
            pColPair.AttachObserver(new PlayerReadyStateObserver());
            pColPair.AttachObserver(new RemoveMissileObserver());

            pColPair = CollisionPairManager.Add(CollisionPair.Name.Bomb_VS_Wall, pBombGroup, pWallGroup);
            pColPair.AttachObserver(new RemoveBombObserver());

            pColPair = CollisionPairManager.Add(CollisionPair.Name.PlayerShip_VS_Wall, pPlayerGroup, pWallGroup);
            pColPair.AttachObserver(new PlayerHitLeftWallObserver());
            pColPair.AttachObserver(new PlayerHitRightWallObserver());

            pColPair = CollisionPairManager.Add(CollisionPair.Name.Missile_VS_Shield, pMissileGroup, pShieldGroup);
            pColPair.AttachObserver(new PlayerReadyStateObserver());
            pColPair.AttachObserver(new RemoveMissileObserver());
            pColPair.AttachObserver(new RemoveBrickObserver());

            pColPair = CollisionPairManager.Add(CollisionPair.Name.Bomb_VS_Shield, pBombGroup, pShieldGroup);
            pColPair.AttachObserver(new RemoveBombObserver());
            pColPair.AttachObserver(new RemoveBrickObserver());

            pColPair = CollisionPairManager.Add(CollisionPair.Name.Wall_VS_UFO, pWallGroup, pUFOGroup);
            pColPair.AttachObserver(new RemoveUFOObserver());

            pColPair = CollisionPairManager.Add(CollisionPair.Name.Missile_VS_UFO, pMissileGroup, pUFOGroup);
            pColPair.AttachObserver(new RemoveMissileObserver());
            pColPair.AttachObserver(new RemoveUFOObserver());

            pColPair = CollisionPairManager.Add(CollisionPair.Name.Bomb_VS_Missile, pBombGroup, pMissileGroup);
            pColPair.AttachObserver(new RemoveMissileObserver());
            pColPair.AttachObserver(new RemoveBombObserver());


            //---------------------------------------------------------------------------------------------------------
            // Input 
            //---------------------------------------------------------------------------------------------------------

            InputSubject pInputSubject;
            pInputSubject = InputManager.GetMoveRightSubject();
            pInputSubject.Attach(new MoveRightObserver());

            pInputSubject = InputManager.GetMoveLeftSubject();
            pInputSubject.Attach(new MoveLeftObserver());

            pInputSubject = InputManager.GetShootMissileSubject();
            pInputSubject.Attach(new ShootMissileObserver());

            pInputSubject = InputManager.GetOnOffShieldSubject();
            pInputSubject.Attach(new OnOffShieldSprites());

            pInputSubject = InputManager.GetOnOffCollisionBoxSubject();
            pInputSubject.Attach(new OnOffCollisionBox());

            FontManager.Add(Font.Name.Score, SpriteBatch.Name.TextSprite, "SCORE-1     HI-SCORE     SCORE-2", Glyph.Name.Consolas36pt, 40, 740);
            FontManager.Add(Font.Name.Player1_Score, SpriteBatch.Name.TextSprite, "0000", Glyph.Name.Consolas36pt, 70, 700);
            FontManager.Add(Font.Name.High_Score, SpriteBatch.Name.TextSprite, "0000", Glyph.Name.Consolas36pt, 300, 700);
            FontManager.Add(Font.Name.Player2_Score, SpriteBatch.Name.TextSprite, "0000", Glyph.Name.Consolas36pt, 520, 700);
            FontManager.Add(Font.Name.Player1_Lives, SpriteBatch.Name.TextSprite, "Lives 3", Glyph.Name.Consolas36pt, 20, 20);
            FontManager.Add(Font.Name.Player2_Lives, SpriteBatch.Name.TextSprite, "Lives 0", Glyph.Name.Consolas36pt, Constant.WINDOW_WIDTH - 200, 20);
            FontManager.Add(Font.Name.Player1_Stage, SpriteBatch.Name.TextSprite, "Stage " + this.numStage, Glyph.Name.Consolas36pt, 250, 20);

            ExplosionGroup pExplosionGrpuo = new ExplosionGroup(GameObject.Name.ExplosionGroup, GameSprite.Name.NullObject, 0.0f, 0.0f);
            GameObjectManager.Attach(pExplosionGrpuo);
            ExplosionManager.Create(pExplosionGrpuo);
        }

        public void ReloadContent()
        {
            this.isGameOver = false;
            this.numStage = 1;

            //this.LoadAliens();
            AlienManager.ResetAllAlienGrid();
            AlienManager.ResetAlienPoints();
            ShieldNodeManager.ResetAllShieldGrid();

            Font pTestMessage = FontManager.Find(Font.Name.Player1_Stage);
            Debug.Assert(pTestMessage != null);
            pTestMessage.UpdateMessage("Stage " + this.numStage);

            ScoreManager.UpdateAllScore();
            AlienManager.ResetNumAliens();

            Player pPlayer = PlayerManager.GetPlayer();
            pPlayer.x = Constant.WINDOW_WIDTH / 2;
            pPlayer.y = 100.0f;
            PlayerManager.SetPlayerAlive();
            pPlayer.SetShootingState(PlayerManager.ShootState.MissileReady);

            GameObject explosion = ExplosionManager.GetPlayerExplosion();
            explosion.x = -100;
            explosion.y = -100;
           
            GameObject p = PlayerManager.GetMissile();
            p.x = -10;

            AlienGroup pG = (AlienGroup)GameObjectManager.Find(GameObject.Name.AlienGroup);
            pG.ResetState();


            SpawnUFO pSpawnUFO = new SpawnUFO();
            TimerManager.Add(TimeEvent.Name.SpawnBomb, pSpawnUFO, 5.0f);
            // Add timer for ufo. 
        }

        public override void Update(float getTime)
        {
            if (!this.didLoadContent)
            {
                this.LoadContent();
                this.didLoadContent = true;
            }

            if(this.haveYouPlayed)
            {
                this.ReloadContent();
                this.haveYouPlayed = false;
            }

            TimerManager.Update(getTime);
            GameObjectManager.Update();
            CollisionPairManager.Process();
            InputManager.Update();
            DelayRemoveManager.Process();

            if (this.GetIsGameOver())
            {
                this.Handle();
            }
        }

        public override void Draw()
        {
            SpriteBatchManager.Draw();
        }

        public void GameOver()
        {
            this.isGameOver = true;
        }

        public bool GetIsGameOver()
        {
            return this.isGameOver;
        }

        private bool didLoadContent;
        private bool didLoadAliens;
        private bool haveYouPlayed;
        private bool isGameOver;
        private int numStage;
        private AlienGroup poAlienGroup;
        private ShieldGroup poShieldGroup;
        private BombGroup poBombGroup;
        private MissileGroup poMissileGroup;
        private UFOGroup poUFOGroup;
        private float offsetForNextState;
        
    }
}
