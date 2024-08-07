using Assets.Scripts.Systems.ColorChangers.Data;
using BNG;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Systems.ColorChangers
{
    internal class ChangeColorOnTakeSystem : MonoBehaviour
    {
        [SerializeField] private Color32 _color = new Color32(255, 0, 0, 255);
        [SerializeField] private Grabber[] _interactable = new Grabber[0];

        private List<GrabbableColorChangeData> _grabbableColorized = new();

        private void Awake()
        {
            foreach (var i in _interactable)
            {
                i.onGrabEvent.AddListener(OnGrabbed);
                i.onReleaseEvent.AddListener(OnRelease);
            }
        }

        private void OnRelease(Grabbable g)
        {
            Debug.Log($"release {g?.name}");

            var same = _grabbableColorized.FirstOrDefault(x => x.Grabbable == g);
            if (same != null) 
            {
                same.ResetColor(); // Сброс цвета
                _grabbableColorized.Remove(same);
            }
        }

        private void OnGrabbed(Grabbable g)
        {
            Debug.Log($"grabbed {g?.name}");

            var same = _grabbableColorized.FirstOrDefault(x => x.Grabbable == g);
            if (same == null) // Инициализация данных смены цвета, если их нет
            {
                same = new GrabbableColorChangeData(g);
                same.Init();
                _grabbableColorized.Add(same);
            }

            same.SetColor(_color); // Установление цвета
        }
    }
}