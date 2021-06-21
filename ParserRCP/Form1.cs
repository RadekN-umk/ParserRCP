using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParserRCP
{
    public partial class Form1 : Form
    {
        List<DzienPracy> DniPracy;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DniPracy = DzienPracy.TworzObiektyPracownikow(openFileDialog1.FileName);
                }
                catch (System.IO.IOException ex)
                {
                    MessageBox.Show("Plik jest aktualnie używany przez inny program.");
                }
                dataGridView1.DataSource = DniPracy;
            }


        }
    }
}
