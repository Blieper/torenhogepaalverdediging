using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gameplay;

public class GameplayStatistics : MonoBehaviour {

    public int MaxHealth;
    public int DefaultHealth;
    public int Health { get { return _health; } }

    private int _health;

	void Start () {
        _health = DefaultHealth;
    }
	
    public void TakeDamage (int Amount) {
        _health -= Amount;
        _health = Mathf.Clamp(_health, 0, MaxHealth);

        if (_health == 0) {
            print("Game over!");
        }
    }
}