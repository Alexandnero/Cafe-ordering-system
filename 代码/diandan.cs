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

    public partial class diandan : Form
    {

        public MainFrom mf=new MainFrom();
        private Form form = null;

        public diandan()
        {
            InitializeComponent();
        }

        public diandan(Form f)
        {
            form = f;
            InitializeComponent();
        }

        private void diandan_Load(object sender, EventArgs e)
        {
            string sql = "select * from commoidty";
            //���ز˵���ʾ
            show(sql, this.listView1);

            //����
            textBox1.Text = UserTable.talbeNo.ToString();
            //ʱ��
            textBox3.Text = DateTime.Now.ToString();
            sum();
            showlist2();
           
        }
        //��ʾ�˵�����
        string name = "";  
        double price = 0;
        string type = "";
        private void show(string sql, ListView lv)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                DBHelper.con.Open();
                // lv.Items.Clear();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    name = reader["CName"].ToString();
                   
                    price = Convert.ToDouble(reader["Cprice"]);
                    type = reader["Ctype"].ToString();



                    ListViewItem lvi = new ListViewItem(reader["CName"].ToString());
                    lvi.Tag = (int)reader["CID"];
                    lv.Items.Add(lvi);
                    lvi.SubItems.AddRange(new string[] { reader["Cprice"].ToString(), reader["Ctype"].ToString() });
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


        //�㵥
        private void button2_Click(object sender, EventArgs e)
        {
            
            int id = (int)this.listView1.SelectedItems[0].Tag;
            string sql = string.Format("select * from commoidty where CID={0}", id);
            //���ز˵���ʾ
            show(sql, listView2);

            try
            {
                string sql1 = string.Format("insert into Diandan (Dtable,DName,Dmoney) values({0},'{1}',{2})", textBox1.Text, name, price);
                SqlCommand cmd = new SqlCommand(sql1, DBHelper.con);
                DBHelper.con.Open();
                listView1.Items.Clear();
                int result = 0;
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            finally
            {
                DBHelper.con.Close();
            }
            sum();
             sql = "select * from commoidty";
            show(sql, this.listView1);
           

        }



        //�����Ʒ�ؼ�
        private void button4_Click(object sender, EventArgs e)
        {
            //��ʾ�����Ʒ�Ĵ���
            accession access = new accession();
            access.ShowDialog();

        }


        //ȷ����ť�¼�..�������� ���� �۸�
        private void button1_Click(object sender, EventArgs e)
        {

            sum();
            if (textBox2.Text == "")
            {
                MessageBox.Show("��������");

            }
            else
            {
                
                try
                {
                    string sql = string.Format("insert into Tables(Ctable,monry,people,time) values ({0},{1},{2},'{3}')",
                               int.Parse(textBox1.Text), label7.Text, textBox2.Text, textBox3.Text);
                    SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                    DBHelper.con.Open();

                    int result = 0;
                    result = cmd.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DBHelper.con.Close();
                
            }

                //��������״̬
            try
            {
                string sql1 = string.Format("update TableNumber set Tablefettle='true',Tablebook='false' where TableId={0}", int.Parse(textBox1.Text));
                SqlCommand cmd = new SqlCommand(sql1, DBHelper.con);
                DBHelper.con.Open();
                int result1 = 0;
                result1 = cmd.ExecuteNonQuery();
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


        //�����ܺ�
        public void sum()
        {
            try
            {
                string sql = string.Format("select sum(Dmoney) from Diandan where Dtable={0}", int.Parse(textBox1.Text));
                SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                DBHelper.con.Open();
                label7.Text = cmd.ExecuteScalar().ToString();
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

        //�ڵڶ�������ʾ�Ѿ����˵�����
        public void showlist2()
        {
            
            try
            {
                string sql = string.Format("select * from Diandan  where Dtable={0}", int.Parse(textBox1.Text));
                SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                DBHelper.con.Open();
                listView2.Items.Clear();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    
                    ListViewItem lvi = new ListViewItem(reader["DName"].ToString());
                    lvi.Tag = (int)reader["TID"];
                    listView2.Items.Add(lvi);
                    lvi.SubItems.AddRange(new string[] { reader["Dmoney"].ToString() });
                }
                reader.Read();
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

        //��������¼�
        private void button3_Click(object sender, EventArgs e)
        {
            if (UserTable.name == "����Ա")
            {
                password pw = new password();
                pw.ShowDialog();
                if (pw.gett())
                {
                    delete1();
                }
            }
            else
            {
                delete1();
            }
            

            showlist2();
            sum();
        }

        //ɾ������;
        private void delete1()
        {
            try
            {
                string sql = string.Format("delete Diandan  where TID={0}", (int)listView2.SelectedItems[0].Tag);
                SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                DBHelper.con.Open();
                int result = 0;
                result = cmd.ExecuteNonQuery();
                if (result == 1)
                {
                    MessageBox.Show("ȡ���ɹ�");

                }
                else
                {
                    MessageBox.Show("ȡ��ʧ��");
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
        }


        private void diandan_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainFrom mf = (MainFrom)form;
            if (mf != null)
            {
                mf.ReloadForm();
            }

        }
       

        

       


    }
}