using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Runtime;
using System.IO;
using System.Net;
using System.Net.Sockets;
namespace index
{
    public partial class MainFrom : Form
    {
        string table;  //当前桌子号 
        PictureBox[] picturebox;
        public static bool timetf = false;
        public static bool timebook = false;
        DateTime tbsj;
        public MainFrom()
        {
            InitializeComponent();
        }
        public DateTime GetBeijingTime()

        {

            DateTime dt;

            WebRequest wrt = null;

            WebResponse wrp = null;

            try

            {

                wrt = WebRequest.Create("http://www.beijing-time.org/time.asp");

                wrp = wrt.GetResponse();

                string html = string.Empty;

                using (Stream stream = wrp.GetResponseStream())

                {

                    using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))

                    {

                        html = sr.ReadToEnd();

                    }

                }

                string[] tempArray = html.Split(';');

                for (int i = 0; i < tempArray.Length; i++)

                {

                    tempArray[i] = tempArray[i].Replace("\r\n", "");

                }

                string year = tempArray[1].Split('=')[1];

                string month = tempArray[2].Split('=')[1];

                string day = tempArray[3].Split('=')[1];

                string hour = tempArray[5].Split('=')[1];

                string minite = tempArray[6].Split('=')[1];

                string second = tempArray[7].Split('=')[1];

                dt = DateTime.Parse(year + "-" + month + "-" + day + " " + hour + ":" + minite + ":" + second);
                tbsj = dt;
            }

            catch (WebException)

            {

                return DateTime.Parse("2011-1-1");

            }

            catch (Exception)

            {

                return DateTime.Parse("2011-1-1");

            }

            finally

            {

                if (wrp != null)

                    wrp.Close();

                if (wrt != null)

                    wrt.Abort();

            }

