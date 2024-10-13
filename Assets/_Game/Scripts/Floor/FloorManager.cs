using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out CharacterBase character))
        {
            character.floorIndex++;
            PoolBrickManager.instance.SpawnBricksInFloorAnColor(character.floorIndex,character.color);
        }
    }
    
}
