using BNG;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI.Navigation
{
    /// <summary>
    /// Джойстик для навигации по канвасу.
    /// </summary>
    internal class JoystickUINavigation : MonoBehaviour
    {
        [SerializeField] private bool _debug;

        [SerializeField] private GameObject _selectedDefault;
        [SerializeField] private JoystickControl _joystick;
        [SerializeField] private Grabbable _updateValueIfGrabbed; 
        [SerializeField, Min(0)] private float _sensitivity = 5f;

        private bool _blockInput;

        private void Awake()
        {
            _joystick.onJoystickChange.AddListener(OnJoystickChange);
        }

        private void OnJoystickChange(float x, float y)
        {
            // Проверка блокировки ввода И удерживания рычага
            if (!_blockInput && (_updateValueIfGrabbed.HeldByGrabbers != null && _updateValueIfGrabbed.HeldByGrabbers.Count > 0))
            {
                Move(GetDir());
                StartCoroutine(WaitDelay(0.5f));
            }
        }

        /// <summary>
        /// Получить направление движения исходя из значений джойстика
        /// </summary>
        /// <returns></returns>
        private MoveDirection GetDir()
        {
            float x = _joystick.angleX;
            float y = _joystick.angleY;

            if (_debug) Debug.Log($"{x}, {y}");

            if (x > _sensitivity)
            {
                return MoveDirection.Right;
            }
            if (x < _sensitivity)
            {
                return MoveDirection.Left;
            }
            if (y > _sensitivity)
            {
                return MoveDirection.Up;
            }
            if (y < _sensitivity)
            {
                return MoveDirection.Down;
            }

            return MoveDirection.None;
        }

        /// <summary>
        /// Переместить курсор
        /// </summary>
        /// <param name="direction"></param>
        public void Move(MoveDirection direction)
        {
            AxisEventData data = new AxisEventData(EventSystem.current);

            data.moveDir = direction;
            data.selectedObject = EventSystem.current.currentSelectedGameObject;

            ExecuteEvents.Execute(data.selectedObject, data, ExecuteEvents.moveHandler);
        }

        private IEnumerator WaitDelay(float delay)
        {
            _blockInput = true;
            yield return new WaitForSeconds(delay);
            _blockInput = false;
        }

        /// <summary>
        /// Отправка события ввода
        /// </summary>
        public void SendClickEvent()
        {
            var evs = EventSystem.current;

            if (!evs.currentSelectedGameObject)
            {
                evs.SetSelectedGameObject(_selectedDefault);
            }
            else
            {
                if (evs.currentSelectedGameObject.TryGetComponent<UnityEngine.UI.Button>(out var b))
                {
                    b.onClick.Invoke();
                }
            }
        }
    }
}