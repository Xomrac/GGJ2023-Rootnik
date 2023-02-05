using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Jam.General
{
    [CreateAssetMenu(fileName = "AudioBank", menuName = "Audio", order = 0)]
    public class AudioBank : SerializedScriptableObject
    {
        public Dictionary<AudioClip, string> sounds;
    }
}