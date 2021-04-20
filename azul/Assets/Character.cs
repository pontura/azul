using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    public Karaoke karaoke;
    public Data data;
    Animator anim;
    public Data[] all;
    int id;
    string currentClip;
    string defaultAnim = "halfTurn";

    [Serializable]
    public class Data
    {
        public AnimationClip clip;
    }

    void Start()
    {        
        Events.FadeVolumeFromTo("music", 0, 1, 0.5f);
        Utils.Shuffle(all);
        anim = GetComponentInChildren<Animator>();
        OnDoneKaraoke();
    }
    void OnDoneKaraoke()
    {
        print("OnDoneKaraoke");
        Events.PlaySound("music", "Music/" + defaultAnim, false);
        anim.CrossFade(defaultAnim, 1);
    }
    public void RandomAnim()
    {
        karaoke.Reset();
        int a = UnityEngine.Random.Range(1, 6);
        Events.PlaySound("ui", "Audio/clickNOTFly_sound" + a, false);
        data = all[id];

        string animName = all[id].clip.name;
        id++;
        if (id > all.Length - 1)
            id = 0;

        if (animName == defaultAnim)
        {
            RandomAnim();
            return;
        }
        karaoke.Init(animName, OnDoneKaraoke);
        //if (currentClip == "singing")
        //{
            currentClip = data.clip.name;
            Invoke(currentClip, 0.1f);
        anim.Play(currentClip);
        // anim.CrossFade(currentClip, 1f);
        //anim.SetBool(currentClip, true);
       
         
        //}
        //else
        //{
        //    anim.SetBool(currentClip, false);
        //    currentClip = "singing";
        //}
    }
    public void dancing()
    {
        
    }
    public void singing()
    {

    }

}
