using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerList;
    [SerializeField] private int              spawnCount = 1;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Transform        spawnArea;

    private void Awake()
    {
        StartCoroutine("Spawn");
    }

    private IEnumerator Spawn()
    {
        while ( true )
        {
            // 일정 수에 도달하면 생성x
            if ( FindObjectsOfType<Player>().Length < spawnCount )
            {
                // playerList에서 비활성화된 player중 하나 활성화
                GameObject player = playerList.Find(x => !x.activeSelf);
                player.transform.position = spawnArea.position;
                player.SetActive(true);
                cameraController.Setup(player);
            }

            yield return null;
        }
    }
}
