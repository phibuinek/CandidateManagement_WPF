using Candidate_BusinessObjects;
using Candidate_Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CandidateManagement_WPF_PhiBLBSE173503
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IHRAccountService hRAccountService;
        public MainWindow()
        {
            InitializeComponent();
            hRAccountService = new HRAccountService();
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Hraccount hraccount = hRAccountService.GetHraccountByEmail(txtEmail.Text);
            if (hraccount != null && txtPassword.Password.Equals(hraccount.Password) && hraccount.MemberRole == 1)
            {
                CandidateProfileWindow profileWindow = new CandidateProfileWindow();
                profileWindow.Show();
            }
            else
            {
                MessageBox.Show("Bye Bye!");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}