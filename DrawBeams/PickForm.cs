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

namespace DrawBeams
{
    public partial class PickForm : System.Windows.Forms.Form
    {
        ExecuteEvent executeEvent = null;
        ExternalEvent eventHandler = null;

        private Controlers _controllor;
        public Controlers Controllor
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

        public PickForm()
        {
            InitializeComponent();
        }

        private void bt_pickcurve_Click(object sender, EventArgs e)
        {
            try
            {
                Controllor.PickModelCurve();
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException appe)
            {

            }
            catch(Autodesk.Revit.Exceptions.InvalidOperationException)
            {

            }
            
        }

        private void bt_floor_Click(object sender, EventArgs e)
        {
            try
            {
                Controllor.PickModelFloor();
            }
            catch(Autodesk.Revit.Exceptions.OperationCanceledException appe)
            {
                
            }
            catch (Autodesk.Revit.Exceptions.InvalidOperationException)
            {

            }
        }

        private void PickForm_Load(object sender, EventArgs e)
        {

            executeEvent = new ExecuteEvent(this);
            eventHandler = ExternalEvent.Create(executeEvent);

            //绑定combo中的数据源
            cbox_Beamsymbols.DataSource = Controllor.pModel.BeamSymbolsName;
        }

        private void bt_Commit_Click(object sender, EventArgs e)
        {
           
            eventHandler.Raise();
            TaskDialog.Show("ss", eventHandler.IsPending.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Controllor.PickModelFace();
        }
    }
}
