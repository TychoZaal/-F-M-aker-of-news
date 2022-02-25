using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ArticlePage", order = 1)]
public class ArticlePage : ScriptableObject
{
    [SerializeField] public string title = "HEADLINE";
    [SerializeField] [TextArea] public string articleText = "";

    [SerializeField] public Sprite articleImage = null;

    [SerializeField] public string minigameScene = "sceneName";

    //Dit moet je handmatig via de inspector weer op false zetten als je opnieuw wil spelen. Heb dit in scriptable object gedaan zodat niet elke keer als je de game start je alle minigames weer opnieuw moet doen
    [SerializeField] public bool isCompleted = false;
}
