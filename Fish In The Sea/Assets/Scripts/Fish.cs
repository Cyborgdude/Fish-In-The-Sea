using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fish : FishGameBase, ILoss
{
    [SerializeField]
    private float speed;
    private float rotateTime;

    private float maxRotateTime = 10;

    [SerializeField]
    private string fishName = "Fish";
    [SerializeField]
    private GameObject nameText;

    [SerializeField]
    private float resilience;
    private float holdTime = 0;
    [SerializeField]
    private Image mask;

    [SerializeField]
    private GameObject bubblesPS;
    [SerializeField]
    private float maxBubbleTimer;
    private float bubbleTimer;





    private void Start()
    {
        bubbleTimer = maxBubbleTimer;
        maxRotateTime = maxRotateTime / speed;
        nameText.GetComponent<Text>().text = fishName;
    }
    public void setValues(float Speed, string FishName, float Resilience)
    {
        speed = Speed;
        maxRotateTime = maxRotateTime / speed;
        fishName = FishName;
        resilience = Resilience;

        nameText.GetComponent<Text>().text = fishName;
    }

    private void Update()
    {
        transform.position += (transform.right*-1) * Time.deltaTime * speed;

        if(bubbleTimer <= 0)
        {
            bubbleTimer = maxBubbleTimer;
            SpawnThenDestroyParticle(bubblesPS, transform);
        }
        else
        {
            bubbleTimer = bubbleTimer - Time.deltaTime;
        }

        if(rotateTime <= 0)
        {
            rotateTime = maxRotateTime;
            transform.Rotate(Vector3.forward * Random.Range(-360, 360));

        }
        else
        {
            rotateTime = rotateTime - Time.deltaTime;
        }

    }

    private void changeResilienceFill()
    {
        float fill = holdTime / resilience;
        mask.fillAmount = fill;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            transform.Rotate(Vector3.forward * Random.Range(-360, 360));
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

            transform.Rotate(Vector3.forward * Random.Range(-360, 360));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tackle")
        {
            float power = collision.GetComponent<Tackle>().GetTacklePower();
            holdTime = holdTime + Time.deltaTime*power;
            changeResilienceFill();
            
            if(holdTime >= resilience)
            {
                GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>().pop();
                Destroy(gameObject);
                GameObject.FindGameObjectWithTag("Timer").GetComponent<Timer>().endTimer();

            }
        }
    }

    public void ILoss()
    {
        Destroy(gameObject);
    }
}
