using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace DeledSceneImporter
{
    public class AttenuationContent
    {
        public double Constant = 1d;
        public double Linear = 0;
        public double Quadratic = 0.0000030d;
    }

    public class LightConeContent
    {
        public int Inner = 30;
        public int Outer = 40;
        public int Falloff = 1;
    }

    public class DXSLight
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

        public TagCollection Tags = new TagCollection();

        public Color Diffuse;
        public Color Specular;

        public Vector3 Position = Vector3.Zero;
        public Vector3 Direction = Vector3.Zero;

        public LightConeContent Cone = new LightConeContent();
        public AttenuationContent Attenuation = new AttenuationContent();

        public DXSLight(XmlNode node, ContentImporterContext context)
        {
            Id = node.Attributes["id"] != null ? int.Parse(node.Attributes["id"].Value) : 0;

            if (node.Attributes["name"] != null)
                Name = node.Attributes["name"].Value;
            if (node.Attributes["type"] != null)
                Type = node.Attributes["type"].Value;
            if (node.Attributes["visible"] != null)
                Visible = bool.Parse(node.Attributes["visible"].Value);
            if (node.Attributes["snap"] != null)
                Snap = bool.Parse(node.Attributes["snap"].Value);
            if (node.Attributes["active"] != null)
                Active = bool.Parse(node.Attributes["active"].Value);
            if (node.Attributes["castShadows"] != null)
                CastShadows = bool.Parse(node.Attributes["castShadows"].Value);

            Intensity = node.Attributes["intensity"] != null ? int.Parse(node.Attributes["intensity"].Value) : 0;
            Range = node.Attributes["range"] != null ? int.Parse(node.Attributes["range"].Value) : 0;

            if (node["comments"] != null)
                Comments = node["comments"].Value;

            if (node["tag"] != null)
            {
                Tags = new TagCollection(node["tag"], context);
            }

            if (node["diffuse"] != null)
            {
                int r = int.Parse(node["diffuse"].Attributes["r"].Value);
                int g = int.Parse(node["diffuse"].Attributes["g"].Value);
                int b = int.Parse(node["diffuse"].Attributes["b"].Value);

                Diffuse = new Color(r, g, b);
            }

            if (node["specular"] != null)
            {
                int r = int.Parse(node["specular"].Attributes["r"].Value);
                int g = int.Parse(node["specular"].Attributes["g"].Value);
                int b = int.Parse(node["specular"].Attributes["b"].Value);

                Specular = new Color(r, g, b);
            }

            if (node["position"] != null)
            {
                float x = float.Parse(node["position"].Attributes["x"].Value);
                float y = float.Parse(node["position"].Attributes["y"].Value);
                float z = float.Parse(node["position"].Attributes["z"].Value);

                Position.X = x;
                Position.Y = y;
                Position.Z = z;
            }

            if (node["direction"] != null)
            {
                float x = float.Parse(node["direction"].Attributes["x"].Value);
                float y = float.Parse(node["direction"].Attributes["y"].Value);
                float z = float.Parse(node["direction"].Attributes["z"].Value);

                Direction.X = x;
                Direction.Y = y;
                Direction.Z = z;
            }

            if (node["attenuation"] != null)
            {
                double c = double.Parse(node["attenuation"].Attributes["constant"].Value);
                double l = double.Parse(node["attenuation"].Attributes["linear"].Value);
                double q = double.Parse(node["attenuation"].Attributes["quadratic"].Value);

                Attenuation.Constant = c;
                Attenuation.Linear = l;
                Attenuation.Quadratic = q;
            }

            if (node["cone"] != null)
            {
                Cone.Inner = int.Parse(node["cone"].Attributes["inner"].Value);
                Cone.Outer = int.Parse(node["cone"].Attributes["outer"].Value);
                Cone.Falloff = int.Parse(node["cone"].Attributes["falloff"].Value);;
            }
        }
    }
}
