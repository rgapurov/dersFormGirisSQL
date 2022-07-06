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
    public partial class frmMain : Form
    {
        /*
         Logın ıslemlerımın gerceklesebılmesı ıcın 
         baglantımın acılması 
        sql ve tsql konutlarımın kullanılması
        ve bu sorgulardan donen degerlerın alınıp execute 
        edılmesı gerekıyor

        İşte tüm bu sebeplerden dolayı 
        aşağıdaki veri türlerini ve onlardan türeyen nesneleri 
        kullanıyorum
       */
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        

        public frmMain()
        {        
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bntGiris_Click(object sender, EventArgs e)
        {
            // kullanıcı adının db'den kontrol edilmesi ve girişin sağlanması
            string sorgu = "SELECT * FROM users where users=@user AND password=@pass";
            con = new SqlConnection("server=DESKTOP-L799HNF\\SQLEXPRESS; Initial Catalog=test; Integrated Security=True");
            cmd = new SqlCommand(sorgu, con);

            cmd.Parameters.AddWithValue("@user", txtUser.Text);
            cmd.Parameters.AddWithValue("@pass", txtPass.Text);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                frmOgrenci frmOG = new frmOgrenci();
                frmOG.activeUser = txtUser.Text;
                frmOG.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş!");
            }
            con.Close();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txtUser.Focus();
            this.AcceptButton = btnEnter;
        }
    }
}
