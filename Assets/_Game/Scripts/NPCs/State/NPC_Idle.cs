using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Idle : NPCStateParrent
{
    float curentBrickCount=0;
    float radomBrickCount= Random.Range(3, 11);
    public NPC_Idle(string name, Animator animator, CharacterBase character, ControllerState controllerState, NPC npc, Rigidbody rb) : base(name, animator, character, controllerState, npc, rb)
    {
    }

    public override void Start()
    {
        base.Start();
        curentBrickCount = NPC.groundBricks.Count;
        if(curentBrickCount>=radomBrickCount)
        {
            radomBrickCount = Random.Range(3,11);
            NPC.agent.SetDestination(NPC.finisPoint.position);
            return;
        }
        NPC.agent.SetDestination(NPC.FindBrickNear());

    }
    public override void Update()
    {
        base.Update();
        if (NPC.agent.velocity.sqrMagnitude>0)controllerState.ChangeState(NPC.patrol);
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
