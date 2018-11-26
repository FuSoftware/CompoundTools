using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompoundTools.CFB
{
    public class CFBHeader
    {
        //public static ulong CFBid = 0xD0CF11E0A1B11AE1;
        public static ulong CFBID = 0xE11AB1A1E011CFD0;

        public ulong FileId { get; set; }
        public byte[] UID { get; set; }
        public ushort FormatRevision { get; set; }
        public ushort FormatVersion { get; set; }
        public ushort BOM { get; set; }
        public ushort SectorSizePow { get; set; }
        public ushort ShortSectorSizePow { get; set; }
        public uint SectorCount { get; set; }
        public int FirstSector { get; set; }
        public uint StreamMinSize { get; set; }
        public int FirstShortSector { get; set; }
        public uint ShortSectorCount { get; set; }
        public int FirstExternalMasterSector { get; set; }
        public uint MasterSectorCount { get; set; }
        public int[] MSAT;

        public bool LittleEndian
        {
            get => BOM == 0xFFFE; //Reversed because C# natively reads in Little Endian
        }

        public bool HasCFBId
        {
            get => this.FileId == CFBID;
        }

        public uint SectorSize
        {
            get => (uint)Math.Pow(2, SectorSizePow);
        }

        public uint ShortSectorSize
        {
            get => (uint)Math.Pow(2, ShortSectorSizePow);
        }

        public CFBHeader(byte[] data)
        {
            this.Parse(data);
        }

        void Parse(byte[] data)
        {
            FileId = BitConverter.ToUInt64(data,0);

            FormatRevision = BitConverter.ToUInt16(data, 24);
            FormatVersion = BitConverter.ToUInt16(data, 26);
            BOM = BitConverter.ToUInt16(data, 28);
            SectorSizePow = BitConverter.ToUInt16(data, 30);
            ShortSectorSizePow = BitConverter.ToUInt16(data, 32);

            SectorCount = BitConverter.ToUInt32(data, 44);
            FirstSector = BitConverter.ToInt32(data, 48);
            StreamMinSize = BitConverter.ToUInt32(data, 56);
            FirstShortSector = BitConverter.ToInt32(data, 60);
            ShortSectorCount = BitConverter.ToUInt32(data, 64);
            FirstExternalMasterSector = BitConverter.ToInt32(data, 68);
            MasterSectorCount = BitConverter.ToUInt32(data, 72);

            MSAT = new int[109];
            for(int i = 0; i < 109; i++)
            {
                int pos = 76 + 4 * i;
                MSAT[i] = BitConverter.ToInt32(data, pos);
            }
        }
    }
}
