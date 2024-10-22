using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class CharacterBase : MonoBehaviour,IBricksable,IWinsable
{
    public List<Brick> groundBricks = new List<Brick>();
    public List<Brick> playerBricks = new List<Brick>();
    public int score { get; set; }
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
    public ControllerState controllerState;
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
        GameManager.instance.AddCharacter(this);
        GameManager.instance.AddWinsable(this);
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
        score = groundBricks.Count;
    }

    public virtual void ClearBrick()
    {
        for(int i=0;i<groundBricks.Count;i++)
        {
            heightBrick -= 0.21f;
            GameObject nBrick = playerBricks[i].gameObject;
            nBrick.gameObject.SetActive(false);
        }
        groundBricks.Clear();
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
        ClearBrick();
    }

    public virtual void OnStop()
    {
        score = 0;
        floorIndex = 0;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(CacheString.TagFinished))
        {
            GameManager.instance.Winner();
            List<CharacterBase> list = new List<CharacterBase>();
            list.AddRange(GameManager.instance.characterBases);
            transform.position = StateLevel.instance.topPositon[0].position;
            list.Remove(this);
            list.Sort((a, b) => b.score.CompareTo(a.score));
            for (int i = 1; i < StateLevel.instance.topPositon.Length; i++)
            {
                if (list.Count <= i) break;
                list[i-1].transform.position = StateLevel.instance.topPositon[i].position;
            }
        }
       
    }
    protected virtual void OnDisable()
    {
    }
    private void OnDestroy()
    {
        GameManager.instance.RemoveWinsable(this);
        GameManager.instance.RemoveCharacter(this);
    }
    public int GetScore() => score;
}
public enum ColorType
{
    Black=0,
    Green=1,
    Red=2,
    Yellow=3,
    none = 4
}
