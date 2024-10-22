using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateParrent : StateParentBase
{
    protected NPC NPC;
    protected Vector3 target;
    protected Rigidbody rb;

    public NPCStateParrent(string name, Animator animator, CharacterBase character, ControllerState controllerState,NPC npc,Rigidbody rb) : base(name, animator, character, controllerState)
    {
        NPC = npc;
        this.rb = rb;
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
        //rb.velocity = rb.velocity + new Vector3(0, -20f, 0);
    }
    public override void Exit()
    {
        base.Exit();
    }
}
