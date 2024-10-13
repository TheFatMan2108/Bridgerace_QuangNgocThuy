using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrick : Brick
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void AddBrick(Brick brick)
    {
        base.AddBrick(brick);
        material.enabled = true;
        color = brick.color;
        ActiveColor();
    }
}
