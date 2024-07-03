using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace textRPG
{

    // ステージを格納するエリアを格納するスペース（すべてのエリアを格納する）
    public class ItemSpace : Panel
    {
        public List<Panel> itemAreas;

        string title;


        public ItemSpace(Panel addPanel)
        {
            itemAreas = new List<Panel> { };
            addPanel.Controls.Add(this);
            this.Size = new Size(650, 500);
            this.Location = new Point(0, 80);
            this.AutoScroll = true;
            this.BackColor = Color.Aqua;
        }

        public ItemAreaTitle ItemAreaTitle
        {
            get => default;
            set
            {
            }
        }
    }



}
