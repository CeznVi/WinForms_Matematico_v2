using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matematico.GameFieldControl
{
    public partial class GameFieldControl : UserControl
    {
        public TableLayoutControlCollection Buttons;

        public GameFieldControl()
        {
            InitializeComponent();
            Buttons = tableLayoutPanel.Controls;
        }


    }
}
