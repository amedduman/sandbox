using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CubeDemo
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] DamageButtonPressedEvent _damageButtonPressedEvent;
        [SerializeField] CubeDeadEvent _cubeDeadEvent;
        [SerializeField] CubeHealth _cubeHealth;
        void OnEnable()
        {
            _damageButtonPressedEvent.AddListener(OnDamageButtonPressed, 1);
        }

        void OnDisable()
        {
            _damageButtonPressedEvent.RemoveListener(OnDamageButtonPressed);
        }

        async UniTask OnDamageButtonPressed(Empty empty)
        {
            Damage(10);
            await UniTask.Delay(TimeSpan.FromSeconds(.5f), ignoreTimeScale: true);
        }

        void Damage(int damageAmount)
        {
            _cubeHealth.Value.CurrentHealth -= damageAmount;
            _cubeHealth.Value.CurrentHealth =
                Mathf.Clamp(_cubeHealth.Value.CurrentHealth, 0, _cubeHealth.Value.MaxHealth);

            transform.DOShakePosition(.5f, new Vector3(0, 1, 0)).OnComplete(() =>
            {
                if (_cubeHealth.Value.CurrentHealth == 0)
                {
                    _cubeDeadEvent.Invoke();
                }
            });
            
        }
    }
}