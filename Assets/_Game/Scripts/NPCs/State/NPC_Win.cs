using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Win : NPCStateParrent
{
    public NPC_Win(string name, Animator animator, CharacterBase character, ControllerState controllerState, NPC npc, Rigidbody rb) : base(name, animator, character, controllerState, npc, rb)
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
        NPC.agent.enabled = true;
        NPC.agent.isStopped = false;
    }
}
