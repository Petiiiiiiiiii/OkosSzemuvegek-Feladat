using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA231201
{
    internal class OkosSzemuveg
    {
        public int Sorszam { get; set; }
        public double KijelzoMeret { get; set; }
        public double ProcesszorTeljesitmeny { get; set; }
        public int KameraFelbontas { get; set; }
        public List<string> Szenzorok { get; set; }
        public string SzenzorokString { get; set; }
        public string TarhelyString { get; set; }
        public int Tarhely { get; set; }
        public int Uzemido { get; set; }

        public OkosSzemuveg(string sor)
        {
            var atmeneti = sor.Split(';');
            this.Sorszam = Convert.ToInt32(atmeneti[0].Remove(atmeneti[0].Length - 1));
            this.KijelzoMeret = Convert.ToDouble(atmeneti[1]);
            this.ProcesszorTeljesitmeny = Convert.ToDouble(atmeneti[2]);
            this.KameraFelbontas = Convert.ToInt32(atmeneti[3]);
            this.SzenzorokString = atmeneti[4];
            var szenzorok = atmeneti[4].Split(',');
            this.Szenzorok = new();
            for (int i = 0; i < szenzorok.Length; i++) this.Szenzorok.Add(szenzorok[i]);
            this.TarhelyString = atmeneti[5];
            var tarhely = atmeneti[5].Split(' ');
            for (int i = 0; i < tarhely.Length; i++) 
            {
                if (tarhely[1] == "TB")
                {
                    this.Tarhely = Convert.ToInt32(tarhely[0]) * 1024;
                }
                else 
                {
                    this.Tarhely = Convert.ToInt32(tarhely[0]);
                }
            } 
            this.Uzemido = Convert.ToInt32(atmeneti[6]);

        }
        public override string ToString()
        {
            return $"Sorszám: {this.Sorszam} \nKijelző mérete: {this.KijelzoMeret} col \nProcesszor teljesítménye: {this.ProcesszorTeljesitmeny} Ghz \nKamera felbontása: {this.KameraFelbontas} MP \nSzenzorok: {this.SzenzorokString} \nTárhely mérete: {this.TarhelyString} \nÜzemidő: {this.Uzemido} óra\n";
        }
        public double InchToCm() 
        {
            return this.KijelzoMeret * 2.54;
        }
    }
}
