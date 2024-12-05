using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctaLib
{
    internal class ProjectUtils
    {

        public static int GetVersion(string path)
        {
            var f = File.ReadAllLines(path + "\\project.strd");
            bool foundMeta = false;
            foreach (var s in f)
            {
                if (foundMeta)
                {
                    var pairs = s.Split("=");
                    if (pairs[0] == "VERSION")
                    {
                        return Int32.Parse(pairs[1]);
                    }
                }

                if (s == "[META]")
                {
                    foundMeta = true;
                }

                if (s == "[/META]")
                {
                    return -1;
                }
            }

            return -1;

        }

    }
}
