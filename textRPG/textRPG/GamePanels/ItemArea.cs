using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace textRPG
{
    public class ItemAreaList : Panel
    {
        private Point location = new Point(0, 60);
        private Size size = new Size(625, 390);

        public List<Panel> itemCells;
        public int areaNumber;
        public ItemAreaList()
        {
            itemCells = new List<Panel> { };

            this.Location = location;
            this.Size = size;
            this.Tag += "ItemArea";

            this.AutoScroll = true;
            this.BackColor = Color.PaleVioletRed;
        }
        public int SetItemCell(ItemSpace addSpace)
        {
            int count = 0;

            Panel lastCon = new Panel();

            foreach (var con in addSpace.itemAreas)
            {
                lastCon = con;
                count++;

                Console.WriteLine("count" + count);
            }

            areaNumber = count;
            this.Name = areaNumber.ToString();

            return areaNumber;
        }
    }

    public class ItemArea : Panel
    {
        public ItemAreaTitle areaFrame;
        public ItemAreaList areaList;

        public string useTable;
        public string useAreaTable;

        private string itemClass;
        private Point location = new Point(0, 0);
        private Size size = new Size(640, 450);
        private int areaNumber;
        public int AreaNumber
        {
            get { return areaNumber; }
        }

        public ItemArea(ItemSpace addSpace, string table, string areaTable)
        {
            useTable = table;
            useAreaTable = areaTable;

            this.Size = size;
            this.Tag += "ItemArea";

            this.Location = location;
            //this.BackColor = ClassPanelChanger.classColors[areaNumber];




            this.Location = location;

            addSpace.Controls.Add(this);
            addSpace.itemAreas.Add(this);



            areaList = new ItemAreaList();
            areaNumber = areaList.SetItemCell(addSpace);
            this.Controls.Add(areaList);

            areaFrame = new ItemAreaTitle(areaNumber, useTable, useAreaTable);
            this.Controls.Add(areaFrame);

            this.Name = areaNumber.ToString();
        }
    }


    public class ItemAreaTitle : Panel
    {
        public int areaNumber;

        public string useTable;
        public string useAreaTable;

        private Size size = new Size(625, 60);
        private Point location = new Point(0, 0);

        private Label areaTitleLabel;
        public TextBox areaTitleText;
        private Label areaExplanationLabel;
        public TextBox areaExplanationText;
        private Button writeButton;

        public ItemAreaTitle(int number, string table, string areaTable)
        {
            areaNumber = number;
            useTable = table;
            useAreaTable = areaTable;

            this.Size = size;
            this.Location = location;
            this.Tag += "ItemArea";
            this.Name = areaNumber.ToString();

            #region
            areaTitleLabel = new Label()
            {
                Text = "ページ名",
                AutoSize = true,
                Location = new Point(5, 2),
                BackColor = Color.MintCream
            };
            this.Controls.Add(areaTitleLabel);
            areaTitleText = new TextBox()
            {
                Text = areaNumber.ToString() + "ページ目",
                Location = new Point(60, 0),
                Size = new Size(500, 20)
            };
            this.Controls.Add(areaTitleText);

            areaExplanationLabel = new Label()
            {
                Text = "説明",
                AutoSize = true,
                Location = new Point(5, 24),
                BackColor = Color.MintCream
            };
            this.Controls.Add(areaExplanationLabel);
            areaExplanationText = new TextBox()
            {
                Location = new Point(60, 22),
                Size = new Size(500, 35),
                Multiline = true
            };
            this.Controls.Add(areaExplanationText);

            writeButton = new Button()
            {
                Text = "保存",
                Size = new Size(60, 25),
                Location = new Point(565, 2),
                BackColor = Color.White
            };
            writeButton.Click += new System.EventHandler(this.writeButtonClickd);
            this.Controls.Add(writeButton);


            #endregion
        }

        public ItemCell ItemCell
        {
            get => default;
            set
            {
            }
        }

        public void writeButtonClickd(object sender, EventArgs e)
        {
            writeTitle();
            
        }
        public void writeTitle()
        {
            Console.WriteLine("保存");
            SqlConnectionStringBuilder builder = statics.SqlConect.SqlConection();
            SqlConnection connection = null;
            try
            {
                // データベース接続の準備

                bool flg = false;
                string @sqlstr = string.Format("SELECT itemAreaID from {0}", useAreaTable);


                using (connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("接続成功。");


                    // SQLの実行
                    using (SqlCommand command = new SqlCommand())
                    {

                        command.Connection = connection;

                        command.CommandText = sqlstr;


                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {

                                Console.WriteLine("a:" + Convert.ToInt32(reader["itemAreaID"]));
                                if (Convert.ToInt32(reader["itemAreaID"]) == areaNumber)
                                {
                                    flg = true;
                                }

                            }
                            reader.Close();
                        }



                        if (!flg)
                        {
                            //sqlstr = string.Format("INSERT INTO itemArea (itemAreaID, areaTitle, areaExplanation) VALUES (@number, @title, @explanation)", areaNumber, areaTitleText.Text, areaExplanationText.Text);
                            sqlstr = string.Format("INSERT INTO {0} (itemAreaID, areaTitle, areaExplanation) VALUES (@number, @title, @explanation)", useAreaTable);
                            command.CommandText = @sqlstr;

                            command.Parameters.AddWithValue("@number", areaNumber);
                            command.Parameters.AddWithValue("@title", areaTitleText.Text);
                            command.Parameters.AddWithValue("@explanation", areaExplanationText.Text);
                        }
                        else
                        {
                            sqlstr = string.Format("UPDATE {0} SET areaTitle = '{1}' , areaExplanation = '{2}' WHERE itemAreaID = {3}", useAreaTable, areaTitleText.Text, areaExplanationText.Text, areaNumber);
                            command.CommandText = @sqlstr;
                        }


                        Console.WriteLine("SQL:" + sqlstr);
                        //command.CommandText = @sqlstr;
                        Console.WriteLine(1);


                        command.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("エラー");
                throw;
            }
            finally
            {
                // データベースの接続終了
                Console.WriteLine("接続終了");
                connection.Close();
            }
        }
    }
}
