using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Patrol : NPCStateParrent
{
    public NPC_Patrol(string name, Animator animator, CharacterBase character, ControllerState controllerState, NPC npc, Rigidbody rb) : base(name, animator, character, controllerState, npc, rb)
    {
    }

    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
        if (NPC.agent.velocity.sqrMagnitude == 0) controllerState.ChangeState(NPC.idle);
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
