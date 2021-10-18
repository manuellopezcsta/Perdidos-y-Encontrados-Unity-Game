using UnityEngine;

public class GrabAndLeaveObjects : MonoBehaviour
{
    public Transform objectHolder;

    public bool carryObject;

    private GameObject other;

    private PlayerWithAxes playerController;

    public string ACTIVE_TAG = "objeto_activo";

    private bool keyDown = false;
    private bool keyUp = false;

    public int OffsetGrab; // Variable para mover el vector de pos de donde va el objeto.

    void Start()
    {
        playerController = GetComponentInParent<PlayerWithAxes>();
    }

    void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        if (Input.GetKeyDown(playerController.interactKey))
        {
            keyDown = true;
        }

        if (Input.GetKeyUp(playerController.interactKey))
        {
            keyUp = true;
        }
    }

    private void ClearInputs()
    {
        keyDown = false;
        keyUp = false;
    }

    private void FixedUpdate()
    {
        CarryObjectLogic();
        ClearInputs();
    }

    private void CarryObjectLogic()
    {
        if (keyDown && !carryObject)
        {
            RaycastHit hit;
            Ray directionRay = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(directionRay, out hit, 10f))
            {
                if (hit.collider.tag.Equals(ACTIVE_TAG))
                {
                    carryObject = true;
                    playerController.SetGrabObejct(true);

                    Vector3 offset = new Vector3(1, 0, 0) * OffsetGrab;

                    if (carryObject)
                    {
                        other = hit.collider.gameObject;
                        other.transform.SetParent(objectHolder);
                        other.gameObject.transform.position = objectHolder.position + offset;
                        other.GetComponent<Rigidbody>().isKinematic = true;
                        other.GetComponent<Rigidbody>().useGravity = false;
                        other.GetComponent<ObjectScript>().SetObjectState(true);
                    }
                }
            }
        }


        if (keyUp && carryObject)
        {
            if (other != null)
            {
                carryObject = false;
                other.GetComponent<Rigidbody>().isKinematic = false;
                other.GetComponent<Rigidbody>().useGravity = true;
                other.GetComponent<Rigidbody>().AddForce(Vector3.down);
                other.GetComponent<ObjectScript>().SetObjectState(false);
            }

            objectHolder.DetachChildren();
            playerController.SetGrabObejct(false);
        }
    }
}
