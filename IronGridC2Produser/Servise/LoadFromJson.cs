using IronGridC2Produser.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IronGridC2Produser.Servise
{
    public class LoadFromJson
    {

        public List<RealTimeReports> LoadRealTimeReportsData(string filePath)
        {
            string json = File.ReadAllText(filePath);
            List<RealTimeReports>? RealTimeReportsList = JsonSerializer.Deserialize<List<RealTimeReports>>(json);
            return RealTimeReportsList ?? new List<RealTimeReports>();
        }
    }
}
