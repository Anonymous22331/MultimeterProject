using System;
using UnityEngine;

public class MultimeterModel : MonoBehaviour
{
    public MultimeterState State { get; private set; } = MultimeterState.Neutral;
    public event Action<MultimeterState, float, float, float, float> OnStateChanged;

    private const float Resistance = 1000f;
    private const float Power = 400f;

    public void SetState(MultimeterState newState)
    {
        if (State == newState) return;

        State = newState;
        float DCVoltage = Mathf.Sqrt(Power * Resistance);
        float current = DCVoltage / Resistance;
        float ACVoltage = 0.01f;

        OnStateChanged?.Invoke(State, DCVoltage, current, Resistance, ACVoltage);
    }
}

public enum MultimeterState
{
    Neutral,
    Current,
    Resistance,
    ACVoltage,
    DCVoltage
}