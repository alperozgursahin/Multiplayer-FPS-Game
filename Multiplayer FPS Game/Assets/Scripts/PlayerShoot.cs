using Mirror;
using UnityEngine;

public class PlayerShoot : NetworkBehaviour
{
    private const string PLAYER_TAG = "Player";

    [SerializeField] 
    private PlayerWeapon weapon;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

    [SerializeField]
    private GameObject weaponGFX;

    [SerializeField]
    private string weaponLayerName = "Weapon";

    void Start()
    {
        if (cam == null)
        {
            Debug.Log("No Camera Referenced!");
            this.enabled = false;
        }

        weaponGFX.layer = LayerMask.NameToLayer(weaponLayerName);

    }

    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    [Client]
    void Shoot()
    {
        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weapon.range, mask))
        {
            if (_hit.collider.tag == PLAYER_TAG)
            {
                CmdPlayerShot(_hit.collider.name, weapon.damage);
            }
        }
    }

    [Command]
    void CmdPlayerShot(string _playerID, int damage)
    {
        Debug.Log(_playerID + " has been shot.");

        Player _player = GameManager.GetPlayer(_playerID);
        _player.RpcTakeDamage(damage);
    }

}
