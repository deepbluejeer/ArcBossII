using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadController : MonoBehaviour
{
    GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        AudioSystem.sharedInstance.PlayPlayerDie();
        AudioSystem.sharedInstance.PlayDieMusic();
        player.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.instance.Restart();
    }
}
