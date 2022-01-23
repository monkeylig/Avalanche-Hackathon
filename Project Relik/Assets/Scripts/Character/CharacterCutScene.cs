using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class CharacterCutScene : MonoBehaviour
{
    [SerializeField]
    private CharacterMovement.MovementDirection walkDirection = CharacterMovement.MovementDirection.None;

    private CharacterMovement character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        character.Run(walkDirection);
    }
}
