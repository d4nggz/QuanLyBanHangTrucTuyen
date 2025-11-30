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
    public partial class FormQuanLyNguoiDung: Form
    {
        public FormQuanLyNguoiDung()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormCTNguoiDung form = new FormCTNguoiDung();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormCTNguoiDung form = new FormCTNguoiDung();
            form.Show();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
