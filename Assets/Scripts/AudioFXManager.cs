using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFXManager : MonoBehaviour
{
    public static AudioFXManager instance;
    public AudioSource soundFXObject;

    private void Awake(){
        if(instance==null){
            instance=this;
        }
    }

    public void PlaySoundFX(AudioClip audio,Transform spawnTransform,float volume){
        AudioSource audioSource=Instantiate(soundFXObject,spawnTransform.position,Quaternion.identity);
        audioSource.clip=audio;
        audioSource.volume=volume;
        audioSource.Play();
        float clipLength=audioSource.clip.length;

        Destroy(audioSource.gameObject,clipLength);
    }
    public void PlayRandomSoundFX(AudioClip[] audios,Transform spawnTransform,float volume){
        AudioSource audioSource=Instantiate(soundFXObject,spawnTransform.position,Quaternion.identity);
        audioSource.clip=audios[Random.Range(0,audios.Length)];
        audioSource.volume=volume;
        audioSource.Play();
        float clipLength=audioSource.clip.length;

        Destroy(audioSource.gameObject,clipLength);
    }
}
