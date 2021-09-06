using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePowerup : MonoBehaviour
{
    public GameObject player;
    public PlayerMove move;
    public bool triggerPowerup;

    // Start is called before the first frame update
    void Start()
    {
        triggerPowerup = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
