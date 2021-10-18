using UnityEngine;

public class PlayerWithAxes : MonoBehaviour
{

    [SerializeField] public KeyCode interactKey;

    [SerializeField] float rotationSpeed = 0.5f;

    public bool grabObject = false;
    public bool isRunning = false; // Solo para visualizacion.

    [Range(0, 10)] [SerializeField] float speed = 0;

    CharacterController characterController;

    [SerializeField] float horizontalMovement;
    [SerializeField] float verticalMovement;
    private Vector3 playerInput;


    [SerializeField] int playerNumber = 1;

    [SerializeField] private float playerSpeed = 35;

    [SerializeField] Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    private Vector3 movePlayer;
    // Para la variable isRunning del animator.
    [SerializeField] Animator animator;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        GetKeyValues();
        MovePlayer();
        // Para setear el trigger de Grab.
        animator.SetBool("grabObject", GravingObject());
        // PARA QUE ANDE OBJECT SCRIPT LINEA 30.
    }


    public void SetGrabObejct(bool value)
    {
        grabObject = value;
    }

    private void MovePlayer()
    {
        CamDirection();
        // Que tenga en cuenta la camara y el input del jugador.
        movePlayer = playerInput.x * camRight + playerInput.z * camForward;
        
        // Para que gire la camara
        characterController.transform.LookAt(characterController.transform.position + movePlayer);
        // Lo movemos
        characterController.Move(movePlayer * playerSpeed * Time.deltaTime);
    }

    private void GetKeyValues()
    {
        if (playerNumber == 2)
        {
            horizontalMovement = Input.GetAxis("Horizontal");
            verticalMovement = Input.GetAxis("Vertical");
        }
        if (playerNumber == 1)
        {
            horizontalMovement = Input.GetAxis("Horizontal2");
            verticalMovement = Input.GetAxis("Vertical2");
        }

        // Para activar la animacion de movimiento
        if((horizontalMovement != 0 || verticalMovement != 0 )&& !isRunning)
        {
            animator.SetBool("isRunning", true);
            isRunning = true;
        }
        if(horizontalMovement == 0 && verticalMovement == 0)
        {
           animator.SetBool("isRunning", false);
            isRunning = false;
        }

        // Esto es para evitar que se mueva mas rapido en diagonal.
        playerInput = new Vector3(horizontalMovement, 0, verticalMovement);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

    }

    private void CamDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        // Fijamos las y en 0 de los vectores de la camara.
        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    // Para los triggers del animator
    private bool GravingObject()
    {
        return grabObject;
    }

}
