using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector3[] points = { };

    [SerializeField] float speed = 10f;

    [Header("Dock time in seconds:")]
    [SerializeField] float dockTime = 5f;

    private int _nextPoint = 0;

    private Vector3 _startPosition;

    public Vector3 _velocity { get; private set; }

    private List<NavMeshAgent> _agentsOnPlatform = new List<NavMeshAgent>();

    private bool _startMove = true;

    private void Start()
    {
        if (points == null || points.Length < 2)
        {
            Debug.LogError("must have 2 or more points");
        }

        _startPosition = transform.position;

        transform.position = currentPoint;

    }

    Vector3 currentPoint
    {
        get
        {
            if (points == null || points.Length == 0)
            {
                return transform.position;
            }
            return points[_nextPoint] + _startPosition;
        }

    }

    private void FixedUpdate()
    {
        if (_startMove)
        {
            var newPosition = Vector3.MoveTowards(transform.position,
            currentPoint, speed * Time.deltaTime);

            if (Vector3.Distance(newPosition, currentPoint) < 0.001)
            {
                StartCoroutine(DockWait());
                newPosition = currentPoint;

                _nextPoint += 1;
                _nextPoint %= points.Length;
            }

            _velocity = (newPosition - transform.position) / Time.deltaTime;

            transform.position = newPosition;

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (points == null || points.Length < 2)
        {
            return;
        }

        Vector3 offsetPosition = transform.position;

        if (Application.isPlaying)
        {
            offsetPosition = _startPosition;
        }

        Gizmos.color = Color.blue;

        for (int p = 0; p < points.Length; p++)
        {
            var p1 = points[p];
            var p2 = points[(p + 1) % points.Length];

            Gizmos.DrawSphere(offsetPosition + p1, 0.1f);

            Gizmos.DrawLine(offsetPosition + p1, offsetPosition + p2);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            _agentsOnPlatform.Add(agent);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent))
        {
            _agentsOnPlatform.Remove(agent);
        }
    }

    IEnumerator DockWait()
    {
        _startMove = false;
        _velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(dockTime);
        _startMove = true;
    }
}
