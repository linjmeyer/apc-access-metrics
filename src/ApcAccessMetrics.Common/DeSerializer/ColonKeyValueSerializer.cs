using System.Collections.Generic;
using System.IO;
using System;

namespace ApcAccessMetrics.Common.DeSerializer
{
    public class ColonKeyValueDeSerializer
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

                    var colon = text.IndexOf(':');
                    var pre = text.Substring(0, colon-1);
                    var post = text.Substring(colon, line.Length);

                    if(!String.IsNullOrWhiteSpace(pre))
                    {
                        dict.Add(pre.Trim(), post?.Trim());
                    }
                }
            }

            return dict;
        }
    }
}