using UnityEngine;
using DG.Tweening;
using System.Collections;

public class Item : MonoBehaviour, IEatable
{
    public Sprite UIIcon;
    public bool Eaten = false;

    private MeshRenderer _meshRenderer;
    private Vector3 _defaultScale;


    public bool IsTriggered => throw new System.NotImplementedException();
    public eSize Size => throw new System.NotImplementedException();

    private void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _defaultScale = transform.localScale;
    }

    public void OnAte()
    {
        if (Inventory.Instance.CheckCapacity() && !Eaten)
        {
            transform.DOScale(Vector3.zero, 0.5f)
            .OnComplete(() =>
            {
                StartCoroutine(Ate());
            });
        }
    }

    private IEnumerator Ate()
    {
        _meshRenderer.enabled = false;
        Eaten = true;
        Inventory.Instance.AddItem(this);

        yield return new WaitForSeconds(5f);
        
        _meshRenderer.enabled = true;

        transform.DOScale(_defaultScale, 0.5f)
        .OnComplete(() =>
        {
            Eaten = false;
        });
    }
}
