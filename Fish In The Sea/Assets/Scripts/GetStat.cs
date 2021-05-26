using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetStat : FishGameBase
{
    [SerializeField]
    private int statNum;
    public void UpdateStat()
    {
        GameObject manager = GameObject.FindGameObjectWithTag("Manager");
        gameObject.GetComponent<Text>().text = manager.GetComponent<GameManager>().GetStat(statNum).ToString();
    }
}
