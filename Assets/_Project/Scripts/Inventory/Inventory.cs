using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [SerializeField] private GameObject _spikePrefab;
    private const int CAPACITY = 1;
    private List<GameObject> collectables = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddItem(Item item)
    {
        if (CheckCapacity())
        {
            GameObject cloneItem = Instantiate(_spikePrefab,transform.parent);
            cloneItem.transform.position = Vector3.zero;
            collectables.Add(cloneItem);
            UIInventory.Instance.AddIcon(item);
        }
    }
 
    public GameObject GetItem()
    {
        if (!CheckCapacity())
        {
            var item = collectables[0];
            item.GetComponent<MeshRenderer>().enabled = true;
            RemoveItem(item);
            return item;
        }
        return null;
    }

    public bool CheckCapacity()
    {
        if (collectables.Count < CAPACITY)
            return true;
        return false;
    }

    private void RemoveItem(GameObject item)
    {
        collectables.Remove(item);
        UIInventory.Instance.RemoveIcon();
    }
}