using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdlePlayerState : PlayerStateParent
{
    public IdlePlayerState(string name, Animator animator, CharacterBase character, ControllerState controllerState, CharacterController controller, Player player) : base(name, animator, character, controllerState, controller, player)
    {
    }

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        if (directionMove.sqrMagnitude != 0) controllerState.ChangeState(player.run);
    }
    public override void FixUpdate()
    {
        base.FixUpdate();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
