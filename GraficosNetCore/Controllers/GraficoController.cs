using GraficosNetCore.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Formatting;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GraficosNetCore.Controllers
{
    public class GraficoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GraficoBarra()
        {
            return View();
        }

        public string graficoBarras()
        {
            List<string> listaX = new List<string> { "Ene-Mar", "Abr-Jun", "Jul-Sep", "Oct-Dic" };

            List<decimal> listaY = new List<decimal>();
            List<decimal> listavalores = new List<decimal>() { 790, 820, 680, 1350 };

            byte[] data = Grafico.crearGraficoBarraVertical(listaX, listavalores);

            return Convert.ToBase64String(data);
        }

            public string graficoInicial1()
        {
            Bitmap oBitmap = new Bitmap(800, 400);
            Graphics grafico = Graphics.FromImage(oBitmap);
            Rectangle rec;
            Pen open = new Pen(Brushes.Black, 3);

            using (MemoryStream ms = new MemoryStream())
            {
                rec = new Rectangle(0, 0, oBitmap.Width, oBitmap.Height);
                grafico.FillRectangle(Brushes.White, rec);

                grafico.DrawLine(open, new Point(100, 300), new Point(400, 100));
                grafico.DrawLine(open, new Point(400, 100), new Point(700, 300));
                grafico.DrawLine(open, new Point(700, 300), new Point(100, 300));

                oBitmap.Save(ms, ImageFormat.Png);
                byte[] data = ms.ToArray();
                return Convert.ToBase64String(data);

            }
        }
        public string graficoInicial()
        {
            Bitmap oBitmap = new Bitmap(800,400);
            Graphics grafico = Graphics.FromImage(oBitmap);
            Rectangle rec;

            using (MemoryStream ms = new MemoryStream())
            {
                

                rec = new Rectangle(0, 50, oBitmap.Width, 350);

                LinearGradientBrush gradiante = new LinearGradientBrush(rec, Color.Tomato, Color.Teal,
                    LinearGradientMode.BackwardDiagonal);


                grafico.FillRectangle(gradiante, rec);
                rec = new Rectangle(0, 0,oBitmap.Width,50);


                grafico.FillRectangle(Brushes.White, rec);

                rec = new Rectangle(100, 100, 100, 50);
                grafico.FillRectangle(Brushes.White, rec);

                rec = new Rectangle(600, 100, 100, 50);
                grafico.FillRectangle(Brushes.White, rec);

                rec = new Rectangle(300, 200, 200, 50);
                grafico.FillRectangle(Brushes.White, rec);

                rec = new Rectangle(100, 300, 600, 50);
                grafico.FillRectangle(Brushes.White, rec);

                StringFormat formato = new StringFormat(StringFormatFlags.NoClip);
                formato.Alignment = StringAlignment.Center;
                grafico.DrawString("Mi Primer grafico", new Font("Arial", 14), Brushes.Black, new Point(400, 0), formato);

                oBitmap.Save(ms, ImageFormat.Png);
                byte[] data = ms.ToArray();
                return Convert.ToBase64String(data);
            }
        }
    }
}


/*
  public string graficoBarras()
        {
            List<string> listaX = new List<string> { "Ene-Mar", "Abr-Jun", "Jul-Sep", "Oct-Dic" };
            //List<decimal> listaY = new List<decimal> { 0, 5000, 10000, 15000 };
            List<decimal> listaY = new List<decimal> ();
            List<decimal> listavalores = new List<decimal>() { 790, 820, 680, 1350 };
            decimal valorMaximo = listavalores.Max();
            int numeroValoresEjeY = 4;
            decimal valorSumar = valorMaximo / (numeroValoresEjeY - 1);

            listaY.Add(0);
            for (int i = 1; i < numeroValoresEjeY - 1; i++)
            {
                listaY.Add(valorSumar * i);
            }
            //listaY.Add(valorSumar);
            //listaY.Add(valorSumar*2);
            listaY.Add(valorMaximo);


            int ancho = 800;
            int alto = 400;
            int fuente = 12;
            int marginVBottomTop = 26;
            int inicioX = 50;
            int inicioGraficarTextX = 150;
            int separacionX = 30;
            int inicioY = alto - marginVBottomTop;
            int espacipADibujar = alto - marginVBottomTop - marginVBottomTop;
            int separacion = (espacipADibujar / (listaY.Count - 1));
            int finAnchoRecta = ancho - inicioX;
            
            Bitmap oBitmap = new Bitmap(800, 400);
            Graphics grafico = Graphics.FromImage(oBitmap);
            Rectangle rec;
            StringFormat formato = new StringFormat(StringFormatFlags.DirectionRightToLeft);
            Pen open = new Pen(Brushes.Black, 3);

            using (MemoryStream ms = new MemoryStream())
            {
                rec = new Rectangle(0, 0, oBitmap.Width, oBitmap.Height);
                grafico.FillRectangle(Brushes.White, rec);
                grafico.DrawLine(new Pen(Brushes.Black), new Point(inicioX, inicioY + (fuente / 2)),
                        new Point(inicioX, marginVBottomTop + (fuente / 2)));

                foreach (decimal value in listaY)
                {
                    grafico.DrawString(value.ToString(),new Font("Arial", fuente),Brushes.Black,
                        new Point(inicioX, inicioY), formato);
                    grafico.DrawLine(new Pen(Brushes.Black), new Point(inicioX, inicioY + (fuente / 2)), 
                        new Point(finAnchoRecta, inicioY + (fuente/2)));
                    inicioY -= separacion;
                }

                inicioY = alto - marginVBottomTop;

                int anchoBarra = (ancho - (inicioX+ inicioGraficarTextX) - (listavalores.Count - 1) * separacionX) / (listavalores.Count);

                StringFormat formato1 = new StringFormat(StringFormatFlags.NoClip);

                int indice = 0;
                decimal escala = espacipADibujar / valorMaximo;
                formato1.Alignment = StringAlignment.Center;

                foreach (string value in listaX)
                {
                    decimal valor = listavalores[indice] * escala;
                    grafico.DrawString(value, new Font("Arial", fuente), Brushes.Black,
                        new Point(inicioGraficarTextX, inicioY + (fuente/2)), formato1);
                    rec = new Rectangle(inicioGraficarTextX - (anchoBarra / 2), 
                        inicioY - (int)valor + (fuente / 2),
                        anchoBarra, (int)valor);
                    LinearGradientBrush gradiante = new LinearGradientBrush(rec, ColorTranslator.FromHtml("#AC80E9"), ColorTranslator.FromHtml("#AC80E9"),
                    LinearGradientMode.BackwardDiagonal);
                    grafico.FillRectangle(gradiante, rec);
                    inicioGraficarTextX += anchoBarra;
                    inicioGraficarTextX += separacionX;
                    indice++;

                }

                    oBitmap.Save(ms, ImageFormat.Png);
                byte[] data = ms.ToArray();
                return Convert.ToBase64String(data);

            }
        }
 */