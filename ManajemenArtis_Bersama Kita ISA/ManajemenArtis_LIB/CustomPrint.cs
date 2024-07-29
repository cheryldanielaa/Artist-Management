using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManajemenArtis_LIB
{
    public class CustomPrint
    {
        private Font fontType;
        private StreamReader prinToFile;
        private float marginLeft, marginRight, marginTop, marginBottom;

        public CustomPrint(Font fontType, StreamReader prinToFile, float marginLeft,
            float marginRight, float marginTop, float marginBottom)
        {
            FontType = fontType;
            PrinToFile = prinToFile;
            MarginLeft = marginLeft;
            MarginRight = marginRight;
            MarginTop = marginTop;
            MarginBottom = marginBottom;
        }

        public Font FontType { get => fontType; set => fontType = value; }
        public StreamReader PrinToFile { get => prinToFile; set => prinToFile = value; }
        public float MarginLeft { get => marginLeft; set => marginLeft = value; }
        public float MarginRight { get => marginRight; set => marginRight = value; }
        public float MarginTop { get => marginTop; set => marginTop = value; }
        public float MarginBottom { get => marginBottom; set => marginBottom = value; }

        private void PrintText (object sender, PrintPageEventArgs e)
        {
            //compute the maximum number of rows in a page
            int maxRow = (int)((e.MarginBounds.Height - MarginTop-MarginBottom) / FontType.GetHeight(e.Graphics));
            float y = MarginTop; //indicate the latest position of y
            int rowNum = 0; //indicate the latest row number

            string rowText = PrinToFile.ReadLine();//text to be printed
            while(rowNum<maxRow && rowText!=null)
            {
                y = marginTop + (rowNum * FontType.GetHeight(e.Graphics));
                //print the text in black colour
                e.Graphics.DrawString(rowText, FontType, Brushes.Black, MarginLeft, y);
                rowNum++;
                rowText = prinToFile.ReadLine();//read the next line to be printed
            }
            if (rowText != null)
            {
                e.HasMorePages = true;
            }
            else
            {
                e.HasMorePages = false;
            }
        }
        public void SendToPrinter()
        {
            PrintDocument p = new PrintDocument();
            p.PrinterSettings.PrinterName = "Microsoft Print to PDF";

            p.PrintPage += new PrintPageEventHandler(PrintText);
            p.Print();

            PrinToFile.Close();
        }
    }
}
