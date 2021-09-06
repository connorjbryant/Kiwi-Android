using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerFlightMeter : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider flightMeter;
    public PlayerMove move;
    private float maxFlightMeter;

    void Start()
    {
        flightMeter = GetComponent<Slider>();
        maxFlightMeter = move.flightMeter;
    }

    // Update is called once per frame
    void Update()
    {
        flightMeter.value = move.flightMeter / maxFlightMeter;
    }
}
