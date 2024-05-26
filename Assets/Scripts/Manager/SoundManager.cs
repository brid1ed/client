using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Manager
{
    public class SoundManager: BaseManager
    {

        private AudioSource source;
        private AudioListener mixer;

        public override bool Init()
        {
            source = this.gameObject.AddComponent<AudioSource>();
            mixer = this.gameObject.AddComponent<AudioListener>();
            
            return false;
            
        }



        public bool SetAudioClip(AudioClip clip)
        {
            this.source.clip = clip;
            return this.source.loop == clip;
        }
        
        public bool JustPlay(AudioClip clip)
        {
            if (clip == null) return false;
            this.source.clip = clip;
            source.volume = 1;
            Play();
            return true;
        }

        public bool JustPlay() {
            // 여러개 한번에 실행하는 방법 구상해야됨.
            if (source.clip == null) return false;
            return true;
        }

        public void Play() {
            this.source.Play();
        }


        IEnumerator SoundFadeIn(AudioSource source, AudioClip clip,
                                float increase = 0.1f)
        {
            if (source == null || clip == null) yield break;

            source.volume = 0;

            yield break;


        }
        
        
        
        
    }
}