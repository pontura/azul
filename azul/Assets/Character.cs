using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Character : MonoBehaviour
{
    public Data data;
    Animator anim;
    public Data[] all;
    int id;
    string currentClip;

    [Serializable]
    public class Data
    {
        public AnimationClip clip;
    }

    void Start()
    {
        Utils.Shuffle(all);
        anim = GetComponentInChildren<Animator>();
        currentClip = "singing";
    }
    public void RandomAnim()
    {
        data = all[id];
        //if (currentClip == "singing")
        //{
            currentClip = data.clip.name;
            Invoke(currentClip, 0.1f);
            anim.CrossFade(currentClip, 0.5f);
            //anim.SetBool(currentClip, true);
            id++;
            if (id > all.Length - 1)
                id = 0;
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
