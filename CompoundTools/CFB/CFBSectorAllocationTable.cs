using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompoundTools.CFB
{
    class CFBSectorAllocationTable
    {
        List<int> SecIDs;
        public List<int> Sectors { get; set; }

        CFBFile Parent;

        public CFBSectorAllocationTable(byte[] data, List<int> sec_ids, CFBFile file)
        {
            this.SecIDs = sec_ids;
            this.Parent = file;
            this.Sectors = Parse(data, sec_ids, file.Header);
        }

        public static List<int> Parse(byte[] data, List<int> sec_ids, CFBHeader header)
        {
            uint SecSize = header.SectorSize;
            List<int> sectors = new List<int>();

            foreach (int sec in sec_ids)
            {
                if (sec < 0)
                {
                    if (sec == -2)
                    {
                        Console.WriteLine("Found EoC while reading SAT");
                        break;
                    }
                }
                else
                {
                    int pos = GetSecPos(sec, header);
                    byte[] bytes = new byte[SecSize];

                    for (int i = pos; i < pos + SecSize; i++)
                    {
                        bytes[i - pos] = data[i];
                    }

                    for (int i = 0; i < bytes.Length; i += 4)
                    {
                        sectors.Add(BitConverter.ToInt32(bytes, i));
                    }
                }
            }

            return sectors;
        }

        public static int GetSecPos(int SecID, CFBHeader header)
        {
            return (int)(512 + (SecID * header.SectorSize));
        }
    }
}
