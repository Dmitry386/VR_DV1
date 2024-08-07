using UnityEngine;

namespace Assets.Scripts.Systems.ColorChangers.Data
{
    /// <summary>
    /// Контроль цвета Renderer (с учетом вложенности)
    /// </summary>
    internal class RendererColorChangeData
    {
        public Renderer Renderer;
        public Color32[] DefaultColors;
        public Material[] CachedMaterials;

        public RendererColorChangeData(Renderer renderer)
        {
            Renderer = renderer;
        }

        public void Init()
        {
            CacheMaterials();
        }

        private void CacheMaterials()
        {
            CachedMaterials = new Material[Renderer.materials.Length];
            DefaultColors = new Color32[Renderer.materials.Length];
            for (int i = 0; i < Renderer.materials.Length; i++)
            {
                CachedMaterials[i] = Renderer.materials[i];
                DefaultColors[i] = CachedMaterials[i].color;
            }
            Renderer.sharedMaterials = CachedMaterials;
        }

        public void SetColors(Color32 color)
        {
            for (int i = 0; i < CachedMaterials.Length; i++)
            {
                CachedMaterials[i].color = color;
            }
        }

        public void ResetColors()
        {
            for (int i = 0; i < CachedMaterials.Length; i++)
            {
                CachedMaterials[i].color = DefaultColors[i];
            }
        }
    }
}
