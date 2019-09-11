using System;
using System.Collections;
using UnityEngine;

namespace Bissash
{
    public class Timer
    {
        private float currentSeconds;
        private float secondsToRun;

        /// <summary>
        /// Constructor que inicia el timer en 0 segundos.
        /// </summary>
        public Timer()
        {
            currentSeconds = 0f;
        }

        /// <summary>
        /// El usuario establece el segundo inicial del timer y tambien establece cuantos segundos de timer.
        /// </summary>
        /// <param name="initialSecond"></param>
        /// <param name="secondsToRun"></param>
        public Timer(float initialSecond, float secondsToRun)
        {
            this.currentSeconds = initialSecond;
            this.secondsToRun = secondsToRun;
        }

        public void RunTimer()
        {
            currentSeconds += Time.deltaTime;
        }

        /// <summary>
        /// Retorna los segundos actuales en el temporizador.
        /// </summary>
        /// <returns></returns>
        public float ElapsedSeconds()
        {
            return currentSeconds;
        }

        public void SetGoalSeconds(float value)
        {
            secondsToRun = value;
        }

        /// <summary>
        /// Reinicia los segundos transcurridos en el timer.
        /// </summary>
        public void ResetTimer()
        {
            currentSeconds = 0f;
        }

        /// <summary>
        /// Retorna true si el tiempo transcurrido sobre pasa el limite de tiempo.
        /// </summary>
        /// <returns></returns>
        public bool TimeHasComplete()
        {
            return currentSeconds >= secondsToRun;
        }

        public void OnTimerEvent(Action action, float seconds, ResetMode resetMode = ResetMode.Automagic)
        {
            RunTimer();

            if(resetMode == ResetMode.Automagic)
            {
                if(currentSeconds >= seconds)
                {
                    action();
                    ResetTimer();
                }
            }
            else if(resetMode == ResetMode.Manual)
            {
                if(currentSeconds >= seconds)
                    action();
            }
        }

        public void OnTimerEvent<T>(Action<T> action, T optional = default, float seconds = 1f,
            ResetMode resetMode = ResetMode.Automagic)
        {
            RunTimer();
            if (resetMode == ResetMode.Automagic && currentSeconds >= seconds)
            {
                action(optional);
                ResetTimer();
            }
            else if (resetMode == ResetMode.Manual && currentSeconds >= seconds)
            {
                action(optional);
            }
        }
    }
}

