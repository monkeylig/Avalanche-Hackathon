using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static CharacterMovement;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterInput : MonoBehaviour
{
    private CharacterControls controls;
    private CharacterMovement character;
    private SwordWielder swordWielder;
    private CharacterCombat characterCombat;
    private Vector2 mousePos;

    private void Awake()
    {
        controls = new CharacterControls();
        controls.Player.Movement.performed += HandleMovement;
        controls.Player.Movement.canceled += HandleMovement;
        controls.Player.Attack.performed += HandleAttack;
        controls.Player.Aim.performed += HandleAim;

        character = GetComponent<CharacterMovement>();
        swordWielder = GetComponent<SwordWielder>();
        characterCombat = GetComponent<CharacterCombat>();

    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void FixedUpdate()
    {
        if (swordWielder)
        {
            swordWielder.AimWeapon(mousePos);
        }
    }

    private void HandleMovement(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        if (inputVector.x > 0.1f)
        {
            character.Run(Direction.Right);
        }
        else if (inputVector.x < -0.1f)
        {
            character.Run(Direction.Left);
        }
        else
        {
            character.Run(Direction.None);
        }

        if (inputVector.y > 0.1f)
        {
            character.Jump();
        }
    }

    private void HandleAttack(InputAction.CallbackContext context)
    {
        if (swordWielder)
        {
            swordWielder.Attack();
        }

        if (characterCombat)
        {
            characterCombat.Attack(0);
        }
    }

    private void HandleAim(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }
}