            return dt;

        }
        public static string GetNetDateTime()
        {
            WebRequest request = null;
            WebResponse response = null;
            WebHeaderCollection headerCollection = null;
            string datetime = string.Empty;
            try
            {
                request = WebRequest.Create("https://www.baidu.com");
                request.Timeout = 3000;
                request.Credentials = CredentialCache.DefaultCredentials;
                response = (WebResponse)request.GetResponse();
                headerCollection = response.Headers;
                foreach (var h in headerCollection.AllKeys)
                { if (h == "Date") { datetime = headerCollection[h]; } }
                return datetime;
            }
            catch (Exception) { return datetime; }
            finally
            {
                if (request != null)
                { request.Abort(); }
                if (response != null)
                { response.Close(); }
                if (headerCollection != null)
                { headerCollection.Clear(); }
            }
        }
        //加载界面显示
        public void MainFrom_Load(object sender, EventArgs e)
        {
            ReloadForm();
            if (UserTable.role.Trim()== "老板")
            {
                系统管理ToolStripMenuItem.Visible = false;
                营业查询ToolStripMenuItem.Visible = true;
                添加管理ToolStripMenuItem.Visible = false;
                座位ToolStripMenuItem.Visible =false;
                服务生管理ToolStripMenuItem.Visible = false;
                toolStrip1.Items[0].Visible = false;
                toolStrip1.Items[1].Visible = false;
                toolStrip1.Items[2].Visible = false;
                toolStrip1.Items[3].Visible = false;
                panel1.Visible = false;
            }
            else if(UserTable.role.Trim()== "店经理")
            {
                系统管理ToolStripMenuItem.Visible = true;
                营业查询ToolStripMenuItem.Visible = true;
                添加管理ToolStripMenuItem.Visible = true;
                服务生管理ToolStripMenuItem.Visible = true;
                toolStrip1.Visible = true;
            }
            else if(UserTable.role.Trim()== "收银员")
            {
                系统管理ToolStripMenuItem.Visible = false;
                营业查询ToolStripMenuItem.Visible = false;
                服务生管理ToolStripMenuItem.Visible = false;
                添加管理ToolStripMenuItem.Visible = true;
                座位ToolStripMenuItem.Visible = true;
                toolStrip1.Visible= true;
            }
            else if (UserTable.role.Trim()== "服务生")
            {
                系统管理ToolStripMenuItem.Visible = false;
                营业查询ToolStripMenuItem.Visible = false;
                服务生管理ToolStripMenuItem.Visible = false;
                会员充值ToolStripMenuItem.Visible = false;
                添加管理ToolStripMenuItem.Visible = true;
                座位ToolStripMenuItem.Visible = true;
                toolStrip1.Visible = true;
                toolStrip1.Items[2].Visible = false;
                toolStrip1.Items[3].Visible = false;
            }
            else
            {
                系统管理ToolStripMenuItem.Enabled = false;
                营业查询ToolStripMenuItem.Visible = false;
                服务生管理ToolStripMenuItem.Visible = false;
                添加管理ToolStripMenuItem.Visible = false;
                座位ToolStripMenuItem.Visible = false;
                toolStrip1.Visible = false;
            }
           
            
        }

       //更新窗体调用的数据
        public void ReloadForm()
        {

            picturebox = new PictureBox[15] { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14, pictureBox15 };
            showpic();
            label19.Text = "";
            label20.Text = "";
            label21.Text = "";
            toop3.Text = UserTable.name;
            toop4.Text = UserTable.role;


        }


        //显示图片方法
        public bool showtable(int numtable)
        {
            bool set = false;
            try
            {
                //查询数据库语句
                string sql = string.Format("select * from TableNumber where TableId={0}", numtable);
                //创建command对象
                SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                //打开数据库
                DBHelper.con.Open();
                //读取数据库
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    set = Convert.ToBoolean(reader["Tablefettle"]);
             
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                DBHelper.con.Close();
            }
            return set;
       
        }
        public bool showtable1(int numtable)
        {
         
            bool set1 = false;
            try
            {
                //查询数据库语句
                string sql = string.Format("select * from TableNumber where TableId={0}", numtable);
                //创建command对象
                SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                //打开数据库
                DBHelper.con.Open();
                //读取数据库
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    set1= Convert.ToBoolean(reader["Tablebook"]);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                DBHelper.con.Close();
            }
            return set1;
        }
        

        //显示图片方法
        public void showpic()
        {
            for (int i = 1; i <= 15; i++)
            {
                if (showtable(i) == true) //如果没座人了就显示第一张图片
                {
                    picturebox[i - 1].Image = imageList1.Images[0];
                    picturebox[i - 1].Enabled = true;
                }
                else  
                {
                    try
                    {
                        if (showtable1(i) == true)
                    {
                        picturebox[i - 1].Image = imageList1.Images[2];// 显示第三张
                        picturebox[i - 1].Enabled = false;
                    }
                    else
                    {
                       
                        picturebox[i - 1].Image = imageList1.Images[1]; // 显示第二张
                        picturebox[i - 1].Enabled = true;
                    }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);

                    }
                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.labtimenow.Text= DateTime.Now.ToString(); //显示当前时间
            if (timetf == true)
            {
                showpic();//数据绑定
                timetf = false;//绑定之后，设为FALSE，不然每秒都刷新，浪费资源

            }
            try
            {
                //String update = "update TableNumber set Tablebook='false' where TableId in(select BookTable from Book where datediff(hh,InvalidTime,'" + dt + "')>=0) and Tablebook='true'";
                String update = "update TableNumber set Tablebook='false' where TableId in(select TableId from cancelbook)";
                if (DBHelper.con.State == ConnectionState.Open)
                {
                    DBHelper.con.Close();
                }
                else
                {

                }
                DBHelper.con.Open();
                SqlCommand cmd = new SqlCommand(update, DBHelper.con);
                cmd.ExecuteNonQuery();
                DBHelper.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
                //MainFrom mf = new MainFrom();
                //if (mf != null)
                //{
                //    mf.ReloadForm();
                //}
           

        }



        //显示1号桌的信息
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            table = "1";
            showtablesm();
            showmain();
            

        }
        //显示2号桌的信息
        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            table = "2";
            showtablesm();
            showmain();
            
        }
        //显示3号桌的信息
        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            table = "3";
            showtablesm();
            showmain();
            
        }
        //显示4号桌的信息
        private void pictureBox4_MouseClick(object sender, MouseEventArgs e)
        {
            table = "4";
            showtablesm();
            showmain();
           
        }
        //显示5号桌的信息
        private void pictureBox5_MouseClick(object sender, MouseEventArgs e)
        {
            table = "5";
            showtablesm();
            showmain();
        }
        //显示6号桌的信息
        private void pictureBox6_MouseClick(object sender, MouseEventArgs e)
        {
            table = "6";
            showtablesm();
            showmain();
        }
        //显示7号桌的信息
        private void pictureBox7_MouseClick(object sender, MouseEventArgs e)
        {
            table = "7";
            showtablesm();
            showmain();
        }

        private void pictureBox8_MouseClick(object sender, MouseEventArgs e)
        {
            table = "8";
            showtablesm();
            showmain();
        }

        private void pictureBox9_MouseClick(object sender, MouseEventArgs e)
        {
            table = "9";
            showtablesm();
            showmain();
        }

        private void pictureBox10_MouseClick(object sender, MouseEventArgs e)
        {
            table = "10";
            showtablesm();
            showmain();
        }

        //显示桌子信息详情
        public void showtablesm()
        {
            this.tablenumber.Text = table;
            UserTable.talbeNo = table;
           
        }

      


        //退出系统
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            DialogResult reader = MessageBox.Show("是否要退出咖啡厅点单系统","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (reader == DialogResult.Yes)
            {
                Application.Exit();
            }
        }




        //结账菜单事件
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (panduanjiezhang())
            {
                jiezhang jz = new jiezhang(this);
                jz.ShowDialog();
            }

        }
        //追加消费事件
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (panduanzuijia())
            {
                insert ins = new insert();
                ins.ShowDialog();
            }
        }

        //点单控件事件
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (panduankaidan())
            {
                diandan dd = new diandan(this);
                dd.ShowDialog();
            }
          
        }


        
        private void showmain()
        {
            bool a=false;
            try
            {
                //查询数据库语句
                string sql = string.Format("select * from TableNumber where TableId={0}",table);
                //创建command对象
                SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                //打开数据库
                DBHelper.con.Open();
                //读取数据库
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    a = Convert.ToBoolean(reader["Tablefettle"]);
                }
                reader.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
               
            }
            finally
            {
                DBHelper.con.Close();
            }
            label19.Text = "";
            label20.Text = "";
            label21.Text = "";

            if (a == true)
            {
                //查询数据库语句
                try 
	{	        
		string sql = string.Format("select * from Tables where Ctable={0}",table);
                //创建command对象
                SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                //打开数据库
                DBHelper.con.Open();
                //读取数据库
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    label19.Text = reader["people"].ToString();
                    label20.Text = reader["time"].ToString();
                    label21.Text = reader["monry"].ToString();
                }
                reader.Close();
	}
	catch (Exception ex)
	{
		
	    MessageBox.Show(ex.Message);
               
            }
            finally
            {
                DBHelper.con.Close();
            }

            }

       
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            vip v = new vip();
            v.ShowDialog();
        }


        bool booltable = false;
        //判断选项
        private bool panduanjiezhang()
        {

            panduan();

            if (booltable == false)
            {
                MessageBox.Show("还未开单无法结账", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                return true;
            }
        }

        //判断开单
        private bool panduankaidan()
        {

            panduan();
            if (booltable == true)
            {
                MessageBox.Show("已经开单了,请点追加消费或结账","提示",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                return true;
            }
        }

        //判断追加
        private bool panduanzuijia()
        {
            panduan();
            if (booltable == false)
            {
                MessageBox.Show("还没开单,不能追加消费", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            else
            {
                return true;
            }

        }


        private void panduan()
        {
            try
            {
                string sql = string.Format("select * from TableNumber where TableId={0}", UserTable.talbeNo);
                //创建command对象
                SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                //打开数据库
                DBHelper.con.Open();
                //读取数据库
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    booltable = Convert.ToBoolean(reader["Tablefettle"]);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                DBHelper.con.Close();
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 添加会员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insertvip tianjiahuiyuan = new insertvip();
            tianjiahuiyuan.Show();
        }

        private void 会员充值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vipchongzhi huiyuanchongzhi = new vipchongzhi();
            huiyuanchongzhi.Show();
        }

        private void 修改会员ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            vip huiyuan = new vip();
            huiyuan.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void 服务生管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 服务生添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            insertwaiter tianjiafuwusheng = new insertwaiter();
            tianjiafuwusheng.Show();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void 修改时间ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //GetBeijingTime();
            tbsj= Convert.ToDateTime(GetNetDateTime());
            if (tbsj != DateTime.Now)
            {  
                UpdateTime.SetDate(tbsj);
                MessageBox.Show("已同步到网络时间！");
            }
            else
            {
                MessageBox.Show("系统时间准确,无需同步！");
            }


        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updatepsd yonghuguanli = new updatepsd();
            yonghuguanli.Show();
        }


        private void 服务生查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findwaiter chaxunfuwusheng = new findwaiter();
            chaxunfuwusheng.Show();
        }

        private void 服务生修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updatewaiter xiugaifuwusheng = new updatewaiter();
            xiugaifuwusheng.Show();
        }

        private void pictureBox11_MouseClick(object sender, MouseEventArgs e)
        {
            table = "11";
            showtablesm();
            showmain();
        }

        private void pictureBox12_MouseClick(object sender, MouseEventArgs e)
        {
            table = "12";
            showtablesm();
            showmain();
        }

        private void pictureBox13_MouseClick(object sender, MouseEventArgs e)
        {
            table = "13";
            showtablesm();
            showmain();
        }

        private void pictureBox14_MouseClick(object sender, MouseEventArgs e)
        {
            table = "14";
            showtablesm();
            showmain();
        }

        private void pictureBox15_MouseClick(object sender, MouseEventArgs e)
        {
            table = "15";
            showtablesm();
            showmain();
        }

        private void 商品销售额ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findSales Sales = new findSales();
            Sales.Show();
        }

        private void 预约座位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Book yuyue = new Book();
            yuyue.Show();
        }

        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            help bangzhu = new help();
            bangzhu.Show();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
        }
    }
     

        
    }
