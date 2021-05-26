using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tackle : FishGameBase
{
    private Vector3 targetPos;
    private Rigidbody2D rb;
    [SerializeField]
    private float tacklePower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(targetPos.x,targetPos.y,0);
    }

    public float GetTacklePower()
    {
        return tacklePower;
    }
}
