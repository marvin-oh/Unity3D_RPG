using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    private Item item;
    private Rect rect;


    private void Awake()    => rect = GetComponent<RectTransform>().rect;

    private void Update()   => transform.position = Input.mousePosition + new Vector3(rect.width/2, rect.height/2, 0);

    private void OnEnable() => GetComponentInChildren<Text>().text = item.desc;


    /// <summary>
    /// 활성화시 인자로 받은 Item을 설정
    /// </summary>
    public void Show(Item _item)
    {
        item = _item;

        gameObject.SetActive(true);
    }
}
