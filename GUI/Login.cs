using BusinessLayer;
using DataAccess;
using System;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmLogin : Form
    {

        IMemberRepository MemberRepository = new MemberRepository();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtLoginEmail.Text;
            string pass = txtLoginPassword.Text;

            if (email.Equals("admin") && pass.Equals("admin"))
            {
                frmManagement management = new frmManagement()
                {
                    IsAdmin = true,
                    MemberRepository = MemberRepository
                };
                management.Show();
            }
            else
            {
                Member member = MemberRepository.Login(email, pass);
                if (member == null)
                {
                    MessageBox.Show("Wrong email or password", "Login");
                }
                else
                {
                    frmManagement management = new frmManagement()
                    {
                        IsAdmin = false,
                        MemberRepository = MemberRepository,
                        MemberInfo = member
                    };
                    management.Show();
                }
            }
        }
    }
}
