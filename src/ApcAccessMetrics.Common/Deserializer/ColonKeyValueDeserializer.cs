using System.Collections.Generic;
using System.IO;
using System;
using Newtonsoft.Json;

namespace ApcAccessMetrics.Common.Deserializer
{
    public class ColonKeyValueDeserializer
    {
        public Dictionary<string, string> Deserialize(string text)
        {
            var dict = new Dictionary<string, string>();
            using (StringReader reader = new StringReader(text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if(String.IsNullOrWhiteSpace(line)) continue;
                    
                    var colon = line.IndexOf(':');
                    var pre = line.Substring(0, colon-1);
                    var post = line.Substring(colon + 1, line.Length - colon - 1);

                    if(!String.IsNullOrWhiteSpace(pre))
                    {
                        dict.Add(pre.Trim(), post?.Trim());
                    }
                }
            }

            return dict;
        }

        public T Deserialize<T>(string text)
        {
            var dict = Deserialize(text);
            var dictJson = JsonConvert.SerializeObject(dict);
            return JsonConvert.DeserializeObject<T>(dictJson);
        }
    }
}