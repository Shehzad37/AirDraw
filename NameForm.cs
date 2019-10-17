using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dibujando_Figuras
{
    public partial class NameForm : Form
    {
        string departmentName = "IT";
         Main main= new Main();
          
          
        
        public NameForm()
        {
            InitializeComponent();
            SetDefault(btnSave);
        }


        private void NameForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
       {
            main.FileName = txtFileName.Text;
            main.Show();
            this.Hide();
            //this.Close();
        }
        private void SetDefault(Button myDefaultBtn)
        {
            this.AcceptButton = myDefaultBtn;
        }
    
       

    }
}
