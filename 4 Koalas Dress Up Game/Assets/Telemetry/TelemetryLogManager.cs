using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class TelemetryLogManager : Singleton<TelemetryLogManager>
{
    public enum EventType
    {
        Interaction,
        MicrogameStart,
        MicrogameComplete
    }

    private float _gameStartTime;
    private int _microgameAttempts = 0;

    private KeyCode[] _allKeyCodesArray;

    protected override void Initialize()
    {
        _allKeyCodesArray = (KeyCode[])Enum.GetValues(typeof(KeyCode));

        base.Initialize();

        DontDestroyOnLoad(gameObject);

        _gameStartTime = Time.time;
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode k in _allKeyCodesArray)
            {
                if (Input.GetKeyDown(k)) { LogPlayerInput(this, k.ToString()); }
            }
        }
    }

    public void GameStart()
    {
        TelemetryLogger.Log(this, "Game Started");
        _gameStartTime = Time.time;
    }

    public void GameEnd()
    {
        TelemetryLogger.Log(this, "Game Ended");
    }

    public void LogEvent(Component sender, EventType eventType)
    {
        switch (eventType)
        {
            case EventType.Interaction:
                InteractionData iData = new InteractionData()
                {
                    gameTime = Time.time - _gameStartTime,
                    interactionName = sender.transform.parent.name
                };

                TelemetryLogger.Log(sender, "Interaction", iData);
                break;

            case EventType.MicrogameStart:
                MicrogameStartData msData = new MicrogameStartData()
                {
                    gameTime = Time.time - _gameStartTime,
                    attemptNumber = ++_microgameAttempts
                };

                TelemetryLogger.Log(sender, "Microgame Start", msData);
                break;

            case EventType.MicrogameComplete:
                MicrogameCompleteData mcData = new MicrogameCompleteData()
                {
                    gameTime = Time.time - _gameStartTime,
                    success = (float)PlayerManager.Instance.discoScore / PlayerManager.Instance.discoMaxScore >= 0.5f,
                    score = PlayerManager.Instance.discoScore
                };

                TelemetryLogger.Log(sender, "Microgame Complete", mcData);
                break;
        }
    }

    public void LogPlayerPosition(Component sender, Vector2 position)
    {
        PlayerPositionData pData = new PlayerPositionData()
        {
            gameTime = Time.time - _gameStartTime,
            position = position
        };

        TelemetryLogger.Log(sender, "Player Position", pData);
    }

    public void LogPlayerInput(Component sender, string keyName)
    {
        PlayerInputData iData = new PlayerInputData()
        {
            gameTime = Time.time - _gameStartTime,
            keyName = keyName
        };

        TelemetryLogger.Log(sender, "Player Input", iData);
    }

    [Serializable]
    private struct InteractionData
    {
        public float gameTime;
        public string interactionName;
    }

    [Serializable]
    private struct MicrogameStartData
    {
        public float gameTime;
        public int attemptNumber;
    }

    [Serializable]
    private struct MicrogameCompleteData
    {
        public float gameTime;
        public bool success;
        public int score;
    }

    [Serializable]
    private struct PlayerPositionData
    {
        public float gameTime;
        public Vector2 position;
    }

    [Serializable]
    private struct PlayerInputData
    {
        public float gameTime;
        public string keyName;
    }
}
