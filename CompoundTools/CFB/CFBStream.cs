using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompoundTools.CFB
{
    class CFBStream
    {
        public List<byte> Data { get; set; }

        public CFBStream(int SecID, CFBFile File)
        {
            this.Data = Parse(SecID, File).ToList();
        }

        public static IEnumerable<byte> Parse(int SecID, CFBFile File)
        {
            List<byte> data = new List<byte>();
            int nextSector = SecID;

            do
            {
                data.AddRange(File.Data.Skip(CFBSAT.GetSecPos(nextSector, File.Header.SectorSize)).Take((int)File.Header.SectorSize));
                nextSector = File.SAT.Sectors[nextSector];
            } while (nextSector >= 0);

            return data;
        }
    }
}
