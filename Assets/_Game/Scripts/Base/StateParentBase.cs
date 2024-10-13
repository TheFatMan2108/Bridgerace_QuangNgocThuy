using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateParentBase : IState
{
    protected string name;
    protected Animator animator;
    protected CharacterBase character;
    protected ControllerState controllerState;
    protected float stateCouter;

    public StateParentBase(string name, Animator animator, CharacterBase character, ControllerState controllerState)
    {
        this.name = name;
        this.animator = animator;
        this.character = character;
        this.controllerState = controllerState;
    }

    public virtual void Start()
    {
        animator.SetBool(name, true);
    }
    public virtual void FixUpdate()
    {

    }
    public virtual void Update()
    {
        stateCouter-=Time.deltaTime;
    }
    public virtual void Exit()
    {
        animator.SetBool(name, false);
    }

    
}
