using System;
using Microsoft.Xna.Framework.Content.Pipeline;
using System.Xml;

namespace DeledSceneImporter
{
    [ContentImporter(".dxs", DisplayName = "DXS Importer - DeleD Scene")]
    public class DxsImporter : ContentImporter<DXSScene>
    {
        public override DXSScene Import(string filename, ContentImporterContext context)
        {
            // load the xml
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            // create the content from the xml
            DXSScene content = new DXSScene(doc, context);

            // save the filename and directory for the processor to use if it needs to
            content.Filename = filename;
            content.Directory = filename.Remove(filename.LastIndexOf('\\'));

            return content;
        }
    }
}