using UnityEngine;

namespace Assets.Scripts.Systems.FPSLimits
{
    /// <summary>
    /// Ограничивает ФПС при запуске awake
    /// </summary>
    internal class FPSLimiter : MonoBehaviour
    {
        [SerializeField] private int _targetFramerate = 144;

        private void Awake()
        {
            Application.targetFrameRate = _targetFramerate;
        }
    }
}
