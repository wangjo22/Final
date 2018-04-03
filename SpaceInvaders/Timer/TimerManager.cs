using System;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract public class TimerManager_Link : Manager
    {
        public TimeEvent_Link pActive = null;
        public TimeEvent_Link pReserve = null;
    }
    public class TimerManager : TimerManager_Link
    {
        //---------------------------------------------------------------------------------
        // Constructors
        //---------------------------------------------------------------------------------
        private TimerManager(int numReserve = 3, int reserveGrow = 1)
            : base()
        {
            // At this point TimeManager is created, now initialize the reserve
            this.BaseInitialize(numReserve, reserveGrow);

            // initialize derived data here
            this.poNodeCompare = new TimeEvent();
            this.deltaTimeForAlien = 1.0f;
        }

        //---------------------------------------------------------------------------------
        // Static Methods
        //---------------------------------------------------------------------------------
        public static void Create(int reserveNum = 3, int reserveGrow = 1)
        {
            Debug.Assert(reserveNum > 0);
            Debug.Assert(reserveGrow > 0);

            // Initialize the singleton here
            Debug.Assert(pInstance == null);

            // Do the initialization
            if (pInstance == null)
            {
                pInstance = new TimerManager(reserveNum, reserveGrow);
            }
        }

        public static void Destroy()
        {
            TimerManager pMan = TimerManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            // Do something clever here
            // track peak number of active nodes
            // print stats on destroy
            // invalidate the singleton
        }

        public static TimeEvent Add(TimeEvent.Name EventName, Command pCommand, float deltaTime)
        {
            Debug.Assert(pCommand != null);
            Debug.Assert(deltaTime > 0.0f);

            TimerManager pMan = TimerManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // Take a node out from Reserve list. 
            // This node is washed in BasePopNode().
            TimeEvent pNode = (TimeEvent)pMan.BasePopNode();
            Debug.Assert(pNode != null);

            pNode.Set(EventName, pCommand, deltaTime);
            pMan.BaseSortedAdd(pNode);
            return pNode;
        }

        public static TimeEvent Find(TimeEvent.Name name)
        {
            TimerManager pMan = TimerManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            // So:  Use the Compare Node - as a reference
            //      use in the Compare() function
            pMan.poNodeCompare.name = name;
            TimeEvent pData = (TimeEvent)pMan.BaseFind(pInstance.poNodeCompare);
            return pData;
        }
        public static void Remove(TimeEvent pNode)
        {
            TimerManager pMan = TimerManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            Debug.Assert(pNode != null);
            pMan.BaseRemove(pNode);
        }
        public static void Dump()
        {
            TimerManager pMan = TimerManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            pMan.BaseDump();
        }

        public static void PrintMe()
        {
            TimeEvent pNode = (TimeEvent)pInstance.BaseGetActive();
            while(pNode != null)
            {
                Debug.WriteLine("\t\t{0}: {1}", pNode.name, pNode.GetTriggerTime());
                pNode = (TimeEvent)pNode.pNext;
            }
            Debug.WriteLine("------------------------------------------------");
        }

        public static void Update(float totalTime)
        {
            TimerManager pMan = TimerManager.PrivGetInstance();
            Debug.Assert(pMan != null);

            // squirrel away
            pMan.currentTime = totalTime;

            // walk the list
            TimeEvent pEvent = (TimeEvent)pMan.BaseGetActive();
            TimeEvent pNextEvent = null;

            // Walk the list until there is no more list OR currTime is greater than timeEvent 
            while (pEvent != null && pMan.currentTime >= pEvent.triggerTime)
            {
                pNextEvent = (TimeEvent)pEvent.pNext;
                pEvent.Process();
                pMan.BaseRemove(pEvent);

                // advance the pointer
                pEvent = pNextEvent;
            }
        }
        public static float GetCurrentTime()
        {
            TimerManager pMan = TimerManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            // return time
            return pMan.currentTime;
        }

        public static float GetAlienDeltatTime()
        {
            TimerManager pMan = TimerManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            // return time
            return pMan.deltaTimeForAlien;
        }

        public static void SetAlienDeltaTime(float newDeltaTime)
        {
            TimerManager pMan = TimerManager.PrivGetInstance();
            Debug.Assert(pMan != null);
            // return time
            pMan.deltaTimeForAlien = newDeltaTime;
        }

        //----------------------------------------------------------------------
        // Override Abstract methods
        //----------------------------------------------------------------------
        protected override bool DerivedCompare(DLink pLinkA, DLink pLinkB)
        {
            Debug.Assert(pLinkA != null);
            Debug.Assert(pLinkB != null);

            TimeEvent pDataA = (TimeEvent)pLinkA;
            TimeEvent pDataB = (TimeEvent)pLinkB;

            Boolean status = false;

            if (pDataA.name == pDataB.name)
            {
                status = true;
            }

            return status;
        }

        protected override DLink DerivedCreateNode()
        {
            DLink pNode = new TimeEvent();
            Debug.Assert(pNode != null);

            return pNode;
        }

        protected override void DerivedDumpNode(DLink pLink)
        {
            Debug.Assert(pLink != null);
            TimeEvent pData = (TimeEvent)pLink;
            //pData.Dump();
        }

        protected override void DerivedWash(DLink pLink)
        {
            Debug.Assert(pLink != null);
            TimeEvent pNode = (TimeEvent)pLink;
            pNode.Wash();
        }


        //----------------------------------------------------------------------
        // Private methods
        //----------------------------------------------------------------------
        private static TimerManager PrivGetInstance()
        {
            // Safety - this forces users to call Create() first before using class
            Debug.Assert(pInstance != null);
            return pInstance;
        }


        //---------------------------------------------------------------------------------
        // Data
        //---------------------------------------------------------------------------------
        private static TimerManager pInstance = null;
        private TimeEvent poNodeCompare;
        protected float currentTime;
        private float deltaTimeForAlien;
    }
}
