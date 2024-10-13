using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerState 
{
   public IState curentState;

    public void InstallState(IState state)
    {
        this.curentState = state;
        curentState.Start();
    }

    public void ChangeState(IState state)
    {
        if(this.curentState == state) return;
        curentState.Exit();
        curentState = state;
        curentState.Start();
    }
}
