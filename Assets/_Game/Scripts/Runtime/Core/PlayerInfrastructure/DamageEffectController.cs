using UnityEngine;

namespace NavySpade.Core.PlayerInfrastructure
{
    public class DamageEffectController
    {
        private readonly SkinnedMeshRenderer _skinnedMeshRenderer;
        private readonly Material _material;
        private readonly Material _defaultMaterial;

        public DamageEffectController(SkinnedMeshRenderer skinnedSkinnedMeshRenderer, Material material)
        {
            _skinnedMeshRenderer = skinnedSkinnedMeshRenderer;
            _defaultMaterial = skinnedSkinnedMeshRenderer.sharedMaterial;
            _material = material;
        }

        public void ChangeMaterial() => _skinnedMeshRenderer.sharedMaterial = _material;
        public void ResetMaterial() => _skinnedMeshRenderer.sharedMaterial = _defaultMaterial;
    }
}