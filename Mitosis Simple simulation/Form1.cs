using System;
using System.Drawing;
using System.Windows.Forms;

namespace Mitosis_Simple_simulation
{
    public partial class Form1 : Form
    {
        Enviroment _enviroment;
        public Form1()
        { 
            InitializeComponent();
            _enviroment = new Enviroment(this.Bounds);
        }
        
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            _enviroment.Draw(g);
            
        }

        private void moving_Tick(object sender, EventArgs e)
        {
            _enviroment.Cycles();
        }

        private void animation_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
           _enviroment.SplitCell(this.PointToClient(Cursor.Position));
          
        }
               
    }
}
