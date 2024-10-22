using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class OneWayBox : MonoBehaviour
{
    [SerializeField]
    private Vector3 triggerScale = Vector3.one * 1.25f;
    private new BoxCollider collider = null;
    private BoxCollider collisionCheckTrigger = null;

    private void Awake()
    {
        collider = GetComponent<BoxCollider>();
        // Adding the BoxCollider and making sure that its sizes match the ones
        // of the OG collider.
        collisionCheckTrigger = gameObject.AddComponent<BoxCollider>();
        collisionCheckTrigger.size = triggerScale;
        collisionCheckTrigger.center = collider.center+ new Vector3(0,0, -0.3f);
        collisionCheckTrigger.isTrigger = true;
    }
    private void Start()
    {
        collider.enabled = true;
    }
    private void OnValidate()
    {
        collider = GetComponent<BoxCollider>();
        collider.isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        TryIgnoreCollision(other);
    }
    private void OnTriggerExit(Collider other)
    {
        Physics.IgnoreCollision(collider, other, false);
    }
    public void TryIgnoreCollision(Collider other)
    {
        if (other.GetComponent<CharacterBase>().velocity.z<0)
        {
                Physics.IgnoreCollision(collider, other, false);
        }
        else
        {
            Physics.IgnoreCollision(collider, other, true);
        }
    }
}
