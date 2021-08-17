using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Management;

namespace discovery.Library.Core
{
    public class performancetuner
    {
        public performancetuner()
        {
            this.ServerRamInvestigation();
        }
        public ulong InstalledRam { get; set; }
        public ulong AvailableRam { get; set; }
        public ulong TotalVirtual { get; set; }
        public ulong AvailableVirtual { get; set; }

        public bool MemoryOveload<T>(ref T obj)
        {
            //Check whether 30% of RAM available for processing (less than this threshold the process must be dumpped)
            if ((this.AvailableRam / this.InstalledRam) < 0.30)
                return true;

            //Check whether an object is big as same as 25% of server available RAM (bigger than this threshold object must be dumpped)
            else if ((this.GetStructureSize(obj) / this.AvailableRam) > 0.025)
                return true;

            return false;

        }
        public ulong GetStructureSize<T>(T obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            long ret = 0;
            using (var ms = new MemoryStream())
            {
                try
                {
                    bf.Serialize(ms, obj);
                    ret = ms.Length;
                }
                catch (Exception ex) { }
            }
            return (ulong)ret;
        }

        public void ServerRamInvestigation()
        {

            ObjectQuery wql = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);
            ManagementObjectCollection results = searcher.Get();

            foreach (ManagementObject result in results)
            {
                this.AvailableRam += (ulong)result["TotalVisibleMemorySize"];
                this.InstalledRam += (ulong)result["FreePhysicalMemory"];
                this.TotalVirtual += (ulong)result["TotalVirtualMemorySize"];
                this.AvailableVirtual += (ulong)result["FreeVirtualMemory"];
            }
            //I nstalledRam = Devices.ComputerInfo().TotalPhysicalMemory;
            //return new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;

        }

    }
}
