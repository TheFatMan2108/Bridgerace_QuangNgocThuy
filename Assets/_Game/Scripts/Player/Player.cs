using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : CharacterBase
{
    public CharacterController controller;
    public bool isWalk = true;
    #region State
    public IdlePlayerState idle {  get; private set; }
    public RunPlayerState run { get; private set; }
    #endregion
    #region Input
    PlayerController playerController;
    public InputAction move { get; private set; }
    #endregion
    #region Info
    public Vector3 directionMove { get; private set; } = Vector3.zero;
    public Vector3 oldDirectionMove { get; private set; } = Vector3.zero;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        OnInitState();
        controller = GetComponent<CharacterController>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        OnInitInput();
    }
    private void OnDisable()
    {
        move.Disable();
        move.performed -= OnMove;
    }
    private void OnInitInput()
    {
        move = playerController.Player.Move;
        move.Enable();
        move.performed += OnMove;
        move.canceled += OffMove;
    }

    private void OnInitState()
    {
        idle = new IdlePlayerState("Idle", animator, this, controllerState, controller, this);
        run = new RunPlayerState("Run", animator, this, controllerState, controller, this);
        playerController = new PlayerController();
    }

    protected override void Start()
    {
        base.Start();
        controllerState.InstallState(idle);
        isWalk = true;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        controllerState.curentState.FixUpdate();
    }

    protected override void Update()
    {
        CheckStairMovement();
        velocity = oldDirectionMove;
        base.Update();
        if (!isWalk) return;
        controllerState.curentState.Update();
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
        if (groundBricks.Count < 1&& oldDirectionMove.z>0&&stairBrick.color!=color)
        {
            controllerState.ChangeState(idle);
            controller.Move(Vector3.zero);
            isWalk = false;
            return;
        }
        isWalk = true;
        if (stairBrick == null|| groundBricks.Count < 1) return ;
        Brick brick = groundBricks[groundBricks.Count - 1];
        if(stairBrick.color==color)return;
        stairBrick.AddBrick(brick);
        RemoveBrick(brick);

    }
   
    private void OnMove(InputAction.CallbackContext callback)
    {
        Vector2 newVector = callback.ReadValue<Vector2>();
        directionMove = new Vector3(newVector.x,0,newVector.y);
        oldDirectionMove = new Vector3(newVector.x, 0, newVector.y);
    }
    private void OffMove(InputAction.CallbackContext callback)
    {
       
        directionMove = Vector3.zero;
    }
}
