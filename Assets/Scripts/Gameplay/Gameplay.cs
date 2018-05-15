using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay {
    public static class Statistics {
        public static int Health;
        public static int MaxHealth;
        public static int DefaultHealth;

        public static void SetHealth (int Amount) {
            Health = Mathf.Clamp(Amount, 0, MaxHealth);
        }

        public static void TakeDamage(int Amount) {
            Health -= Amount;
            Health = Mathf.Clamp(Health, 0, MaxHealth);

            if (Health == 0) {
                MonoBehaviour.print("Game Over!");
            }
        }

        public static void Initialise () {
            SetHealth(DefaultHealth);
        }
    }
}
