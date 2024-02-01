using UnityEngine;

public class PartScripts : MonoBehaviour
{
    public PartData pt;

    public GameObject Part;
    public GUIStyle style;

    [HideInInspector]
    public int Row, Column;

    public Collider cubeCollider;
    public Rigidbody rb;
    public GameObject glassParticle;

    private void OnEnable()
    {
        Reset();
    }

    private void OnCollisionEnter(Collision col)
    {
        var obj = col.transform.GetComponent<DestroyObjectsScript>();
        if (obj)
        {
            return;
        }

        if(pt.partName == "Red" || pt.partName == "Blue")
        {
            rb.useGravity = true;
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        if (pt.partName == "Glass")
        {
            gameObject.SetActive(false);
            ObjectPooler.Instance.SpawnFromPoolSecond("GlassParticle", col.gameObject.transform.position, Quaternion.identity);
        }
    }

    private void Reset()
    {
        gameObject.GetComponent<Renderer>().material.color = pt.defaultColor;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = false;
    }

    public void ReturnPool()
    {
        ObjectPooler.Instance.ReturnPool(pt.poolName, gameObject);
    }
}
