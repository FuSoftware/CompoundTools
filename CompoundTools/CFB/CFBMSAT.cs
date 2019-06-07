using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompoundTools.CFB
{
    public class CFBMasterSector
    {
        public List<int> Sectors { get; set; }
        public int NextSector { get; set; }

        public CFBMasterSector(byte[] Data, int Pos, uint SecSize)
        {
            Sectors = new List<int>();
            byte[] bytes = new byte[SecSize];
            for (int i = Pos; i < Pos + SecSize; i++)
            {
                bytes[i-Pos] = Data[i];
            }

            for (int i = 0; i < bytes.Length - 4; i+=4)
            {
                Sectors.Add(BitConverter.ToInt32(bytes, i));
            }

            NextSector = BitConverter.ToInt32(bytes, bytes.Length - 4);
        }
    }

    public class CFBMSAT
    {
        List<CFBMasterSector> MasterSectors;
        List<int> HeaderSectors;
        public List<int> Sectors
        {
            get
            {
                List<int> S = new List<int>(HeaderSectors);
                foreach(var sect in MasterSectors) S.AddRange(sect.Sectors);
                return S;
            }
        }

        public CFBMSAT(byte[] data, CFBFile file)
        {
            this.Parse(data, file);
        }

        public void Parse(byte[] data, CFBFile file)
        {
            this.HeaderSectors = new List<int>(file.Header.MSAT);
            this.MasterSectors = ParseMSAT(data, file.Header);
        }

        public static List<CFBMasterSector> ParseMSAT(byte[] data, CFBHeader header)
        {
            List<CFBMasterSector> msat = new List<CFBMasterSector>();
            uint nSec = header.MasterSectorCount;
            CFBMasterSector currentSector;
            int nextSector = 0;

            if(header.FirstExternalMasterSector >= 0)
            {
                nextSector = header.FirstExternalMasterSector;
                do
                {
                    currentSector = new CFBMasterSector(data, nextSector, header.SectorSize);
                    nextSector = currentSector.NextSector;
                } while (nextSector > 0);
            }

            Console.WriteLine("Found {0} tokens in the MSAT, expected {1}", msat.Count, nSec);

            return msat;
        }
    }
}
