using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    [SerializeField] private int owner = 1;
    
    [SerializeField] private GameManager gameManager;

    ObjectScript objectScript;

    private AudioSource audioSource;
    
    [SerializeField] AudioClip incorrectSound;
    [SerializeField] AudioClip correctSound;

    public string ACTIVE_TAG = "objeto_activo";
    public string PLAYER_TAG = "Player";

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals(PLAYER_TAG))
        {
            return;
        }

        if (other.tag.Equals(ACTIVE_TAG))
        {
            if (!other.gameObject.GetComponent<ObjectScript>().isGrabbed)
            {
                PlaySound(correctSound);

                Destroy(other.gameObject);

                gameManager.ScorePoint(owner);

                return;
            }

            return;
        }
        
        if (!other.tag.Equals(ACTIVE_TAG))
        {
            objectScript = other.GetComponent<ObjectScript>();
            PlaySound(incorrectSound);
            other.transform.position = objectScript.startingPosition;
            return;
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource.isPlaying)
        {
            return;
        }
        audioSource.clip = clip;
        audioSource.Play();
    }
}
