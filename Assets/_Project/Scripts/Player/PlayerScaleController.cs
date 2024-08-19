using DG.Tweening;
using UnityEngine;

public class PlayerScaleController : MonoBehaviour
{
    public static PlayerScaleController Instance;

    private const int MAX_EATEN_COUNT = 2;
    
    public Vector3 SlimeSize;
    
    private float[] allSlimeScale = new float[] {0.2f, 0.4f, 0.6f, 0.8f, 1.1f, 1.3f};
    private int currentScaleIndex;
    
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

        Debug.Log(currentEatenSlimeCount);

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
        SlimeSize = Vector3.one * allSlimeScale[currentScaleIndex];
        
        if (currentScaleIndex != 0)
            _playerController.transform.DOScale(SlimeSize, 0.5f);
        else
            _playerController.transform.localScale = SlimeSize;
    }
}