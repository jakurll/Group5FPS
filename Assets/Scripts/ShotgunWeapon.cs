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

    // Get particle system and stop ti from playing when enabled
    private void OnEnable()
    {
        _shotgun = GetComponent<ParticleSystem>();
        _shotgun.Stop();
    }

    //Get parent that holds the model
    // Get animator, bossAcidCheck script
    // Set the ammo to max ammo
    // Then set it false since you start with the rifle out
    private void Start()
    {
        _parent = GameObject.Find("ShotgunPrefab");
        _animator = _parent.GetComponentInChildren<Animator>();
        _textureCheck = GetComponentInParent<BossAcidCheck>();
        _currentAmmo = maxAmmo;
        _parent.SetActive(false);
    }

    // Check to see if player has switched weapon
    // If the weapon has ammo and is out, then the player can shoot it
    // Play animations subtract from ammo and start cooldown
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

        // This would update the textures for the BossAcidCheck script
        if (_numCollisions > 0)
        {
           // _textureCheck.GetTexture();
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


    // Switch weapons logic
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

    // Cooldown between shotgun shots
    IEnumerator CoolDown()
    {
        _isOut = false;
        yield return new WaitForSeconds(coolDown);
        _isOut = true;
    }
}
