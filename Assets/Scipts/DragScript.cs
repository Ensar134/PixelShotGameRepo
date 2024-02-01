using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class DragScript : MonoBehaviour
{
    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;

    private Rigidbody rb;
    private SphereCollider col;

    private bool isShoot;
    private float forceMultiplier = 5.7f;
    public bool isShootable = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePressDownPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 forceInit = (Input.mousePosition - mousePressDownPos);

            if (forceInit.x > 160)
            {
                forceInit.x = 160;
            }
            if (forceInit.x < -160)
            {
                forceInit.x = -160;
            }
            if (forceInit.y > 0)
            {
                forceInit.y = 0;
            }
            if (forceInit.y < -160)
            {
                forceInit.y = -160;
            }


            Vector3 forceV = (new Vector3(forceInit.y, forceInit.z, -forceInit.x)) * forceMultiplier;

            if (!isShoot)
                DrawTrajectory.Instance.UpdateTrajectory(forceV, rb, transform.position);
        }
        if (Input.GetMouseButtonUp(0))
        {
            rb.useGravity = true;
            col.isTrigger = false;
            DrawTrajectory.Instance.HideLine();
            mouseReleasePos = Input.mousePosition;

            Vector3 shootForce = mouseReleasePos - mousePressDownPos;

            //switch case
            if (shootForce.x > 160)
            {
                shootForce.x = 160;
            }
            if (shootForce.x < -160)
            {
                shootForce.x = -160;
            }
            if (shootForce.y > 0)
            {
                shootForce.y = 0;
            }
            if (shootForce.y < -160)
            {
                shootForce.y = -160;
            }

            Shoot(shootForce);
        }            
    }
    
    void Shoot(Vector3 Force)
    {
        if (isShoot)
            return;
         
        rb.AddForce(new Vector3(Force.y, Force.z, -Force.x) * forceMultiplier);
        isShoot = true;

        Spawner.Instance.NewSpawnRequest();
    }
}