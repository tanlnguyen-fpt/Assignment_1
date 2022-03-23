using BusinessLayer;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmManagement : Form
    {
        readonly BindingSource source = new BindingSource();

        DataGridViewRow row;
        public IMemberRepository MemberRepository { get; set; } = new MemberRepository();
        public bool IsAdmin { get; set; }
        public Member MemberInfo { get; set; }

        public frmManagement()
        {
            InitializeComponent();
        }

        private void frmManagement_Load(object sender, EventArgs e)
        {
            txtID.ReadOnly = !IsAdmin;
            txtEmail.ReadOnly = !IsAdmin;
            btnAdd.Enabled = IsAdmin;
            btnDelete.Enabled = false;
            btnUpdate.Enabled = IsAdmin ? false : true;
            grbMembers.Enabled = IsAdmin;

            if (IsAdmin)
            {
                LoadMember(MemberRepository.GetMembers());
            }
            else
            {
                txtID.Text = MemberInfo.MemberID.ToString();
                txtName.Text = MemberInfo.MemberName;
                txtEmail.Text = MemberInfo.Email;
                txtPassword.Text = MemberInfo.Password;
                cboCountry.Text = MemberInfo.Country;
                cboCity.Text = MemberInfo.City;
            }
        }

        public void LoadMember(IEnumerable<Member> list)
        {
            var members = list;
            try
            {
                source.DataSource = members;

                dgvMembers.DataSource = null;
                dgvMembers.DataSource = source;
            }
            catch
            {
                MessageBox.Show("Can not load member");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                IsEmpty();
                MemberRepository.Add(new Member(
                    int.Parse(txtID.Text),
                    txtName.Text,
                    txtEmail.Text,
                    txtPassword.Text,
                    cboCity.Text,
                    cboCountry.Text));

                LoadMember(MemberRepository.GetMembers());

                row = null;

                txtID.ReadOnly = false;
                txtEmail.ReadOnly = false;

                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;

                txtID.Text = string.Empty;
                txtName.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtPassword.Text = string.Empty;
                cboCountry.SelectedIndex = -1;
                cboCity.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add");
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                IsEmpty();
                MemberRepository.Update(new Member(
                    int.Parse(txtID.Text),
                    txtName.Text,
                    txtEmail.Text,
                    txtPassword.Text,
                    cboCity.Text,
                    cboCountry.Text));

                if (IsAdmin)
                {
                    LoadMember(MemberRepository.GetMembers());

                    txtID.Text = string.Empty;
                    txtName.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    cboCountry.SelectedIndex = -1;
                    cboCity.SelectedIndex = -1;
                }

                row = null;

                txtID.ReadOnly = IsAdmin?false:true;
                txtEmail.ReadOnly = IsAdmin ? false : true;

                btnDelete.Enabled = false;
                btnUpdate.Enabled = IsAdmin ? false : true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                MemberRepository.Delete(int.Parse(txtID.Text));

                LoadMember(MemberRepository.GetMembers());

                row = null;

                txtID.ReadOnly = false;
                txtEmail.ReadOnly = false;

                btnDelete.Enabled = false;
                btnUpdate.Enabled = false;

                txtID.Text = string.Empty;
                txtName.Text = string.Empty;
                txtEmail.Text = string.Empty;
                txtPassword.Text = string.Empty;
                cboCountry.SelectedIndex = -1;
                cboCity.SelectedIndex = -1;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvMembers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            row = dgvMembers.SelectedRows[0];

            Member m = (Member)row.DataBoundItem;

            if (m != null)
            {
                txtID.Text = m.MemberID.ToString();
                txtName.Text = m.MemberName;
                txtEmail.Text = m.Email;
                txtPassword.Text = m.Password;
                cboCountry.Text = m.Country;
                cboCity.Text = m.City;

                txtID.ReadOnly = true;
                txtEmail.ReadOnly = true;

                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;
            }
        }

        private void IsEmpty()
        {
            if (txtName.Text == "" || txtID.Text == "" || txtEmail.Text == "" || txtPassword.Text == "")
            {
                throw new Exception("Empty information!");
            }
        }
    }
}
