using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlayer : PlayerStateParent
{
    public WinPlayer(string name, Animator animator, CharacterBase character, ControllerState controllerState, CharacterController controller, Player player) : base(name, animator, character, controllerState, controller, player)
    {
    }

   
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }
    public override void FixUpdate()
    {
        base.FixUpdate();
    }
    public override void Exit()
    {
        base.Exit();
        player.controller.enabled = true;
    }

}
