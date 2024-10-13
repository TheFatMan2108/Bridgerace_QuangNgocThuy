using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBricksable 
{
    void AddBrick(Brick brick);
    void RemoveBrick(Brick brick);
    void ClearBrick();
}
