using UnityEngine;

namespace CodeBase
{
    public class AudioController : MonoBehaviour
    {
        public static AudioController Instance;

        public bool pauseMute = false;
        public bool globalMute = true;
        public bool muteMusic = false;
        public AudioSource music;
        private AudioListener audioListener;


        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;
            } 

            GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");

            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
            }

            DontDestroyOnLoad(this.gameObject);
        }

        public void MuteBgMusic()
        {
            muteMusic = !muteMusic;
            music.mute = !music.mute;
        }


        public void MuteAllSounds()
        {
            if (globalMute == true)
            {
                AudioListener.volume = 0;
                return;
            }
            else if (globalMute == false)
            {
                if (pauseMute)
                    AudioListener.volume = 0;
                else
                    AudioListener.volume = 1;

                //if (AudioListener.volume == 1)
                //    AudioListener.volume = 0;
                //else
                //    AudioListener.volume = 1;
            }

        }

        public void init()
        {
            pauseMute = false;
            MuteAllSounds();
        }
    }
}
