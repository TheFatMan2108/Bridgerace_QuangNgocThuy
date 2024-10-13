using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateParent : StateParentBase
{
    protected CharacterController controller;
    protected Player player;
    protected Vector3 directionMove,oldDirection;
    Vector3 velocity;
    public PlayerStateParent(string name, Animator animator, CharacterBase character, ControllerState controllerState,CharacterController controller, Player player) : base(name, animator, character, controllerState)
    {
        this.controller = controller;
        this.player = player;
    }

    public override void Start()
    {
        base.Start();
        velocity = Vector3.zero;
    }

    public override void Update()
    {
        base.Update();
        directionMove = player.directionMove;
        oldDirection = player.oldDirectionMove;
        if(oldDirection.sqrMagnitude != 0 )
        {
            player.child.localRotation = Quaternion.LookRotation(oldDirection);
        }
        velocity = directionMove.normalized * player.speed;
        velocity = velocity + new Vector3(0, -20f, 0);
        player.controller.Move(velocity * Time.deltaTime);
    }
    public override void FixUpdate()
    {
        base.FixUpdate();
    }
    public override void Exit()
    {
        base.Exit();
       // rb.velocity =new Vector3(0,-5,0);
    }

}
