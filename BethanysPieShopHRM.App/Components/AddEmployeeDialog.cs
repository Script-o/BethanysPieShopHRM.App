using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;

namespace BethanysPieShopHRM.App.Components
{
    public partial class AddEmployeeDialog
    {
        public Employee Employee { get; set; } =
            new Employee
            {
                CountryId = 1,
                JobCategoryId = 1,
                BirthDate = DateTime.Now,
                JoinedDate = DateTime.Now,
                Zip = "1000",
                City = "City",
                Street = "Street",
                Comment = "Comment",
                //Country = new Country { CountryId = 1, Name = "Belgium" },
                //JobCategory = new JobCategory { JobCategoryId = 1, JobCategoryName = "Pie research" },
                PhoneNumber = "555-555-5555"
            };

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        public bool ShowDialog { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        public void Show()
        {
            ResetDialog();
            ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        private void ResetDialog()
        {
            Employee = new Employee
            {
                CountryId = 1,
                JobCategoryId = 1,
                BirthDate = DateTime.Now,
                JoinedDate = DateTime.Now,
                Zip = "1000",
                City = "City",
                Street = "Street",
                Comment = "Comment",
                //Country = new Country { CountryId = 1, Name = "Belgium" },
                //JobCategory = new JobCategory { JobCategoryId = 1, JobCategoryName = "Pie research" },
                PhoneNumber = "555-555-5555"
            };
        }

        protected async Task HandleValidSubmit()
        {
            await EmployeeDataService.AddEmployee(Employee);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
