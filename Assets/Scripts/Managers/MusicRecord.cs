using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public enum MusicType
{
    Menu, Game, Victory, Credits, GameOver
}

public class MusicRecord : MonoBehaviour
{
    public SceneMusic[] musicForScenes;
    private AudioClip currentLoop;
    

    public AudioSource audioSource;
    
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Audio");
        if(objs.Length >1){
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartSceneMusic();
        SceneManager.sceneLoaded += (scene, mode) => StartSceneMusic();
    }

    private void StartSceneMusic()
    {
        var sceneInfo = GameObject.FindWithTag("SceneInfo")?.GetComponent<SceneInfo>();
        var currentSceneType = (sceneInfo != null) ? sceneInfo.sceneMusicType : MusicType.Game;
        var currentMusicInfo = musicForScenes.First(music => music.musicType == currentSceneType);
        currentLoop = currentMusicInfo.loop;
        audioSource.clip = currentMusicInfo.intro;
        audioSource.loop = false;
        audioSource.Play();
    }

    private void Update()
    {
        if (audioSource.isPlaying) return;
        audioSource.clip = currentLoop;
        audioSource.loop = true;
        audioSource.Play();
    }
}

[Serializable]
public class SceneMusic
{
    public MusicType musicType;
    public AudioClip intro;
    public AudioClip loop;
}
