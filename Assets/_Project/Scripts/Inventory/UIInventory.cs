using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public static UIInventory Instance;

    [SerializeField] Image _itemIcon;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddIcon(Item item)
    {
        _itemIcon.sprite = item.UIIcon;
    }

    public void RemoveIcon()
    {
        _itemIcon.sprite = null;
    }
}