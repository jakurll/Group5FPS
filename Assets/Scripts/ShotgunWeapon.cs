using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ShotgunWeapon : MonoBehaviour
{
    private ParticleSystem _shotgun;
    [SerializeField] int maxAmmo = 0;
    public int _curAmmo;

    private bool _isOut = false;

    private Color _initColor;

    [Header("Dev Testing")]
    [SerializeField] private bool rToReload;
    private void OnEnable()
    {
        _shotgun = GetComponent<ParticleSystem>();
        _shotgun.Stop();
        _curAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSwitching();

        if (_curAmmo > 0 && _isOut)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _shotgun.Play();
                _curAmmo--;
            }
        }

        // THIS IS FOR DEV TESTING
        if (rToReload && _isOut)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                _curAmmo = maxAmmo;
            }
        }
    }

    // When hit decrease "Health" aka paintAmount while increasing the albedo color
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.layer == 7)
        {
            var enemy = other.GetComponent<EnemyStats>();

            enemy.enemyHealth--;
            _initColor = enemy._shader.GetColor("_Albedo");
            var newColor = Color.Lerp(_initColor, Color.magenta, 0.05f);
            enemy._shader.SetVector("_Albedo", newColor);
            
        }
    }

    public void WeaponSwitching()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _isOut = false;
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _isOut = true;
        }
    }
}
