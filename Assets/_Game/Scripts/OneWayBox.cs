using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class OneWayBox : MonoBehaviour
{
    [Tooltip("The direction that the other object should be coming from for entry.")]
    [SerializeField]
    private Vector3 entryDirection = Vector3.forward;
    [Tooltip("Should the entry direction be used as a local direction?")]
    [SerializeField]
    private bool localDirection = false;
    [Tooltip("How large should the trigger be in comparison to the original collider?")]
    [SerializeField]
    private Vector3 triggerScale = Vector3.one * 1.25f;
    [Tooltip("The collision will activate only when the penetration depth of the intruder is smaller than this threshold.")]
    [SerializeField]
    private float penetrationDepthThreshold = 0.2f;

    [Tooltip("The original collider. Will always be present thanks to the RequireComponent attribute.")]
    private new BoxCollider collider = null;
    [Tooltip("The trigger that we add ourselves once the game starts up.")]
    private BoxCollider collisionCheckTrigger = null;

    /// <summary> The entry direction, calculated accordingly based on whether it is a local direction or not. 
    /// This is pretty much what I've done in the video when copy-pasting the local direction check, but written as a property. </summary>
    public Vector3 PassthroughDirection => localDirection ? transform.TransformDirection(entryDirection.normalized) : entryDirection.normalized;

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

    private void OnValidate()
    {
        collider = GetComponent<BoxCollider>();
        collider.isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        TryIgnoreCollision(other);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.TransformPoint(collider.center), PassthroughDirection * 2);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.TransformPoint(collider.center), -PassthroughDirection * 2);
    }
}
