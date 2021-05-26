using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class Timer : FishGameBase
{
    [SerializeField]
    private float maxTime = 60;
    private float currentTime;
    private bool countdown = true;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = maxTime;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = Mathf.Round(currentTime).ToString();
        if(countdown == true)
        {
            if(currentTime <= 0)
            {
                countdown = false;
                Loss();
            }
            else
            {
                currentTime = currentTime - Time.deltaTime;
            }
        }
    }

    public void StartTimer(float MaxTime)
    {
        maxTime = MaxTime;
        currentTime = maxTime;
        countdown = true;
    }

    public void endTimer()
    {
        countdown = false;
    }
    private void Loss()
    {
        List<ILoss> LossObjects = new List<ILoss>();
        LossObjects.AddRange(FindObjectsOfType<MonoBehaviour>().OfType<ILoss>().ToList());
        foreach (ILoss loss in LossObjects)
        {
            loss.ILoss();
        }
    }
}
