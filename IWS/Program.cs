using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardDispenserTest
{
    static class Program
    {
       //try detached head
	   //try 2 分离头
	   //try3 fenlitou
        [STAThread]
        static void Main()
        {
			//添加一个注释
			//我是来制造冲突的
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
			//fetch and merge  test
			//fetch and merge  test 2222
			//fetch and merge  test 333
            Application.Run(new Form1());
        }
		//我要增加一个函数
		int function(int x,int y)
		{
			return y+x;
		}
		//临时增加要给函数，测试完之后要删除
		int tempFunc( string x)
		{
			return "hahhah ";
		}
    }
}
