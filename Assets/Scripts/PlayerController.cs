using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public float jumpHeight;
    public float speed;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public Transform head;
    private bool grounded;
    public float swordDelay;
    private float swordDelayCounter;
    private bool swordCount;
    private Rigidbody2D rgb;
    Animator anim;
    bool dead;
    public ParticleSystem deathAnim;
    ParticleSystem deathAnima;
    Transform headdead;
    public Collider2D attackArea;

    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        deathAnima = Instantiate(deathAnim);
        headdead = Instantiate(head);
        deathAnima.gameObject.SetActive(false);
        headdead.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    void Update()
    {
        if (!dead)
        {
            Jump();
            Shield();
            Sword();
            Walk();
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            AudioSystem.sharedInstance.PlayOption();
            rgb.velocity = new Vector2(0, jumpHeight);
        }
    }

    void Shield()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            AudioSystem.sharedInstance.PlayPlayerAttack();
            anim.SetTrigger("Shield");
        }
    }

    void Sword()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            AudioSystem.sharedInstance.PlayPlayerAttack();
            anim.SetTrigger("Sword");
        }
    }

    void Walk()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Sword"))
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Shield"))
                rgb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rgb.velocity.y);
        }

        else if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Sword") && !grounded)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Shield") && !grounded)
                rgb.velocity = new Vector2(0, rgb.velocity.y);
        }

        anim.SetFloat("Speed", Mathf.Abs(rgb.velocity.x));

        if (rgb.velocity.x > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (rgb.velocity.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            EnemyManager.sharedInstance.playerDead = true;
            cam.GetComponent<AudioSource>().Stop();
            dead = true;
            deathAnima.transform.position = transform.position;
            headdead.transform.position = transform.position;
            deathAnima.gameObject.SetActive(true);
            headdead.gameObject.SetActive(true);
            PointSystem.sharedInstance.SaveHiScore();
        }
    }
}