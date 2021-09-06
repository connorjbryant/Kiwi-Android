using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHeatMeter : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider heatMeter;
    public PlayerMove move;
    private float maxHeatMeter;

    void Start()
    {
        heatMeter = GetComponent<Slider>();
        maxHeatMeter = move.tempflightMeter;
    }

    // Update is called once per frame
    void Update()
    {
        heatMeter.value = move.heatMeter / maxHeatMeter;
    }
}
