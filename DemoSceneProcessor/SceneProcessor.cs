using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;

using DeledSceneImporter;
using Microsoft.Xna.Framework.Content;

namespace DemoSceneProcessor
{
    [ContentSerializerRuntimeType("DeleDemo.Light, DeleDemo")]
    public sealed class GameLightContent
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

    [ContentSerializerRuntimeType("DeleDemo.Scene, DeleDemo")]
    public sealed class GameSceneContent
    {
        public string Filename;
        public string Directory;

        public string Version = string.Empty;
        public string Name = string.Empty;
        public string Author = string.Empty;
        public string Comments = string.Empty;

        public int PrimitiveCount = 0;
        public List<ModelContent> Models = new List<ModelContent>();

        public Color AmbientLight = Color.White;
        public List<GameLightContent> Lights = new List<GameLightContent>();
    }

    [ContentProcessor(DisplayName = "DXS DemoSceneProcessor")]
    public class SceneProcessor : ContentProcessor<DXSScene, GameSceneContent>
    {
        public override GameSceneContent Process(DXSScene input, ContentProcessorContext context)
        {
            // just copy over for now...
            GameSceneContent scene = new GameSceneContent()
            {
                Filename = input.Filename,
                Directory = input.Directory,
                Version = input.Version,
                Name = input.Name,
                Author = input.Author,
                Comments = input.Comments,
                AmbientLight = input.AmbientLight,
                PrimitiveCount = input.PrimitiveCount
            };

            foreach (DXSLight light in input.Lights)
            {
                // todo: more...
                scene.Lights.Add(new GameLightContent()
                {
                    Id = light.Id,
                    Name = light.Name,
                    Range = light.Range,
                    Intensity = light.Intensity,
                    Diffuse = light.Diffuse,
                    Specular = light.Specular,
                    Position = light.Position,
                    Tags = new Dictionary<string,string>(light.Tags)
                });
            }

            foreach (DXSPrimitive p in input.Primitives)
            {
                MeshBuilder builder = MeshBuilder.StartMesh(p.Name);

                int texCoordId = builder.CreateVertexChannel<Vector2>(VertexChannelNames.TextureCoordinate(0));

                builder.SetMaterial(new BasicMaterialContent());

                foreach (DeledSceneImporter.VertexContent v in p.Vertices)
                {
                    builder.CreatePosition(v.Vertex);
                }

                foreach (PolygonContent poly in p.Polys)
                {
                    foreach (DeledSceneImporter.VertexContent vert in poly.Vertices)
                    {
                        AddVertex(builder, vert.Id, texCoordId, vert.UV.X, vert.UV.Y);
                    }
                }

                // Chain to the ModelProcessor so it can convert the mesh we just generated.
                MeshContent mesh = builder.FinishMesh();

                ModelContent m = context.Convert<MeshContent, ModelContent>(mesh, "ModelProcessor");

                scene.Models.Add(m);
            }

            return scene;
        }

        /// <summary>
        /// Helper for adding a new triangle vertex to a MeshBuilder,
        /// along with an associated texture coordinate value.
        /// </summary>
        static void AddVertex(MeshBuilder builder, int vertex,
                              int texCoordId, float u, float v)
        {
            builder.SetVertexChannelData(texCoordId, new Vector2(u, v));

            builder.AddTriangleVertex(vertex);
        }
    }
}