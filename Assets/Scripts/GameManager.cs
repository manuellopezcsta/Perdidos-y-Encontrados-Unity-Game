using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject tableroImagen;
    [SerializeField] Material[] arrayImagenes;
    private MeshRenderer meshRenderer;

    [SerializeField] private int p1Score = 0;
    [SerializeField] private int p2Score = 0;

    [SerializeField] private Transform p1StartingPos;
    [SerializeField] private Transform p2StartingPos;
    [SerializeField] private Transform p1Transform;
    [SerializeField] private Transform p2Transform;

    private string previousTag;
    GameObject[] objetosSobrantes;

    [SerializeField] private int scoreToWin = 5;

    [SerializeField] TextMeshPro textoP1;
    [SerializeField] TextMeshPro textoP2;


    [SerializeField] List<string> tags = new List<string>();
    public string ACTIVE_TAG = "objeto_activo";
    private string selectedTag = "";

    private void Start()
    {
        meshRenderer = tableroImagen.GetComponent<MeshRenderer>();

        ObjectToFind();
    }

    void ImagenABuscar(int imageNumber)
    {
        meshRenderer.material = arrayImagenes[imageNumber];
    }

    public void ScorePoint(int playerNumber)
    {
        if (playerNumber == 1) p1Score++;

        if (playerNumber == 2) p2Score++;
        
        UpdateScoreBoard();
        CheckforWinCondition();

        p1Transform.position = p1StartingPos.position;
        p2Transform.position = p2StartingPos.position;

        ClearAllTags();

        ObjectToFind();
    }

    private void ObjectToFind()
    {
        int index = Random.Range(0, tags.Count);

        selectedTag = tags[index];

        if (!string.IsNullOrEmpty(selectedTag))
        {
            var objectsToFind = GameObject.FindGameObjectsWithTag(selectedTag);

            if (objectsToFind != null && objectsToFind.Length > 0)
            {
                foreach (var obj in objectsToFind)
                {
                    obj.tag = ACTIVE_TAG;
                }
            }
        }

        switch(selectedTag)
        {
            case "objeto_banana":
                ImagenABuscar(1);
                break;
            case "objeto_chocolate":
                ImagenABuscar(3);
                break;
            case "objeto_fantasma":
                ImagenABuscar(2);
                break;
            case "objeto_paraguas":
                ImagenABuscar(0);
                break;
        }
    }

    private void ClearAllTags()
    {
        var remainingObjects = GameObject.FindGameObjectsWithTag(selectedTag);

        if (remainingObjects != null && remainingObjects.Length > 0)
        {
            foreach (var remaining in remainingObjects)
            {
                remaining.tag = selectedTag;
            }
        }

        selectedTag = string.Empty;
    }

    private void CheckforWinCondition()
    {
        if(p1Score == scoreToWin)
        {
            SceneManager.LoadScene("p1winscreen");
            return;
        }
        
        if(p2Score == scoreToWin)
        {
            SceneManager.LoadScene("p2winscreen");
            return;
        }
    }

    private void UpdateScoreBoard()
    {
        textoP1.text = "P1: " + p1Score;
        textoP2.text = "P2: " + p2Score;
    }
}
