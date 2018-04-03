using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class TimeEvent_Link : DLink
    {
    }
    public class TimeEvent : TimeEvent_Link
    {
        //----------------------------------------------------------------
        // Enum
        //----------------------------------------------------------------
        public enum Name
        {
            AnimationSprite,
            MovementSprite,
            SpawnBomb,
            PlayerExplosion,
            Uninitialized
        }

        //----------------------------------------------------------------
        // Constructor
        //----------------------------------------------------------------
        public TimeEvent()
            : base()
        {
            this.name = Name.Uninitialized;
            this.pCommand = null;
            triggerTime = 0.0f;
        }

        //----------------------------------------------------------------
        // Methods
        //----------------------------------------------------------------
        public void Set(Name name, Command inCommand, float deltaTime)
        {
            Debug.Assert(inCommand != null);

            this.name = name;
            this.pCommand = inCommand;
            this.deltaTime = deltaTime;
            this.triggerTime = TimerManager.GetCurrentTime() + deltaTime;
        }

        public void Process()
        {
            Debug.Assert(pCommand != null);
            pCommand.Execute(deltaTime);
        }

        public new void Clear()
        {
            this.name = Name.Uninitialized;
            this.pCommand = null;
            this.triggerTime = 0.0f;
        }

        public void Wash()
        {
            base.Clear();
            this.Clear();
        }
        public float GetTriggerTime()
        {
            return this.triggerTime;
        }

        public float GetDeltaTime()
        {
            return this.deltaTime;
        }

        override protected float DerivedCompareValue()
        {
            return this.triggerTime;
        }

        //-----------------------------
        // Data
        //-----------------------------
        public Name name;
        public Command pCommand;
        public float triggerTime;
        protected float deltaTime;
    }
}
