using System.Collections;
using UnityEngine;

public class SingleObjectSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject prefab;
    public Transform spawnPoint;
    public float checkInterval = 5f;

    private GameObject currentObject;
    private Coroutine checkRoutine;

    // --------------------
    // LIFECYCLE
    // --------------------

    private void OnEnable()
    {
        RestartSpawner();
    }

    private void OnDisable()
    {
        if (checkRoutine != null)
            StopCoroutine(checkRoutine);
    }

    // --------------------
    // MAIN LOGIC
    // --------------------

    void RestartSpawner()
    {
        // удаляем старый объект если есть
        if (currentObject != null)
            Destroy(currentObject);

        Spawn();

        // перезапускаем проверку
        if (checkRoutine != null)
            StopCoroutine(checkRoutine);

        checkRoutine = StartCoroutine(CheckLoop());
    }

    IEnumerator CheckLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            if (currentObject == null)
            {
                Spawn();
            }
        }
    }

    // --------------------
    // SPAWN
    // --------------------

    void Spawn()
    {
        if (!prefab)
            return;

        Vector3 pos = spawnPoint ? spawnPoint.position : transform.position;

        currentObject = Instantiate(prefab, pos, Quaternion.identity, transform);
    }
}
