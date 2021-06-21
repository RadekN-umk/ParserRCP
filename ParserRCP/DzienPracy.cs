using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParserRCP
{
    class DzienPracy
    {
        public string KodPracownika { set; get; }
        public DateTime Data { set; get; }
        public TimeSpan GodzinaWejscia { set; get; }
        public TimeSpan GodzinaWyjscia { set; get; }

        public static List<DzienPracy> TworzObiektyPracownikow(string csv_nazwa)
        {
            var PlikCSV = File.ReadAllLines(csv_nazwa);
            List<DzienPracy> DniPracy = new List<DzienPracy>();

            // przypadek dla osobnych wierszy z WE/WY
            if (SprawdzFormat(PlikCSV.First())) 
            {
                foreach (string x in PlikCSV)
                {
                    string[] row = x.Split(';');
                    DzienPracy dzien = DniPracy.FirstOrDefault(t => t.Data == Convert.ToDateTime(row[1]) && t.KodPracownika == row[0]);
                    if (dzien != default) { }
                    else
                    {
                        dzien = new DzienPracy();
                        dzien.KodPracownika = row[0];
                        if(dzien.KodPracownika == "2298")
                        {
                            dzien.KodPracownika = "2298";
                        }
                        dzien.Data = Convert.ToDateTime(row[1]);
                        
                    }
                    TimeSpan godzina; 
                    // w paru miejscach brakuje godziny, TryParse zwraca 00:00:00
                    TimeSpan.TryParse(row[2], out godzina);
                    if (row[3] == "WE") dzien.GodzinaWejscia = godzina;
                    else dzien.GodzinaWyjscia = godzina;
                    DniPracy.Add(dzien);
                }
            }
            // przypadek dla "pełnych" wierszy
            else
            {
                foreach (string x in File.ReadAllLines(csv_nazwa))
                {
                    string[] row = x.Split(';');
                    
                    if (DniPracy.Any(t => t.Data == Convert.ToDateTime(row[1]) && t.KodPracownika == row[0])) { }
                    else
                    {
                        DzienPracy dzien = new DzienPracy();
                        dzien.KodPracownika = row[0];
                        dzien.Data = Convert.ToDateTime(row[1]);
                        dzien.GodzinaWejscia = TimeSpan.Parse(row[2]);
                        dzien.GodzinaWyjscia = TimeSpan.Parse(row[3]);
                        DniPracy.Add(dzien);
                    }
                }
            }

            return DniPracy;
        }

        private static bool SprawdzFormat(string row)
        {
            return row.Contains("WE") || row.Contains("WY");
        }
    }
}
