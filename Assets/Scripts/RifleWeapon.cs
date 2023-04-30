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

    // Get particle system and stop it from playing when enabled
    private void OnEnable()
    {
        _rifle = GetComponent<ParticleSystem>();
        _rifle.Stop();
    }

    //Get parent that holds the model
    // Get animator, bossAcidCheck script
    // Set the ammo to max ammo
    private void Start()
    {
        _parent = GameObject.Find("MachineGunPrefab");
        _animator = _parent.GetComponentInChildren<Animator>();
        _textureCheck = GetComponentInParent<BossAcidCheck>();
        _currentAmmo = maxAmmo;
    }

    // Check to see if player has switched weapon
    // If the weapon has ammo and is out, then the player can shoot it
    // if the ammo hits zero stop playing the particle effect
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

        // This would update the textures for the BossAcidCheck script
        if (_numCollisions > 0)
        {
           // _textureCheck.GetTexture();
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

    // This would set a counter to zero after firing and particle collisions have stopped
    // So the BossAcid texture would only update for the collisions that happened.
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


    // Switch weapons logic
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
