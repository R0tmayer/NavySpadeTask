
using NavySpade.Core.Interfaces;
using UnityEngine;

namespace NavySpade.Core.Health
{
    public class CameraTracker : ITickable
    {
        private readonly Camera _camera;
        private readonly Transform _target;
        private readonly Vector3 _offset;

        public CameraTracker(Camera camera, Transform target, Vector3 offset)
        {
            _camera = camera;
            _target = target;
            _offset = offset;
        }


        public void Tick()
        {
            _camera.transform.position = _target.position + _offset;
        }
    }
}