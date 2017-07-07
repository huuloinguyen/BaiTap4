using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTSDemo1
{
    public class Word
    {
        public Word(string tenfile, string amtiet, string vitribatdau, string vitriketthuc, string ngucanhtrai, string ngucanhphai) {

            this.tenfile = tenfile;
            this.amtiet = amtiet;
            this.vitribatdau = vitribatdau;
            this.vitriketthuc = vitriketthuc;
            this.ngucanhtrai = ngucanhtrai;
            this.ngucanhphai = ngucanhphai;
        }

        public string tenfile = string.Empty;
        public string amtiet = string.Empty;
        public string vitribatdau = string.Empty;
        public string vitriketthuc = string.Empty;
        public string ngucanhtrai = string.Empty;
        public string ngucanhphai = string.Empty;
    }
}
