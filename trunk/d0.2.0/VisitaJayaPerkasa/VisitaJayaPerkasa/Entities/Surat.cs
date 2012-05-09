using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisitaJayaPerkasa.Entities
{
    public class Surat
    {
        public String NoSurat { get; set; }
        public DateTime Tgl { get; set; }
        public Guid? CustomerID { get; set; }
        public Guid? SupplierID { get; set; }


        //Entity view
        public String CustomerName { get; set; }
        public String supplierName { get; set; }

        public String GenerateNoSurat(int lastValue, EnumSurat type)
        {
            lastValue += 1;
            String nolString = "";

            for (int i = lastValue.ToString().Trim().Length; i < 4; i++)
                nolString += "0";

            switch (type) { 
                case EnumSurat.RO:
                    nolString += lastValue + "VJP/RO/" + DateTime.Today.Year;
                    break;
                case EnumSurat.LeadTime:
                    nolString += lastValue + "VJP/LT/" + DateTime.Today.Year;
                    break;
                case EnumSurat.PenawaranHarga:
                    nolString += lastValue + "VJP/PH/" + DateTime.Today.Year;
                    break;
                case EnumSurat.ShippingInstruction:
                    nolString += lastValue + "VJP/SI/" + DateTime.Today.Year;
                    break;
            }

            return nolString;
        }
    }
}
