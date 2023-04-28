using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ShotgunWeapon : MonoBehaviour
{
    private ParticleSystem _shotgun;
    private BossAcidCheck _textureCheck;
    private Animator _animator;
    private GameObject _parent;
    [SerializeField] GameObject shotgunGameObject;
    public int maxAmmo = 0;
    public int _currentAmmo;
    private int _numCollisions;

    [Header("Cool down Time in seconds:")]
    [SerializeField] private float coolDown;

    private bool _isOut = false;

    private Color _initColor;

    [Header("Dev Testing")]
    [SerializeField] private bool rToReload;

    public bool IsOut { get { return _isOut; } set { _isOut = value; } }

    private void OnEnable()
    {
        _shotgun = GetComponent<ParticleSystem>();
        _shotgun.Stop();
    }

    private void Start()
    {
        _parent = GameObject.Find("ShotgunPrefab");
        _animator = _parent.GetComponentInChildren<Animator>();
        _textureCheck = GetComponentInParent<BossAcidCheck>();
        _currentAmmo = maxAmmo;
        _parent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSwitching();

        if (_currentAmmo > 0 && _isOut)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _shotgun.Play();
                _animator.Play("ShotgunRecoil");
                _currentAmmo--;
                StartCoroutine(CoolDown());
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

        if (_numCollisions > 0)
        {
           // _textureCheck.GetTexture();
        }
    }

    private void LateUpdate()
    {
        _numCollisions = 0;
    }

    // When hit decrease "Health" aka paintAmount while increasing the albedo color
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.layer == 7)
        {
            var enemy = other.GetComponentInParent<EnemyStats>();

            enemy.enemyHealth--;
            _initColor = enemy._shader.GetColor("_Albedo");
            var newColor = Color.Lerp(_initColor, Color.magenta, 0.05f);
            enemy._shader.SetVector("_Albedo", newColor);
        }

        if (other.GetComponent<Collider>() != null)
        {
            _numCollisions++;
        }
    }

    public void WeaponSwitching()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _isOut = false;
            shotgunGameObject.SetActive(false);
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _isOut = true;
            shotgunGameObject.SetActive(true);
        }
    }

    IEnumerator CoolDown()
    {
        _isOut = false;
        yield return new WaitForSeconds(coolDown);
        _isOut = true;
    }
}
