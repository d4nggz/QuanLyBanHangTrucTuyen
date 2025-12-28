using BUS;
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
    public partial class FormBaoCaoTonKho : Form
    {   
        BUS_Product bus = new BUS_Product();
        public FormBaoCaoTonKho()
        {
            InitializeComponent();
        }

        private void FormBaoCaoTonKho_Load(object sender, EventArgs e)
        {
            LoadComboBox();
        }
        void LoadComboBox()
        {
            DataTable dt = bus.GetAllCategories();
            cboCategory.DataSource = dt;
            cboCategory.DisplayMember = "name";    
            cboCategory.ValueMember = "category_id"; 

            if (cboCategory.Items.Count > 0)
            {
                cboCategory.SelectedIndex = 0;
            }
        }

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategory.SelectedValue == null) return;

            int catID;
            if (int.TryParse(cboCategory.SelectedValue.ToString(), out catID))
            {
                DataTable dt = bus.GetReportData(catID);

                rptTonKho rpt = new rptTonKho();
                dt.TableName = "InventoryTable";
                rpt.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rpt;
                crystalReportViewer1.Refresh();
            }
        }
    }
}
