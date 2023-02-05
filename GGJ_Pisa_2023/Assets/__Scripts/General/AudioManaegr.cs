using Riutilizzabile;
using UnityEngine;

namespace Jam.General
{
    public class AudioManaegr : SingletonDDOL<AudioManaegr>
    {
        public AudioBank bank;
        public AudioSource fx;
        public AudioSource music;
        public void PlayFx(string name)
        {
            fx.PlayOneShot(bank.sounds[name]);
        }
        public void playmusic(string name)
        {
            music.Stop();
            music.clip = bank.sounds[name];
            music.Play();
        }
        
    }
}