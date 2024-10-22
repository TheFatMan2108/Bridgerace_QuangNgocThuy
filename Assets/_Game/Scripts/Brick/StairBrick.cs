using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairBrick : Brick
{
    protected override void Awake()
    {
        base.Awake();
        material.enabled = false;

    }
    public override void AddBrick(Brick brick)
    {
        material.enabled = true;
        color = brick.color;
        ActiveColor();
        brick.gameObject.SetActive(true);
    }

}
