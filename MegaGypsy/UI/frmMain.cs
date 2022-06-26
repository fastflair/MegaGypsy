using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Linq;
//using Excel = Microsoft.Office.Interop.Excel;

//  Since October 7, 2015, the game has used a 5/69 (white balls) + 1/26 (Powerballs) matrix from which winning numbers are chosen
// resulting in odds of 1 in 292,201,338 of winning a jackpot per play.[1] Each play costs $2, or $3 with the Power Play option

namespace MegaGypsy {
    public partial class frmMain : Form {
        private static bool debug = true;
        int[] ABallsPicked = null;
        int[] AmegaBallPicked = null;
        LottoThermo[] ALotThermPicks = new LottoThermo[70];
        LottoThermo[] ALotThermMega = new LottoThermo[25];
        int countDrawings = 0;
        double totalProbablity = 0.0;
        double totalProbablityMega = 0.0;

        int[] Picks = new int[6];
        public frmMain() {
            InitializeComponent();
            getWebHistoryPB();
            buildWeibulldist();
            generateLottoW();
        }

        private void generateLottoW() {
            string text = "";
            rtbPicks.Text = "";
            int numbPicks = Convert.ToInt32(txtPickNum.Text);
            CryptoRandom rng = new CryptoRandom();

            for (int pickNum = 0; pickNum < numbPicks; pickNum++) {
                Picks = new int[6];
                bool passTest = false;

                while (!passTest) {
                    for (int i = 0; i < 5; i++) {
                        Picks[i] = (lotteryDistributionW(rng.NextDouble()));
                    }
                    Picks[5] = (powerBallDistributionW(rng.NextDouble()));
                    passTest = validatePicks();
                }
                Array.Sort(Picks, 0, 5);
                for (int i = 0; i < 6; i++) {
                    text += Picks[i].ToString() + " - ";
                }
                text = text.Remove(text.Length - 3) + "\n";
            }
            rtbPicks.Text = text;
        }
        private void generateLotto() {
            string text = "";
            rtbPicks.Text = "";
            int numbPicks = Convert.ToInt32(txtPickNum.Text);
            CryptoRandom rng = new CryptoRandom();

            for (int pickNum = 0; pickNum < numbPicks; pickNum++) {
                Picks = new int[6];
                bool passTest = false;

                while (!passTest) {
                    for (int i = 0; i < 5; i++) {
                        Picks[i] = (lotteryDistribution(rng.NextDouble()));
                    }
                    Picks[5] = (powerBallDistribution(rng.NextDouble()));
                    passTest = validatePicks();
                }
                Array.Sort(Picks, 0, 5);
                for (int i = 0; i < 6; i++) {
                    text += Picks[i].ToString() + " - ";
                }
                text = text.Remove(text.Length - 3) + "\n";
            }
            rtbPicks.Text = text;
        }

        // rules
        // sum of main 5 drawn numbers must be between 80 and 181
        // all six numbers must have at least 2 even and 2 odd numbers
        private int lotteryDistributionW(double random) {
            // distribution below is based drawings after Oct 7 2015, 5 balls per drawing
            double tempSum = 0.0;
            double freq = 0.0;
            int ball = 0;

            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 1; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 2; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 3; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 4; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 5; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 6; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 7; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 8; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 9; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 10; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 11; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 12; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 13; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 14; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 15; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 16; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 17; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 18; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 19; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 20; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 21; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 22; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 23; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 24; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 25; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 26; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 27; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 28; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 29; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 30; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 31; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 32; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 33; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 34; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 35; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 36; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 37; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 38; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 39; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 40; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 41; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 42; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 43; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 44; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 45; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 46; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 47; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 48; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 49; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 50; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 51; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 52; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 53; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 54; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 55; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 56; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 57; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 58; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 59; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 60; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 61; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 62; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 63; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 64; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 65; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 66; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 67; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 68; tempSum += freq;
            freq = ALotThermPicks[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablity) return 69; tempSum += freq;
            return 0;
        }
        private int powerBallDistributionW(double random) {
            // distribution below is based on drawings since Oct 7 2015
            double tempSum = 0.0;
            double freq = 0.0;
            int ball = 0;

            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 1; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 2; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 3; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 4; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 5; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 6; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 7; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 8; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 9; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 10; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 11; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 12; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 13; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 14; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 15; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 16; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 17; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 18; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 19; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 20; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 21; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 22; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 23; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 24; tempSum += freq;
            freq = ALotThermMega[ball++].currentProbability; if (random <= (double)(freq + tempSum) / totalProbablityMega) return 25; tempSum += freq;
            return 0;
        }

