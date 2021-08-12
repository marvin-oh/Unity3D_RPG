using System.Collections;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[]     playerList;
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
                foreach ( GameObject player in playerList )
                {
                    if ( !player.activeSelf )
                    {
                        player.transform.position = spawnArea.position;
                        player.SetActive(true);
                        cameraController.Setup(player);
                        break;
                    }
                }
            }

            yield return null;
        }
    }
}
