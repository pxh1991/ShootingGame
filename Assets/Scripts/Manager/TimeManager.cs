using UnityEngine;
using System.Collections.Generic;
using System;

namespace Shooting.Tool
{    public class TimerManager
    {
        private const int InvalidTimerId = 0; 
        private static int TimerIdCnt; 
        private static float CurTime; 
        private static readonly Dictionary<int, TimerBase> TimerMap = new Dictionary<int, TimerBase>();
        private static readonly List<TimerBase> TimerList = new List<TimerBase>();

        public static int AddTimer(Action handler, float delay = 0, float interval = 0, int playCnt = 1,
            bool useLogicTime = false)
        {
            TimerBase p = new TimerData(delay, interval, playCnt, handler);
            p.useLogicTime = useLogicTime;
            return AddTimerImpl(p);
        }

        public static int AddTimer<T>(Action<T> handler, T arg1, float delay = 0, float interval = 0, int playCnt = 1,
            bool useLogicTime = true)
        {
            TimerBase p = new TimerData<T>(delay, interval, playCnt, handler, arg1);
            p.useLogicTime = useLogicTime;
            return AddTimerImpl(p);
        }

        public static int AddTimer<T, U>(Action<T, U> handler, T arg1, U arg2, float delay = 0, float interval = 0,
            int playCnt = 1, bool useLogicTime = true)
        {
            TimerBase p = new TimerData<T, U>(delay, interval, playCnt, handler, arg1, arg2);
            p.useLogicTime = useLogicTime;
            return AddTimerImpl(p);
        }

        public static int AddTimer<T, U, V>(Action<T, U, V> handler, T arg1, U arg2, V arg3, float delay = 0,
            float interval = 0, int playCnt = 1, bool useLogicTime = true)
        {
            TimerBase p = new TimerData<T, U, V>(delay, interval, playCnt, handler, arg1, arg2, arg3);
            p.useLogicTime = useLogicTime;
            return AddTimerImpl(p);
        }

        private static int AddTimerImpl(TimerBase p)
        {
        #if UNITY_EDITOR
            foreach (var t in TimerList)
            {
                if (t == p)
                {
                    Debug.LogError("重复添加定时器 " + p.GetCallBack().Method.ToString());
                }
            }
        #endif


            if (++TimerIdCnt == InvalidTimerId)
                ++TimerIdCnt;
            p.timerid = TimerIdCnt;
            TimerMap.Add(p.timerid, p);
            TimerList.Add(p);

            return p.timerid;
        }

        public static void DelTimer(ref int timerId)
        {
            if (timerId == InvalidTimerId)
                return;

            TimerBase t;

            if (TimerMap.TryGetValue(timerId, out t))
                t.valid = false;

            TimerMap.Remove(timerId);
            timerId = 0;
        }

        public static void DelTimer(int timerId)
        {
            if (timerId == InvalidTimerId)
                return;

            TimerBase t;

            if (TimerMap.TryGetValue(timerId, out t))
                t.valid = false;

            TimerMap.Remove(timerId);
        }

        public static void Tick()
        {
            var realDelta = Time.realtimeSinceStartup - CurTime;
            CurTime = Time.realtimeSinceStartup;

            for (var i = TimerList.Count - 1; i >= 0; i--)
            {
                var delta = TimerList[i].useLogicTime ? Time.deltaTime : realDelta;

                if (TimerList[i].valid && TimerList[i].DoAction(delta)) continue;
                if (TimerMap.ContainsKey(TimerList[i].timerid))
                    TimerMap.Remove(TimerList[i].timerid);

                TimerList.RemoveAt(i);
            }
        }

        public static bool hasAddTimer(int timeID)
        {
            TimerBase timer;
            return TimerMap.TryGetValue(timeID, out timer) && timer.valid;
        }


        public static string GetCallBackInfo()
        {
            var text = TimerList.Count.ToString() + "\r\n";
            for (var i = TimerList.Count - 1; i >= 0; i--)
            {
                text += TimerList[i].GetCallBack().Target + " " + TimerList[i].GetCallBack().Method + "\r\n";
            }

            return text;
        }


        public static void Reset()
        {
            TimerMap.Clear();
        }


        public static float GetLastTime(int timerId)
        {
            TimerBase t;
            if (TimerMap.TryGetValue(timerId, out t))
            {
                return t.GetLastTime();
            }

            return 0;
        }

        #region 内部类

 
        private abstract class TimerBase
        {
            public int timerid;
            public bool valid; 
            private float interval;
            private float nextTime;

            private int playCnt;


            public bool useLogicTime;

            protected abstract void OnDoAction();

            public abstract Delegate GetCallBack();

            public TimerBase(float startTime, float interval, int playCnt)
            {
                this.interval = interval;
                nextTime = startTime;
                this.playCnt = playCnt;
                valid = true;
            }


            public bool DoAction(float delta)
            {
                nextTime -= delta;
                if (!(nextTime <= 0)) return true;
                nextTime = interval;
                OnDoAction();
                if (playCnt <= 0) return true;
                playCnt--;
                return playCnt != 0;
            }


            public float GetLastTime()
            {
                return nextTime;
            }
        }


        private class TimerData : TimerBase
        {
            private Action action;

            public TimerData(float startTime, float interval, int playCnt, Action action)
                : base(startTime, interval, playCnt)
            {
                this.action = action;
            }

            protected override void OnDoAction()
            {
                action();
            }

            public override Delegate GetCallBack()
            {
                return action;
            }
        }


        private class TimerData<T> : TimerBase
        {
            private Action<T> action;
            private T arg;

            public TimerData(float startTime, float interval, int playCnt, Action<T> action, T arg)
                : base(startTime, interval, playCnt)
            {
                this.action = action;
                this.arg = arg;
            }

            protected override void OnDoAction()
            {
                try
                {
                    action(arg);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }
            }

            public override Delegate GetCallBack()
            {
                return action;
            }
        }


        private class TimerData<T, U> : TimerBase
        {
            private Action<T, U> action;
            private T arg1;
            private U arg2;

            public TimerData(float startTime, float interval, int playCnt, Action<T, U> action, T arg1, U arg2)
                : base(startTime, interval, playCnt)
            {
                this.action = action;
                this.arg1 = arg1;
                this.arg2 = arg2;
            }

            protected override void OnDoAction()
            {
                action(arg1, arg2);
            }

            public override Delegate GetCallBack()
            {
                return action;
            }
        }


        private class TimerData<T, U, V> : TimerBase
        {
            private Action<T, U, V> action;
            private T arg1;
            private U arg2;
            private V arg3;

            public TimerData(float startTime, float interval, int playCnt, Action<T, U, V> action, T arg1, U arg2,
                V arg3)
                : base(startTime, interval, playCnt)
            {
                this.action = action;
                this.arg1 = arg1;
                this.arg2 = arg2;
                this.arg3 = arg3;
            }

            protected override void OnDoAction()
            {
                action(arg1, arg2, arg3);
            }

            public override Delegate GetCallBack()
            {
                return action;
            }
        }

        #endregion
    }
}