using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework;

namespace DeledSceneImporter
{
    public class DXSScene
    {
        public string Filename;
        public string Directory;

        public string Version = string.Empty;
        public string Name = string.Empty;
        public string Author = string.Empty;
        public string Comments = string.Empty;

        public int PrimitiveCount = 0;
        public Dictionary<string, List<DXSMaterial>> Materials = new Dictionary<string, List<DXSMaterial>>();
        public List<DXSPrimitive> Primitives = new List<DXSPrimitive>();

        public Color AmbientLight = Color.White;
        public List<DXSLight> Lights = new List<DXSLight>();

        public DXSScene(XmlDocument document, ContentImporterContext context)
        {
            XmlNode sceneNode = document["scene"];

            Version = sceneNode.Attributes["version"].Value;

            XmlNode settingsNode = document.SelectSingleNode("scene/settings");
            if (settingsNode != null)
            {
                Name = settingsNode.Attributes["name"].Value;
                Author = settingsNode.Attributes["author"].Value;
                Comments = settingsNode.Attributes["comments"].Value;

                if (settingsNode["ambient"] != null)
                {
                    int r = int.Parse(settingsNode["ambient"].Attributes["r"].Value);
                    int g = int.Parse(settingsNode["ambient"].Attributes["g"].Value);
                    int b = int.Parse(settingsNode["ambient"].Attributes["b"].Value);

                    AmbientLight = new Color(r, g, b);
                }
            }

            XmlNode materialsNode = document.SelectSingleNode("scene/materials");
            if(materialsNode != null)
            {
                foreach (XmlNode matCat in materialsNode.SelectNodes("category"))
                {
                    string catName = matCat.Attributes["name"].Value;
                    List<DXSMaterial> mats = new List<DXSMaterial>();
                    foreach (XmlNode matNode in matCat.SelectNodes("material"))
                    {
                        mats.Add(new DXSMaterial(matNode, context));
                    }
                    Materials.Add(catName, mats);
                }
            }

            foreach (XmlNode primNode in document.SelectNodes("scene/primitives/primitive"))
            {

                Primitives.Add(new DXSPrimitive(primNode, context));
                PrimitiveCount++;

            }

            foreach (XmlNode lightNode in document.SelectNodes("scene/lights/light"))
            {
                Lights.Add(new DXSLight(lightNode, context));
            }
        }
    }
}
