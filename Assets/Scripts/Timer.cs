using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Timer : MonoBehaviour
{
    DateTime currentAction;
    long ticks;
    [SerializeField] TextMeshProUGUI actionTimer;

    [SerializeField] Vector3Int offsetTime;
    public bool isCooldownActive = false;

    public void Init()
    {
        actionTimer.text = "";
    }

    public void StartCooldown()
    {
        currentAction = DateTime.Now + new TimeSpan(offsetTime.x, offsetTime.y, offsetTime.z);
        ticks = currentAction.Ticks;
        actionTimer.text = GetTimeDifference(currentAction);
    }

    public void UpdateCooldown()
    {
        // get the ticks from the string
        // long ticks = long.Parse(cooldownString);

        // calculate the time remaining in the cooldown
        TimeSpan timeRemaining = new DateTime(ticks) - DateTime.Now;

        // check if there is time left in the cooldown
        if (timeRemaining.Ticks > 0)
        {
            // cooldown has time remaining
            int days = timeRemaining.Days;
            int hours = timeRemaining.Hours;
            int mins = timeRemaining.Minutes;
            int seconds = timeRemaining.Seconds;

            //
            // do something here to update game for this cooldown
            //
            actionTimer.text = GetTimeDifference(hours, mins, seconds, days);
        }
        else
        {
            // cooldown is done
            // StopCooldown(cooldownName);
            actionTimer.text = "Time Up!";
            isCooldownActive = true;
            //
            // do something here to update game for this cooldown
            //
        }
    }

    string GetTimeDifference(DateTime _)
    {
        return _.ToString("H:mm:ss");
    }

    string GetTimeDifference(int hours, int mins, int seconds, int day = 0)
    {
        DateTime temp = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hours, mins, seconds);
        return temp.ToString("H:mm:ss");
    }
}
