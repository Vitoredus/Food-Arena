using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoSingleton<AudioMaster>
{
    
    public AudioSource coinSound;

    public void CoinSound()
    {
        coinSound.Play();
    }
}