        private int lotteryDistribution(double random) {
            // distribution below is based drawings after Oct 7 2015, 5 balls per drawing
            int totalNumbers = AmegaBallPicked.Length * 5;
            int tempSum = 0;
            int freq = 0;
            int ball = 1;

            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 1; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 2; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 3; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 4; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 5; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 6; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 7; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 8; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 9; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 10; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 11; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 12; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 13; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 14; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 15; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 16; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 17; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 18; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 19; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 20; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 21; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 22; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 23; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 24; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 25; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 26; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 27; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 28; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 29; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 30; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 31; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 32; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 33; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 34; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 35; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 36; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 37; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 38; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 39; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 40; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 41; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 42; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 43; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 44; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 45; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 46; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 47; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 48; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 49; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 50; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 51; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 52; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 53; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 54; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 55; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 56; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 57; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 58; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 59; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 60; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 61; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 62; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 63; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 64; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 65; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 66; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 67; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 68; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 69; tempSum += freq;
            freq = countPicks(ball++); if (random <= (double)(freq + tempSum) / totalNumbers) return 70; tempSum += freq;
            return 0;
        }
        private int powerBallDistribution(double random) {
            // distribution below is based on drawings since Oct 7 2015
            int totalNumbers = AmegaBallPicked.Length;
            int tempSum = 0;
            int freq = 0;
            int ball = 1;

            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 1; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 2; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 3; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 4; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 5; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 6; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 7; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 8; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 9; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 10; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 11; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 12; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 13; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 14; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 15; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 16; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 17; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 18; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 19; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 20; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 21; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 22; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 23; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 24; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 25; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 26; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 27; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 28; tempSum += freq;
            freq = countPicksMega(ball++);; if (random <= (double)(freq + tempSum) / totalNumbers) return 29; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 30; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 31; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 32; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 33; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 34; tempSum += freq;
            freq = countPicksMega(ball++);  if (random <= (double)(freq + tempSum) / totalNumbers) return 35; tempSum += freq;
            return 0;
        }

        private bool validatePicks() {
            bool testEvenOdd = false;
            bool testDuplicates = false;
            bool sumTest = false;
            int odd = 0;
            int even = 0;
            for (int i = 0; i < Picks.Length; i++) {
                if (Picks[i] % 2 == 0) {
                    even++;
                } else {
                    odd++;
                }
            }
            if (even > 1 && odd > 1) testEvenOdd = true;

            // test for duplicate numbers, except powerball
            for (int i = 0; i < Picks.Length - 1; i++) {
                for (int j = 0; j < i; j++) {
                    if (Picks[j] == Picks[i]) {
                        testDuplicates = true;
                    }
                }
            }

            // test sum range, no powerball
            int sum = 0;
            for (int i = 0; i < Picks.Length - 1; i++) {
                sum += Picks[i];
            }
            if (sum >= 80 && sum <= 181) sumTest = true;

            if (testEvenOdd && !testDuplicates && sumTest) return true;
            return false;
        }

        private void button1_Click(object sender, EventArgs e) {
            generateLotto();
        }

