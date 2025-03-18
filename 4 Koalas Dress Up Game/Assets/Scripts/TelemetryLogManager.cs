using System;
using System.Collections;
using System.Collections.Generic;
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

    protected override void Initialize()
    {
        base.Initialize();

        DontDestroyOnLoad(gameObject);

        _gameStartTime = Time.time;
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
                    success = PlayerManager.Instance.discoScore / PlayerManager.Instance.discoMaxScore >= 0.5f,
                    score = PlayerManager.Instance.discoScore
                };

                TelemetryLogger.Log(sender, "Microgame Complete", mcData);
                break;
        }
    }

    public void LogPlayerPosition(Component sender, Vector2 position)
    {
        TelemetryLogger.Log(sender, "Player Position", position);
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
}
