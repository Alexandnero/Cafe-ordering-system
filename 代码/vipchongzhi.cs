using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace index
{
    public partial class vipchongzhi : Form
    {
        public vip v = new vip();

        private Form form = null;

        public vipchongzhi()
        {
            InitializeComponent();
        }

         public vipchongzhi(Form f)
        {
            form = f;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text==""){
                MessageBox.Show("请输入会员号");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("请输入充值金额");
            }
            else {
                try
                {
                    int money = Convert.ToInt32(textBox2.Text);
                    string sql = string.Format("update VIp set Vipprice=Vipprice+{1} where VIP={0}", textBox1.Text, money);
                    DBHelper.con.Open();
                    SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                    int num = cmd.ExecuteNonQuery();
                    if (num == 1)
                    {
                        MessageBox.Show("充值成功，金额为：" + money);

                    }
                    else
                    {
                        MessageBox.Show("充值失败");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    DBHelper.con.Close();
                }

                this.Close();

            }
        }

        private void vipchongzhi_FormClosed(object sender, FormClosedEventArgs e)
        {
            vip v = new vip();
            v.show();
           
        }

        private void vipchongzhi_Load(object sender, EventArgs e)
        {

        }

    }
}