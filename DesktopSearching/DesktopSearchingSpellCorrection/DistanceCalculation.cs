using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopSearchingSpellCorrection
{
    public static class DistanceCalculation
    {
        public static FileSystemModel CalculateDistance(string amac)
        {
            Constants.allFileSystems.ForEach(fsm => fsm.Distance = EditDistance.LevDistance(fsm.Name, amac));
            return Constants.allFileSystems.OrderBy(fsm => fsm.Distance).FirstOrDefault();
        }
    }
}
