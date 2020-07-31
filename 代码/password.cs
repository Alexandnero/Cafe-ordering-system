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
    public partial class password : Form
    {
        public password()
        {
            InitializeComponent();
        }


        string pass1 = "";
        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                string sql = string.Format("select PassWord from admin  where UId='店经理'");
                SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                DBHelper.con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    pass1 = reader["PassWord"].ToString();

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

            if (textBox1.Text != pass1)
            {
                MessageBox.Show("密码不正确,请与店经理联系","提示",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                
            }
            this.Close();

        }

        public  bool gett()
        {

            if (textBox1.Text == pass1)
            {
                return true;
            }
            else
            {
                return false;
            }

            
        }

        private void password_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}