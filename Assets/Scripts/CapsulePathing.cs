using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsulePathing : MonoBehaviour
{
    [SerializeField]
    CapsuleCollider2D capsule;

    [SerializeField]
    int spawnPositions;

    [SerializeField]
    float secondsBetweenLoops;

    [SerializeField]
    GameObject toSpawn;

    Vector3 upLeft, upRight, downRight, downLeft;

    GameObject mover;

    float elapsed = 0;

    float radius = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetPositions();
    }

    // Update is called once per frame
    void Update()
    {
        if (mover)
        {
            elapsed += Time.deltaTime;
            mover.transform.position = GetPosition((elapsed % secondsBetweenLoops) / secondsBetweenLoops);
        }
    }

    void SetPositions()
    {
        Debug.LogWarning(capsule.bounds.extents);
        upLeft = new Vector3(-capsule.bounds.extents.x / 2, capsule.bounds.extents.y) + capsule.bounds.center;
        upRight = new Vector3(capsule.bounds.extents.x / 2, capsule.bounds.extents.y) + capsule.bounds.center;
        downLeft = new Vector3(-capsule.bounds.extents.x / 2, -capsule.bounds.extents.y) + capsule.bounds.center;
        downRight = new Vector3(capsule.bounds.extents.x / 2, -capsule.bounds.extents.y) + capsule.bounds.center;
        Instantiate(toSpawn, upLeft, Quaternion.identity, transform);
        Instantiate(toSpawn, upRight, Quaternion.identity, transform);
        Instantiate(toSpawn, downLeft, Quaternion.identity, transform);
        Instantiate(toSpawn, downRight, Quaternion.identity, transform);
        mover = Instantiate(toSpawn, downRight, Quaternion.identity, transform);
        radius = capsule.bounds.extents.y;
    }

    Vector3 GetPosition(float progress)
    {
        //Debug.LogWarning(progress);
        if (progress <= .25)
        {
            float tempProgress = progress / .25f;
            return Vector3.Lerp(upLeft, upRight, tempProgress);
        }
        else if (progress <= .5)
        {
            float tempProgress = (progress - .25f) / .25f;
            return Vector3.Slerp(upRight, downRight, tempProgress) + new Vector3(radius / 1.5f * Mathf.Sin(Mathf.PI * tempProgress), 0, 0);
        }
        else if (progress <= .75)
        {
            float tempProgress = (progress - .5f) / .25f;
            return Vector3.Lerp(downRight, downLeft, tempProgress);
        }
        else
        {
            float tempProgress = (progress - .75f) / .25f;
            return Vector3.Slerp(downLeft, upLeft, tempProgress) + new Vector3(-radius / 1.5f * Mathf.Sin(Mathf.PI * tempProgress), 0, 0);
        }
    }
}
