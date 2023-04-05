using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public TextMeshProUGUI healthUI;
    PlayerScript playerInfo;
    
    void Start()
    {
        playerInfo = GetComponent<PlayerScript>();
        SetStat();
    }
    
    void Update()
    {
        playerInfo = GetComponent<PlayerScript>();
        SetStat();
    }

    public void SetStat()
    {
        healthUI.text = playerInfo.PlayerHealth.ToString();
    }
    
}
