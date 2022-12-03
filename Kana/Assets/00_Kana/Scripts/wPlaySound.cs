using System.Collections;
using System.Collections.Generic;
using mainspace;
using UnityEngine;

public class wPlaySound : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    public void PlayClickSound()
    {
        mainspace.SoundsManger.instance.clickBtn();
    }

    public void PlayStartSound()
    {
        mainspace.SoundsManger.instance.clickStart_web();
    }
}
