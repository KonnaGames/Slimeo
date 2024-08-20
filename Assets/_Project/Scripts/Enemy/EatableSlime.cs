using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[Serializable]
public class EatableSlime : MonoBehaviour, IEatable
{
    [SerializeField] private AIController _controller;
    private bool _eaten = false;

    public bool IsTriggered { get; private set; }

    public eSize Size => _controller.SlimeSize;

    public void OnAte()
    {
        if (PlayerScaleController.Instance.SlimeSize >= _controller.SlimeSize && !_eaten)
        {
            _eaten = true;
            IsTriggered = true;
            _controller.IsAttackable = false;
            EnemyManager.Instance.ActivateEnemiesAttack();

            _controller.transform.DOMove(_controller.TargetPlayer.transform.position, 0.5f);
            _controller.transform.DOScale(Vector3.zero, 0.3f)
            .OnComplete(() =>
            {
                PlayerScaleController.Instance.IncreaseEatenSlimeCount();
                _controller.gameObject.SetActive(false);
            });
            _controller.IsDie= true;

            EnemyManager.Instance.CheckFightState();
            Debug.Log("Slime Yendi");
        }
    }
}
