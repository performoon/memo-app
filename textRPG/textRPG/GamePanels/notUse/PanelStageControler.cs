using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace textRPG
{
    class PanelStageControler : GamePanelParent
    {
        private Button button1;
        private Button button2;

        StageSpace ss1;
        StageArea sa1;
        public PanelStageControler(Form addForm):base(addForm, "stagePanelTest", Color.Brown, null)
        {
            //this.Name = "stagePanelTest";
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();

            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);

            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(595, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(595, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (ss1 == null)
            {
                ss1 = new StageSpace(this);
                Console.WriteLine("ss生成");
            }
            if (sa1 == null)
            {
                sa1 = new StageArea(ss1);
                //ss1.stageAreas.Add(sa1);
                Console.WriteLine("sa生成");
            }
            StageCell sc1 = new StageCell(sa1);
            //sa1.stageCells.Add(sc1);

            Console.WriteLine("sc生成");
        }

        // ステージエリア生成
        private void button2_Click(object sender, EventArgs e)
        {
            if (ss1 == null)
            {
                ss1 = new StageSpace(this);
                Console.WriteLine("ss生成");
            }
            sa1 = new StageArea(ss1);
        }
    }

    // ステージを格納するエリアを格納するスペース（すべてのエリアを格納する）
    public class StageSpace : Panel
    {
        public List<Panel> stageAreas;

        public StageSpace(Panel addPanel)
        {
            stageAreas = new List<Panel> { };
            addPanel.Controls.Add(this);
            this.Size = new Size(600, 500);
            this.Location = new Point(0, 0);
            this.AutoScroll = true;
            this.BackColor = Color.Aqua;
        }
    }

    // ステージを格納するエリア（エリア１単位）
    public class StageArea : Panel
    {
        public List<Panel> stageCells;

        private Point location = new Point(0, 0);
        private Size size = new Size(550, 450);
        private int areaNumber;
        public int AreaNumber
        {
            get { return areaNumber; }
        }

        public StageArea(StageSpace addSpace)
        {
            stageCells = new List<Panel> { };
            this.Size = size;
            int count = 0;

            Panel lastCon = new Panel();

            foreach (var con in addSpace.stageAreas)
            {
                lastCon = con;
                count++;
            }
            Console.WriteLine(count);
            if (count == 0)
            {
                location.X = 0;
                this.Location = location;
            }
            else
            {
                location.Y = lastCon.Location.Y + lastCon.Size.Height + 1;
                Console.WriteLine(lastCon.Location);
                Console.WriteLine(lastCon.Size);


                Console.WriteLine(location);
                this.Location = location;
            }
            areaNumber = count + 1;

            this.Location = location;
            this.AutoScroll = true;
            this.BackColor = Color.Red;

            addSpace.Controls.Add(this);
            addSpace.stageAreas.Add(this);
        }
    }

    // ステージ一つ一つ（ステージ１単位）
    public class StageCell : Panel
    {
        private Label stageNameLabel;
        private Button stageSelectButton;
        private string stageName;
        private int stageNumber;

        private Point location = new Point(0, 0);
        private Size size = new Size(99, 200);

        public StageCell(StageArea addArea)
        {

            this.Size = size;

            int count = 0;
            /*
            var panels = Factory.findControl(addArea);
            
            foreach(Control control in panels)
            {
                if (control.Location.X > max_x)
                {
                    max_x = control.Location.X;
                }
            }
            */
            Panel lastCon = new Panel();
            foreach (var con in addArea.stageCells)
            {
                lastCon = con;
                count++;
            }
            if (count == 0)
            {
                location.X = 0;
                this.Location = location;
            }
            else if (count % 5 == 0)
            {
                location.X = 0;
                location.Y = lastCon.Location.Y + lastCon.Size.Height + 1;
                this.Location = location;
            }
            else
            {
                location.X = lastCon.Location.X + lastCon.Size.Width + 1;
                location.Y = lastCon.Location.Y;
                Console.WriteLine(location);
                this.Location = location;
            }
            /*
            if (count == 0)
            {
                location.X = 0;
                this.Location = location;
            }
            else
            {
                location.X = lastCon.Location.X + lastCon.Size.Width + 1;

                Console.WriteLine(location);
                this.Location = location;
            }
            */

            this.BackColor = Color.Orange;
            this.stageNumber = count + 1;
            this.stageName = "stage" + addArea.AreaNumber + "-" + this.stageNumber;

            stageNameLabel = new Label();
            stageNameLabel.Text = stageName;
            stageNameLabel.Location = new Point(18, 9);

            stageSelectButton = new Button();
            stageSelectButton.Location = new Point(18, 156);
            stageSelectButton.Size = new Size(63, 30);
            stageSelectButton.Text = "出撃";
            stageSelectButton.Click += new System.EventHandler(StageButtonClickd);

            this.Controls.Add(stageNameLabel);
            this.Controls.Add(stageSelectButton);

            addArea.Controls.Add(this);
            addArea.stageCells.Add(this);
        }
        public void StageButtonClickd(object sender, EventArgs e)
        {
            // 現在のステージをこのステージに変更する処理を描く
            Console.WriteLine(stageName + "出撃");
        }
    }
}