        private void btnCheckWin_Click(object sender, EventArgs e) {
            // get array of current winning numbers and powerball
            String s1 = txtWin.Text;
            int[] win = convStringtoIntA(s1);
            // load history
            string filename = SelectTextFile("%USERPROFILE%\\My Documents");
            // check winners and calculate prizes
            int winnings = 0;
            try {
                using (StreamReader sr = new StreamReader(filename)) {
                    String line = sr.ReadToEnd();
                    char[] seps = { '\r', '\n' };
                    String[] values = line.Split(seps);
                    int[] myPick;
                    for (int i = 0; i < values.Length; i++) {
                        if (values[i].Length > 2) {
                            myPick = convStringtoIntA(values[i]);
                            winnings += checkWin(myPick, win);
                        }
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }
            txtWin.Text += " - " + winnings;

        }
        private int[] convStringtoIntA(string s1) {
            char[] seps = { ',', '-' };
            String[] values = s1.Split(seps);
            int[] win = new int[values.Length];

            for (int x = 0; x < win.Length; x++) {
                win[x] = Convert.ToInt32(values[x].ToString());
            }
            return win;
        }
        private string SelectTextFile(string initialDirectory) {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter =
               "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.InitialDirectory = initialDirectory;
            dialog.Title = "Select a text file";
            return (dialog.ShowDialog() == DialogResult.OK)
               ? dialog.FileName : null;
        }
        private int checkWin(int[] myPick, int[] win) {
            int winnings = 0;
            int matches = 0;
            bool PowerBall = false;
            if (myPick[5] == win[5]) PowerBall = true;
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 5; j++) {
                    if (win[i] == myPick[j]) matches++;
                }
            }
            if (matches == 5 && PowerBall) {
                winnings = 50000000;
            } else if (matches == 5) {
                winnings = 2000000;
            } else if (matches == 4 && PowerBall) {
                winnings = 40000;
            } else if (matches == 4 || (matches == 3 && PowerBall)) {
                winnings = 200;
            } else if (matches == 3 || (matches == 2 && PowerBall)) {
                winnings = 14;
            } else if (PowerBall || (matches == 1 && PowerBall)) {
                winnings = 12;
            }
            return winnings;
        }

        #region ExternalFileHandling
        //private object[,] loadExcelData() {
        //    // Get loaded files
        //    ArrayList listExcelProcIDs = new ArrayList();
        //    foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("EXCEL")) {
        //        listExcelProcIDs.Add(proc.Id);
        //    }
        //    // Specify worksheet name
        //    string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //    string filename = OpenFile(myDocumentsPath + "\\lotto");
        //    if (filename == "") return null;

        //    Excel.Application excelApp = new Excel.Application();  // Creates a new Excel Application
        //    excelApp.Visible = true;  // Makes Excel visible to the user.

        //    // The following line if uncommented adds a new workbook
        //    //Excel.Workbook newWorkbook = excelApp.Workbooks.Add();

        //    Excel.Workbook excelWorkbook = null;

        //    try {
        //        excelWorkbook = excelApp.Workbooks.Open(filename, 0,
        //            false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true,
        //            false, 0, true, false, false);
        //    } catch {
        //        //Create a new workbook if the existing workbook failed to open.
        //        MessageBox.Show("Error: Could not open excel file.");
        //        return null;
        //    }

        //    // The following gets the Worksheets collection
        //    Excel.Sheets excelSheets = excelWorkbook.Worksheets;

        //    // The following gets Sheet1 for editing - 1 can be replaced with the string name of the desired sheet
        //    Excel.Worksheet excelWorksheet = excelSheets.get_Item(1);

        //    // The following gets cell A1 for editing
        //    Excel.Range excelCell = (Excel.Range)excelWorksheet.get_Range("A1", "A1");

        //    Excel.Range firstCell = excelWorksheet.get_Range("A1", Type.Missing);
        //    Excel.Range lastCell = excelWorksheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell, Type.Missing);

        //    object[,] cellValues;

