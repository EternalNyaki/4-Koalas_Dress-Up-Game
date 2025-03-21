using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Bson;
using UnityEngine;

public class MovingNPC : MonoBehaviour
{
    private float _speed = 3f;

    private Direction _direction = Direction.Right;
    private Vector2 _directionVector = new(0, 1);

    private Animator _animator;

    public void Initialize(float speed, Vector2 positionOffset, Direction direction, AnimationClip walkAnimation)
    {
        _animator = GetComponent<Animator>();

        _speed = speed;
        transform.position += (Vector3)positionOffset;
        _direction = direction;
        switch (direction)
        {
            case Direction.Up:
                _directionVector = Vector2.up;
                break;

            case Direction.Down:
                _directionVector = Vector2.down;
                break;

            case Direction.Left:
                _directionVector = Vector2.left;
                break;

            case Direction.Right:
                _directionVector = Vector2.right;
                break;
        }

        //Bit of code taken from this response on the Unity forums: https://discussions.unity.com/t/how-to-change-animation-clips-of-an-animator-state-at-runtime-is-there-a-way/183135/4
        AnimatorOverrideController aoc = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        foreach (var a in aoc.animationClips)
        {
            anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(a, walkAnimation));
        }
        aoc.ApplyOverrides(anims);
        _animator.runtimeAnimatorController = aoc;
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.position += (Vector3)(_speed * _directionVector * Time.deltaTime);
    }
}
