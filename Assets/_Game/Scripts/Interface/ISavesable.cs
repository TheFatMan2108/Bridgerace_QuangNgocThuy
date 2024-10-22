using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISavesable 
{
    void Save(ref DataGame data);
    void Load(DataGame data);
}
