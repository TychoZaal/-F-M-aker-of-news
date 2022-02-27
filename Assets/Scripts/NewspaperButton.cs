using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperButton : MonoBehaviour
{
    [SerializeField]
    private CameraMovement cam;

    [SerializeField]
    private Minigame minigame;

    [SerializeField]
    private Vector3 cameraZoomedPosition;

    [SerializeField]
    private CameraMovement.Focus focus;

    [SerializeField]
    private GameObject articleOveriew;

    private void Start()
    {
        if (articleOveriew != null)
            cam.articleOverlayButtons.Add(articleOveriew);
    }

    public void ButtonPressed()
    {
        Debug.LogError("Button");

        cam.ZoomIn(cameraZoomedPosition, focus);

        if (minigame != null)
            minigame.ActivateScene();

        if (articleOveriew != null)
            articleOveriew.SetActive(true);
    }
}
