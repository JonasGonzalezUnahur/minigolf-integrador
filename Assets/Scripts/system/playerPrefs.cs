using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPrefs : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetFloat("musicVolume", 1);
        PlayerPrefs.SetFloat("soundVolume", 1);
    }
}
