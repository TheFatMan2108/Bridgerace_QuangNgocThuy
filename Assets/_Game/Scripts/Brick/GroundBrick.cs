using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBrick : Brick
{
    public int index = 0;

    protected override void Awake()
    {
        base.Awake();
        color = RandomColor();
        ActiveColor();
        
    }
    public override void AddBrick(Brick brick)
    {
        base.AddBrick(brick);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out CharacterBase player))
        {
            if (player == null) return;
            if(player.color != color)return;
            player.AddBrick(this);
            AddBrick(this);
        }
    }
}
