using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace DeledSceneImporter
{
    public class MaterialLayerContent
    {

    }

    public class RaytracingContent
    {

    }

    public class DXSMaterial
    {
        public int Id = 0;
        public string Name = string.Empty;
        public bool Used = true;
        public bool Lightmap = true;
        public bool CastShadows = true;
        public bool ReceiveShadows = true;

        public RaytracingContent Raytracing = new RaytracingContent();

        public List<MaterialLayerContent> Layers = new List<MaterialLayerContent>();

        public DXSMaterial(XmlNode node, ContentImporterContext context)
        {
            Id = node.Attributes["id"] != null ? int.Parse(node.Attributes["id"].Value) : 0;

            if (node.Attributes["name"] != null)
                Name = node.Attributes["name"].Value;
            if (node.Attributes["used"] != null)
                Used = bool.Parse(node.Attributes["used"].Value);
            if (node.Attributes["lightmap"] != null)
                Lightmap = bool.Parse(node.Attributes["lightmap"].Value);
            if (node.Attributes["castShadows"] != null)
                CastShadows = bool.Parse(node.Attributes["castShadows"].Value);
            if (node.Attributes["receiveShadows"] != null)
                ReceiveShadows = bool.Parse(node.Attributes["receiveShadows"].Value);
        }
    }
}
