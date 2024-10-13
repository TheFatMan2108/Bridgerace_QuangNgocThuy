using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class CharacterBase : MonoBehaviour,IBricksable,IWinsable
{
    public List<Brick> groundBricks = new List<Brick>();
    public List<Brick> playerBricks = new List<Brick>();
    public GameObject brick;
    public Transform spawnPoint;
    public ColorType color;
    public float speed = 10f;
    public Transform child;
    public Renderer skinMesh;
    public int floorIndex = 0;
    public Transform checkStair;
    public float checkStairDistance = 1f;
    public LayerMask lmStair;
    public Vector3 velocity;
    protected Animator animator;
    protected MatColor matColors;
    protected ControllerState controllerState;
    protected float heightBrick;

    protected virtual void Awake()
    {
        OnInit();
    }

    private void OnInit()
    {
        animator = GetComponentInChildren<Animator>();
        controllerState = new ControllerState();
        matColors = Resources.Load<MatColor>("Colors/Colors1");
       
        
    }

    protected virtual void OnEnable()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        Material[] materials = skinMesh.materials;
        for (int i = 0; i < materials.Length; i++)
        {
        materials[i] = GetColor(color);
        }
        skinMesh.materials = materials;
    }

    protected virtual void Start()
    {
        child = transform.GetChild(0);
    }
    protected virtual void FixedUpdate()
    {
    }

    protected virtual void Update()
    {
     
    }
    public virtual void AddBrick(Brick brick)
    {
        groundBricks.Add(brick);
        heightBrick += 0.21f;
        if (groundBricks.Count > playerBricks.Count)
        {
            GameObject newBrick = Instantiate(this.brick, spawnPoint.transform.position + new Vector3(0, heightBrick, 0), Quaternion.identity,spawnPoint);
            newBrick.transform.localRotation = Quaternion.identity  ;
            newBrick.GetComponent<Collider>().enabled = false;
            newBrick.AddComponent<PlayerBrick>().AddBrick(brick);
            playerBricks.Add(newBrick.GetComponent<PlayerBrick>());
        }
        else
        {
            Brick bri = playerBricks[groundBricks.Count-1];
            bri.gameObject.SetActive(true);
        }
        
    }

    public virtual void ClearBrick()
    {
        
    }

    public virtual void RemoveBrick(Brick brick)
    {
        heightBrick -= 0.21f;
        groundBricks.Remove(brick);
        int index = groundBricks.Count;
        brick.gameObject.SetActive(true);
        GameObject nBrick = playerBricks[index].gameObject;
        nBrick.gameObject.SetActive(false);
    }

    public virtual void OnWin()
    {
       
    }

    public virtual void OnStop()
    {
       
    }
    public Material GetColor(ColorType colorType)
    {
        int index = (int)colorType;
        return matColors.listmat[index];
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawLine(checkStair.transform.position, checkStair.transform.position + new Vector3(0, checkStairDistance * -1));
    }
}
public enum ColorType
{
    Black=0,
    Green=1,
    Red=2,
    Yellow=3,
    none = 4
}
