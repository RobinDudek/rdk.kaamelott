using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace rdk.kaamelott.Resources
{
    public class Resources
    {
        public static Stream GetSampleStreamFromResources(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            Stream stream = null;
            try
            {
                stream = assembly.GetManifestResourceStream(
                    "rdk.kaamelott.Resources.Samples." + filename);
            }
            catch { }
            return stream;
        }

        public static string GetJSONFromResources(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            string json = null;
            try
            {
                var stream = assembly.GetManifestResourceStream("rdk.kaamelott.Resources." + filename);
                using (StreamReader sr = new StreamReader(stream))
                {
                    while (!sr.EndOfStream)
                    {
                        json += sr.ReadLine();
                    }
                    stream.Close();
                }
            }
            catch { }
            return json;
        }
    }
}
