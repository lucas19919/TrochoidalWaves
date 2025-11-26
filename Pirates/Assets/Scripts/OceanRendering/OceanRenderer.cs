using UnityEngine;
using System.Collections.Generic;

public class OceanRenderer : MonoBehaviour
{
    [Header("Settings")]
    public Transform ship;
    public Transform oceanSegment;

    [SerializeField] int renderDistance = 5;
    [SerializeField] float tileSize = 6f;

    private List<Transform> activeSegments = new List<Transform>();
    private Vector2Int currentGridCenter = new Vector2Int(int.MaxValue, int.MaxValue);

    private void Start()
    {
        InitializeGrid();
    }

    private void Update()
    {
        if (ship == null) return;

        UpdateGridPositions();
    }

    void InitializeGrid()
    {
        int sideLength = (renderDistance * 2) + 1;

        for (int x = 0; x < sideLength; x++)
        {
            for (int z = 0; z < sideLength; z++)
            {
                Transform newSegment = Instantiate(oceanSegment, Vector3.zero, Quaternion.identity);
                newSegment.parent = this.transform;
                activeSegments.Add(newSegment);
            }
        }
    }

    void UpdateGridPositions()
    {
        int shipGridX = Mathf.RoundToInt(ship.position.x / tileSize);
        int shipGridZ = Mathf.RoundToInt(ship.position.z / tileSize);

        if (shipGridX == currentGridCenter.x && shipGridZ == currentGridCenter.y)
            return;

        currentGridCenter = new Vector2Int(shipGridX, shipGridZ);

        int listIndex = 0;

        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int z = -renderDistance; z <= renderDistance; z++)
            {
                Vector3 targetPos = new Vector3(
                    (shipGridX + x) * tileSize,
                    0,
                    (shipGridZ + z) * tileSize
                );

                if (listIndex < activeSegments.Count)
                {
                    activeSegments[listIndex].position = targetPos;
                    listIndex++;
                }
            }
        }
    }
}