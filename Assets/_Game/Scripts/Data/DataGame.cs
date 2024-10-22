using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataGame 
{
    public int Level;
    public string Name;
    public bool isOnMusic;

    public DataGame()
    {
        Level = 0;
        Name = string.Format("Player #{0}",3453453453534);
        isOnMusic = true;
    }
}
