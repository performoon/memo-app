using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace textRPG
{
    public partial class Form1 : Form
    {
        Panel[] panels;
        PanelCharacterControler characterCon;
        PanelItemControler itemCon;

        SqlConnectionStringBuilder builder;
        public Form1()
        {
            InitializeComponent();

            SqlConnection connection = null;

            builder = statics.SqlConect.SqlConection();


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
                        command.CommandText = @"Select * from item";

                        //command.ExecuteNonQuery();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine(reader["itemID"] + ":"
                                                + reader["itemName"] + ":"
                                                + reader["itemClass"]);
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
                connection.Close();
            }

            /*
            string constr = @"Data Source=localhost\SQLEXPRESS;
                            Initial Catalog=idea;
                            Connect Timeout=60;
                            User ID = admin;
                            Password = qwe123;";
            

            var dt = new DataTable();
            using (var con = new SqlConnection(constr))
            {
                var sqlstr = "Select * from item";
                try
                {
                    var com = new SqlCommand(sqlstr, con);
                    var sda = new SqlDataAdapter(com);
                    sda.Fill(dt);
                }
                catch (Exception err)
                {
                    Console.WriteLine("エラー");
                }

            }
            */


            characterCon = new PanelCharacterControler(this, "character");
            itemCon = new PanelItemControler(this, "item");
            panels = new GamePanelParent[]
            {
                characterCon,
                itemCon,
            };
            for (int i= 0; i < panels.Length; i++)
            {
                panels[i].Visible = false;
            }

            menuButtonClick(panel_menu_item, itemCon);
        }

        internal PanelCharacterControler PanelCharacterControler
        {
            get => default;
            set
            {
            }
        }

        internal PanelItemControler PanelItemControler
        {
            get => default;
            set
            {
            }
        }

        private void button_menu_character_Click(object sender, EventArgs e)
        {
            menuButtonClick(panel_menu_character, characterCon);
        }
        private void button_menu_item_Click(object sender, EventArgs e)
        {
            menuButtonClick(panel_menu_item, itemCon);
        }

        void menuButtonClick(Panel button, Panel find)
        {
            ClassPanelChanger.MenuButtonPush(this, button, panel_menu, find, "gamePanel");
        }



        

    }

    
}