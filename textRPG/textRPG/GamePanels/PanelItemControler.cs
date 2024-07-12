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
    class PanelItemControler:GamePanelParent
    {
        
        public PanelItemControler(Form addForm, string useTable) : base(addForm, "itemPanelTest", Color.Green, useTable)
        {
            itemClassMenu = new menuPanel("menu");
            this.Controls.Add(itemClassMenu);


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
            this.button1.Text = "アイテム生成";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(595, 27);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "ページ生成";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);

            createIS();

            SqlConnectionStringBuilder builder = statics.SqlConect.SqlConection();
            SqlConnection connection = null;
            try
            {
                // データベース接続の準備

                using (connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("接続成功。");


                    // SQLの実行
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        string sqlstr = string.Format("SELECT * from itemArea");
                        command.CommandText = @sqlstr;

                        //command.ExecuteNonQuery();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // SQLの型をC#の型に変換する.参考：https://qiita.com/Kakeishi_Misa/items/ab9851fc54e549478194

                                if (ia1 == null)
                                {
                                    Console.WriteLine("ia生成しました");
                                    createIA();
                                }
                                while (Convert.ToInt32(reader["itemAreaID"]) != ia1.AreaNumber)
                                {
                                    Console.WriteLine("area:"+Convert.ToInt32(reader["itemAreaID"]));
                                    createIA();
                                }
                                if (Convert.ToInt32(reader["itemAreaID"]) == ia1.AreaNumber)
                                {
                                    ia1.areaFrame.areaTitleText.Text = Convert.ToString((reader["areaTitle"]));
                                    itemClassMenu.itemClassMenuButtons[Convert.ToInt32(reader["itemAreaID"])-1].nameLabel.Text = Convert.ToString((reader["areaTitle"]));
                                    ia1.areaFrame.areaExplanationText.Text = Convert.ToString((reader["areaExplanation"]));
                                }

                            }
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

            try
            {
                // データベース接続の準備

                using (connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("接続成功。");


                    
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        string sqlstr = string.Format("SELECT * from item");
                        command.CommandText = @sqlstr;

                        command.ExecuteNonQuery();
                        Console.WriteLine("↑全表示");
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (ia1 == null)
                                {
                                    createIA();
                                }


                                
                                // SQLの型をC#の型に変換する.参考：https://qiita.com/Kakeishi_Misa/items/ab9851fc54e549478194

                                if (ia1 == null)
                                {
                                    createIA();
                                }
                                else
                                {
                                    int i = 0;
                                    currentArea = (ItemArea)is1.itemAreas[0];
                                    while ((Convert.ToInt32(reader["itemAreaID"]) != ((ItemArea)is1.itemAreas[i]).AreaNumber))
                                    {
                                        Console.WriteLine("i = "+i);
                                        i++;
                                    }

                                    
                                    if (Convert.ToInt32(reader["itemAreaID"]) == ((ItemArea)is1.itemAreas[i]).AreaNumber)
                                    {
                                        int j = 0;
                                        currentArea = (ItemArea)is1.itemAreas[i];
                                        if (currentArea.areaList.itemCells.Count == 0)
                                        {
                                            Console.WriteLine("セル一個も無いから作る");
                                            createIC();
                                        }
                                        
                                        ItemCell nowCell = (ItemCell)currentArea.areaList.itemCells[j];

                                        try
                                        {
                                            
                                            // 読み込んだアイテムIDに対応するitemCellを探索
                                            while ((Convert.ToInt32(reader["itemID"])) != nowCell.ItemNumber)
                                            {
                                                Console.WriteLine("セルみたよ" + Convert.ToInt32(reader["itemID"]) + ":" + nowCell.ItemNumber);
                                                if ((Convert.ToInt32(reader["itemID"])) != nowCell.ItemNumber)
                                                {
                                                    Console.WriteLine("セル不一致だよ"+Convert.ToInt32(reader["itemID"]) + ":" + nowCell.ItemNumber);
                                                }

                                                try
                                                {
                                                    
                                                    if (currentArea.areaList.itemCells[j + 1] == null)
                                                    {
                                                        Console.WriteLine("セル無いから作る");
                                                        createIC();
                                                    }
                                                }
                                                catch (IndexOutOfRangeException e)
                                                {
                                                    Console.WriteLine("エラーしたから作る");
                                                    createIC();
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine("エラーしたから作る２");
                                                    createIC();
                                                }
                                                
                                                //createIC();

                                                
                                                j++;
                                                nowCell = (ItemCell)currentArea.areaList.itemCells[j];
                                            }
                                            
                                        }
                                        catch(IndexOutOfRangeException e)
                                        {
                                            Console.WriteLine("pien");
                                        }catch(Exception e)
                                        {
                                            Console.WriteLine("TT");
                                        }


                                        nowCell = (ItemCell)currentArea.areaList.itemCells[j];
                                        Console.WriteLine(Convert.ToInt32(reader["itemID"])+":" + nowCell.ItemNumber);

                                        if (Convert.ToInt32(reader["itemID"]) == nowCell.ItemNumber)
                                        {
                                            Console.WriteLine(Convert.ToInt32(reader["itemID"]) + ":" + nowCell.ItemNumber);
                                            ItemCell ic = (ItemCell)currentArea.areaList.itemCells[j];
                                            ic.nameTextBox.Text = Convert.ToString((reader["itemName"]));
                                            ic.classTextBox.Text= Convert.ToString((reader["itemClass"]));
                                            ic.explanationTextBox.Text = Convert.ToString((reader["itemExplanation"]));
                                            
                                        }
                                        
                                        
                                    }
                                    
                                }
                            }
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

        public ItemSpace ItemSpace
        {
            get => default;
            set
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createIC();
        }
        // ステージエリア生成
        private void button2_Click(object sender, EventArgs e)
        {
            createIA();
        }

        private void createIC()
        {
            createIS();
            if (ia1 == null)
            {
                createIA();

                Console.WriteLine("ia生成");
            }
            ic1 = new ItemCell(currentArea, useTable, useAreaTable);
            //sa1.stageCells.Add(sc1);

            Console.WriteLine("ic生成");
        }

        private void createIA()
        {
            createIS();
            ia1 = new ItemArea(is1, useTable, useAreaTable);
            Console.WriteLine("ia生成");

            menuCell mc1 = new menuCell(itemClassMenu);
            mc1.Click += new System.EventHandler(this.menuButton_Click);
            mc1.nameLabel.Click += new System.EventHandler(this.menuButton_Click);

            currentArea = ia1;
            ClassPanelChanger.MenuButtonPush(is1, itemClassMenu.itemClassMenuButtons[currentArea.AreaNumber-1], itemClassMenu, currentArea, "ItemArea");
        }
        private void createIS()
        {
            if (is1 == null)
            {
                is1 = new ItemSpace(this);
                Console.WriteLine("is生成");
            }
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            Panel currentPanel = new Panel();
            int idx = -1;
            for(int i=0;i< itemClassMenu.itemClassMenuButtons.Count; i++)
            {
                
                if ((object)(itemClassMenu.itemClassMenuButtons[i].nameLabel) == sender)
                {
                    currentPanel = itemClassMenu.itemClassMenuButtons[i];
                    idx = i;
                    break;
                }
                else 
                if ((object)(itemClassMenu.itemClassMenuButtons[i]) == sender)
                {
                    currentPanel = itemClassMenu.itemClassMenuButtons[i];
                    idx = i;
                    /*
                    Console.WriteLine(is1.itemAreas[idx].Name);
                    Console.WriteLine("idx は"+idx);
                    */
                    break;
                }
            }
            ClassPanelChanger.MenuButtonPush(is1, currentPanel, itemClassMenu,is1.itemAreas[idx],"ItemArea");
            currentArea = (ItemArea)is1.itemAreas[idx];
        }
    }
}
