using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;

    private List<Item> items = new List<Item>();
    private List<Slot> slots = new List<Slot>();
    private string     filePath;

    private void Awake() => filePath = Application.dataPath + "/Resources/inventory.txt";

    private void OnEnable()
    {
        Cursor.visible   = true;                     // 마우스 커서를 보이지 않게
        Cursor.lockState = CursorLockMode.Confined;  // 마우스 커서 위치 고정

        Load();

        GameObject content   = GetComponentInChildren<VerticalLayoutGroup>().gameObject;
        float width          = content.GetComponent<RectTransform>().rect.width;
        float height         = 0;
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);

        foreach ( Item item in items )
        {
            GameObject slotClone = Instantiate(slotPrefab, content.transform);
            Slot slotComponent   = slotClone.GetComponent<Slot>();
            slotComponent.Item   = item;
            slots.Add(slotComponent);

            height = content.GetComponent<RectTransform>().rect.height + slotClone.GetComponent<RectTransform>().rect.height + 3f;
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        }
    }

    public void OnDisable()
    {
        Cursor.visible   = false;                     // 마우스 커서를 보이지 않게
        Cursor.lockState = CursorLockMode.Locked;     // 마우스 커서 위치 고정

        foreach ( Slot slot in slots ) { Destroy(slot.gameObject); }

        slots.Clear();
    }

    private void Load()
    {
        items.Clear();

        if ( !File.Exists(filePath) )
        {
            items.Add(new Item("체력물약", 1, "소비시 체력을 100 회복한다.", 0));
            items.Add(new Item("마나물약", 10, "소비시 마나를 50 회복한다.", 1));
            Save();
        }
        //string code     = File.ReadAllText(filePath);
        //byte[] bytes    = System.Convert.FromBase64String(code);
        //string jsonData = System.Text.Encoding.UTF8.GetString(bytes);
        string jsonData = File.ReadAllText(filePath);
        items           = JsonUtility.FromJson<Serialization<Item>>(jsonData).target;
    }

    private void Save()
    {
        string jsonData = JsonUtility.ToJson(new Serialization<Item>(items));
        //byte[] bytes    = System.Text.Encoding.UTF8.GetBytes(jsonData);
        //string code     = System.Convert.ToBase64String(bytes);
        //File.WriteAllText(filePath, code);
        File.WriteAllText(filePath, jsonData);
    }
}
