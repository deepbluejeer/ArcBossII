using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void Action();
    Action CurrentScreen { get; set; }
    public static GameManager instance;
    enum OptionSelect { start = 0, credits = 1, exit = 2 };
    Coroutine cutsceneCoroutine;

    //Cutscene
    public Text cutscene1;
    public Text cutscene2;
    public Text cutscene3;
    public Text cutscene4;
    public Text cutscene5;

    public Transform fairyCut;
    public Transform sternumCut;

    bool cutsceneFinished;
    bool startedCutscene;

    //Start Screen
    //cedcc0 = white
    //1c2e1c = dark

    public Button op1;
    public Button op2;
    public Button op3;
    OptionSelect option;
    Color32 selected;
    Color32 deselected;
    public Text startText;
    public Text credits;
    public Text exit;
    public Text thankyou;

    public Text flavor1;
    public Text flavor2;
    public Text flavor3;
    public Image skelSelector1;
    public Image skelSelector2;
    public Image skelSelector3;

    //In-game
    public Text tryAgain;

    private void Start()
    {
        selected = new Color32(208, 222, 194, 255);
        deselected = new Color32(101, 135, 101, 255);
        instance = GetComponent<GameManager>();
        if (SceneManager.GetActiveScene().name == "Start")
        {
            CurrentScreen = Cutscene;
            option = OptionSelect.start;
        }

        else if (SceneManager.GetActiveScene().name == "Debug")
            CurrentScreen = DoNothing;

    }

    private void Update()
    {
        CurrentScreen();
    }

    public void Cutscene()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StopCoroutine(cutsceneCoroutine);
            cutsceneFinished = true;
        }

        if (!startedCutscene || cutsceneFinished)
        {
            startedCutscene = true;
            cutsceneCoroutine = StartCoroutine(CutsceneTime());
        }

        if (cutsceneFinished)
        {
            cutscene1.gameObject.SetActive(false);
            cutscene2.gameObject.SetActive(false);
            cutscene3.gameObject.SetActive(false);
            cutscene4.gameObject.SetActive(false);
            cutscene5.gameObject.SetActive(false);

            sternumCut.gameObject.SetActive(true);
            fairyCut.gameObject.SetActive(true);
            flavor1.gameObject.SetActive(true);
            flavor2.gameObject.SetActive(true);
            flavor3.gameObject.SetActive(true);
            op1.gameObject.SetActive(true);
            op2.gameObject.SetActive(true);
            op3.gameObject.SetActive(true);
            skelSelector1.gameObject.SetActive(true);

            CurrentScreen = BeginScreen;
        }
    }

    IEnumerator CutsceneTime()
    {
        if (!cutsceneFinished)
        {
            yield return new WaitForSeconds(1f);

            cutscene1.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);

            AudioSystem.sharedInstance.PlayLaugh();
            fairyCut.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);

            cutscene2.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);

            cutscene3.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);

            cutscene4.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);

            sternumCut.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);

            cutscene5.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);

            cutsceneFinished = true;
        }
    }

    public void BeginScreen()
    {
        CycleOptions();
        SelectThisOne();

        switch (option)
        {
            case (OptionSelect.start):
                startText.color = selected;
                credits.color = deselected;
                exit.color = deselected;
                skelSelector1.gameObject.SetActive(true);
                skelSelector2.gameObject.SetActive(false);
                skelSelector3.gameObject.SetActive(false);
                break;

            case (OptionSelect.credits):
                startText.color = deselected;
                credits.color = selected;
                exit.color = deselected;
                skelSelector1.gameObject.SetActive(false);
                skelSelector2.gameObject.SetActive(true);
                skelSelector3.gameObject.SetActive(false);
                break;

            case (OptionSelect.exit):
                startText.color = deselected;
                credits.color = deselected;
                exit.color = selected;
                skelSelector1.gameObject.SetActive(false);
                skelSelector2.gameObject.SetActive(false);
                skelSelector3.gameObject.SetActive(true);
                break;
        }
    }

    void CycleOptions()
    {
        if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") < 0)
        {
            AudioSystem.sharedInstance.PlayOption();
            if (option == OptionSelect.exit) option = OptionSelect.start;
            else option += 1;
        }
        else if (Input.GetButtonDown("Vertical") && Input.GetAxis("Vertical") > 0)
        {
            AudioSystem.sharedInstance.PlayOption();
            if (option == OptionSelect.start) option = OptionSelect.exit;
            else option -= 1;
        }
    }

    public void MouseOnStart()
    {
        startText.color = selected;
        credits.color = deselected;
        exit.color = deselected;
        skelSelector1.gameObject.SetActive(true);
        skelSelector2.gameObject.SetActive(false);
        skelSelector3.gameObject.SetActive(false);

        option = OptionSelect.start;
    }

    public void MouseOnCredits()
    {
        startText.color = deselected;
        credits.color = selected;
        exit.color = deselected;
        skelSelector1.gameObject.SetActive(false);
        skelSelector2.gameObject.SetActive(true);
        skelSelector3.gameObject.SetActive(false);

        option = OptionSelect.credits;
    }

    public void MouseOnExit()
    {
        startText.color = deselected;
        credits.color = deselected;
        exit.color = selected;
        skelSelector1.gameObject.SetActive(false);
        skelSelector2.gameObject.SetActive(false);
        skelSelector3.gameObject.SetActive(true);

        option = OptionSelect.exit;
    }

    void SelectThisOne()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            switch (option)
            {
                case OptionSelect.start:
                    SceneManager.LoadScene("Debug");
                    break;

                case OptionSelect.credits:
                    CurrentScreen = CreditsScreen;
                    break;

                case OptionSelect.exit:
                    Application.Quit();
                    break;
            }
        }
    }

    public void CreditsScreen()
    {
        thankyou.gameObject.SetActive(true);
        skelSelector1.gameObject.SetActive(false);
        skelSelector2.gameObject.SetActive(false);
        skelSelector3.gameObject.SetActive(false);
        op1.gameObject.SetActive(false);
        op2.gameObject.SetActive(false);
        op3.gameObject.SetActive(false);

        sternumCut.gameObject.SetActive(false);
        fairyCut.gameObject.SetActive(false);
        flavor1.gameObject.SetActive(false);
        flavor2.gameObject.SetActive(false);
        flavor3.gameObject.SetActive(false);

        if (Input.GetButtonDown("Fire1"))
        {
            thankyou.gameObject.SetActive(false);
            CurrentScreen = Cutscene;
        }
    }

    public void DoNothing()
    { }

    void MainScreen()
    {
        sternumCut.gameObject.SetActive(true);
        fairyCut.gameObject.SetActive(true);
        flavor1.gameObject.SetActive(true);
        flavor2.gameObject.SetActive(true);
        flavor3.gameObject.SetActive(true);
        op1.gameObject.SetActive(true);
        op2.gameObject.SetActive(true);
        op3.gameObject.SetActive(true);
        skelSelector1.gameObject.SetActive(true);
    }

    public void Restart()
    {
        tryAgain.gameObject.SetActive(true);

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("Start");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }
}
