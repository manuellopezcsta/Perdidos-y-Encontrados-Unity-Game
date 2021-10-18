using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public Behaviour halo;
    public Vector3 startingPosition;
    public bool isGrabbed = false;

    void Start()
    {
        halo = (Behaviour)GetComponent("Halo");
        startingPosition = transform.position;

        SetObjectState(false);
    }

    public void SetObjectState(bool value)
    {
        halo.enabled = value;
        isGrabbed = value;
    }         
}