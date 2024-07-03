using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace textRPG
{
    abstract class GamePanelParent : Panel
    {
        public string useTable;
        public string useAreaTable;

        public Button button1;
        public Button button2;

        public menuPanel itemClassMenu;

        public ItemSpace is1;
        public ItemArea ia1;
        public ItemCell ic1;

        public ItemArea currentArea;
        public GamePanelParent(Form addForm, string panelName, Color setColor, string table)
        {
            this.Name = panelName;
            this.Size = new Size(800, 600);
            this.Location = new Point(105, 12);
            this.Tag += "gamePanel";
            //this.BackColor = setColor;

            useTable = table;
            useAreaTable = useTable + "Area";
            addForm.Controls.Add(this);
        }
    }
}
