using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Database : MonoBehaviour
{
    public List<Level> allLevels;

    [SerializeField] private Transform map;
    public int currentLevel = 0;
    public Text playerHealthText;
    private int allLevelsNumber;
    public GameObject bossLevel;
    public GameObject bonusLevel;
    public GameObject oldGround;
    public GameObject bonusGround;
    public GameObject playerGo;

    private void Start()
    {
        PlayerPrefs.SetInt("Level", currentLevel);
        allLevelsNumber = allLevels.Count;
    }

    private void Update()
    {
        playerHealthText.text = PlayerPrefs.GetInt("Life").ToString();
    }

    public void GetLevel()
    {
        EndlessLevel();

        foreach(Transform child in map.transform)
        {
            Destroy(child.gameObject);
        }

        if (allLevels[currentLevel].bossLevel == true)
        {
            StartCoroutine(WaitLevelLoad());
            map.DOMove(new Vector3(4, 0, 0), 3 * 0.5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        }
        else if (allLevels[currentLevel].bonusLevel == true)
        {
            playerGo.SetActive(false);
            map.DOPause();
            oldGround.SetActive(false);
            bonusGround.SetActive(true);
            StartCoroutine(WaitBonusLevelLoad());
        }
        else
        {
            playerGo.SetActive(true);
            map.DOPause();
            bonusGround.SetActive(false);
            oldGround.SetActive(true);
        }

        PlayerPrefs.SetInt("Life", allLevels[currentLevel].playerLife);
        allLevels[currentLevel].LoadLevel();
    }

    IEnumerator WaitLevelLoad()
    {
        bossLevel.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        bossLevel.SetActive(false);
    }

    IEnumerator WaitBonusLevelLoad()
    {
        bonusLevel.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        bonusLevel.SetActive(false);
    }

    private void EndlessLevel()
    {
        if (currentLevel == allLevelsNumber)
        {
            currentLevel = 0;
        }
    }
}
