using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace textRPG
{
    
    public static class ClassPanelChanger
    {
        public static Color[] classColors = new Color[] {
        Color.Red,
        Color.Green,
        Color.Blue,
        Color.White
        };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="control">切り替えるdisplayPanelが存在するControl</param>
        /// <param name="thisButtonPanel">押したボタンのPanel</param>
        /// <param name="menu">押したボタンがあるmenuPanel</param>
        /// <param name="displayPanel">表示を切り替えたいdisplayPanel</param>
        /// <param name="tag">切り替えたいdisplayPanelが属するtagグループ名</param>
        public static void MenuButtonPush(Control control, Panel thisButtonPanel,Panel menu , Panel displayPanel, string tag)
        {
            ClassPanelChanger.button_color_reset(menu);
            thisButtonPanel.BackColor = Color.White;
            ClassPanelChanger.panelChanger(control, displayPanel.Name, tag);
        }


        /// <summary>
        /// 渡されたコントロールの中の、タググループから、指定された名前のコントロールを見つけ出し、そのコントロールを表示して他の同タググループのコントロールは非表示にする
        /// 簡単に言うと、パネルを切り替えるためのメソッド
        /// </summary>
        /// <param name="findControl">切り替えたいパネルがあるコントロール</param>
        /// <param name="panel_name">表示したいパネルの名前</param>
        /// <param name="change_tag">切り替えるパネルのタグ</param>
        public static void panelChanger(Control findControl, string panel_name, string change_tag)
        {
            var panels = Factory.findControl(findControl);
            foreach (Control control in panels)
            {
                if (control.Tag != null)
                {
                    if (control.Tag.ToString().Equals(change_tag))
                    {
                        Console.Write(control.Name.ToString() + " " + panel_name);
                        if (control.Name.ToString().Equals(panel_name))
                        {
                            Console.WriteLine("付く" + control.Name);
                            control.Visible = true;
                        }
                        else
                        {
                            Console.WriteLine("消える" + control.Name);
                            control.Visible = false;
                        }

                    }
                }
            }
        }
        /// <summary>
        /// findControlを呼び出し、List化したPanelのバックグラウンドカラーをボタンのリストのバックグラウンドカラーと同じにリセットする
        /// </summary>
        /// <param name="panel_menu_name">リセットしないボタンのパネル</param>
        /// <param name="findPanel">findControlに渡す、つまりList化したい列のPanel</param>
        public static void button_color_reset(Panel findPanel)
        {
            var panels = Factory.findControl(findPanel);


            foreach (Control control in panels)
            {
                //if (control.GetType().Equals(typeof(Panel)))
                if (control.Tag != null)
                {
                    if (control.Tag.ToString().Equals("Frame"))
                    {
                        control.BackColor = findPanel.BackColor;
                        //control.BackColor = Color.Orange;
                    }
                }
            }
        }
    }
}
