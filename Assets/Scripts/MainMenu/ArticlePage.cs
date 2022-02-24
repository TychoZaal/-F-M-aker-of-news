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
}
