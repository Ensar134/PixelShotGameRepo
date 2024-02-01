using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections;
using MoreMountains.NiceVibrations;

public class DestroyObjectsScript : MonoBehaviour
{
    public GameObject map;
    public int inactiveObj = 0;
    public Slider slider;
    public int totalObjects = 0;
    public int coin;
    public int highScore;
    private int life;
    private int calculate = 0;
    public GameObject coinPopUpText;
    public GameObject playerLifePopUpText;
    public GameObject redParticle;
    public GameObject blueParticle;

    private void Start()
    {
        coin = PlayerPrefs.GetInt("Coin");
    }

    private void EndLevel()
    {
        totalObjects = map.transform.childCount;

        for (int i = 0; i < map.transform.childCount; i++)
        {
            if (map.transform.GetChild(i).gameObject.CompareTag("Glass"))
            {
                totalObjects -= 1;
            }
        }

        slider.maxValue = totalObjects;
        slider.value = inactiveObj;
    }

    private void Update()
    {
        EndLevel();
        PlayerPrefs.SetInt("Coin", coin);
    }

    IEnumerator WaitWinScreen()
    {
        yield return new WaitForSecondsRealtime(5);
    }

    IEnumerator WaitAnimationsForWin()
    {
        yield return new WaitForSecondsRealtime(2);
    }

    IEnumerator WaitLifeAnim()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        ExtraLifeAnim();
    }

    private void OnCollisionEnter(Collision collision)
    {
        life = PlayerPrefs.GetInt("Life");

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.DOScale(0f, 2f).SetEase(Ease.OutElastic);
            collision.gameObject.SetActive(false);
            PlayerPrefs.SetInt("Life", PlayerPrefs.GetInt("Life") - 1);
            life = PlayerPrefs.GetInt("Life");

            if (life == 0)
            {
                CanvasManager.instance.ChangeMenu(Panel.Lose);
            }
        }

        else if (collision.gameObject.CompareTag("Diamond"))
        {
            collision.gameObject.SetActive(false);
            calculate++;
            if (calculate == 50)
            {
                ExtraLifeAnim();
            }
        }

        else
        {
            coin = PlayerPrefs.GetInt("Coin");
            inactiveObj++;
            MMVibrationManager.StopAllHaptics(true);
            MMVibrationManager.Haptic(HapticTypes.RigidImpact);
            coin += 10;
            PlayerPrefs.SetInt("Coin", coin);
            collision.gameObject.SetActive(false);

            ObjectPooler.Instance.SpawnFromPoolSecond("CoinAnimation", collision.gameObject.transform.position, Quaternion.identity);

            string particleName = collision.transform.GetComponent<PartScripts>().pt.particleName;
            ObjectPooler.Instance.SpawnFromPoolSecond(particleName, collision.gameObject.transform.position, Quaternion.identity);       
        }

        if (inactiveObj == totalObjects)
        {
            StartCoroutine(WaitWinScreen());

            inactiveObj = 0;
            slider.value = 0;

            coin = PlayerPrefs.GetInt("Coin");
            highScore = coin + 5000;
            PlayerPrefs.SetInt("Score", highScore);

            StartCoroutine(WaitLifeAnim());
        }
    }

    private void ExtraLifeAnim()
    {
        calculate = 0;

        if (life != 0)
        {
            ObjectPooler.Instance.SpawnFromPoolSecond("LifePointAnimation", new Vector3(21.6f, 1f, 5.9f) , Quaternion.identity);

            life = PlayerPrefs.GetInt("Life");
            life -= 1;
            PlayerPrefs.SetInt("Life", life);
            coin = PlayerPrefs.GetInt("Coin");
            coin += 2000;

            StartCoroutine(WaitLifeAnim());

            if(life == 0)
            {
                StartCoroutine(WaitAnimationsForWin());
                CanvasManager.instance.ChangeMenu(Panel.Win);
            }
        }

        
    }
}
