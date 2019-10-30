using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisappear : MonoBehaviour {

    void OnEnable()
    {
        StartCoroutine(InactiveAfterTime());
    }

    IEnumerator InactiveAfterTime()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
