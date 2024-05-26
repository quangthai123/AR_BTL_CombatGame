using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private List<AudioSource> sfx;
    [SerializeField] private AudioSource music;
    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    // Update is called once per frame
    public void PlayeSFX(int i)
    {
        sfx[i].Play();
    }
}
