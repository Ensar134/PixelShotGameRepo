using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusMatScript : MonoBehaviour
{
    Renderer rend;
    Rigidbody rb;
    private int endGame = 0;
    public ParticleSystem rainbowParticle;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        ObjectPooler.Instance.SpawnFromPoolSecond("ConfettiParticle", new Vector3(0, 0, 0), Quaternion.identity);
    }

    private void OnMouseDown()
    {
        endGame += 1;
        rb.AddForce(-Vector3.right * 1300f);

        if (endGame == 4)
        {
            ObjectPooler.Instance.SpawnFromPoolSecond("RainbowParticle", transform.position, Quaternion.identity);
            gameObject.SetActive(false);

            for (int i = 0; i <= 50; i++)
            {
                GameObject diamons = ObjectPooler.Instance.SpawnFromPool("DiamondCube", gameObject.transform.position, Quaternion.Euler(0f,0f,90f));
                diamons.GetComponent<Rigidbody>().AddRelativeForce(Random.onUnitSphere * 2f);
            }
        }
    }

    private void Update()
    {
        rend.material.mainTextureOffset += new Vector2(0f, -0.7f * Time.deltaTime);
    }
}
