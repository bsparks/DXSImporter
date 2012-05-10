using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace DeledSceneImporter
{
    public class TagCollection : Dictionary<string, string>
    {
        public TagCollection() { }

        public TagCollection(XmlNode node, ContentImporterContext context)
        {
            string tagString = node.Value;

            // each key/value pair is pipe delimited
            string[] pairs = tagString.Split('|');
            foreach (string p in pairs)
            {
                // the first space denotes the key
                string[] pair = p.Split(' ');

                string key = pair[0];
                string val = String.Join(" ", pair, 1, pair.Length - 1);

                // if for some reason the key is duplicated
                if (this.ContainsKey(key))
                {
                    if (this[key] == val)
                    {
                        context.Logger.LogWarning("", new ContentIdentity(), "Multiple tags of name {0} found with value {1}", new object[] { key, val });
                    }
                    else
                    {
                        throw new Exception(string.Format("Multiple tags of name {0} exist with different values: {1} and {2}", key, val, this[key]));
                    }
                }
                else
                {
                    Add(key, val);
                }
            }
        }
    }
}
