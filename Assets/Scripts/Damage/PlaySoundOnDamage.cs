using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnDamage : OnDamageBehavior
{

    [SerializeField] AudioClip[] clips;
    private AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.loop = false;
    }
    
    protected override void OnDamaged(float damage)
    {
        if (source.isPlaying) return;
        source.clip = clips[Random.Range(0, clips.Length - 1)];
        source.Play();
    }
}
