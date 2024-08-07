using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.Keyboards
{
    /// <summary>
    /// Система клавиатуры. Набор текста и визуализация.
    /// </summary>
    internal class InteractiveKeyboard : MonoBehaviour
    {
        [SerializeField] private TMP_Text _output;
        private string _inputed;

        private void Awake()
        {
            UpdateVisualization();
        }

        public void UpdateVisualization()
        {
            _output.text = _inputed;
        }

        public void SendInput(string text)
        {
            _inputed += text;
            UpdateVisualization();
        }
         
        public void Clear()
        {
            _inputed = string.Empty;
            UpdateVisualization();
        }
    }
}