using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kusget2
{
    public partial class FrmRapor : Form
    {
        public FrmRapor()
        {
            InitializeComponent();
        }
        dbSecimEntities db=new dbSecimEntities();
        private void FrmRapor_Load(object sender, EventArgs e)
        {
            var liste=(from x in db.tblOylar select x).ToList();
            gridControl1.DataSource = liste;
        }
    }
}
