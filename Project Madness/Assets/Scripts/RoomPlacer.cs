using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomPlacer : MonoBehaviour
{
    public GameObject[] RoomPrefabs;
    public GameObject StartingRoom;

    private GameObject[,] spawnedRooms;
    private void Start()
    {
        spawnedRooms = new GameObject[11, 11];
        spawnedRooms[5, 5] = StartingRoom;

        for (int i = 0; i < 12; i++)
        {
            PlaceOneRoom();
        }
    }

    // Update is called once per frame
    private void PlaceOneRoom()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;

                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;

                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));

            }
        }

        GameObject newRoom = Instantiate(RoomPrefabs[Random.Range(0, RoomPrefabs.Length)]);
        Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
        newRoom.transform.position = new Vector2(position.x, position.y) * 11;

        spawnedRooms[position.x, position.y] = newRoom;
    }
}
