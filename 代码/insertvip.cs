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
    public partial class insertvip : Form
    {
        public vip v = new vip();
        private Form form = null;

        

        public insertvip()
        {
            InitializeComponent();
        }

        public insertvip(Form f)
        {
            form = f;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if (textBox1.Text =="")
           {
               MessageBox.Show("请输入完整信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
           }　
           else  if (textBox1.Text == textBox2.Text)
            {
                try
                {
                    string sql = string.Format("insert into VIp (VIppass,Vipprice) values ({0},{1})", textBox1.Text, textBox3.Text);
                    DBHelper.con.Open();
                    SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                    int num = cmd.ExecuteNonQuery();
                    if (num == 1)
                    {
                        sql = "select @@identity from VIp";
                        cmd.CommandText = sql;
                        int num2 = Convert.ToInt32(cmd.ExecuteScalar());
                        MessageBox.Show("会员增加成功,会员号为" + num2,"恭喜",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("会员增加失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            else {
                MessageBox.Show("两次密码输入不一致", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void insertvip_FormClosed(object sender, FormClosedEventArgs e)
        {
            vip v =new vip();
            v.show();
        }

        private void insertvip_Load(object sender, EventArgs e)
        {

        }

        private void insertvip_Load_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}