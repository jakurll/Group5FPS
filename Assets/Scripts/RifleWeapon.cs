using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class RifleWeapon : MonoBehaviour
{
    private ParticleSystem _rifle;
    public GameObject rifleGameObject;

    [SerializeField] private int maxAmmo = 0;
    [SerializeField] private float firingSpeed;
    public float _currentAmmo;

    private bool _isOut = true;
    private Color _initColor;

    [Header("Dev Testing")]
    [SerializeField] private bool rToReload;

    private void OnEnable()
    {
        _rifle = GetComponent<ParticleSystem>();
        _rifle.Stop();

        _currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSwitching();

        if (_currentAmmo > 0 && _isOut)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _rifle.Play();
            }

            if (Input.GetMouseButton(0))
            {
                _currentAmmo -= 1 * firingSpeed * Time.deltaTime;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _rifle.Stop();
            }
        }
        else
        {
            _rifle.Stop();
            
            if (_currentAmmo < 0)
            {
                _currentAmmo = 0;
            }
        }

        // THIS IS FOR DEV TESTING
        if (rToReload && _isOut)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _currentAmmo = maxAmmo;
            }
        }
    }

    // When hit decrease "Health" aka paintAmount while increasing the albedo color
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.layer == 6)
        {
            var enemy = other.GetComponent<EnemyStats>();

            enemy.enemyHealth--;
            _initColor = enemy._shader.GetColor("_Albedo");
            var newColor = Color.Lerp(_initColor, Color.yellow, 0.05f);
            enemy._shader.SetVector("_Albedo", newColor);
        }
    }

    public void WeaponSwitching()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _isOut = true;
            rifleGameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _isOut = false;
            rifleGameObject.SetActive(false);
        }
    }
}
