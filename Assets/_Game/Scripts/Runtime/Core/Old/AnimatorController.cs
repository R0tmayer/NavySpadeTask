using UnityEngine;

namespace NavySpade.Core.Old
{
    public class AnimatorController
    {
        private readonly Animator _animator;
        private static readonly int isRun = Animator.StringToHash("IsRun");

        public AnimatorController(Animator animator)
        {
            _animator = animator;
        }

        public void SetRunAnimation() => _animator.SetBool(isRun, true);
        public void SetIdleAnimation() => _animator.SetBool(isRun, false);
    }
}