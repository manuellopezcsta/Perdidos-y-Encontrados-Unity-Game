using UnityEngine;

public class Player : MonoBehaviour
{   
    [SerializeField] KeyCode keyRight;
    [SerializeField] KeyCode keyLeft;
    [SerializeField] KeyCode keyUp;
    [SerializeField] KeyCode keyDown;

    [SerializeField] public KeyCode interactKey;

    //[SerializeField] float rotationSpeed = 0.5f;

    public bool grabObject = false;
    public bool isRunning = false;

    private float xValue = 0f;
    private float zValue = 0f;

    [Range(0, 10)]
    [SerializeField] float speed = 0;

    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void Update()
    {
        this.MovePlayer();
    }

    public void SetGrabObejct(bool value)
    {
        grabObject = value;
    }

    private void MovePlayer()
    {
        GetKeyValue();
        
        Vector3 moveValues = new Vector3(xValue * speed, 0, zValue * speed);

        characterController.Move(moveValues);

        RotatePlayer();
    }

    private void RotatePlayer()
    {
        Vector3 movement = new Vector3(xValue, 0.0f, zValue);

        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }

    }

    void GetKeyValue()
    {
        if (Input.GetKeyDown(keyUp))
        {
            this.isRunning = true;
            zValue = 0.05f;
        }

        if (Input.GetKeyUp(keyUp))
        {
            this.isRunning = false;
            zValue = 0f;
        }

        if (Input.GetKeyDown(keyDown))
        {
            this.isRunning = true;
            zValue = -0.05f;
        }

        if (Input.GetKeyUp(keyDown))
        {
            this.isRunning = false;
            zValue = 0f;
        }

        if (Input.GetKeyDown(keyRight))
        {
            this.isRunning = true;
            xValue = 0.05f;
        }

        if (Input.GetKeyUp(keyRight))
        {
            this.isRunning = false;
            xValue = 0f;
        }

        if (Input.GetKeyDown(keyLeft))
        {
            this.isRunning = true;
            xValue = -0.05f;
        }

        if (Input.GetKeyUp(keyLeft))
        {
            this.isRunning = false;
            xValue = 0f;
        }
        Vector3 movement = new Vector3(xValue, 0.0f, zValue);
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }
}
