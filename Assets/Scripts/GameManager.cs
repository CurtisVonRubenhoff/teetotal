using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private List<string> coworkerMessages = new List<string>();
    [SerializeField]
    private List<string> coworkerReplies = new List<string>();

    [SerializeField]
    private float annoyanceDelayStartingMininmum = 5f;
    [SerializeField]
    private float annoyanceDelayFinalMininmum = 2f;
    [SerializeField]
    private float annoyanceDelayStartingMaximum = 15f;
    [SerializeField]
    private float annoyanceDelayFinalMaximum = 5f;

    [SerializeField]
    private float delayDegredationTime = 120f;

    private int currentMessage;
    [SerializeField]

    private float currentTime = 0f;
    private float annoyanceTime = 0f;

    [SerializeField]
    private AudioSource msgSend;
    [SerializeField]
    private AudioSource msgRcv;



    public bool amAnnoyed = false;

    [SerializeField]
    private Button replyButton;
    [SerializeField]
    private TextMeshPro textBox;
    [SerializeField]
    private TextMeshProUGUI timeIndicator;

    [SerializeField]
    private MiuController miu;

    [SerializeField]
    private GameObject StartScreen;

    public bool gameRunning = false;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartAnnoyanceDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if(gameRunning) {
            if (!amAnnoyed)
                annoyanceTime = Time.fixedUnscaledTime;

            timeIndicator.text = new TimeSpan(0, 0, (int)Time.fixedUnscaledTime).ToString(@"mm\:ss");
        }
    }

    private IEnumerator StartAnnoyanceDelay() {
        var degredationFactor = annoyanceTime/delayDegredationTime;
        degredationFactor = (degredationFactor > 1) ? 1 : degredationFactor;
        var seedMin = annoyanceDelayStartingMaximum - ((annoyanceDelayStartingMaximum - annoyanceDelayStartingMininmum) * degredationFactor);
        var seedMax = annoyanceDelayFinalMininmum - ((annoyanceDelayFinalMaximum - annoyanceDelayFinalMininmum) * degredationFactor);
        var seed = UnityEngine.Random.Range(seedMin, seedMax);
        yield return new WaitForSeconds(seed);
        var which = UnityEngine.Random.Range(0f, 1f);
        Debug.Log(which);

        if (which > 0.5f) {
            RecieveMessage();
        } else {
            HelloMiu();
        }
    }

    private void RecieveMessage() {
        msgRcv.Play();
        amAnnoyed = true;
        currentMessage =(int)UnityEngine.Random.Range(0, coworkerMessages.Count);
        var message = coworkerMessages[currentMessage];
        textBox.text = $"Co-Worker: {message}";
        replyButton.interactable = true;
    }

    private void HelloMiu() {
        amAnnoyed = true;
        miu.amOn = true;
    }

    public void GoodByeMiu() {
        amAnnoyed = false;
        miu.amOn = false;
        StartCoroutine(StartAnnoyanceDelay());
    }

    public void ReplyToMessage() {
        msgSend.Play();
        amAnnoyed = false;
        var message = coworkerReplies[currentMessage];
        textBox.text += $"\nYou: {message}";
        replyButton.interactable = false;
        StartCoroutine(StartAnnoyanceDelay());
    }

    public void YouLose() {
        gameRunning = false;
        StopAllCoroutines();

    }

    public void StartGame() {
        gameRunning = true;
        StartScreen.SetActive(false);
    }
}



/*

ideas:

messages from annoying coworkers
news alerts
porn popups

cat

robo calls


*/
