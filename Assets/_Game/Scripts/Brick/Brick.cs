using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Brick : MonoBehaviour,IBricksable,IChangeColor
{
   public ColorType color;
   public MeshRenderer material;
   protected MatColor matColors;
  

    protected virtual void Awake()
    {
        material = GetComponent<MeshRenderer>();
        matColors = Resources.Load<MatColor>("Colors/Colors1");
    }

    protected ColorType RandomColor()
    {
        int index = UnityEngine.Random.Range(0,4);
        switch (index)
        {
            case 0:return ColorType.Black;
            case 1:return ColorType.Green;
            case 2:return ColorType.Red;
            case 3:return ColorType.Yellow;
            default:return ColorType.Black;
        }
    }

    protected virtual void OnEnable()
    {
        
    }

    public void ActiveColor()
    {
        Material[] materials = material.materials;
        materials[0] = ChangeColor(color);
        material.materials = materials;
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    public virtual void AddBrick(Brick brick)
    {
       
    }

    public virtual void ClearBrick()
    {
        
    }

    public virtual void RemoveBrick(Brick brick)
    {
        gameObject.SetActive(true);
    }

    public Material ChangeColor(ColorType colorType)
    {
        int index = (int)colorType;
        return matColors.listmat[index];
    }

    
}
