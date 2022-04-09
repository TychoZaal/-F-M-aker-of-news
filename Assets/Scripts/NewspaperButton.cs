using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NewspaperButton : MonoBehaviour
{
    [SerializeField]
    private CameraMovement cam;

    [SerializeField]
    private Minigame minigame;

    [SerializeField]
    private Vector3 cameraZoomedPosition;

    [SerializeField]
    private NewspaperButton parentButton;

    [SerializeField]
    private CameraMovement.Focus focus;

    [SerializeField]
    public List<Image> buttonsToActivate, parentButtons;

    private void Start()
    {
        cam.allButtons.Add(GetComponent<Image>());
    }

    public void ButtonPressed()
    {
        cam.ResetButtons(buttonsToActivate);

        cam.ZoomIn(cameraZoomedPosition, focus);

        if (minigame != null)
            minigame.ActivateScene();
    }
    
    public void ActivateParentButtons()
    {
        if (parentButton == null)
        {
            return;
        }

        cam.ZoomIn(parentButton.cameraZoomedPosition, parentButton.focus);
        cam.ResetButtons(parentButtons);
    }
}
