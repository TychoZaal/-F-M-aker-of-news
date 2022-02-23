using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewspaperButton : MonoBehaviour
{
    [SerializeField]
    private CameraMovement cam;

    [SerializeField]
    private Vector3 cameraZoomedPosition;

    [SerializeField]
    private CameraMovement.Focus focus;

    public void ButtonPressed()
    {
        Debug.LogError("Button Pressed");

        cam.ZoomIn(cameraZoomedPosition, focus);
    }
}
