using System.Collections.Generic;
using UnityEngine;

/*Simple progression tracker that any script can reference*/
public class ProgressionTracker
{
    public enum Objectives
    {
        EnterHallway2F,
        GreyLockedDoorDiscovered2F,
        BathroomScareEvent2F,
        GreyKeyAcquired2F,
        GreyLockedDoorUnlocked2F,
        //Create more as necessary...
    }

    public static Dictionary<Objectives, bool> TaskCompletion = new()
    {
        { Objectives.EnterHallway2F, false },
        { Objectives.GreyLockedDoorDiscovered2F, false },
        { Objectives.BathroomScareEvent2F, false },
        { Objectives.GreyKeyAcquired2F, false },
        { Objectives.GreyLockedDoorUnlocked2F, false }
        //Create more as necessary...
    };

    public void UpdateObjective(Objectives objective)
    {
        TaskCompletion[objective] = true;
    }
    
}


