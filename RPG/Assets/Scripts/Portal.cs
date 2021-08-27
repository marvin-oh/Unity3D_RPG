using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private string portalName;
    [SerializeField] private string portalDesc;
    [SerializeField] private int    price;

    public string Name  => portalName;
    public string Desc  => portalDesc;
    public int    Price => price;
    public Vector3 Location => transform.position + new Vector3(0, 2, 0);


    private void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("Player") )
        {
            GameManager.Instance.PortalPanel.SetActive(true);
            GameManager.Instance.PortalPanel.GetComponent<PortalPanel>().Setup(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.CompareTag("Player") ) { GameManager.Instance.PortalPanel.SetActive(false); }
    }
}
