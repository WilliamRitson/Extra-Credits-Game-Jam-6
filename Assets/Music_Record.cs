using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Record : MonoBehaviour
{
    public AudioClip[] Music_List;

    public float Music_Volume;

    public float SFX_Volume;

    private AudioClip Audio;

    public int Chosen_Music_Nb;

    public AudioSource Source;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Audio = Music_List[Chosen_Music_Nb];
        Source.clip = Audio;
        Source.volume = Music_Volume/100;
        Source.Play();
    }

}
