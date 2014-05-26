using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;


namespace Unv.RaceEngineLib.Storage
{
	public class XNA_XML_Exporter
	{
		public static void WriteToFile(string filepath, object data)
		{
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(filepath, settings))
			{
				IntermediateSerializer.Serialize(writer, data, null);
			}
		}
	}
}
