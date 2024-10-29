using Candidate_BusinessObjects;
using Candidate_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CandidateManagement_WPF_PhiBLBSE173503
{
    /// <summary>
    /// Interaction logic for CandidateProfileWindow.xaml
    /// </summary>
    public partial class CandidateProfileWindow : Window
    {
        private ICandidateProfileService profileService;
        private IJobPostingService jobPostingService;
        public CandidateProfileWindow()
        {
            InitializeComponent();
            profileService = new CandidateProfileService();
            jobPostingService = new JobPostingService();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            loadDataInit();
        }

        private void loadDataInit()
        {
            this.dtgCandidateProfile.ItemsSource = profileService.GetCandidateProfiles();
            this.cmbPostID.ItemsSource = jobPostingService.GetJobPostings();
            this.cmbPostID.DisplayMemberPath = "JobPostingTitle";
            this.cmbPostID.SelectedValuePath = "PostingId";

            txtCandidateID.Text = "";
            txtBirthday.Text = "";
            txtDescription.Text = "";
            txtImageUrl.Text = "";
            txtFullName.Text = "";
            cmbPostID.SelectedValue = "";
        }

        private void btnAdd_Click_1(object sender, RoutedEventArgs e)
        {
            CandidateProfile candidate = new CandidateProfile();
            candidate.CandidateId = txtCandidateID.Text;
            candidate.Fullname = txtFullName.Text; 
            candidate.Birthday = DateTime.Parse(txtBirthday.Text);
            candidate.ProfileUrl = txtImageUrl.Text;
            //candidate.Posting = jobPostingService.GetJobPostingById(cmbPostID.SelectedValue.ToString());
            candidate.PostingId = cmbPostID.SelectedValue.ToString();
            candidate.ProfileShortDescription = txtDescription.Text;
            if(profileService.AddCandidateProfile(candidate))
            {
                MessageBox.Show("Add successful!");
                loadDataInit();
            }
            else 
            {
                MessageBox.Show("Contact 113!");
            }

        }

        private void dtgCandidateProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;
            if (row != null)
            {
                DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;
                string id = ((TextBlock)RowColumn.Content).Text;
                CandidateProfile profile = profileService.GetCandidateProfileById(id);
                if (profile != null)
                {
                    txtCandidateID.Text = profile.CandidateId;
                    txtBirthday.Text = profile.Birthday.ToString();
                    txtDescription.Text = profile.ProfileShortDescription;
                    txtImageUrl.Text = profile.ProfileUrl;
                    txtFullName.Text = profile.Fullname;
                    cmbPostID.SelectedValue = profile.PostingId;
                }
            }
            
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            string candidateID = txtCandidateID.Text;
            if(candidateID.Length > 0 && profileService.DeleteCandidateProfile(candidateID)) 
            {
                MessageBox.Show("Delete Successfull!");
                loadDataInit();
            } 
            else 
            {
                MessageBox.Show("Something wrong!");    
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            CandidateProfile candidate = new CandidateProfile();
            candidate.CandidateId = txtCandidateID.Text;
            candidate.Fullname = txtFullName.Text;
            candidate.Birthday = DateTime.Parse(txtBirthday.Text);
            candidate.ProfileUrl = txtImageUrl.Text;
            //candidate.Posting = jobPostingService.GetJobPostingById(cmbPostID.SelectedValue.ToString());
            candidate.PostingId = cmbPostID.SelectedValue.ToString();
            candidate.ProfileShortDescription = txtDescription.Text;
            if (profileService.UpdateCandidateProfile(candidate))
            {
                MessageBox.Show("Update successful!");
                loadDataInit();
            }
            else
            {
                MessageBox.Show("Contact 113!");
            }
        }
    }
}
