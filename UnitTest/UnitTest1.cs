using SecurityMS.Infrastructure.Data.Entities;

namespace UnitTest
{
    public class UnitTest1
    {
        private readonly List<IncomeTaxesMatrix> _incomeTaxesMatrix = new List<IncomeTaxesMatrix>() {
            new IncomeTaxesMatrix() {Id = 1, RangeFrom = 0, RangeTo = 15000, TaxesPercentage = 0, TaxesExemption = 1},
            new IncomeTaxesMatrix() {Id = 2, RangeFrom = 8000, RangeTo = 30000, TaxesPercentage = 0.1M, TaxesExemption = 0.85M },
            new IncomeTaxesMatrix() {Id = 2, RangeFrom = 30000, RangeTo = 45000, TaxesPercentage = 0.15M, TaxesExemption = 0.45M },
            new IncomeTaxesMatrix() {Id = 2, RangeFrom = 45000, RangeTo = 200000, TaxesPercentage = 0.2M, TaxesExemption =  0.075M},
            new IncomeTaxesMatrix() {Id = 2, RangeFrom = 200000, TaxesPercentage = 0.225M, TaxesExemption = 0 },
        };
        [Fact]
        public void calculateTaxes()
        {
            SalaryReportEmployeesReport employeeSalary = new SalaryReportEmployeesReport()
            {
                Id = Guid.NewGuid(),
                BaseSalary = 63735,
                AdvancePaymentInstallment = 0,
                Insurance = 406,
                Penalities = 0,
                ExtraDeductions = 0,
                Rewards = 0
            };

            var taxes = Math.Round(employeeSalary.CalculateTaxes(_incomeTaxesMatrix));
            Assert.Equal("13322", taxes.ToString());
        }

        [Fact]
        public void calculateTaxes2()
        {
            SalaryReportEmployeesReport employeeSalary = new SalaryReportEmployeesReport()
            {
                Id = Guid.NewGuid(),
                BaseSalary = 10623,
                AdvancePaymentInstallment = 0,
                Insurance = 85,
                Penalities = 0,
                ExtraDeductions = 0,
                Rewards = 0
            };

            var taxes = Math.Round(employeeSalary.CalculateTaxes(_incomeTaxesMatrix));
            Assert.Equal("1491", taxes.ToString());
        }
        [Fact]
        public void calculateTaxes3()
        {
            SalaryReportEmployeesReport employeeSalary = new SalaryReportEmployeesReport()
            {
                Id = Guid.NewGuid(),
                BaseSalary = 3154,
                AdvancePaymentInstallment = 0,
                Insurance = 78,
                Penalities = 0,
                ExtraDeductions = 0,
                Rewards = 0
            };

            var taxes = Math.Round(employeeSalary.CalculateTaxes(_incomeTaxesMatrix));
            Assert.Equal("27", taxes.ToString());
        }
        [Fact]
        public void calculateTaxes4()
        {
            SalaryReportEmployeesReport employeeSalary = new SalaryReportEmployeesReport()
            {
                Id = Guid.NewGuid(),
                BaseSalary = 4320,
                AdvancePaymentInstallment = 0,
                Insurance = 85,
                Penalities = 0,
                ExtraDeductions = 0,
                Rewards = 0
            };

            var taxes = Math.Round(employeeSalary.CalculateTaxes(_incomeTaxesMatrix));
            Assert.Equal("196", taxes.ToString());
        }
    }
}