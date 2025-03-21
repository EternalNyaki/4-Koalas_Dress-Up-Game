using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovingNPCAnimations.asset", menuName = "Dress-Up/Moving NPC Animations")]
public class MovingNPCAnimations : ScriptableObject
{
    public AnimationClip[] up;
    public AnimationClip[] down;
    public AnimationClip[] left;
    public AnimationClip[] right;
}
