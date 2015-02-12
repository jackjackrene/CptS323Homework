using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework01
{
    public interface FileReader
    {
        List<Target> ReadTargets();
    }
    public class INIReader : FileReader
    {
        public string path;

        public INIReader(string INIPath)
        {
            path = @INIPath;
        }

        public List<Target> ReadTargets()
        {
            Target target;
            string Name = null;
            double x = 0;
            double y = 0;
            double z = 0;
            bool Frd = true;
            int Pts = 0;
            int FR = 0;
            int SR = 0;
            bool CSSWH = true;
            string fileLine;
            int counter = 1;
            string tab = "\t";
            char tabDelim = Convert.ToChar(tab);
            char[] delimiter = {'=', ' ', tabDelim};

            // create list of targets
            List<Target> TargetList = new List<Target>();

            // read in file to string array
            string[] lines = System.IO.File.ReadAllLines(@path);

            // loop through file
            for (counter = 1; counter < lines.Length; counter++)
            {
                fileLine = lines[counter];
                string[] words = fileLine.Split(delimiter);

                switch (words[0].ToUpper())
                {
                    case "NAME":
                        Name = words[1];
                        break;
                    case "X":
                        x = Convert.ToDouble(words[1]);
                        break;
                    case "Y":
                        y = Convert.ToDouble(words[1]);
                        break;
                    case "Z":
                        z = Convert.ToDouble(words[1]);
                        break;
                    case "FRIEND":
                        Frd = Convert.ToBoolean(words[1]);
                        break;
                    case "POINTS":
                        Pts = Convert.ToInt32(words[1]);
                        break;
                    case "FLASHRATE":
                        FR = Convert.ToInt32(words[1]);
                        break;
                    case "SPAWNRATE":
                        SR = Convert.ToInt32(words[1]);
                        break;
                    case "CANSWAPSIDESWHENHIT":
                        CSSWH = Convert.ToBoolean(words[1]);
                        break;
                    case "[TARGET]":
                        target = new Target(Name, x, y, z, Frd, Pts, FR, SR, CSSWH);
                        TargetList.Add(target);
                        break;
                    default:
                        break;
                }
            }

            // add final target
            target = new Target(Name, x, y, z, Frd, Pts, FR, SR, CSSWH);
            TargetList.Add(target);

            return TargetList;
        }
    }
}
