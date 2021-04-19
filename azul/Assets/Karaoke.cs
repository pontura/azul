using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Karaoke : MonoBehaviour
{
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
    void Start()
    {
        image.enabled = false;
        data = JsonUtility.FromJson<Data>(textAsset.text);
    }
    System.Action OnDone;
    public void Init(string animName, System.Action OnDone)
    {
        this.OnDone = OnDone;
        timer = 0;
        id = 0;
        print(animName);
        subsActive = AllDataFor(animName);

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
    void Update()
    {
        if (subsActive == null || subsActive.Count == 0)
            return;

        timer += Time.deltaTime;

        SubData subDataActive = subsActive[id];

        if(id<subsActive.Count-1 &&  timer> subsActive[id+1].entrada)
        {
            id++;
        }
        if (timer > subDataActive.salida)
        {
            image.enabled = false;
              if (id >= subsActive.Count)
                Done();
        } else
        if (timer > subDataActive.entrada)
        {
            
            image.enabled = true;
            image.sprite = Resources.Load<Sprite>("Karaoke/" + subDataActive.png);
          
        }        
    }
    public void Reset()
    {
        image.enabled = false;
        subsActive.Clear();
    }
    void Done()
    {
        OnDone();
        Reset();
    }
}
