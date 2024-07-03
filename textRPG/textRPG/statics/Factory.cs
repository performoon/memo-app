using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace textRPG
{
    static class Factory
    {
        /// <summary>
        /// Controlを渡すと、その子オブジェクトのListを返す
        /// このプロジェクトの使用用途→名前、部署名、会社名それぞれのグループボックスを渡すとそのグループのテキストボックスをList化する
        /// 参考元：https://www.wareko.jp/blog/recursively-get-all-controls-on-form-in-csharp#toc1
        /// </summary>
        /// <param name="parent">子オブジェクトをList化したい親オブジェクト</param>
        /// <returns></returns>
        /// 
        public static IEnumerable<Control> findControl(Control parent)
        {
            List<Control> controls = new List<Control>();

            foreach (Control child in parent.Controls)
            {
                controls.AddRange(findControl(child));
            }

            controls.Add(parent);

            return controls;
        }
    }
}
