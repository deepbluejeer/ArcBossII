using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fairy : MonoBehaviour
{
    IFairyBehaviour fairyBehaviour;
    public enum Behaviour { easy, medium, hard };
    public Behaviour currentBehaviour;
    public Transform edgeCheck;
    public int speed;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    public float groundCheckRadius;
    public Transform player;
    public Transform jumpCheck;
    public ParticleSystem partAnim;
    public Collider2D attackArea;
    public Collider2D dominoCollider;
    public bool attackedFromRight;
    public int points;
    Canvas canvas;
    public Text gainedPoints;
    Camera cam;
    int multiplier = 1;

    // Use this for initialization
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        gainedPoints.transform.SetParent(canvas.transform);
        gainedPoints.rectTransform.localPosition = Vector3.zero;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fairyBehaviour = GetComponent<IFairyBehaviour>();
        GetCurrentBehaviour();
        fairyBehaviour.Enter(this);
    }

    // Update is called once per frame
    void Update()
    {
        StartBehaviour();
    }

    void StartBehaviour()
    {
        if (fairyBehaviour != null)
            fairyBehaviour.CurrentAction();
    }

    void Die()
    {
        AudioSystem.sharedInstance.PlayEnemyDie();
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(cam, transform.position);
        ParticleSystem deathAnim = Instantiate(partAnim);
        deathAnim.transform.position = transform.position;
        fairyBehaviour.CurrentAction = fairyBehaviour.Exit;
        PointSystem.sharedInstance.AddScore(points * multiplier);

        gainedPoints.rectTransform.anchoredPosition = screenPos - (Vector2)canvas.GetComponent<RectTransform>().sizeDelta/2 + new Vector2(0,30f);

        gainedPoints.text = "+" + points * multiplier;
        gainedPoints.gameObject.SetActive(true);
        multiplier = 1;
        gameObject.SetActive(false);
    }

    void GetCurrentBehaviour()
    {
        switch (currentBehaviour)
        {
            case Behaviour.easy:
                fairyBehaviour = new Easy();
                break;

            case Behaviour.medium:
                fairyBehaviour = new Medium();
                break;

            case Behaviour.hard:
                fairyBehaviour = new Hard();
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "AttackArea")
        {
            Die();
        }
        else if (collision.tag == "ShieldBash")
        {
            if (collision.transform.position.x > transform.position.x)
                attackedFromRight = true;

            fairyBehaviour.CurrentAction = fairyBehaviour.Domino;
        }

        else if (collision.tag == "Fairy")
        {
            //The enemy that hit this one.
            Fairy fairy = collision.transform.parent.GetComponent<Fairy>();
            multiplier += fairy.multiplier;
            if (fairy != null)
            fairy.Die();
            if (collision.transform.position.x > transform.position.x)
                attackedFromRight = true;

            fairyBehaviour.CurrentAction = fairyBehaviour.Domino;
        }
    }
}
