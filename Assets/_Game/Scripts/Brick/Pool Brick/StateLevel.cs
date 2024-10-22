using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class StateLevel : MonoBehaviour, IWinsable
{
    public static StateLevel instance { get; private set; }
    public List<Brick> bricks = new List<Brick>();
    public List<Transform> startPoints = new List<Transform>();
    public Transform[] floors;
    public Transform[] topPositon;
    public NavMeshSurface meshSurface;
    public int width = 7;
    public int height = 20;
    public GameObject brick;
    public int floor = 0;
    bool isStart = false;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        SpawnBrick();
    }
    private void OnEnable()
    {

    }
    private void Start()
    {
        meshSurface.BuildNavMesh();
        GameManager.instance.AddWinsable(this);
        isStart = true;
    }
    private void Update()
    {

    }
    private void LateUpdate()
    {
        if (isStart) SetPositionCharacter();
        isStart = false;
    }
    private void SetPositionCharacter()
    {
        foreach (var character in GameManager.instance.characterBases)
        {
            int index = Random.Range(0, startPoints.Count);
            character.transform.position = startPoints[index].position;
            if (character is Player)
            {
                Player player = character as Player;
                character.controllerState.ChangeState(player.idle);
                player.controller.enabled = true;
            }
            else
            {
                NPC npc = character as NPC;
                    npc.agent.enabled = true;
                    npc.agent.isStopped = false;
                    npc.finisPoint = topPositon[0];
                    character.controllerState.ChangeState(npc.idle);
            }
            startPoints.RemoveAt(index);
        }
    }

    private void SpawnBrick()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject nBrick = Instantiate(brick, new Vector3(j, 0, i) + floors[floor].transform.position, Quaternion.identity, transform);
                nBrick.AddComponent<GroundBrick>();
                bricks.Add(nBrick.GetComponent<GroundBrick>());
            }
        }
    }
    public void SpawnBricksInFloorAnColor(int floor, ColorType color)
    {
        for (int i = 0; i < bricks.Count; i++)
        {
            if (bricks[i].color == color)
            {
                Brick brick = bricks[i];
                brick.transform.position = brick.transform.position - floors[floor - 1].position;
                brick.transform.position = brick.transform.position + floors[floor].position;

            }
        }

    }
    private void OnDestroy()
    {
        GameManager.instance.RemoveWinsable(this);
    }
    public void OnWin()
    {
    }
    public void OnStop()
    {
        Destroy(gameObject);
    }

}
