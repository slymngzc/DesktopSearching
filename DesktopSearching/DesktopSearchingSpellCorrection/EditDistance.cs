using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopSearchingSpellCorrection
{
    public static class EditDistance
    {
        public static int LevDistance(string kaynak, string amac)
        {
            kaynak = kaynak.Replace(" ","").ToLower();
            amac = amac.Replace(" ","").ToLower();
            int kuz = kaynak.Length;//n
            int auz = amac.Length;//m

            int[,] MesMatrix = new int[kuz + 1, auz + 1];

            if (kuz == 0)
            {
                return auz;
            }

            if (auz == 0)
            {
                return kuz;
            }

            for (int i = 0; i <= kuz; i++)
            {
                MesMatrix[i, 0] = i;
            }

            for (int j = 0; j <= auz; j++)
            {
                MesMatrix[0, j] = j;
            }

            for (int i = 1; i <= kuz; i++)
            {
                for (int j = 1; j <= auz; j++)
                {
                    int maliyet = amac[j - 1] == kaynak[i - 1] ? 0 : 1;

                    MesMatrix[i, j] = Math.Min(Math.Min(MesMatrix[i - 1, j] + 1, MesMatrix[i, j - 1] + 1), MesMatrix[i - 1, j - 1] + maliyet);

                }
            }

            return MesMatrix[kuz, auz];

        }
    }
}
