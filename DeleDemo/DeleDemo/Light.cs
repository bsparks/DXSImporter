using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DeleDemo
{
    public sealed class Light
    {
        public int Id = 0;
        public string Name = string.Empty;
        public string Type = string.Empty;
        public bool Visible = true;

        public int Range = 0;
        public bool Active = true;
        public bool CastShadows = true;
        public bool Snap = false;
        public int Intensity = 0;
        public string Comments = string.Empty;

        public Dictionary<string, string> Tags;

        public Color Diffuse;
        public Color Specular;

        public Vector3 Position = Vector3.Zero;
        public Vector3 Direction = Vector3.Zero;
    }
}
