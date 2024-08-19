using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatableSlime : MonoBehaviour, IEatable
{
    [SerializeField] private AIController _controller;
    private bool _eaten = false;

    public bool IsTriggered => throw new System.NotImplementedException();

    public eSize size => throw new System.NotImplementedException();
     
    public void OnAte()
    {
        if (PlayerScaleController.Instance.SlimeSize.magnitude >= _controller.SlimeSize.magnitude && !_eaten)
        {
            _eaten = true;
            _controller.IsAttackable = false;
            EnemyManager.Instance.ActivateEnemiesAttack();

            _controller.transform.DOMove(_controller.TargetPlayer.transform.position, 0.5f);
            _controller.transform.DOScale(Vector3.zero, 0.3f)
            .OnComplete(() =>
            {
                PlayerScaleController.Instance.IncreaseEatenSlimeCount();
                _controller.gameObject.SetActive(false);
            });
        }
    }
}
