using UnityEngine;
using System.Collections;

public static class Events
{
    public static System.Action ResetApp = delegate { };

    public static System.Action<string, string, bool> PlaySound = delegate { };
    public static System.Action<string, float> ChangeVolume = delegate { };
    public static System.Action<string, float, float> FadeVolume = delegate { };
    public static System.Action<string, float, float, float> FadeVolumeFromTo = delegate { };
}
   
