using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR;
using static UnityEngine.GraphicsBuffer;

public class NPC : CharacterBase
{
    public Vector3 target  = Vector3.zero;
    public Transform finisPoint;
    public NavMeshAgent agent;
    public float distanceCheckBrick = 10;
    #region State
    public NPC_Idle idle { get; private set; }
    public NPC_Patrol patrol { get; private set; }
    public NPC_Attack attack { get; private set; }
    protected Rigidbody rb;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        OnInit();
    }

    private void OnInit()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        idle = new NPC_Idle("Idle", animator, this, controllerState, this, rb);
        patrol = new NPC_Patrol("Run",animator,this,controllerState, this, rb);
        attack = new NPC_Attack("Run",animator, this,controllerState, this, rb);
    }

    protected override void Start()
    {
        base.Start();
        controllerState.InstallState(idle);
    }

    protected override void Update()
    {
        CheckStairMovement();
        velocity = agent.velocity;
        base.Update();
        controllerState.curentState.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        controllerState.curentState.FixUpdate();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
    }
    public override void AddBrick(Brick brick)
    {
        base.AddBrick(brick);
    }

    public override void ClearBrick()
    {
        base.ClearBrick();
    }

    public override void OnStop()
    {
        base.OnStop();
    }

    public override void OnWin()
    {
        base.OnWin();
    }

    public override void RemoveBrick(Brick brick)
    {
        base.RemoveBrick(brick);
    }

    public void CheckStairMovement()
    {
        Physics.Raycast(checkStair.transform.position, Vector3.down, out RaycastHit hit, checkStairDistance, lmStair);
        if (hit.transform == null) return;
        hit.transform.TryGetComponent(out StairBrick stairBrick);
        if (groundBricks.Count < 1 && agent.velocity.z > 0 && stairBrick.color != color)
        {
            controllerState.ChangeState(idle);
            agent.SetDestination(FindBrickNear());
            return;
        }
        if (stairBrick == null || groundBricks.Count < 1) return;
        Brick brick = groundBricks[groundBricks.Count - 1];
        if (stairBrick.color == color) return;
        stairBrick.AddBrick(brick);
        RemoveBrick(brick);

    }
    public Vector3 FindBrickNear()
    {
        Vector3 lastPoint = Vector3.zero;
        float minDistance = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        List<Brick> bricks = PoolBrickManager.instance.bricks;
        foreach (Brick brick in bricks)
        {
            if (!brick.gameObject.activeInHierarchy || brick.color != color) continue;
            float distance = Vector3.Distance(currentPos, brick.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                lastPoint = brick.transform.position;
            }
        }
        return lastPoint;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,distanceCheckBrick);
    }
}