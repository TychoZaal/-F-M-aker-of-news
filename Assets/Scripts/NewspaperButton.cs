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
    private GameObject articleOveriew;

    [SerializeField]
    public List<Button> buttonsToActivate, parentButtons;

    private void Start()
    {
        if (articleOveriew != null)
            cam.articleOverlayButtons.Add(articleOveriew);

        cam.allButtons.Add(GetComponent<Button>());
    }

    public void ButtonPressed()
    {
        cam.ResetButtons(buttonsToActivate);

        cam.ZoomIn(cameraZoomedPosition, focus);

        if (minigame != null)
            minigame.ActivateScene();

        if (articleOveriew != null)
            articleOveriew.SetActive(true);
    }
    
    public void ActivateParentButtons()
    {
        if (parentButton.parentButton == null)
        {
            return;
        }

        cam.ZoomIn(parentButton.parentButton.cameraZoomedPosition, parentButton.parentButton.focus);
        cam.ResetButtons(parentButtons);
    }
}
