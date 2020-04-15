using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tinyTools
{
    public partial class PersonForm : Form
    {
        private Controllor _controllor;
        public Controllor Controllor
        {
            get
            {
                return _controllor;
            }
            set
            {
                this._controllor = value;
                this.textBox1.DataBindings.Add("Text", Controllor.pModel, "ID");
            }
        }

        public PersonForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            Controllor.UpdateData();
        }
    }
}
