using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject       PlayerPrefab;
    [SerializeField]
    private CameraController cameraController;

    private void Awake()
    {
        GameObject player = Instantiate(PlayerPrefab, transform);
        cameraController.Setup(player);
    }
}
