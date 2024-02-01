using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{
    [SerializeField]
    private LineRenderer _lineRenderer;

    [SerializeField]
    [Range(5, 100)]
    private int _lineSegmentCount = 50;

    private List<Vector3> _linePoints = new List<Vector3>();

    #region Singleton

    public static DrawTrajectory Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidbody, Vector3 startingPoint)
    {
        Vector3 velocity = (forceVector / rigidbody.mass) * Time.fixedDeltaTime;
        float FlightDuration = (2 * velocity.x) / Physics.gravity.x;
        float stepTime = FlightDuration / _lineSegmentCount;

        _linePoints.Clear();
        _linePoints.Add(startingPoint);
          
        for(int i = 0; i < _lineSegmentCount - 20; i++)
        {
            float stepTimePassed = stepTime * i;

            Vector3 MovementVector = new Vector3(
                velocity.x * stepTimePassed - 0.5f * Physics.gravity.x * stepTimePassed * stepTimePassed,
                velocity.y * stepTimePassed,
                velocity.z * stepTimePassed
            );
            Vector3 NewPointOnLine = -MovementVector + startingPoint;

            if (Physics.Raycast(_linePoints[i], NewPointOnLine - _linePoints[i], out RaycastHit hit, (NewPointOnLine - _linePoints[i]).magnitude))
            {
                if (hit.transform.CompareTag("Cubes") || hit.transform.CompareTag("Glass"))
                {
                    _lineRenderer.SetColors(Color.green, Color.green);
                    break;
                }
                else
                {
                    _lineRenderer.SetColors(Color.grey,Color.grey);
                }

                Vector3 NewPointOnLine2 = Vector3.Reflect(NewPointOnLine, hit.normal);
                NewPointOnLine2.z += hit.point.z * 2;

                if (hit.transform.CompareTag("TopWall"))
                {
                    NewPointOnLine2.z = NewPointOnLine.z;
                }               

                if (Physics.Raycast(_linePoints[i], NewPointOnLine2 - _linePoints[i], out RaycastHit hit2, (NewPointOnLine2 - _linePoints[i]).magnitude))
                {
                    if (hit2.transform.CompareTag("Cubes") || hit2.transform.CompareTag("Glass"))
                    {
                        _lineRenderer.SetColors(Color.green, Color.green);
                        break;
                    }
                    else
                    {
                        _lineRenderer.SetColors(Color.grey, Color.grey);
                    }

                    Vector3 NewPointOnLine3 = Vector3.Reflect(NewPointOnLine2, hit2.normal);
                    NewPointOnLine3.z += hit2.point.z * 2;

                    if (hit2.transform.CompareTag("TopWall"))
                    {
                        NewPointOnLine3.z = hit2.point.z;
                    }

                    if (Physics.Raycast(_linePoints[i], NewPointOnLine3 - _linePoints[i], out RaycastHit hit3, (NewPointOnLine3 - _linePoints[i]).magnitude))
                    {
                        if (hit3.transform.CompareTag("Cubes") || hit3.transform.CompareTag("Glass"))
                        {
                            _lineRenderer.SetColors(Color.green, Color.green);
                            break;
                        }
                        else
                        {
                            _lineRenderer.SetColors(Color.grey, Color.grey);
                        }

                        Vector3 NewPointOnLine4 = Vector3.Reflect(NewPointOnLine3, hit3.normal);
                        NewPointOnLine4.z += hit3.point.z * 2;

                        if (hit3.transform.CompareTag("TopWall"))
                        {
                            NewPointOnLine4.z = hit3.point.z;
                        }
                        else
                        {
                            _linePoints.Add(NewPointOnLine4);
                        }
                    }
                    else
                    {
                        _linePoints.Add(NewPointOnLine3);
                    }
                }
                else {
                    _linePoints.Add(NewPointOnLine2);
                }
            }
            else {
                _linePoints.Add(NewPointOnLine);
            }        
        }

        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
    }

    public void HideLine()
    {
        _lineRenderer.positionCount = 0;
    }
}
