using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CompoundTools.CFB;

namespace CompoundTools
{
    static class Extensions
    {
        public static T[] SubArray<T>(this T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
    }
    

    class Program
    {
        [STAThread]
        public static void Main()
        {
            string file = @"D:\Synos\C_B102.grf";
            byte[] data = File.ReadAllBytes(file);
            CFBFile cFile = new CFBFile(data);

            CFBTreeWindow tw = new CFBTreeWindow(cFile);
            tw.ShowDialog();

            Console.ReadLine();
        }
    }
}
