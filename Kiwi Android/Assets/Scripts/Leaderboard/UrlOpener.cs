using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UrlOpener : MonoBehaviour
{
    public string Url;

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Open()
    {
        Application.OpenURL(Url);
    }
}
