using UnityEngine;

public class AnimatorPlayerController : MonoBehaviour
{
    private Animator playerAnimator;
    private Player playerController;


    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerController = GetComponent<Player>();
    }

    void Update()
    {
        if (!GravingObject())
        {
            playerAnimator.SetBool("isRunning", IsPlayerMoving());
        }
        else
        {
            playerAnimator.SetBool("isRunning", false);
        }
        
        playerAnimator.SetBool("grabObject", GravingObject());
    }

    private bool GravingObject()
    {
        return playerController.grabObject;
    }

    private bool IsPlayerMoving()
    {
        return playerController.isRunning;
    }
}
