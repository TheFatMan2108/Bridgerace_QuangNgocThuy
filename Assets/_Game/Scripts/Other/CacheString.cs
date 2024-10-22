using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CacheString : MonoBehaviour
{
    public static string TagFinished { get; private set; } = "Finish";
    public static string StateIlde { get; private set; } = "Idle";
    public static string StateRun { get; private set; } = "Run";
    public static string StateWin { get; private set; } = "Win";
}
