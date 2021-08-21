using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }
    private void Awake() => Instance = this;

    [SerializeField] private Text noticeText;
    private bool         onNotice;
    private List<string> buffer = new List<string>();


    public void Notice(string message) => StartCoroutine(NoticeWithBuffer(message));

    public IEnumerator NoticeWithBuffer(string message)
    {
        buffer.Add(message);

        if ( !onNotice )
        {
            onNotice = true;

            while ( buffer.Count > 0 )
            {
                noticeText.text = buffer[0];
                buffer.RemoveAt(0);

                yield return new WaitForSeconds(1);

                noticeText.text = "";
            }

            onNotice = false;
        }
    }
}
