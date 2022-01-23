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

    private void Awake()
    {
        controls = new CharacterControls();
        controls.Player.Movement.performed += HandleMovement;
        controls.Player.Movement.canceled += HandleMovement;
        controls.Player.Attack.performed += HandleAttack;

        character = GetComponent<CharacterMovement>();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void HandleMovement(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        if (inputVector.x > 0.1f)
        {
            character.Run(MovementDirection.Right);
        }
        else if (inputVector.x < -0.1f)
        {
            character.Run(MovementDirection.Left);
        }
        else
        {
            character.Run(MovementDirection.None);
        }

        if (inputVector.y > 0.1f)
        {
            character.Jump();
        }
    }

    private void HandleAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack!");
        //weapon.Attack();
    }
}
