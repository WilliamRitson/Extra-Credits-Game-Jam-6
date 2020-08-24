using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMusicScript : MonoBehaviour
{

    public float Value;

    private Slider zlider;

    public GameObject AS;

    private void Awake()
    {
        zlider = this.GetComponent<Slider>();
        AS = GameObject.Find("MusicManager");
        zlider.value = AS.GetComponent<AudioSource>().volume;
    }
    void Update()
    {
        Value = zlider.value;
        AS.GetComponent<AudioSource>().volume = zlider.value;
    }
}
