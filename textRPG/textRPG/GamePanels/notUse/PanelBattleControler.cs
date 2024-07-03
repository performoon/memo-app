using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace textRPG
{
    class PanelBattleControler : GamePanelParent
    {
        BattleScreen bs;

        public PanelBattleControler(Form addForm):base(addForm, "battlePanelTest", Color.AliceBlue, null)
        {
            bs = new BattleScreen(this);
        }
    }
    
    class BattleScreen : Panel
    {
        public BattleScreen(Panel addPanel)
        {
            this.Location = new Point(0, 50);
            this.Size = new Size(670, 200);
            this.BackColor = Color.Red;

            addPanel.Controls.Add(this);
        }
    }
}
