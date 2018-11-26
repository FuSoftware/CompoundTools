using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompoundTools.CFB
{
    class CFBSSAT
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
            uint SecSize = header.SectorSize;
            int nextSector = header.FirstShortSector;
            List<int> sectors = new List<int>();

            do
            {
                int pos = GetSecPos(nextSector, header);
                byte[] bytes = new byte[SecSize];

                for (int i = pos; i < pos + SecSize; i++)
                {
                    bytes[i - pos] = data[i];
                }

                for (int i = 0; i < bytes.Length; i += 4)
                {
                    sectors.Add(BitConverter.ToInt32(bytes, i));
                }
            } while (nextSector > 0);

            Console.WriteLine("Found {0} ShortSectors, excpeted {1}", sectors.Count, header.ShortSectorCount);

            return sectors;
        }

        public static int GetSecPos(int SecID, CFBHeader header)
        {
            return (int)(512 + (SecID * header.SectorSize));
        }
    }
}
