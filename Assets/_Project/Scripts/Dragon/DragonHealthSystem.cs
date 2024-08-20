using UnityEngine;
using System.Collections; 

public class DragonHealthSystem : MonoBehaviour
{
    [SerializeField] private int _hearthCount;
    [SerializeField] private Renderer _renderer;
    private float _blinkDuration = 0.1f;
    private int _blinkCount = 5;

    public void Damage(int value)
    {
        _hearthCount -= value;
        StartCoroutine(Blink());

        Debug.Log("Dragon hasar aldý!!!!!! ");

        if (_hearthCount <= 0)
            Destroy(transform.parent.gameObject);
    }
    
    private IEnumerator Blink()
    {
        for (int i = 0; i < _blinkCount; i++)
        {
            _renderer.enabled = false;
            yield return new WaitForSeconds(_blinkDuration);
            _renderer.enabled = true;
            yield return new WaitForSeconds(_blinkDuration);
        }
    }
}