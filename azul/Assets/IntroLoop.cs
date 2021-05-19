using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroLoop : MonoBehaviour
{
    public GameObject signal;

    void Start()
    {
        signal.SetActive(false);
        Restart();
    }
    void Restart()
    {
        signal.SetActive(false);
        Invoke("Init", 3);
    }
    private void Init()
    {
        signal.SetActive(true);
        Vector2 pos = new Vector2 (Random.Range(-200, 200), Random.Range(-180, 180));
        signal.transform.localPosition = pos;
        Invoke("Restart", 2);
    }
    public void OnClicked()
    {
        CancelInvoke();
        signal.SetActive(false);
    }
}
