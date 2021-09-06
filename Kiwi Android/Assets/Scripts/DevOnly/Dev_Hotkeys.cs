using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dev_Hotkeys : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject Obstacles;
    public PlayerShoot playerShoot;
    public PlayerMove playerMove;
    private float originalScrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1)){
            playerShoot.numKiwiAmmo = 99;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerMove.flightMeter = 999;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayerPrefs.SetInt("numCoins", 999999);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerMove.isWitch = !playerMove.isWitch;
        }
    }
}
