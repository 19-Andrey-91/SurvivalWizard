
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SurvivalWizard
{
    public interface ICanTakeDamage
    {
        public bool TakeDamage(float value);
        public bool TakeDurationDamage(float durationDamage, float duration, float partsAmount);
    }
}
