using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace textRPG
{// アイテム一つ一つ（ステージ１単位）
    public class ItemCell : Panel
    {
        public string useTable;
        public string useAreaTable;

        public Label nameLabel;
        public TextBox nameTextBox;
        public Label classLabel;
        public TextBox classTextBox;
        public Label explanationLabel;
        public TextBox explanationTextBox;

        public Button writeButton;
        public Button crearButton;
        public Button readButton;


        private string itemName;
        private int itemNumber;
        public int ItemNumber
        {
            get { return itemNumber; }
        }
        private int itemAreaNumber;

        private Point location = new Point(0, 0);
        private Size size = new Size(300, 200);

        public ItemCell(ItemArea addArea, string table, string areaTable)
        {
            useTable = table;
            useAreaTable = areaTable;

            itemAreaNumber = addArea.AreaNumber;
            this.Size = size;

            int count = 0;                  // 追加するItemAreaにあるItemCellの数
            Panel lastCon = new Panel();    // 最後のItemCellインスタンス

            // 最後のItemCellインスタンスを取得
            foreach (var con in addArea.areaList.itemCells)
            {
                lastCon = con;
                count++;
            }
            if (count == 0)
            {
                location.X = 0;
                this.Location = location;
            }
            else if (count % 2 == 0)
            {
                location.X = 0;
                location.Y = lastCon.Location.Y + lastCon.Size.Height + 5;
                this.Location = location;
            }
            else
            {
                location.X = lastCon.Location.X + lastCon.Size.Width + 5;
                location.Y = lastCon.Location.Y;
                Console.WriteLine(location);
                this.Location = location;
            }

            this.BackColor = Color.DarkGray;
            this.itemNumber = count + 1;
            this.itemName = "stage" + addArea.AreaNumber + "-" + this.itemNumber;


            #region アイテムの名前と分類と説明の欄にかかわるコントロール軍の初期化
            nameLabel = new Label()
            {
                Text = "アイテム名",
                AutoSize = true,
                Location = new Point(5, 0),
                BackColor = Color.MintCream
            };
            nameTextBox = new TextBox()
            {
                Text = itemAreaNumber + ":" + itemNumber,
                Location = new Point(0, nameLabel.Location.Y + 12),
                Size = new Size(300, 20),
            };
            this.Controls.Add(nameLabel);
            this.Controls.Add(nameTextBox);

            classLabel = new Label()
            {
                Text = "分類",
                AutoSize = true,
                Location = new Point(5, 32),
                BackColor = Color.MintCream
            };
            classTextBox = new TextBox()
            {
                Location = new Point(0, classLabel.Location.Y + 12),
                Size = new Size(300, 20),
            };
            this.Controls.Add(classLabel);
            this.Controls.Add(classTextBox);

            explanationLabel = new Label()
            {
                Text = "説明",
                AutoSize = true,
                Location = new Point(5, 64),
                BackColor = Color.MintCream,

            };
            explanationTextBox = new TextBox()
            {
                Location = new Point(0, explanationLabel.Location.Y + 12),
                Multiline = true,
                Size = new Size(300, 100),
            };
            this.Controls.Add(explanationLabel);
            this.Controls.Add(explanationTextBox);

            writeButton = new Button()
            {
                Text = "保存",
                Size = new Size(60, 25),
                Location = new Point(240, 175),
                BackColor = Color.White
            };
            writeButton.Click += new System.EventHandler(this.writeButtonClickd);
            this.Controls.Add(writeButton);

            readButton = new Button()
            {
                Text = "読み込み",
                Size = new Size(60, 25),
                Location = new Point(180, 175),
                BackColor = Color.White
            };
            readButton.Click += new System.EventHandler(this.readButtonClickd);
            this.Controls.Add(readButton);

            crearButton = new Button()
            {
                Text = "クリア",
                Size = new Size(60, 25),
                Location = new Point(120, 175),
                BackColor = Color.White
            };
            crearButton.Click += new System.EventHandler(this.crearButtonClickd);
            this.Controls.Add(crearButton);
            #endregion




            addArea.areaList.Controls.Add(this);
            addArea.areaList.itemCells.Add(this);
        }
        public void writeButtonClickd(object sender, EventArgs e)
        {
            Console.WriteLine("保存");
            SqlConnectionStringBuilder builder = statics.SqlConect.SqlConection();
            SqlConnection connection = null;
            try
            {
                // データベース接続の準備

                bool flg = false;
                // string @sqlstr = string.Format("SELECT COUNT(itemID) FROM item WHERE itemAreaID = {0} AND itemID = {1}", itemAreaNumber, itemNumber);
                string @sqlstr = string.Format("SELECT * FROM {0}", useTable);


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

                                Console.WriteLine("a:" + Convert.ToInt32(reader["itemAreaID"]) + Convert.ToInt32(reader["itemID"]));
                                if (Convert.ToInt32(reader["itemAreaID"]) == itemAreaNumber && Convert.ToInt32(reader["itemID"]) == itemNumber)
                                {
                                    Console.WriteLine("trueeeeee!!");
                                    flg = true;
                                }

                            }
                        }


                        if (!flg)
                        {
                            Console.WriteLine("一致しているデータがないよ。インサートするよ");
                            //sqlstr = string.Format("INSERT INTO itemArea (itemAreaID, areaTitle, areaExplanation) VALUES (@number, @title, @explanation)", areaNumber, areaTitleText.Text, areaExplanationText.Text);
                            sqlstr = string.Format("INSERT INTO {0} (itemAreaID, itemID, itemName, itemClass, itemExplanation) VALUES (@areaNumber, @number, @name, @class, @explanation)", useTable);
                            command.CommandText = @sqlstr;

                            command.Parameters.AddWithValue("@areaNumber", itemAreaNumber);
                            command.Parameters.AddWithValue("@number", itemNumber);
                            command.Parameters.AddWithValue("@name", nameTextBox.Text);
                            command.Parameters.AddWithValue("@class", classTextBox.Text);
                            command.Parameters.AddWithValue("@explanation", explanationTextBox.Text);
                        }
                        else
                        {
                            Console.WriteLine("一致してるデータがあったよ。アップデートするよ");
                            sqlstr = string.Format("UPDATE {0} SET itemName = '{1}', itemClass = '{2}', itemExplanation = '{3}' WHERE itemAreaID = {4} AND itemID = {5}", useTable, nameTextBox.Text, classTextBox.Text, explanationTextBox.Text, itemAreaNumber, itemNumber);
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


        public void readButtonClickd(object sender, EventArgs e)
        {
            Console.WriteLine("読み込み");
            SqlConnectionStringBuilder builder = statics.SqlConect.SqlConection();
            SqlConnection connection = null;
            try
            {
                // データベース接続の準備

                bool flg = false;
                // string @sqlstr = string.Format("SELECT COUNT(itemID) FROM item WHERE itemAreaID = {0} AND itemID = {1}", itemAreaNumber, itemNumber);
                string @sqlstr = string.Format("SELECT * FROM {0}", useTable);


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

                                Console.WriteLine("a:" + Convert.ToInt32(reader["itemAreaID"]) + Convert.ToInt32(reader["itemID"]));
                                if (Convert.ToInt32(reader["itemAreaID"]) == itemAreaNumber && Convert.ToInt32(reader["itemID"]) == itemNumber)
                                {
                                    nameTextBox.Text = Convert.ToString(reader["itemName"]);
                                    classTextBox.Text = Convert.ToString(reader["itemClass"]);
                                    explanationTextBox.Text = Convert.ToString(reader["itemExplanation"]);
                                    flg = true;
                                }

                            }
                        }


                        if (!flg)
                        {
                            Console.WriteLine("一致しているデータがないよ。");
                        }
                        else
                        {
                            Console.WriteLine("一致してるデータがあったよ。読み込んだよ！");
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
        public void crearButtonClickd(object sender, EventArgs e)
        {
            Console.WriteLine("クリア");
            SqlConnectionStringBuilder builder = statics.SqlConect.SqlConection();
            SqlConnection connection = null;
            try
            {
                // データベース接続の準備

                bool flg = false;
                // string @sqlstr = string.Format("SELECT COUNT(itemID) FROM item WHERE itemAreaID = {0} AND itemID = {1}", itemAreaNumber, itemNumber);
                string @sqlstr = string.Format("SELECT * FROM {0}", useTable);


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

                                Console.WriteLine("a:" + Convert.ToInt32(reader["itemAreaID"]) + Convert.ToInt32(reader["itemID"]));
                                if (Convert.ToInt32(reader["itemAreaID"]) == itemAreaNumber && Convert.ToInt32(reader["itemID"]) == itemNumber)
                                {
                                    flg = true;
                                }

                            }
                        }


                        if (!flg)
                        {
                            Console.WriteLine("一致しているデータがないよ。");
                        }
                        else
                        {
                            Console.WriteLine("一致してるデータがあったよ。消したよ！");
                            sqlstr = string.Format("DELETE FROM {0} WHERE itemAreaID = {1} AND itemID = {2}", useTable, itemAreaNumber, itemNumber);
                            Console.WriteLine("SQL:" + sqlstr);
                            command.CommandText = @sqlstr;
                            command.ExecuteNonQuery();
                            nameTextBox.Text = itemAreaNumber + ":" + itemNumber;
                            classTextBox.Text = "";
                            explanationTextBox.Text = "";
                        }
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
