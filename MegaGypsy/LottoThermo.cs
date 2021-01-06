using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MegaGypsy {
    public class LottoThermo {
        public List<int> LNumberDrawings = new List<int>();
        public double meanTimeBetweenPicks = 1;
        public double minTimeBetweenPicks = 1;
        public double maxTimeBetweenPicks = 1;
        public double range = 1;
        public double currentProbability = 0.0;

        public void calcStats() {
            try {
                double total = 0.0;
                for (int i = 0; i < LNumberDrawings.Count - 1; i++) {
                    total += (double)(LNumberDrawings[i] - LNumberDrawings[i + 1]);
                }
                meanTimeBetweenPicks = total / ((double)LNumberDrawings.Count - 1.0);

                minTimeBetweenPicks = (LNumberDrawings[0] - LNumberDrawings[1]) - meanTimeBetweenPicks;
                maxTimeBetweenPicks = (LNumberDrawings[0] - LNumberDrawings[1]) - meanTimeBetweenPicks;
                double timeBetweenPicks = 0;
                for (int i = 1; i < LNumberDrawings.Count - 1; i++) {
                    meanTimeBetweenPicks = total / ((double)LNumberDrawings.Count - 1.0);
                    timeBetweenPicks = (double)(LNumberDrawings[i] - LNumberDrawings[i + 1]) - meanTimeBetweenPicks;
                    if (maxTimeBetweenPicks < timeBetweenPicks) maxTimeBetweenPicks = timeBetweenPicks;
                    if (minTimeBetweenPicks > timeBetweenPicks) minTimeBetweenPicks = timeBetweenPicks;
                }
                range = maxTimeBetweenPicks - minTimeBetweenPicks;
            } catch (Exception ex) {
                Console.Out.WriteLine(ex.Message);
            }
        }

    }
}
