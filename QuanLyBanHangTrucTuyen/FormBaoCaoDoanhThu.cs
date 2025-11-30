using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyBanHangTrucTuyen
{
    public partial class FormBaoCaoDoanhThu: Form
    {
        public FormBaoCaoDoanhThu()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormBaoCaoDoanhThu_Load(object sender, EventArgs e)
        {

        }
    }
}
