﻿
using SurvivalWizard.Enemys;
using UnityEngine;

namespace SurvivalWizard.Spells
{
    public class FireBall : Spell
    {
        private void Update()
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(Damage);
            }
        }
    }
}
