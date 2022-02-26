using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private VisualEffect swordImpact = null;

    private float weaponAngle;
    private CharacterCombat characterCombat = null;

    public float WeaponAngle
    {
        get { return weaponAngle; }
        set
        {
            weaponAngle = value;
            transform.rotation = Quaternion.AngleAxis(weaponAngle, new Vector3(0, 0, 1));
        }
    }

    private void Awake()
    {
        characterCombat = GetComponent<CharacterCombat>();
    }

    private void OnDestroy()
    {
        if (swordImpact)
        {
            Destroy(swordImpact.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MoveToPoint(Vector3 point)
    {
        transform.position = point;
    }

    public void Attack()
    {
        characterCombat.Attack(0);
    }

}
