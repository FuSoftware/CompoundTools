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

        public CFBSAT SAT { get; set; }
        public CFBHeader Header { get; set; }
        public CFBMSAT MSAT { get; set; }
        public CFBSSAT SSAT { get; set; }
        public CFBDirectoryEntry RootDirectory { get; set; }
        public CFBStream RootStream {get;set;}
        public CFBElement RootElement { get; set; }
        public byte[] Data { get; set; }

        public CFBFile(byte[] data)
        {
            this.Parse(data);
        }

        void Parse(byte[] data)
        {
            this.Data = data;
            this.Header = new CFBHeader(data);
            this.MSAT = new CFBMSAT(data, this);
            this.SAT = new CFBSAT(data, this.MSAT.Sectors, this);
            this.RootStream = new CFBStream(0, this);

            this.RootElement = CFBElement.ParseHierarchy(0, this);
            //Console.WriteLine(CFBElement.PrintableHierarchy(this.RootElement));
            Console.WriteLine(this.RootElement.TotalChildrenCount());
            //this.SSAT = new CFBSSAT(data, this);
        }
    }
}
