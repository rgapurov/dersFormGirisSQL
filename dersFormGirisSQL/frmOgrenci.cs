using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dersFormGirisSQL
{
    public partial class frmOgrenci : Form
    {
        public string activeUser;
        

        SqlConnection con;
        SqlCommand cmd;
        DataSet ds;
        SqlDataAdapter dAdapter;
        

        public frmOgrenci()
        {
            InitializeComponent();           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmOgrenci_Load(object sender, EventArgs e)
        {
            lblUser.Text = activeUser;
            string sorgu = "SELECT * FROM student";
            GetData(sorgu);
            
        }
        void GetData(string sorgu)
        {
            // datagridview' verilerin çekilmesi
            con = new SqlConnection("server=DESKTOP-L799HNF\\SQLEXPRESS; Initial Catalog=test; Integrated Security=True");
            dAdapter = new SqlDataAdapter(sorgu, con);
            ds = new DataSet();
            con.Open();
            dAdapter.Fill(ds, "students");
            dgvStudents.DataSource = ds.Tables["students"];
            con.Close();

            ClearText();
        }

        void AddStudent(string no, string name, string surname)
        {
            cmd = new SqlCommand();
            con = new SqlConnection("server=DESKTOP-L799HNF\\SQLEXPRESS; Initial Catalog=test; Integrated Security=True");
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "insert into student(studentNo,studentName,studentSurname) " +
                              "values (" + no + ",'" + name + "','" + surname + "')";
            cmd.ExecuteNonQuery();
            con.Close();

            GetData("Select * from student");

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddStudent(txtNo.Text, txtName.Text, txtSurname.Text);
        }

        void DeleteStudent(string no)
        {
            cmd = new SqlCommand();
            con = new SqlConnection("server=DESKTOP-L799HNF\\SQLEXPRESS; Initial Catalog=test; Integrated Security=True");
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "delete student where studentNo = " + no;
            cmd.ExecuteNonQuery();
            con.Close();

            GetData("Select * from student");
            
            ClearText();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteStudent(txtNo.Text);
        }

        private void dgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            txtNo.Text = dgvStudents.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = dgvStudents.CurrentRow.Cells[1].Value.ToString();
            txtSurname.Text = dgvStudents.CurrentRow.Cells[2].Value.ToString();
            // seçim yapılınca güncelle ve sil butonu aktifleşmesi
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }
        void ClearText()
        {
            txtNo.Text = "";
            txtName.Text = "";
            txtSurname.Text = "";
            // textler temizlenince güncelle ve sil butonu tıklanmamalıdır.
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
        void UpdateStudent(string no)
        {
            cmd = new SqlCommand();
            con = new SqlConnection("server=DESKTOP-L799HNF\\SQLEXPRESS; Initial Catalog=test; Integrated Security=True");
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update student set studentNo = " + txtNo.Text + ",  studentName ='" + txtName.Text + 
                              "', studentSurname ='" + txtSurname.Text + "' where studentNo = " + no;
            cmd.ExecuteNonQuery();
            con.Close();

            GetData("Select * from student");
        }

        private void lblClear_Click(object sender, EventArgs e)
        {
            ClearText();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateStudent(dgvStudents.CurrentRow.Cells[0].Value.ToString());
        }
    }
}