        //    Excel.Range worksheetCells = excelWorksheet.get_Range(firstCell, lastCell);
        //    cellValues = worksheetCells.Value2 as object[,];
        //    excelWorkbook.Close(false);
        //    foreach (System.Diagnostics.Process proc in System.Diagnostics.Process.GetProcessesByName("EXCEL")) {
        //        if (!listExcelProcIDs.Contains(proc.Id))
        //            proc.Kill();
        //    }
        //    return (cellValues);
        //}
        private string OpenFile(string InitialDirectory) {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = InitialDirectory;
            openFileDialog1.Filter = "Excel macro files (*.xlsm)|*.xlsm|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                try {
                    return openFileDialog1.FileName;
                } catch (Exception ex) {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
            return "";
        }
        #endregion

        private void button2_Click(object sender, EventArgs e) {
            //object[,] excelData = loadExcelData();
            getWebHistoryPB();
        }
        private void getWebHistoryPB() {
            //  Since October 7, 2015, the game has used a 5/69 (white balls) + 1/26 (Powerballs) matrix from which winning numbers are chosen
            // resulting in odds of 1 in 292,201,338 of winning a jackpot per play.[1] Each play costs $2, or $3 with the Power Play option

            string URL = @"https://www.texaslottery.com/export/sites/lottery/Games/Mega_Millions/Winning_Numbers/megamillions.csv";
            string results = WebUtils.GetCSV(URL);
            List<int> ballsPicked = new List<int>();
            List<int> megaBallPicked = new List<int>();
            DateTime firstDate = new DateTime(2017, 10, 28);
            DateTime test;

            // Init Weibull
            for(int i=0; i< ALotThermPicks.Length; i++) {
                ALotThermPicks[i] = new LottoThermo();
            }
            for (int i = 0; i < ALotThermMega.Length; i++) {
                ALotThermMega[i] = new LottoThermo();
            }

            // Manually parse results
            string[] values = results.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for(int i=0; i<values.Length; i++) {
                values[i] = values[i].Replace("\r\n", "");
                values[i] = values[i].Replace("Mega Millions", "");
            }
            countDrawings = 0;
            string testDate = "";
            for (int i = 1; i < values.Length; i += 10) {
                testDate = values[i] + "/" + values[i + 1] + "/" + values[i + 2];
                DateTime.TryParse(testDate, out test);
                if (test >= firstDate) {
                    countDrawings++;
                    try {
                        if (debug) Console.Out.WriteLine("Date = " + testDate + ": " + values[i + 3] + ", " + values[i + 4] + ", " + values[i + 5] + " , " + values[i + 6] + " , " + values[i + 7] + " - " + values[i + 8] + " : " + values[i + 9] );
                        ballsPicked.Add(Convert.ToInt32(values[i + 3]));
                        ballsPicked.Add(Convert.ToInt32(values[i + 4]));
                        ballsPicked.Add(Convert.ToInt32(values[i + 5]));
                        ballsPicked.Add(Convert.ToInt32(values[i + 6]));
                        ballsPicked.Add(Convert.ToInt32(values[i + 7]));
                        megaBallPicked.Add(Convert.ToInt32(values[i + 8]));

                        //Build Weibull
                        ALotThermPicks[(Convert.ToInt32(values[i + 3])) - 1].LNumberDrawings.Add(countDrawings);
                        ALotThermPicks[(Convert.ToInt32(values[i + 4])) - 1].LNumberDrawings.Add(countDrawings);
                        ALotThermPicks[(Convert.ToInt32(values[i + 5])) - 1].LNumberDrawings.Add(countDrawings);
                        ALotThermPicks[(Convert.ToInt32(values[i + 6])) - 1].LNumberDrawings.Add(countDrawings);
                        ALotThermPicks[(Convert.ToInt32(values[i + 7])) - 1].LNumberDrawings.Add(countDrawings);
                        ALotThermMega[(Convert.ToInt32(values[i + 8])) - 1].LNumberDrawings.Add(countDrawings);

                    } catch (Exception ex) {
                        Console.Out.WriteLine(ex.Message);
                    }
                 }
            }
            Console.Out.WriteLine("Number of drawings: " + countDrawings);
            // Init Weibull
            for (int i = 0; i < ALotThermPicks.Length; i++) {
                for (int j = 0; j < ALotThermPicks[i].LNumberDrawings.Count; j++)
                    ALotThermPicks[i].LNumberDrawings[j] = countDrawings - ALotThermPicks[i].LNumberDrawings[j];
            }
            for (int i = 0; i < ALotThermMega.Length; i++) {
                for (int j = 0; j < ALotThermMega[i].LNumberDrawings.Count; j++)
                    ALotThermMega[i].LNumberDrawings[j] = countDrawings - ALotThermMega[i].LNumberDrawings[j];
            }

            ABallsPicked = ballsPicked.ToArray();
            AmegaBallPicked = megaBallPicked.ToArray();
        }

