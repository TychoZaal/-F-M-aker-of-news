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

    public void ButtonPressed()
    {
        cam.ZoomIn(cameraZoomedPosition, focus);

        if (minigame != null)
            minigame.ActivateScene();
    }
}
