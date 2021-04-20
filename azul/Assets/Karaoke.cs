using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Karaoke : MonoBehaviour
{
    public GameObject[] positionsGO;
    public Animator anim;
    public Data data;
    [Serializable]
    public class Data
    {
        public SubData[] Subs;
    }
    [Serializable]
    public class SubData
    {
        public string anim;
        public string png;
        public int entrada;
        public int salida;
    }
    public Image image;
    public TextAsset textAsset;
    public int id;
    public List<SubData> subsActive;
    public float timer;
    public float songDuration;
    void Start()
    {
        foreach (GameObject go in positionsGO)
        {
            go.GetComponent<Image>().enabled = (false);
        }
        anim.gameObject.SetActive(false);
        data = JsonUtility.FromJson<Data>(textAsset.text);
    }
    System.Action OnDone;
    public void Init(string animName, System.Action OnDone)
    {
        songDuration = 0;
        CancelInvoke();
        idLoaded = -1;
        this.OnDone = OnDone;
        timer = 0;
        id = 0;
        subsActive = AllDataFor(animName);
        bool loop = false;
        if (subsActive.Count == 0)
            loop = true;
        Events.PlaySound("music", "Music/" + animName, loop);
        print("karaoke: " + animName);
        Invoke("CalculateSongDuration", 1);
    }
    void CalculateSongDuration()
    {
        AudioSource asss = GetComponent<AudioManager>().GetAudioSource("music");
        songDuration = asss.clip.length;
    }
    public List<SubData> AllDataFor(string animName)
    {
        List<SubData> arr = new List<SubData>();
        foreach(SubData sd in data.Subs)
        {
            if (sd.anim == animName)
                arr.Add(sd);
        }
        return arr;
    }
    int idLoaded;
    void Update()
    {
        if (subsActive == null || subsActive.Count == 0)
            return;

        timer += Time.deltaTime;
        if (songDuration>1 && timer > songDuration)
        {
            Done();
            return;
        }
        SubData subDataActive = subsActive[id];

        if(id<subsActive.Count-1 &&  timer> subsActive[id+1].entrada)
        {
            id++;
        }
        if (timer > subDataActive.salida)
        {
            anim.Play("subs_off");
            if (id >= subsActive.Count)
                Done();
        }
        else
        if (timer > subDataActive.entrada)
        {
            if (idLoaded != id)
            {
                idLoaded = id;
                anim.gameObject.SetActive(true);
                anim.Play("subs_on");
                image.sprite = Resources.Load<Sprite>("Karaoke/" + subDataActive.png);
                image.SetNativeSize();
                image.transform.SetParent(positionsGO[UnityEngine.Random.Range(0, positionsGO.Length)].transform);
                image.transform.localScale = Vector3.one;
                image.transform.localPosition = Vector3.zero;
            }
        }
    }
    public void Reset()
    {
        subsActive.Clear();
        anim.gameObject.SetActive(false);
    }
    void Done()
    {
        OnDone();
        Invoke("Reset", 0.25f);
        anim.Play("subs_off");
    }
}
