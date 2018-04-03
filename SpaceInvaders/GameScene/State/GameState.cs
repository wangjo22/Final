using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class GameState
    {
        protected GameState()
        {

        }
        public enum SceneName
        {
            Select,
            Player1,
            Player2,
            GameOver,
            Undefined
        }
        abstract public void Handle();
        abstract public void LoadContent();
        abstract public void Update(float getTime);
        abstract public void Draw();
        abstract public void MoveToNextStage();
        public GameState.SceneName GetStateName()
        {
            return this.name;
        }
        protected SceneName name;
    }
}
