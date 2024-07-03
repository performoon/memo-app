using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace textRPG
{
    class menuPanel : Panel
    {
        public List<menuCell> itemClassMenuButtons;
        private Point location = new Point(0, 0);
        private Size panelSize = new Size(500, 60);

        public menuPanel(string name)
        {
            this.Name = name;
            this.SetBounds(5, 5, panelSize.Width, panelSize.Height);
            this.BackColor = Color.SkyBlue;
            this.Visible = true;
            this.AutoScroll = true;

            this.itemClassMenuButtons = new List<menuCell> { };

            /*
                this.itemClassMenuButtons[i] = new Panel();
                this.itemClassMenuButtons[i].Name = i.ToString();
                this.itemClassMenuButtons[i].SetBounds(location.X+(buttonSize.Width+1)*i,location.Y, buttonSize.Width, buttonSize.Height);
                this.itemClassMenuButtons[i].BackColor = Color.Coral;
                this.itemClassMenuButtons[i].Visible = true;
                this.itemClassMenuButtons[i].Tag = "Frame";
           

                this.Controls.Add(itemClassMenuButtons[i]);
                Console.WriteLine(i);
            */
        }

        internal menuCell menuCell
        {
            get => default;
            set
            {
            }
        }
    }
    class menuCell : Panel
    {
        private string menuName;
        private int menuNumber;
        public Label nameLabel;

        private Point buttonLocation = new Point(0, 0);
        private Size buttonSize = new Size(70, 40);

        public menuCell(menuPanel addMenu)
        {
            this.Size = buttonSize;

            int count = 0;                  // 追加するItemAreaにあるItemCellの数
            Panel lastCon = new Panel();    // 最後のItemCellインスタンス

            // 最後のItemCellインスタンスを取得
            foreach (var con in addMenu.itemClassMenuButtons)
            {
                lastCon = con;
                count++;
            }
            if (count == 0)
            {
                buttonLocation.X = 0;
                this.Location = buttonLocation;
            }
            else
            {
                buttonLocation.X = lastCon.Location.X + lastCon.Size.Width + 5;
            }

            Console.WriteLine(count);

            this.Location = buttonLocation;



            this.Visible = true;
            this.Tag = "Frame";
            this.BackColor = Color.Coral;
            this.menuNumber = count + 1;


            nameLabel = new Label();
            nameLabel.Text = this.menuNumber.ToString();
            nameLabel.AutoSize = false;
            nameLabel.Size = buttonSize;

            nameLabel.Visible = true;

            //nameLabel.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));

            this.Controls.Add(nameLabel);


            addMenu.Controls.Add(this);
            addMenu.itemClassMenuButtons.Add(this);
        }
    }
}
