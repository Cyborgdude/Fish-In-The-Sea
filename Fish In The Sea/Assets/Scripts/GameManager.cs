using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
public class GameManager : FishGameBase, ILoss
{
    [Header("Fish Prefabs")]
    [SerializeField]
    private GameObject[] FishPrefabs;

    [Header("Current Fish")]
    [SerializeField]
    private int size = 1;
    [SerializeField]
    private float speed = 4;
    [SerializeField]
    private float resilience = 100;
    [SerializeField]
    private float patience = 60;
    [SerializeField]
    private string fishName = "Fish";
    [SerializeField]
    private string bio = "blub";
    [SerializeField]
    private Sprite pic;
    [SerializeField]
    private Color32 fishColor;
    [SerializeField]
    private int voice;
    [SerializeField]
    private bool male = true;

    private GameObject activeFish;

    [Header("Upgrades")]
    [SerializeField]
    private GameObject bigTackle;
    [SerializeField]
    private float rodStrength = 0;
    [SerializeField]
    private float baitTaste = 0;
    [SerializeField]
    private float stamina = 0;

    [Header("Other Managers")]
    [SerializeField]
    private GameObject timer;
    [SerializeField]
    private GameObject tuna;
    [SerializeField]
    private GameObject tunaName;
    [SerializeField]
    private GameObject tunaBio;
    [SerializeField]
    private GameObject tunaPic;
    [SerializeField]
    private GameObject upgradeCanvas;
    [SerializeField]
    private GameObject[] tackleButtons;
    [SerializeField]
    private GameObject beegTackle;
    [SerializeField]
    private GameObject titleCanvas;
    [SerializeField]
    private GameObject titleButton;

    [SerializeField]
    private GameObject flopBook;
    [SerializeField]
    private GameObject flopBookName;
    [SerializeField]
    private GameObject flopBookPic;


    [Header("Arrays")]
    [SerializeField]
    private string[] fishNames;
    [SerializeField]
    private string[] fishBios;
    [SerializeField]
    private Sprite[] MfishPics;
    [SerializeField]
    private Sprite[] FfishPics;
    [SerializeField]
    private AudioClip[] Mgreetings;
    [SerializeField]
    private AudioClip[] Mfailures;
    [SerializeField]
    private AudioClip[] Mflavourtowns;
    [SerializeField]
    private AudioClip[] Fgreetings;
    [SerializeField]
    private AudioClip[] Ffailures;
    [SerializeField]
    private AudioClip[] Fflavourtowns;

    [Header("Other")]
    [SerializeField]
    private AudioClip tunaMusic;
    [SerializeField]
    private AudioClip dateMusic;
    [SerializeField]
    private AudioClip tutorialMessage;
    [SerializeField]
    private AudioClip NoAudioClip;
    [SerializeField]
    private AudioClip YesAudioClip;
    private bool firstLoss;




    public void ILoss()
    {
        if(firstLoss == false)
        {
            titleCanvas.SetActive(true);
            firstLoss = true;
            GetComponent<AudioSource>().PlayOneShot(tutorialMessage);
            Invoke("showTitleButton", 3.5f);

            GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Stop();
        }
        else
        {
            if(male == true)
            {
                GetComponent<AudioSource>().PlayOneShot(Mfailures[voice]);
            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(Ffailures[voice]);
            }

            GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Stop();
            GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().clip = tunaMusic;
            GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Play();
            GenereteFish();
            upgradeCanvas.SetActive(true);
        }

    }

    public void pop()
    {
        if(male == true)
        {
            GetComponent<AudioSource>().PlayOneShot(Mflavourtowns[voice]);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(Fflavourtowns[voice]);
        }

        flopBookName.GetComponent<Text>().text = fishName;
        flopBookPic.GetComponent<Image>().sprite = pic;
        flopBookPic.GetComponent<Image>().color = fishColor;
        flopBook.SetActive(true);

        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Stop();
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().clip = tunaMusic;
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Play();
    }
    public void beginGame()
    {
        titleCanvas.SetActive(false);
        GenereteFish();
        tuna.SetActive(true);

        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Stop();
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().clip = tunaMusic;
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Play();
    }

    private void showTitleButton()
    {
        titleButton.SetActive(true);
    }

