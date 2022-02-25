using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    

    public void SetMiniGameCompleted(ArticlePage articlePage)
    {
        articlePage.isCompleted = true;
    }

    public void ReturnToMain()
    {

    }
}
