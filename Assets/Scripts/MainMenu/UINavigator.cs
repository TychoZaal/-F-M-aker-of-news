using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINavigator : MonoBehaviour
{
    [SerializeField] ArticlePage pageInformation = null;
    [SerializeField] ArticleContentCreator articleContentCreator = null;

    public void OpenArticle()
    {
        Debug.Log("Open Article");
        articleContentCreator.updateContent(pageInformation);
    }
}
