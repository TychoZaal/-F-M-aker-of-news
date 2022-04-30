using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 overViewPosition;

    public enum Focus { Overview, Article};
    [SerializeField]
    private Focus focus = Focus.Overview;

    [SerializeField]
    private float movementSpeed = 1.0f;

    public bool isMoving = false;

    [SerializeField]
    private List<Minigame> minigames;

    public List<Image> allButtons = new List<Image>();

    public static CameraMovement instance;

    [SerializeField]
    float ZoomAmount = 0; //With Positive and negative values
    [SerializeField]
    float MaxToClamp = 10;
    [SerializeField]
    float rotSpeed = 10;
    [SerializeField]
    private float yMin, yMax;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        // transform.position = overViewPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ZoomOut();

        if (focus == Focus.Overview) Scroll();
    }

    public void ResetButtons(List<Image> exceptions)
    {
        for (int i = 0; i < allButtons.Count; i++)
        {
            allButtons[i].raycastTarget = false;
            allButtons[i].GetComponent<Button>().interactable = false;
        }

        for (int i = 0; i < exceptions.Count; i++)
        {
            exceptions[i].raycastTarget = true;
            exceptions[i].GetComponent<Button>().interactable = true;
        }
    }

    private void Scroll()
    {
        ZoomAmount += Input.GetAxis("Mouse ScrollWheel");
        ZoomAmount = Mathf.Clamp(ZoomAmount, -MaxToClamp, MaxToClamp);
        var translate = Mathf.Min(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")), MaxToClamp - Mathf.Abs(ZoomAmount));

        Vector3 newPos = gameObject.transform.position + new Vector3(0, translate * rotSpeed * Mathf.Sign(Input.GetAxis("Mouse ScrollWheel")), 0);

        if (newPos.y <= yMin) return;
        if (newPos.y >= yMax) return;

        transform.position = newPos;

        overViewPosition = transform.position;
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            isMoving = true;
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
        isMoving = false;
    }

    public void ZoomIn(Vector3 location, Focus focus)
    {
        if (isMoving) return;

        // ClearLog();

        StartCoroutine(MoveToPosition(transform, location, movementSpeed));
        this.focus = focus;
    }

    public void ClearLog()
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }

    public void ZoomOut()
    {
        // ClearLog();

        if (isMoving) return;

        for (int i = 0; i < minigames.Count; i++)
        {
            if (minigames[i] != null)
                minigames[i].DeactivateScene();
        }

        for (int i = 0; i < allButtons.Count; i++)
        {
            if (allButtons[i].raycastTarget)
            {
                allButtons[i].GetComponent<NewspaperButton>().ActivateParentButtons();
                break;
            }
        }
    }

    public IEnumerator ResetCamera(float timer)
    {
        yield return new WaitForSeconds(timer);

        ZoomOut();
    }
}
