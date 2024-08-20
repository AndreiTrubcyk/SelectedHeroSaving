using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour
{
    private const string GAME_SCENE = "GameScene";

    public void LoadScene()
    {
        SceneManager.LoadScene(GAME_SCENE);
    }
}
