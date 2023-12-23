using CubeDemo;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image _fillImage;
    [SerializeField] DamageButtonPressedEvent _damageButtonPressedEvent;
    [SerializeField] CubeHealth _cubeHealth;
    
    void Awake()
    {
        UpdateHealthBar(GetHealthBarValue());
    }
    
    void OnEnable()
    {
        _damageButtonPressedEvent.AddListener(OnCubeDamaged, 2);
    }

    void OnDisable()
    {
        _damageButtonPressedEvent.RemoveListener(OnCubeDamaged);
    }
    
    async UniTask OnCubeDamaged(Empty empty)
    {
        UpdateHealthBar(GetHealthBarValue());
        await UniTask.NextFrame();
    }
    
    void UpdateHealthBar(float value)
    {
        _fillImage.transform.localScale = new Vector3(value, 1, 1);
    }

    float GetHealthBarValue()
    {
        return (float) _cubeHealth.Value.CurrentHealth / _cubeHealth.Value.MaxHealth;
    }
}
