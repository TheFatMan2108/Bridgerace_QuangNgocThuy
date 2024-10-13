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
        material.enabled = true;
        color = brick.color;
        ActiveColor();
        brick.gameObject.SetActive(true);
    }

    public override void ClearBrick()
    {
        base.ClearBrick();
    }

    public override void RemoveBrick(Brick brick)
    {
        base.RemoveBrick(brick);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }

   


}
