using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoBrew
{
    public partial class Welcome: Form
    {
        public Welcome()
        {
            InitializeComponent();
        }

        private void drinkCategoriesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.drinkCategoriesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dataSet);

        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dataSet.DrinkCategories' table. You can move, or remove it, as needed.
            this.drinkCategoriesTableAdapter.Fill(this.dataSet.DrinkCategories);

        }
    }
}
