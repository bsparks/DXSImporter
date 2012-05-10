using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DeleDemo
{
    public class Scene
    {
        public string Filename;
        public string Directory;

        public string Version = string.Empty;
        public string Name = string.Empty;
        public string Author = string.Empty;
        public string Comments = string.Empty;

        public int PrimitiveCount = 0;
        public List<Model> Models = new List<Model>();

        public Color AmbientLight;
        public List<Light> Lights = new List<Light>();

        public void Draw(Matrix view, Matrix projection)
        {
            foreach (Model model in Models)
            {
                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.View = view;
                        effect.Projection = projection;

                        effect.EnableDefaultLighting();

                        effect.AmbientLightColor = this.AmbientLight.ToVector3();
                    }

                    mesh.Draw();
                }
            }
        }
    }
}
