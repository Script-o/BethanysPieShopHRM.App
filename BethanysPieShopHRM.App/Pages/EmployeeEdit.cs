﻿using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BethanysPieShopHRM.App.Pages
{
    public partial class EmployeeEdit
    {
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }
        [Inject]
        public ICountryDataService CountryDataService { get; set; }
        [Inject]
        public IJobCategoryDataService JobCategoryDataService { get; set; } 

        [Parameter]
        public string EmployeeId { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public Employee Employee { get; set; } = new Employee();
        public List<Country> Countries { get; set; } = new List<Country>();
        public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();

        protected string CountryId = string.Empty;
        protected string JobCategoryId = string.Empty;

        //used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        private ElementReference LastNameInput;

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await LastNameInput.FocusAsync();
        }

        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            Countries = (await CountryDataService.GetAllCountries()).ToList();
            //Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
            JobCategories = (await JobCategoryDataService.GetAllJobCategories()).ToList();

            int.TryParse(EmployeeId, out var employeeId);

            if (employeeId == 0) //new employee is being created
            {
                //add some defaults
                Employee = new Employee { CountryId = 1, JobCategoryId = 1, BirthDate = DateTime.Now, JoinedDate = DateTime.Now };
            }
            else
            {
                Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
            }

            CountryId = Employee.CountryId.ToString();
            JobCategoryId = Employee.JobCategoryId.ToString();
        }

        private IReadOnlyList<IBrowserFile> selectedFiles;
        private void OnInputFileChange(InputFileChangeEventArgs e)
        {
            selectedFiles = e.GetMultipleFiles();
            Message = $"{selectedFiles.Count} files(s) selected";
            StateHasChanged();
        }

        protected async Task HandleValidSubmit()
        {
            //This is just me experimenting
            Country country = new Country();
            country.CountryId = int.Parse(CountryId);
            country.Name = "Belgim";

            JobCategory job = new JobCategory();
            job.JobCategoryId = int.Parse(JobCategoryId);
            job.JobCategoryName = "Piemen";

            //Employee.Country = country;
            //Employee.JobCategory = job;




            Saved = false;
            Employee.CountryId = int.Parse(CountryId);
            Employee.JobCategoryId = int.Parse(JobCategoryId);

            

            if (Employee.EmployeeId == 0) //new
            {
                if (selectedFiles != null)
                {
                    var file = selectedFiles[0];
                    Stream stream = file.OpenReadStream();
                    MemoryStream ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    stream.Close();

                    Employee.ImageName = file.Name;
                    Employee.ImageContent = ms.ToArray();
                }

                var addedEmployee = await EmployeeDataService.AddEmployee(Employee);
                if (addedEmployee != null)
                {
                    StatusClass = "alert-success";
                    Message = "New employee added successfully.";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new employee. Please try again.";
                    Saved = false;
                }
            }
            else
            {
                Console.WriteLine("HEEEEEEEY.");
                await EmployeeDataService.UpdateEmployee(Employee);
                StatusClass = "alert-success";
                Message = "Employee updated successfully.";
                Saved = true;
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
            Saved = true;
        }

        protected async Task DeleteEmployee()
        {
            await EmployeeDataService.DeleteEmployee(Employee.EmployeeId);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/employeeoverview");
        }
    }
}