    public void GenereteFish()
    {
        //Gender
        male = !male;

        //Pick Fish Size
        size = Random.Range(0, 3);

        //Pick Fish Speed
        speed = Random.Range(5, 8);

        speed = speed - (rodStrength/2);
        if(speed < 1)
        {
            speed = 1;
        }

        //Pick Fish Resilience
        resilience = Random.Range(50, 121);

        resilience = resilience - ((baitTaste*1.5f) * 10);

        if(resilience < 30)
        {
            resilience = 30;
        }

        //Pick Fish Patience
        patience = Random.Range(30, 61);

        patience = patience + (stamina * 10);

        //Pick Fish Color
        fishColor = new Color32((byte)Random.Range(100, 255), (byte)Random.Range(100, 255), (byte)Random.Range(100, 255), 255);

        //Pick Fish Name
        int FNIndex = Random.Range(0, fishNames.Length);
        fishName = fishNames[FNIndex];

        //Pick Fish Bio
        int BIndex = Random.Range(0, fishBios.Length);
        bio = fishBios[BIndex];

        //Pick Fish Image
        if(male == true)
        {
            int IMIndex = Random.Range(0, MfishPics.Length);
            pic = MfishPics[IMIndex];
        }
        else
        {
            int IMIndex = Random.Range(0, FfishPics.Length);
            pic = FfishPics[IMIndex];
        }


        //Pick Fish Voice
        if (male == true)
        {
            voice = Random.Range(0, Mgreetings.Length);
        }
        else
        {
            voice = Random.Range(0, Fgreetings.Length);
        }


        tunaName.GetComponent<Text>().text = fishName;
        tunaBio.GetComponent<Text>().text = bio;
        tunaPic.GetComponent<Image>().sprite = pic;
        tunaPic.GetComponent<Image>().color = fishColor;
        GetComponent<AudioSource>().PlayOneShot(NoAudioClip, 0.2f);
    }

    public void SpawnFish()
    {
        timer.GetComponent<Timer>().StartTimer(patience);

        activeFish = Instantiate(FishPrefabs[size]);
        activeFish.GetComponent<Fish>().setValues(speed, fishName, resilience);
        activeFish.GetComponent<SpriteRenderer>().color = fishColor;
        tuna.SetActive(false);

        if(male == true)
        {
            GetComponent<AudioSource>().PlayOneShot(Mgreetings[voice]);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(Fgreetings[voice]);
        }

        GetComponent<AudioSource>().PlayOneShot(YesAudioClip);
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Stop();
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().clip = dateMusic;
        GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            QuitGame();
        }


    }

    public void UpgradeStamina()
    {
        stamina = stamina + 1;
        TurnOnTuna();
    }
    
    public void UpgradeRodStrength()
    {
        rodStrength = rodStrength + 1f;
        TurnOnTuna();
    }

    public void UpgradeBaitTaste()
    {
        baitTaste = baitTaste + 1f;
        TurnOnTuna();
    }

    public void UpgradeTackleSize()
    {
        for(int i = 0; i < tackleButtons.Length; i++)
        {
            tackleButtons[i].gameObject.SetActive(false);
        }

        bigTackle = Instantiate(beegTackle);
        TurnOnTuna();
    }

    private void TurnOnTuna()
    {
        List<GetStat> StatObjects = new List<GetStat>();
        StatObjects.AddRange(FindObjectsOfType<GetStat>().ToList());
        foreach (GetStat stat in StatObjects)
        {
            stat.UpdateStat();
        }

        upgradeCanvas.SetActive(false);
        tuna.SetActive(true);
    }

    public float GetStat(int statNum)
    {
        Debug.Log("GetStat");
        switch(statNum)
        {
            case 1:
                return rodStrength;
            case 2:
                return baitTaste;
            case 3:
                return stamina;
        }
        return 404f;
    }

    public void ResetGame()
    {
        flopBook.SetActive(false);
        rodStrength = 0;
        baitTaste = 0;
        stamina = 0;
        firstLoss = false;
        titleButton.SetActive(false);
        patience = 15f;
        speed = 6f;
        resilience = 200f;

        List<GetStat> StatObjects = new List<GetStat>();
        StatObjects.AddRange(FindObjectsOfType<GetStat>().ToList());
        foreach (GetStat stat in StatObjects)
        {
            stat.UpdateStat();
        }

        Destroy(bigTackle);
        for (int i = 0; i < tackleButtons.Length; i++)
        {
            tackleButtons[i].gameObject.SetActive(true);
        }

        SpawnFish();

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

