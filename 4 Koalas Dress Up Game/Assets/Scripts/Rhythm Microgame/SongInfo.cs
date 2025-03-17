using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RhythmMicrogame
{
    [CreateAssetMenu(fileName = "SongInfo.asset", menuName = "Microgames/Song Info")]
    public class SongInfo : ScriptableObject
    {
        public int bpm;
        public float crotchet
        {
            get { return (float)60 / bpm; }
        }
        public float offset;
        public float countIn;
        public float duration;
        public AudioClip audio;
        public NoteInfo[] chartInfo;
    }

    [Serializable]
    public struct NoteInfo
    {
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public Direction direction;
        public float beat;
    }
}
