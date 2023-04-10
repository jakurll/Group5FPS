using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintPool : MonoBehaviour
{
    private bool _isIn = false;
    private float _numColor = -.05f;
    private Material _shader;
    private bool _check;

    [Header("Time between +Paint")]
    [SerializeField] float time;

    [Header("Amount added to ammo")]
    [SerializeField] float rifleAmmo;
    [SerializeField] int shotgunAmmo;

    private void Start()
    {
        _shader = GetComponentInParent<Renderer>().material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            _isIn = true;

            var rifle = other.gameObject.GetComponentInChildren<RifleWeapon>();
            var shotgun = other.gameObject.GetComponentInChildren<ShotgunWeapon>();

            //CheckWeapon(rifle);

            StartCoroutine(FillPaint(rifle, shotgun));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        var rifle = other.gameObject.GetComponentInChildren<RifleWeapon>();
        var shotgun = other.gameObject.GetComponentInChildren<ShotgunWeapon>();

        if (rifle.IsOut && _check)
        {
            CheckWeapon(rifle);
            StartCoroutine(FillPaint(rifle, shotgun));
            _check = false;
        }
        else if (!rifle.IsOut && !_check)
        {
            CheckWeapon(rifle);
            StartCoroutine(FillPaint(rifle, shotgun));
            _check = true;
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
            if (rifle.IsOut)
            {
                rifle._currentAmmo += rifleAmmo;
            }
            else
            {
                shotgun._currentAmmo += shotgunAmmo;
            }

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

    public void CheckWeapon(RifleWeapon rifle)
    {
        if (rifle.IsOut && _shader.GetFloat("_TurnGun") > 0f)
        {
            StopAllCoroutines();
            StartCoroutine(ChangeColor(true));
        }
        else if (!rifle.IsOut && _shader.GetFloat("_TurnGun") < 1.5f)
        {
            StopAllCoroutines();
            StartCoroutine(ChangeColor(false));
        }
    }

    IEnumerator ChangeColor(bool rifle)
    {
        if (rifle)
        {
            while (_shader.GetFloat("_TurnGun") > 0f)
            {
                yield return new WaitForSeconds(0.05f);
                _numColor -= 0.05f;
                _shader.SetFloat("_TurnGun", _numColor);
            }
        }
        else
        {
            while (_shader.GetFloat("_TurnGun") < 1.5f)
            {
                yield return new WaitForSeconds(0.05f);
                _numColor += 0.05f;
                _shader.SetFloat("_TurnGun", _numColor);
            }
        }
    }
}
