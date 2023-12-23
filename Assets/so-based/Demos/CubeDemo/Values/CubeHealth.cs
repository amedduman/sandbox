using UnityEngine;

[CreateAssetMenu]
public class CubeHealth : ValueSO<PlayerHealthData> { }

[System.Serializable]
public struct PlayerHealthData
{
    public int MaxHealth;
    public int CurrentHealth;
}