using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SymbolTargetSetter : MonoBehaviour
{
    [SerializeField]
    List<GameSymbolRevealerTarget> symbols;

    [SerializeField]
    CircleCollider2D circleCollider2D;

    [SerializeField]
    SymbolTargetPoint targetPrefab;

    float anglePerPoint;

    List<SymbolTargetPoint> targetPoints = new List<SymbolTargetPoint>();


    // Start is called before the first frame update
    void Start()
    {
        anglePerPoint = 360.0f / GameManager.Manager.CurrentMath.TotalSymbols;
        SpawnSpots();
    }

    void SpawnSpots()
    {
        for (int i = 0; i < GameManager.Manager.CurrentMath.TotalSymbols; i++)
        {
            Vector3 initialVector = Vector3.up * circleCollider2D.radius;
            var newTarget = Instantiate(targetPrefab, transform);
            newTarget.transform.localPosition = Quaternion.Euler(0, 0, anglePerPoint * i) * initialVector;
            targetPoints.Add(newTarget);
        }

        for (int i = 0; i < symbols.Count; i++)
        {
            symbols[i].TargetPoint = targetPoints[i];
            symbols[i].OriginPosition = transform;
        }
    }
}
