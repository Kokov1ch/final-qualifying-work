using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ships
{
    //Это класс корабля. Он умеет считывать из икселя, хранить шпангоуты
    //Рассчитывать объём тоже может
    public class Vessel
    {
        public void readFromExcel(string docPath)
        {
            Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook = ObjExcel.Workbooks.Open(@docPath, 0, false, 5, "", "", false, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);
            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
            ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];

            int numCol = 1;

            Microsoft.Office.Interop.Excel.Range usedColumn1 = ObjWorkSheet.UsedRange.Columns[1];
            Microsoft.Office.Interop.Excel.Range usedColumn2 = ObjWorkSheet.UsedRange.Columns[2];

            System.Array myvalues1 = (System.Array)usedColumn1.Cells.Value2;
            System.Array myvalues2 = (System.Array)usedColumn2.Cells.Value2;


            string[] strArray1 = myvalues1.OfType<object>().Select(o => o.ToString()).ToArray();
            string[] strArray2 = myvalues2.OfType<object>().Select(o => o.ToString()).ToArray();

            // Выходим из программы Excel.
            ObjExcel.Quit();

            Console.WriteLine(strArray1[0]);

            //Vessel one = new Vessel();

            //oneFrame.cordinates.Add(newPair);
            //one.frames.Add(oneFrame);
            //one.frames.ToArray();

            bool[] Shpaki = new bool[strArray1.Length];
            for (int i = 0; i < Shpaki.Length; i++)
            {
                if (this.isAFrameCoordinate(strArray1[i])) Shpaki[i] = true; else Shpaki[i] = false;
            }

            int thisLineA = 0;
            int thisLineB = 0;
            int c = 0;
            while (thisLineA < strArray1.Length-1)
            {


                Vessel.Frame newFrame = new Vessel.Frame();
                newFrame.position = this.getDoubleFrameF(strArray1[thisLineA]);
                thisLineA++;
                //thisLineB++;

                while (!Shpaki[thisLineA] && thisLineA<strArray1.Length-1 && thisLineB < strArray2.Length-1)
                {
                    newFrame.cordinates.Add(new Vessel.Frame.Pair(newFrame.getDoubleFrame(strArray1[thisLineA]), double.Parse(strArray2[thisLineB])));
                    thisLineA++;
                    thisLineB++;
                }
                this.frames.Add(newFrame);

                /*if (thisLine < Shpaki.Length-1)
                {
                    while (!Shpaki[thisLine]&& thisLine < Shpaki.Length-1)
                    {
                        newFrame.cordinates.Add(new Vessel.Frame.Pair(newFrame.getDoubleFrame(strArray1[thisLine]), double.Parse(strArray2[thisLine - c])));
                        if (thisLine < Shpaki.Length - 1)
                            thisLine++;
                        else
                        {
                            break;
                        }
                    }
                    c++;
                    //c++;
                    this.frames.Add(newFrame);

                }

                if (thisLine == Shpaki.Length - 1) break;*/
            }
            
            //this.frames[frames.Count].cordinates.Add(new Vessel.Frame.Pair(newFrame.getDoubleFrame(strArray1[thisLineA]), double.Parse(strArray2[thisLine -])));
            int aye = 5;
        }
        public List<Frame> frames = new List<Frame>();
        //Проверяет, является ли строка координатой шпангоута
        public bool isAFrameCoordinate(string toCheck)
        {
            if ((toCheck[toCheck.Length - 1] == 'F') || (toCheck[toCheck.Length - 1] == 'A'))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Извлекает координату шпангоута в дабл
        public double getDoubleFrameF(string strCord)
        {
            if (strCord[strCord.Length - 1] == 'F')
            {
                return double.Parse(strCord.Substring(0, strCord.Length - 1));
            }
            else
                return -double.Parse(strCord.Substring(0, strCord.Length - 1));

        }
        //объём через сумму объёмов призм
        public double Volume()
        {
            double vol = 0;
            for(int i = 0; i < frames.Count-1; i++)
            {
                //vol = vol + (frames[i].getSquare() + frames[i + 1].getSquare()) / 2 * (frames[i].position - frames[i + 1].position);
                double h = Math.Abs(frames[i].position - frames[i + 1].position);
                vol = vol +  h/3 * 
                    (frames[i].getSquare() + frames[i + 1].getSquare() + Math.Sqrt(frames[i].getSquare() * frames[i + 1].getSquare()));
            }
            return vol;
        }

        //Класс шпангоутов. Хранит лист координат точек и считает площадь по формуле Гаусса.
        public class Frame
        {
            public List<Pair> cordinates = new List<Pair>();
            public class Pair
            {
                public double x, y;
                public Pair(double x, double y)
                {
                    this.x = x;
                    this.y = y;
                }
            }
            public double getSquare()
            {
                double sum = 0;
                for(int i = 0; i<cordinates.Count; i++)
                {
                    if (i == cordinates.Count - 1)
                    {
                        sum = sum + cordinates[i].x * (cordinates[0].y - cordinates[i - 1].y);
                    } else
                    if(i==0)
                    {
                        sum = sum + cordinates[i].x * (cordinates[i + 1].y - cordinates[cordinates.Count-1].y);
                    } else
                    sum = sum + cordinates[i].x * (cordinates[i + 1].y - cordinates[i - 1].y);
                }
                return Math.Abs(sum);
            }
            public double getDoubleFrame(string strCord)
            {
                if (strCord[strCord.Length - 1] == 'P')
                {
                    return -double.Parse(strCord.Substring(0, strCord.Length - 1));
                }
                else
                if (strCord[strCord.Length - 1] == 'S')
                {
                    return double.Parse(strCord.Substring(0, strCord.Length - 1));
                }
                else
                {
                    return double.Parse(strCord);
                }
            }

            public double position;

        }
    }
}
