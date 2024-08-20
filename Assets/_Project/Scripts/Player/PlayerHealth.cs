using System;
using System.Collections;
using Test;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    public PlayerEaterTest eaterTest;

    public bool IsPlay = true;

    private float immunity = 0.5f;
    private float timer = 0;

    public EatableSlime lastHit = null;

    [SerializeField] private int hearthCount;
    [SerializeField] private Renderer _renderer;
    private float blinkDuration = 0.1f;
    private int blinkCount = 5;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void SetHearthCount(int count)
    {
        hearthCount = count;
    }

    public void Damage(int value)
    {
        hearthCount -= value;
        StartCoroutine(Blink());
        Debug.Log("Took Damage");
    }

    public void Die()
    {
        IsPlay = false;
    }

    private IEnumerator Blink()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            _renderer.enabled = false;
            yield return new WaitForSeconds(blinkDuration);
            _renderer.enabled = true;
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out EatableSlime eatable))
        {
            if ((int)eatable.Size <= (int)PlayerScaleController.Instance.SlimeSize) 
                return;
            
            lastHit = eatable;
            if (timer > immunity)
            {
                Damage(1);
                Debug.Log("Took Damage");
                timer = 0;
            }
        }
        else
        {
            lastHit = null;
        }
    }
}