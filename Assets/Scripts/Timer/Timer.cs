using TMPro;
using UnityEngine;

namespace AldhaDev.Timer
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI actionTimer;
        public float totalTime = 60.0f; // Tiempo total en segundos

        [SerializeField] private Color normal;
        [SerializeField] private Color hurryUp;

        public float duration = 10f;  // Default duration
        public bool isRunning;

        private bool _isHurry;

        private bool _hasFinished;

        public bool hasFinished => _hasFinished;

        public void Init()
        {
            actionTimer.text = "";
            actionTimer.color = normal;
        }

        private void UpdateTimerText()
        {
            int minutes = Mathf.FloorToInt(duration / 60);
            int seconds = Mathf.FloorToInt(duration % 60);
            string timeLiteral = $"{minutes:00}:{seconds:00}";
            actionTimer.text = timeLiteral;
        }
    
        // public void StartTimer(float waitTime = 0f, Action action = null)
        public void StartTimer()
        {
            duration = totalTime;
            // actionToExecute = action;
            isRunning = true;
            UpdateTimerText();
        }

        public void StopTimer() => isRunning = false;

        public void TogglePause(bool isTimerRunning) => this.isRunning = isTimerRunning;

        public void ResetTimer()
        {
            StopTimer();
            duration = totalTime;
        }

        public void UpdateTimer()
        {
            if (!isRunning) return;
            if (duration < 0)
            {
                _hasFinished = true;
                actionTimer.text = $"<b> Time Up! </b>";
                isRunning = false;
            }
            else
            {
                if (duration <= (totalTime / 3) && !_isHurry)
                {
                    actionTimer.color = hurryUp;
                    _isHurry = true;
                }
                UpdateTimerText();
            }
            duration -= Time.deltaTime;

            // if (actionToExecute != null)
            // {
            //     actionToExecute();
            // }
        }
    }
}
