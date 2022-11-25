using SecurityMS.Infrastructure.Data.Entities;

namespace UnitTest
{
    public class UnitTest1
    {
        private readonly List<IncomeTaxesMatrix> _incomeTaxesMatrix = new List<IncomeTaxesMatrix>() {
            new IncomeTaxesMatrix() {Id = 1, RangeFrom = 0, RangeTo = 30000, TaxesPercentage = 0, TaxesExemption = 0},
            new IncomeTaxesMatrix() {Id = 2, RangeFrom = 15000, RangeTo = 30000, TaxesPercentage = 0.025M, TaxesExemption = 0M },
            new IncomeTaxesMatrix() {Id = 3, RangeFrom = 30000, RangeTo = 45000, TaxesPercentage = 0.1M, TaxesExemption = 0M },
            new IncomeTaxesMatrix() {Id = 4, RangeFrom = 45000, RangeTo = 60000, TaxesPercentage = 0.15M, TaxesExemption =  0M},
            new IncomeTaxesMatrix() {Id = 4, RangeFrom = 60000, RangeTo = 200000, TaxesPercentage = 0.2M, TaxesExemption =  0M},
            new IncomeTaxesMatrix() {Id = 4, RangeFrom = 200000, RangeTo = 600000, TaxesPercentage = 0.225M, TaxesExemption =  0M},
            new IncomeTaxesMatrix() {Id = 5, RangeFrom = 600000, TaxesPercentage = 0.25M, TaxesExemption = 0 },
        };
        [Fact]
        public void calculateTaxes()
        {
            EmployeesSalaryReportDetails employeeSalary = new EmployeesSalaryReportDetails()
            {
                Id = Guid.NewGuid(),
                BaseSalary = 3286.67M,
                AdvancePaymentInstallment = 0,
                Insurance = 0,
                Penalities = 0,
                ExtraDeductions = 0,
                Rewards = 0
            };
            employeeSalary.CalculateTaxes(_incomeTaxesMatrix);
            var taxes = Math.Round(employeeSalary.Taxes);
            Assert.Equal("20", taxes.ToString());
        }

        [Fact]
        public void calculateTaxes2()
        {
            EmployeesSalaryReportDetails employeeSalary = new EmployeesSalaryReportDetails()
            {
                Id = Guid.NewGuid(),
                BaseSalary = 10623,
                AdvancePaymentInstallment = 0,
                Insurance = 85,
                Penalities = 0,
                ExtraDeductions = 0,
                Rewards = 0
            };

            employeeSalary.CalculateTaxes(_incomeTaxesMatrix);
            var taxes = Math.Round(employeeSalary.Taxes);
            Assert.Equal("1491", taxes.ToString());
        }
        [Fact]
        public void calculateTaxes3()
        {
            EmployeesSalaryReportDetails employeeSalary = new EmployeesSalaryReportDetails()
            {
                Id = Guid.NewGuid(),
                BaseSalary = 3154,
                AdvancePaymentInstallment = 0,
                Insurance = 78,
                Penalities = 0,
                ExtraDeductions = 0,
                Rewards = 0
            };

            employeeSalary.CalculateTaxes(_incomeTaxesMatrix);
            var taxes = Math.Round(employeeSalary.Taxes);
            Assert.Equal("27", taxes.ToString());
        }
        [Fact]
        public void calculateTaxes4()
        {
            EmployeesSalaryReportDetails employeeSalary = new EmployeesSalaryReportDetails()
            {
                Id = Guid.NewGuid(),
                BaseSalary = 4320,
                AdvancePaymentInstallment = 0,
                Insurance = 85,
                Penalities = 0,
                ExtraDeductions = 0,
                Rewards = 0
            };

            employeeSalary.CalculateTaxes(_incomeTaxesMatrix);
            var taxes = Math.Round(employeeSalary.Taxes);
            Assert.Equal("196", taxes.ToString());
        }
    }
}