using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wLoadAd : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        mainspace.AdsManger.instance.RequestBanner();
    }
}