        private int countPicks(int ballNum) {
            int count = 0;
            for(int i=0; i< ABallsPicked.Length; i++) {
                if (ABallsPicked[i] == ballNum) {
                    count++;
                }
            }
            return count;
        }

        private int countPicksMega(int ballNum) {
            int count = 0;
            for (int i = 0; i < AmegaBallPicked.Length; i++) {
                if (AmegaBallPicked[i] == ballNum) count++;
            }
            return count;
        }

        public void buildWeibulldist() {
            //ALotThermPicks[0].LDT.Reverse();
            totalProbablity = 0.0;
            totalProbablityMega = 0.0;
            double daysSinceLastPick = 0.0;
            double instantMeanTimeBetweenPick = 0.0;

            for(int i=0; i<ALotThermPicks.Length; i++) {
                ALotThermPicks[i].calcStats();
                daysSinceLastPick = (double)(countDrawings + 1) - ALotThermPicks[i].LNumberDrawings[0];
                instantMeanTimeBetweenPick = daysSinceLastPick - ALotThermPicks[i].meanTimeBetweenPicks;
                ALotThermPicks[i].currentProbability = (instantMeanTimeBetweenPick - ALotThermPicks[i].minTimeBetweenPicks) * (100.0 / ALotThermPicks[i].range);
                if (ALotThermPicks[i].currentProbability > 99.0 || ALotThermPicks[i].currentProbability < 1.0) {
                    Console.Out.WriteLine("Anomaly in probability of ball " + (i + 1) + ": " + ALotThermPicks[i].currentProbability);
                    if (ALotThermPicks[i].currentProbability < 1.0) {
                        ALotThermPicks[i].currentProbability = 1.0;
                    } else if (ALotThermPicks[i].currentProbability > 99.0) {
                        ALotThermPicks[i].currentProbability = 99.0;
                    }
                }
                totalProbablity += ALotThermPicks[i].currentProbability;

            }

            daysSinceLastPick = 0.0;
            instantMeanTimeBetweenPick = 0.0;
            for (int i = 0; i < ALotThermMega.Length; i++) {
                ALotThermMega[i].calcStats();
                daysSinceLastPick = (double)(countDrawings + 1) - ALotThermMega[i].LNumberDrawings[0];
                instantMeanTimeBetweenPick = daysSinceLastPick - ALotThermMega[i].meanTimeBetweenPicks;
                ALotThermMega[i].currentProbability = (instantMeanTimeBetweenPick - ALotThermMega[i].minTimeBetweenPicks) * (100.0 / ALotThermMega[i].range);
                if (ALotThermMega[i].currentProbability > 99.0 || ALotThermMega[i].currentProbability < 1.0) {
                    Console.Out.WriteLine("Anomaly in probability of Mega  ball " + (i + 1) + ": " + ALotThermMega[i].currentProbability);
                    if (ALotThermMega[i].currentProbability < 1.0) {
                        ALotThermMega[i].currentProbability = 1.0;
                    } else if (ALotThermMega[i].currentProbability > 99.0) {
                        ALotThermMega[i].currentProbability = 99.0;
                    }
                } else {
                    Console.Out.WriteLine("        in probability of Mega  ball " + (i + 1) + ": " + ALotThermMega[i].currentProbability);
                }
                totalProbablityMega += ALotThermPicks[i].currentProbability;
            }
        }
    }
}
