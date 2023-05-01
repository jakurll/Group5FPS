using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintPool : MonoBehaviour
{
    [SerializeField] Movement player;
    private bool _isIn = false;
    private float _numColor = -.05f;
    private Material _shader;
    private bool _check;

    [Header("Time between +Paint")]
    [SerializeField] float time;

    [Header("Amount added to ammo")]
    [SerializeField] float rifleAmmo;
    [SerializeField] int shotgunAmmo;
    [SerializeField] uint healthAmount;

    private void Start()
    {
        _shader = GetComponentInParent<Renderer>().material;
    }

    // If palyer enters paint pool get the rifle and shotgun components and start filling them
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

    // While staying in check which gun is out and continue filling
    // And make sure ammo does not exceed max
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

    // If player leaves stop filling and check to make sure ammo does not exceed the maximum amount
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

    //FIll paint based onwhich weapon is out
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

            player.health += healthAmount;
            CheckAmmo(rifle, shotgun);
        }
    }

    //check both weapons to make sure ammo does nto exceed max
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

    // Depending on which weapon is out change color of pool
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

    //Change color of pool until reaches desired color
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
