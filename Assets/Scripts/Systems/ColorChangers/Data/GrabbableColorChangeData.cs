using BNG;
using UnityEngine;

namespace Assets.Scripts.Systems.ColorChangers.Data
{
    /// <summary>
    /// Контроль цвета взятого предмета (с учетом вложенности Renderer)
    /// </summary>
    internal class GrabbableColorChangeData
    {
        public Grabbable Grabbable;
        public RendererColorChangeData[] Renderers = new RendererColorChangeData[0];

        public GrabbableColorChangeData(Grabbable grabbable)
        {
            Grabbable = grabbable;
        }

        public void Init()
        {
            var r = Grabbable.GetComponentsInChildren<Renderer>();
            Renderers = new RendererColorChangeData[r.Length];

            for (int i = 0; i < r.Length; i++)
            {
                Renderers[i] = new RendererColorChangeData(r[i]);
                Renderers[i].Init();
            }
        }

        /// <summary>
        /// Сброс цвета
        /// </summary>
        public void ResetColor()
        {
            for (int i = 0; i < Renderers.Length; i++)
            {
                Renderers[i].ResetColors();
            }
        }

        /// <summary>
        /// Установка цвета 
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Color32 color)
        {
            for (int i = 0; i < Renderers.Length; i++)
            {
                Renderers[i].SetColors(color);
            }
        }
    }
}