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
    public partial class insert : Form
    {
        public insert()
        {
            InitializeComponent();
        }

        private void insert_Load(object sender, EventArgs e)
        {
            string sql = "select * from commoidty";
            //���ز˵���ʾ
            show(sql, this.listView1);
            //����
            textBox1.Text = UserTable.talbeNo.ToString();
            try
            {
                string sql1 = string.Format("select * from Tables where Ctable={0}", int.Parse(UserTable.talbeNo));
                SqlCommand cmd = new SqlCommand(sql1, DBHelper.con);
                DBHelper.con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["Ctable"].ToString();
                    textBox2.Text = reader["people"].ToString();
                    textBox3.Text = reader["time"].ToString();
                    label7.Text = reader["monry"].ToString();

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
        //��ʾ�����Ʒ�Ĵ���
        private void button4_Click(object sender, EventArgs e)
        {
            //��ʾ�����Ʒ�Ĵ���
            accession access = new accession();
            access.ShowDialog();

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
                    delete();
                }
            }
            else
            {
                delete();
            }
            showlist2();
            sum();
        }

        //ɾ������

        private void delete()
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
                    MessageBox.Show("ȡ���ɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("ȡ��ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

       
            //ȷ����ť�¼�..�������� ���� �۸�
        private void button1_Click(object sender, EventArgs e)
        {

            sum();
           
               int result=0;
                //����������Ϣ
                try
                {
                    string sql = string.Format("update Tables set monry={0} where Ctable={1}",
                        label7.Text, int.Parse(textBox1.Text));
                    SqlCommand cmd = new SqlCommand(sql, DBHelper.con);
                    DBHelper.con.Open();         
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

            if (result==1)
            {
              this.Close();
            }

            }
    }
}

    
