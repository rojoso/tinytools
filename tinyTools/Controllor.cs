using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//其中也有选择过滤器的相关定义，还需要在选择过滤器中进行研究
namespace tinyTools
{
    public class Controllor
    {
        public PersonForm pView;
        public Person pModel;

        public Controllor(PersonForm pView) 
        {
            pModel = new Person() { ID = "rrr" };
            this.pView = pView;
            this.pView.Controllor = this;
        }

        public void UpdatetoDatabase(Person person)
        {
            System.Windows.Forms.MessageBox.Show(person.ID, "nihao");
        }

        public void UpdateData()
        {
            UpdatetoDatabase(pModel);
        }

    }

    

}
