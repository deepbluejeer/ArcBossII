using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Easy : IFairyBehaviour
{
    public Action CurrentAction { get; set; }
    Transform edgeCheck;
    Fairy fairy;
    Rigidbody2D rgb;
    bool walled;
    bool dominoEffect;

    public void Enter(Fairy currentFairy)
    {
        fairy = currentFairy;
        rgb = fairy.GetComponent<Rigidbody2D>();
        CurrentAction = GetReady;
    }

    public void GetReady()
    {
        fairy.gainedPoints.gameObject.SetActive(false);
        rgb.isKinematic = true;
        rgb.velocity = Vector2.zero;
        fairy.transform.Rotate(0, 91, 0);
        fairy.attackArea.gameObject.SetActive(false);
        fairy.GetComponent<BoxCollider2D>().enabled = false;
        fairy.dominoCollider.gameObject.SetActive(false);
        fairy.transform.localScale = new Vector3(fairy.transform.localScale.x, fairy.transform.localScale.y, Math.Abs(fairy.transform.localScale.z));
        CurrentAction = Wait;
    }

    public void Wait()
    {
        fairy.transform.position = Vector3.MoveTowards(fairy.transform.position, new Vector3(fairy.transform.position.x, fairy.transform.position.y, 0), Time.deltaTime);
        if (fairy.transform.position.z == 0)
        {
            fairy.transform.Rotate(0, -91, 0);
            rgb.isKinematic = false;
            fairy.attackArea.gameObject.SetActive(true);
            fairy.GetComponent<BoxCollider2D>().enabled = true;
            CurrentAction = Execute;
        }
    }

    public void Execute()
    {
        walled = Physics2D.OverlapCircle(fairy.edgeCheck.position, fairy.groundCheckRadius, fairy.whatIsWall);
        rgb.velocity = new Vector2(-fairy.speed, rgb.velocity.y);
        //Check if should move left or right.
        if (walled)
        {
            fairy.speed *= -1;
            fairy.transform.localScale = new Vector3(fairy.transform.localScale.x, fairy.transform.localScale.y, -fairy.transform.localScale.z);
        }
    }

    public void Domino()
    {
        if (!dominoEffect)
        {
            fairy.dominoCollider.gameObject.SetActive(true);
            if (fairy.attackedFromRight)
                rgb.velocity = new Vector2(-1f, 2f);
            else
                rgb.velocity = new Vector2(1f, 2f);
            dominoEffect = true;
        }

        if (Physics2D.OverlapCircle(fairy.edgeCheck.position, fairy.groundCheckRadius, fairy.whatIsGround))
        {
            fairy.StartCoroutine(WaitBeforeResuming());
        }
    }

    IEnumerator WaitBeforeResuming()
    {
        yield return new WaitForSeconds(0.5f);
        dominoEffect = false;
        fairy.dominoCollider.gameObject.SetActive(false);
        fairy.attackedFromRight = false;
        CurrentAction = Execute;
    }

    public void Exit()
    {
        walled = false;
        dominoEffect = false;
        fairy.dominoCollider.gameObject.SetActive(false);
        fairy.attackedFromRight = false;
        fairy.speed = Math.Abs(fairy.speed);
        fairy.transform.localScale = new Vector3(fairy.transform.localScale.x, fairy.transform.localScale.y, Math.Abs(fairy.transform.localScale.z));
        CurrentAction = GetReady;
    }
}
