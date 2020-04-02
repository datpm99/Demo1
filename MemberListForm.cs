using Model;
using Model.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _4_Winf_DataGridView_Q1
{
    public partial class MemberListForm : Form
    {
        MemberDAO dao = new MemberDAO();
        string demo = "hello world";
        public MemberListForm()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;
        }

        private void MemberListForm_Load(object sender, EventArgs e)
        {
            loadf();
        }
        public void loadf()
        {
            List<Member> members = dao.GetMembers();
            dataGridView1.DataSource = members;
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Edit";
            btn.Text = "Edit";
            btn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(btn);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<int> lid = new List<int>();
            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells[0].Value)==true)
                {
                    int id = Convert.ToInt32(row.Cells[1].Value);
                    lid.Add(id);
                }
            }
            int count = lid.Count;
            string mess = "Deleted " + count + " member(s).";
            for(int i=0; i<lid.Count; i++)
            {
                dao.delete(lid[i]);
            }
            demo = "good";
            MessageBox.Show(mess);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.ColumnIndex;
            if(index == 5)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
                var edit = new EditMemberInfo(id);
                edit.Show();
            }

        }

        private void MemberListForm_Activated(object sender, EventArgs e)
        {
            List<Member> members = dao.GetMembers();
            dataGridView1.DataSource = members;
        }
    }
}
