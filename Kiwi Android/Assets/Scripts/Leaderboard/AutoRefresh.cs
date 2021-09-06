using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoRefresh : MonoBehaviour
{
    public string SceneName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(0.1f);
        //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Resources.UnloadUnusedAssets();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
