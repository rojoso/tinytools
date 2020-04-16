
//————————————————
//版权声明：本文为CSDN博主「万里归来少年心」的原创文章，遵循 CC 4.0 BY - SA 版权协议，转载请附上原文出处链接及本声明。
//原文链接：https://blog.csdn.net/liyazhen2011/java/article/details/88825836

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;
using System.Windows.Forms;


namespace DrawBeams
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Programs: IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {

            Controlers controllor = new Controlers(new PickForm(),commandData);
            controllor.pView.Show();
            return Result.Succeeded;

        }

    }
}
