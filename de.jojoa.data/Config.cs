using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace RealifeGM.de.jojoa.data
{
	class Config
	{
		private Dictionary<string, string> values = new Dictionary<string, string>();

		public Config(string path)
		{
        	if(File.Exists(path))
			{
				XElement file = XElement.Load(path);
				foreach(var el in file.Elements())
				{
					values.Add(el.Name.LocalName, el.Value);
				}
			}
		}

        public string getConfig(string key)
        {
        	if(!values.ContainsKey(key))
        		return null;

        	return values[key];
        }
	}
}