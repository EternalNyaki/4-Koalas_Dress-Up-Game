using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RhythmMicrogame
{
    public class Conductor : Singleton<Conductor>
    {
        public float songPosition { get; private set; }
        private bool playing = false;

        public AudioClip countInAudio;

        public SongInfo song { get; private set; }

        public Sprite upNoteSprite, downNoteSprite, leftNoteSprite, rightNoteSprite;

        private AudioSource audioPlayer;

        private float dspStartTime;

        protected override void Initialize()
        {
            audioPlayer = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            if (playing)
            {
                songPosition = (float)(AudioSettings.dspTime - dspStartTime - song.offset) / song.crotchet;
            }
        }

        public bool isPlaying()
        {
            return playing;
        }

        public void SetSong(SongInfo song)
        {
            if (!audioPlayer.isPlaying)
            {
                this.song = song;
                audioPlayer.clip = song.audio;
            }
        }

        public IEnumerator Play()
        {
            if (song.crotchet * 4 > song.offset)
            {
                StartCoroutine(CountIn());
                yield return new WaitForSeconds(song.crotchet * 4 - song.offset);
                audioPlayer.Play();
            }
            else
            {
                audioPlayer.Play();
                yield return new WaitForSeconds(song.offset - song.crotchet * 4);
                StartCoroutine(CountIn());
            }
            dspStartTime = (float)AudioSettings.dspTime;
            playing = true;
        }

        private IEnumerator CountIn()
        {
            for (int i = 0; i < 3; i++)
            {
                audioPlayer.PlayOneShot(countInAudio);
                yield return new WaitForSeconds(song.crotchet);
            }
            audioPlayer.PlayOneShot(countInAudio);
        }
    }
}
