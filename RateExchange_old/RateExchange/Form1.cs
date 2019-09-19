using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RateExchange
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string link = "http://www.cbr.ru/scripts/XML_daily.asp?date_req=21.08.2019";
            CbrParser.ReaderCbr(link);
            label1.Text = "Готово";

        }
    }
}
