using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.Revit.UI;
using Autodesk.Revit.DB;


namespace _3DbeamsBycurve
{
    public partial class ChsForm : System.Windows.Forms.Form
    {
        ExecuteEvent executeEvent = null;
        ExternalEvent eventHandler = null;

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

            }
        }

        public ChsForm()
        {
            InitializeComponent();
        }

        private void ChsForm_Load(object sender, EventArgs e)
        {
            executeEvent = new ExecuteEvent(this);
            eventHandler = ExternalEvent.Create(executeEvent);

            //绑定combo中的数据源
            cbox_BeamSymbols.DataSource = Controllor.pModel.BeamSymbolsName;
            cbox_Levels.DataSource = Controllor.pModel.levelsName;
        }

        private void bt_Commit_Click(object sender, EventArgs e)
        {
            eventHandler.Raise();
        }
    }
}
