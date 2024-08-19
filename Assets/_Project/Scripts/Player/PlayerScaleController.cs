using System;
using System.Drawing;
using DG.Tweening;
using UnityEngine;

public class PlayerScaleController : MonoBehaviour
{
    public static PlayerScaleController Instance;

    private const int MAX_EATEN_COUNT = 2;
    
    public eSize SlimeSize;
    
    private float[] allSlimeScale = new float[] {0.2f, 0.4f, 0.6f, 0.8f, 1.1f, 1.3f};
    private int currentScaleIndex;

    public float GetScaleByEnum(eSize size)
    {
        return allSlimeScale[(int)size];
    }
    
    private PlayerController _playerController;
    private int currentEatenSlimeCount;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        currentScaleIndex = 0;
        currentEatenSlimeCount = 0;
        SetScale();
    }

    public void IncreaseEatenSlimeCount()
    {
        currentEatenSlimeCount++;
        CheckScale();
    }

    private void CheckScale()
    {
        if (currentEatenSlimeCount == MAX_EATEN_COUNT)
        {
            currentScaleIndex++;

            if (currentScaleIndex > allSlimeScale.Length)
                return;

            currentEatenSlimeCount = 0;
            SetScale();
        }
    }
   
    private void SetScale()
    {
        if (currentScaleIndex >= Enum.GetValues(typeof(eSize)).Length) return;
        
        SlimeSize = (eSize)currentScaleIndex;
        
        float getScale = GetScaleByEnum(SlimeSize);
        Vector3 newScale = Vector3.one * getScale;
        
        if (currentScaleIndex != 0)
            _playerController.transform.DOScale(newScale, 0.5f);
        else
            _playerController.transform.localScale = newScale;
    }
}