using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemPaint : MonoBehaviour
{
    // Using the OnParticleCollision method we fill a list with those collisions
    // and if the surface is paintable then we add paint to each of those positions.

    public PaintManager paintManager;
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;
    [Space]
    [SerializeField] Color _paintColor;
    [Space]
    [SerializeField] private float _minRadius = 0.05f;
    [SerializeField] private float _maxRadius = 0.2f;
    [SerializeField] private float _strength = 1;
    [SerializeField] private float _hardness = 1;

    private void Awake()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Paintable p = other.GetComponent<Paintable>();
        if (p != null)
        {
            for (int i = 0; i < numCollisionEvents; i++)
            {
                Vector3 pos = collisionEvents[i].intersection;
                float radius = Random.Range(_minRadius, _maxRadius);
                paintManager.Paint(p, pos, radius, _hardness, _strength, _paintColor);
            }
        }
    }
}
