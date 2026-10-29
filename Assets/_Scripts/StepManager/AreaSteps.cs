using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AreaSteps
{
    public string areaName;              // e.g., "Kitchen", "Bedroom", "Living Room"
    public List<Step> steps;             // Sequence of steps for this area
}