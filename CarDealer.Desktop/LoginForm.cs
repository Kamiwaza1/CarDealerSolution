using System.Text.Json;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using Microsoft.Data.SqlClient;
using System.Data;
using CarDealer.Data;
using Model;

namespace CarDealer.Desktop
{
    public partial class LoginForm : Form
    {
      private readonly UserRepository _userRepository;
       
        public LoginForm()
    {
      InitializeComponent();
_userRepository = new UserRepository();
  }

    private void btnCancel_Click(object sender, EventArgs e)
        {
         txtUsername.Clear();
 txtPassword.Clear();

 txtUsername.Focus();
        }
      
      private void btnLogin_Click(object sender, EventArgs e)
{
       string email = txtUsername.Text.Trim();
     string password = txtPassword.Text;

          
 if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
{
  MessageBox.Show("Please enter both email and password", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    return;
            }

            try
       {

    User? authenticatedUser = _userRepository.AuthenticateUser(email, password);

      if (authenticatedUser != null)
       {
           
        _userRepository.UpdateLastLogin(authenticatedUser.UserId);

        
    string welcomeName = !string.IsNullOrWhiteSpace(authenticatedUser.FirstName) 
     ? authenticatedUser.FirstName 
            : authenticatedUser.Email;
          
         MessageBox.Show($"Welcome, {welcomeName}!", "Login Successful", 
      MessageBoxButtons.OK, MessageBoxIcon.Information);

           // Open menu form and hide login
          Menuform form2 = new Menuform();
     form2.Show();
       this.Hide();
    }
       else
                {
        MessageBox.Show("Invalid email or password", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
          txtPassword.Clear();
        txtUsername.Clear();
   txtUsername.Focus();
       }
            }
            catch (SqlException sqlEx)
{
    MessageBox.Show($"SQL Error:\n\n{sqlEx.Message}\n\nError Number: {sqlEx.Number}\nState: {sqlEx.State}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
     MessageBox.Show($"Error:\n\n{ex.Message}\n\nType: {ex.GetType().Name}", "Error Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
   }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
