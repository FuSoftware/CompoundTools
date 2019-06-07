using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompoundTools.CFB
{
    public class CFBSSAT
    {
        List<int> SecIDs;
        public List<int> Sectors { get; set; }

        CFBFile Parent;

        public CFBSSAT(byte[] data, CFBFile file)
        {
            this.Parent = file;
            this.Sectors = Parse(data, file.Header);
        }

        public static List<int> Parse(byte[] data, CFBHeader header)
        {
            return null;
        }

        public static int GetSecPos(int SecID, uint SectorSize)
        {
            return (int)(512 + (SecID * SectorSize));
        }
    }
}
