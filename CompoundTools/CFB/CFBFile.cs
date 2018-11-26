using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompoundTools.CFB
{
    
    class CFBFile
    {
        enum SpecialSector
        {
            FREE = -1,
            EndOfChain = -2,
            SAT = -3,
            MSAT = -4
        }

        public CFBSectorAllocationTable SAT;
        public CFBHeader Header;
        public CFBMSAT MSAT;
        public CFBSSAT SSAT;

        public CFBFile(byte[] data)
        {
            this.Parse(data);
        }

        void Parse(byte[] data)
        {
            this.Header = new CFBHeader(data);
            this.MSAT = new CFBMSAT(data, this);
            this.SAT = new CFBSectorAllocationTable(data, this.MSAT.Sectors, this);
            this.SSAT = new CFBSSAT(data, this);
        }
    }
}
