using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class RifleWeapon : MonoBehaviour
{
    private ParticleSystem _rifle;
    public GameObject rifleGameObject;
    private Animator _animator;
    private GameObject _parent;
    private BossAcidCheck _textureCheck;
    private int _numCollisions;

    public int maxAmmo = 0;
    [SerializeField] private float firingSpeed;
    public float _currentAmmo;

    private bool _isOut = true;
    private Color _initColor;

    [Header("Dev Testing")]
    [SerializeField] private bool rToReload;

    public bool IsOut { get {return _isOut; } set { _isOut = value; } }

    private void OnEnable()
    {
        _rifle = GetComponent<ParticleSystem>();
        _rifle.Stop();
    }
    private void Start()
    {
        _parent = GameObject.Find("MachineGunPrefab");
        _animator = _parent.GetComponentInChildren<Animator>();
        _textureCheck = GetComponentInParent<BossAcidCheck>();
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
                _animator.Play("MachineGun");
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

        if (_numCollisions > 0)
        {
            _textureCheck.GetTexture();
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

    private void LateUpdate()
    {
        _numCollisions = 0;
    }

    // When hit decrease "Health" aka paintAmount while increasing the albedo color
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.layer == 6)
        {
            var enemy = other.GetComponentInParent<EnemyStats>();

            enemy.enemyHealth--;
            _initColor = enemy._shader.GetColor("_Albedo");
            var newColor = Color.Lerp(_initColor, Color.yellow, 0.05f);
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
