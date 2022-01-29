using UnityEngine;

namespace Daniell.Runtime.Systems.DialogueNodes
{
    [CreateAssetMenu(fileName = "Test Character", menuName = "Character")]
    public class Character : ScriptableObject
    {
        public string Name;
    }
}

public class PlayerHealth : MonoBehaviour
{
    public float Health { get; private set; }

    private Animator _stateMachine;

    private void Awake()
    {
        // Get ref to the state machine
        _stateMachine = GetComponent<Animator>();

        // Set default health
        Health = 100;
    }

    private void TakeDamage(float damage)
    {
        Health -= damage;

        // Here you can call your state machine and tell it you took damage, triggering the transition to the invicible state
        _stateMachine.SetTrigger("Player Took Damage");
    }

    // And this could be called by your state machine directly when the invincible state needs to be triggered
    public void MakePlayerInvincible()
    {
        // Do stuff to make the player invincible
    }
}