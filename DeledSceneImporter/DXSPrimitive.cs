using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.Globalization;

namespace DeledSceneImporter
{
    public class VertexContent
    {
        public int Id = 0;
        public Vector3 Vertex;
        public int JointId = -1;
        public Vector2 UV = Vector2.Zero;
        public Vector2 UV1 = Vector2.Zero; // i think this is lightmapping
    }

    public class PolygonContent
    {
        public int MaterialId = 0;
        public List<VertexContent> Vertices = new List<VertexContent>();
    }

    public class DXSPrimitive
    {
        public int Id = 0;
        public string Name = string.Empty;
        public string Type = string.Empty;
        public bool Visible = true;

        public string Snap = "vertex";
        public bool AutoUV = true;

        public int GroupId = -1;
        public int SkeletonId = -1;

        public string Comments = string.Empty;
        public TagCollection Tags = new TagCollection();
        public List<VertexContent> Vertices = new List<VertexContent>();
        public List<PolygonContent> Polys = new List<PolygonContent>();

        public DXSPrimitive(XmlNode node, ContentImporterContext context)
        {
            Id = node.Attributes["id"] != null ? int.Parse(node.Attributes["id"].Value) : 0;

            if (node.Attributes["name"] != null)
                Name = node.Attributes["name"].Value;
            if (node.Attributes["type"] != null)
                Type = node.Attributes["type"].Value;
            if (node.Attributes["visible"] != null)
                Visible = bool.Parse(node.Attributes["visible"].Value);
            if (node.Attributes["snap"] != null)
                Snap = node.Attributes["snap"].Value;
            if (node.Attributes["autoUV"] != null)
                AutoUV = bool.Parse(node.Attributes["autoUV"].Value);            

            GroupId = node.Attributes["groupID"] != null ? int.Parse(node.Attributes["groupID"].Value) : -1;
            SkeletonId = node.Attributes["skeletonID"] != null ? int.Parse(node.Attributes["skeletonID"].Value) : -1;

            if (node["comments"] != null)
                Comments = node["comments"].Value;

            if (node["tag"] != null)
            {
                Tags = new TagCollection(node["tag"], context);
            }

            foreach (XmlNode v in node.SelectNodes("vertices/vertex"))
            {
                VertexContent vtx = new VertexContent();
                vtx.Id = v.Attributes["id"] != null ? int.Parse(v.Attributes["id"].Value) : 0;
                vtx.JointId = v.Attributes["jointID"] != null ? int.Parse(v.Attributes["jointID"].Value) : -1;
                vtx.Vertex.X = v.Attributes["x"] != null ? float.Parse(v.Attributes["x"].Value, CultureInfo.InvariantCulture) : 0;
                vtx.Vertex.Y = v.Attributes["y"] != null ? float.Parse(v.Attributes["y"].Value, CultureInfo.InvariantCulture) : 0;
                vtx.Vertex.Z = v.Attributes["z"] != null ? float.Parse(v.Attributes["z"].Value, CultureInfo.InvariantCulture) : 0;

                Vertices.Add(vtx);
            }

            foreach (XmlNode p in node.SelectNodes("polygons/poly"))
            {
                PolygonContent poly = new PolygonContent();
                poly.MaterialId = int.Parse(p.Attributes["mid"].Value);
                foreach (XmlNode pv in p.SelectNodes("vertex"))
                {
                    VertexContent vtx = new VertexContent();
                    vtx.Id = pv.Attributes["vid"] != null ? int.Parse(pv.Attributes["vid"].Value) : 0;
                    vtx.UV.X = pv.Attributes["u0"] != null ? float.Parse(pv.Attributes["u0"].Value) : 0;
                    vtx.UV.Y = pv.Attributes["v0"] != null ? float.Parse(pv.Attributes["v0"].Value) : 0;
                    vtx.UV1.X = pv.Attributes["u1"] != null ? float.Parse(pv.Attributes["u1"].Value) : 0;
                    vtx.UV1.Y = pv.Attributes["v1"] != null ? float.Parse(pv.Attributes["v1"].Value) : 0;

                    poly.Vertices.Add(vtx);
                }

                Polys.Add(poly);
            }
        }
        
    }
}
