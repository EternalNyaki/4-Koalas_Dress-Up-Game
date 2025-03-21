using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public GameObject movingNPCPrefab;

    public Direction direction;

    public MovingNPCAnimations animations;

    public float npcMinSpeed;
    public float npcMaxSpeed;
    public float npcMinOffset;
    public float npcMaxOffset;
    public float minSpawnInterval;
    public float maxSpawnInterval;

    private Vector2 _directionVector;
    private AnimationClip[] _npcAnimations;

    private float _timeToNextSpawn = 0;

    // Start is called before the first frame update
    void Start()
    {
        switch (direction)
        {
            case Direction.Up:
                _directionVector = Vector2.left;
                _npcAnimations = animations.up;
                break;

            case Direction.Down:
                _directionVector = Vector2.right;
                _npcAnimations = animations.down;
                break;

            case Direction.Left:
                _directionVector = Vector2.down;
                _npcAnimations = animations.left;
                break;

            case Direction.Right:
                _directionVector = Vector2.up;
                _npcAnimations = animations.right;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeToNextSpawn <= 0)
        {
            SpawnNPC();

            _timeToNextSpawn = Random.Range(minSpawnInterval, maxSpawnInterval);
        }

        _timeToNextSpawn -= Time.deltaTime;
    }

    private void SpawnNPC()
    {
        MovingNPC npc = Instantiate(movingNPCPrefab, transform.position, Quaternion.identity).GetComponent<MovingNPC>();

        if (npc == null) { throw new System.Exception("NPC prefab does not have a MovingNPC component. Add one or use a different prefab"); }

        npc.Initialize(Random.Range(npcMinSpeed, npcMaxSpeed), _directionVector * Random.Range(npcMinOffset, npcMaxOffset), direction, _npcAnimations[Random.Range(0, _npcAnimations.Length)]);
    }
}
