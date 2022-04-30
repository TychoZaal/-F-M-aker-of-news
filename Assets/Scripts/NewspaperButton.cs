using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NewspaperButton : MonoBehaviour
{
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

    [SerializeField]
    private Page page;

    [SerializeField]
    private Transform cameraZoomPosition;

    private void Awake()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    private void Start()
    {
        cam.allButtons.Add(GetComponent<Image>());
    }

    private void Update()
    {
        float pageYPosition = 0;
        if (page != null) pageYPosition = page.transform.position.y;

        if (cameraZoomPosition != null)
            cameraZoomedPosition = cameraZoomPosition.position;
    }

    public void ButtonPressed()
    {
        if (cam.isMoving) return;

        cam.ResetButtons(buttonsToActivate);

        for (int i = 0; i < buttonsToActivate.Count; i++)
        {
            Debug.LogError(buttonsToActivate[i].gameObject.name);
        }

        cam.ZoomIn(cameraZoomedPosition, focus);

        if (minigame != null)
        {
            minigame.ActivateScene();
        }
    }
    
    public void ActivateParentButtons()
    {
        if (parentButton == null)
        {
            Debug.LogError("No parent");
            return;
        }

        Debug.LogError("Current: " + gameObject.name);
        cam.ZoomIn(parentButton.parentButton.cameraZoomedPosition, parentButton.parentButton.focus);
        cam.ResetButtons(parentButtons);
        Debug.LogError("Parent: " + parentButton.parentButton.gameObject.name);
    }
}
