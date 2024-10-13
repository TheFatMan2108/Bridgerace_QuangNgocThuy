using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolBrickManager : MonoBehaviour
{
   public static PoolBrickManager instance {  get; private set; }
    public List<Brick> bricks = new List<Brick>();
    public GameObject brick;
    public Transform[] floors;
    public int floor = 0;
    private void Awake()
    {
        if (instance == null)instance = this;
        else Destroy(gameObject);
    }
    void Start()
    {
       for(int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 20; j++)
            {
                //GameObject nBrick =  Instantiate(brick,new Vector3(j, 0, i)+ floors[floor].transform.position, Quaternion.identity);
                GameObject nBrick =  Instantiate(brick,new Vector3(j, 0, i) + floors[floor].transform.position, Quaternion.identity, transform);
                nBrick.AddComponent<GroundBrick>();
              bricks.Add(nBrick.GetComponent<GroundBrick>());
            }
        } 
    }

   public void SpawnBricksInFloorAnColor(int floor,ColorType color)
    {
        for(int i=0; i < bricks.Count; i++)
        {
            if (bricks[i].color == color)
            {
                Brick brick = bricks[i];
                brick.transform.position = brick.transform.position - floors[floor - 1].position;
                brick.transform.position = brick.transform.position + floors[floor].position;

            }
        }
       
    }
}
