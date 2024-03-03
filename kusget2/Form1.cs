using DevExpress.XtraTabbedMdi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace kusget2
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void ViewChildForm(Form _form)
        {
            if (!FormAktifMi(_form))
            {
                _form.MdiParent = this;
                _form.Show();
            }

        }
        private bool FormAktifMi(Form form)
        {
            bool aktif = false;
            if (MdiChildren.Count() > 0)
            {
                foreach (var item in MdiChildren)
                {
                    if (form.Name == item.Name)
                    {
                        xtraTabbedMdiManager1.Pages[item].MdiChild.Activate();
                        aktif = true;
                    }

                }
            }
            return aktif;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmKayit kayit = new FrmKayit();

            ViewChildForm(kayit);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FrmKayit kayit = new FrmKayit();

            ViewChildForm(kayit);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmRapor rapor = new FrmRapor();

            ViewChildForm(rapor);
        }
    }
}
