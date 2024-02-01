using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Level")]
public class Level : ScriptableObject
{
    public List<Vector3> redCubePositions = new List<Vector3>();
    public List<Vector3> blueCubePositions = new List<Vector3>();
    public List<Vector3> obstaclePositions = new List<Vector3>();
    public List<Vector3> glassCubePositions = new List<Vector3>();
    public List<string> cubeList = new List<string>();
    public GameObject redCube;
    public GameObject blueCube;
    public GameObject obstacles;
    public GameObject glassCube;
    public int playerLife;
    public bool bossLevel = false;
    public bool bonusLevel = false;
    public GameObject bonusLevelPrefab;

    private int objCount;
    private GameObject map;

    public void SaveLevel()
    {
        map = GameObject.Find("Map");
        objCount = map.transform.childCount;

        for (int i = 0; i < objCount; i++)
        {
            if (map.transform.GetChild(i).name == "Red")
            {
                redCubePositions.Add(map.transform.GetChild(i).transform.position);
                cubeList.Add("Red");
            }

            if (map.transform.GetChild(i).name == "Blue")
            {
                blueCubePositions.Add(map.transform.GetChild(i).transform.position);
                cubeList.Add("Blue");
            }

            if (map.transform.GetChild(i).name == "Obstacle")
            {
                obstaclePositions.Add(map.transform.GetChild(i).transform.position);
            }

            if (map.transform.GetChild(i).name == "Glass")
            {
                glassCubePositions.Add(map.transform.GetChild(i).transform.position);
            }

            cubeList.Add(map.transform.GetChild(i).name);
        }
    }

    public void LoadLevel()
    {
        map = GameObject.Find("Map");

        if (map.transform.childCount != 0)
        {
            foreach(Transform child in map.transform)
            {                
                child.gameObject.SetActive(false);
                map.transform.DetachChildren();
            }

            if (obstacles.activeInHierarchy)
            {
                obstacles.SetActive(false);
            }
        }

        if(bonusLevel == true)
        {
            ObjectPooler.Instance.SpawnFromPool("BonusLevel", new Vector3(3.3f, -1f, 10f), Quaternion.identity);
        }

        for (int i = 0; i < redCubePositions.Count; i++)
        {
            ObjectPooler.Instance.SpawnFromPool("RedCube", redCubePositions[i], Quaternion.identity);
        }

        for (int i = 0; i < blueCubePositions.Count; i++)
        {
            ObjectPooler.Instance.SpawnFromPool("BlueCube", blueCubePositions[i], Quaternion.identity);
        }

        for (int i = 0; i < obstaclePositions.Count; i++)
        {
            ObjectPooler.Instance.SpawnFromPoolThird("Obstacle", obstaclePositions[i], Quaternion.identity);
        }

        for (int i = 0; i < glassCubePositions.Count; i++)
        {
            ObjectPooler.Instance.SpawnFromPoolThird("GlassCube", glassCubePositions[i], Quaternion.identity);
        }
    }
}