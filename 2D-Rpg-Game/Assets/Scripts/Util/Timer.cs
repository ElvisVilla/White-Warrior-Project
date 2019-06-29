using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bissash.Util
{
    public enum TimeMode { LessEqualsMode, LessThanMove }

    public class Timer
    {
        private float currentSeconds;
        private float secondsToRun;
        public bool ItsTime { get { return currentSeconds > secondsToRun; } }

        public Timer(float timeToRun)
        {
            secondsToRun = timeToRun;
        }

        public Timer()
        {
            currentSeconds = 0f;
        }

        public void RunTimer()
        {
            currentSeconds += Time.smoothDeltaTime;
        }

        public float ElapsedSeconds()
        {
            return currentSeconds;
        }

        public void ResetTimer()
        {
            currentSeconds = 0f;
        }

        public void OnTimerEvent<T>(Action<T> action, T optional, float seconds, TimeMode timeMode)
        {
            currentSeconds += Time.deltaTime;
            switch(timeMode)
            {
                case TimeMode.LessEqualsMode:
                    if (currentSeconds >= seconds)
                    {
                        ResetTimer();
                        action(optional);
                    } break;
                case TimeMode.LessThanMove:
                    if (currentSeconds > seconds)
                    {
                        ResetTimer();
                        action(optional);
                    }
                    break;
            }
        }

        public bool IsTimeCompleted(float seconds, TimeMode timeMode)
        {
            currentSeconds += Time.deltaTime;
            bool itsTime = false;

            void SetValues(out float ptimer, out bool pistime)
            {
                ptimer = 0f;
                pistime = true;
            }

            switch (timeMode)
            {
                case TimeMode.LessEqualsMode:
                    if (currentSeconds >= seconds)
                        SetValues(out currentSeconds, out itsTime);
                    break;
                case TimeMode.LessThanMove:
                    if (currentSeconds > seconds)
                        SetValues(out currentSeconds, out itsTime);
                    break;
            }
            return itsTime;
        }
    }
}

