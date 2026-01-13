using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform player;
    private void OnEnable()
    {
        player.transform.position = transform.position;
    }
}
