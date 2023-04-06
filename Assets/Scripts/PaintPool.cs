using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintPool : MonoBehaviour
{
    private bool _isIn = false;

    [Header("Time between +Paint")]
    [SerializeField] float time;

    [Header("Amount added to ammo")]
    [SerializeField] float rifleAmmo;
    [SerializeField] int shotgunAmmo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            _isIn = true;

            var rifle = other.gameObject.GetComponentInChildren<RifleWeapon>();
            var shotgun = other.gameObject.GetComponentInChildren<ShotgunWeapon>();

            StartCoroutine(FillPaint(rifle, shotgun));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            var rifle = other.gameObject.GetComponentInChildren<RifleWeapon>();
            var shotgun = other.gameObject.GetComponentInChildren<ShotgunWeapon>();

            _isIn = false;
            StopCoroutine(FillPaint(rifle, shotgun));
            CheckAmmo(rifle, shotgun);
        }
    }

    IEnumerator FillPaint(RifleWeapon rifle, ShotgunWeapon shotgun)
    {
        while (_isIn)
        {
            yield return new WaitForSeconds(time);
            rifle._currentAmmo += rifleAmmo;
            shotgun._currentAmmo += shotgunAmmo;

            CheckAmmo(rifle, shotgun);
        }
    }

    private void CheckAmmo(RifleWeapon rifle, ShotgunWeapon shotgun)
    {
        if (rifle._currentAmmo > rifle.maxAmmo)
        {
            rifle._currentAmmo = rifle.maxAmmo;
        }

        if (shotgun._currentAmmo > shotgun.maxAmmo)
        {
            shotgun._currentAmmo = shotgun.maxAmmo;
        }
    }
}
