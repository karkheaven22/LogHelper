namespace LogHelper.Formula
{
    public class Property
    {
        private static void ValidatePositiveValue(double value, string paramName)
        {
            if (value <= 0) throw new ArgumentException($"{paramName} must be greater than 0.", paramName);
        }

        /// <summary>
        /// Payment Equal Amortization
        /// </summary>
        /// <param name="LoanAmount"></param>
        /// <param name="Interest"></param>
        /// <param name="Period"></param>
        /// <returns></returns>
        public static double Pay_EA(double LoanAmount, double Interest, int Period)
        {
            ValidatePositiveValue(LoanAmount, nameof(LoanAmount));
            ValidatePositiveValue(Interest, nameof(Interest));
            ValidatePositiveValue(Period, nameof(Period));

            var n = Interest / 1200;
            var i = Math.Pow(1 + n, Period);
            var l = LoanAmount * (n + n / (i - 1));

            return Math.Round(l, 2, MidpointRounding.ToEven);
        }

        /// <summary>
        /// Equal principal repayment
        /// </summary>
        /// <param name="LoanAmount"></param>
        /// <param name="Interest"></param>
        /// <param name="Period"></param>
        /// <returns></returns>
        public static (double, double) Pay_EPR(double LoanAmount, double Interest, int Period)
        {
            ValidatePositiveValue(LoanAmount, nameof(LoanAmount));
            ValidatePositiveValue(Interest, nameof(Interest));
            ValidatePositiveValue(Period, nameof(Period));

            var a = Math.Round(LoanAmount / Period, 2);
            var n = LoanAmount * Interest / 1200;
            var r = Math.Round(n / Period, 2);

            return ((a + n), r);
        }

        public static double Find_Period(double LoanAmount, double Interest, double Repayment)
        {
            var n = Interest / 1200;
            var ratio = Repayment / LoanAmount;
            var denominator = 1 + n;
            var numerator = denominator + n / (ratio - n);
            var Period = Math.Log(numerator) / Math.Log(denominator);
            return Math.Round(Period, 0);
        }

        static double CompoundInterest(double LoanAmount, double Interest, int tenure)
        {
            // (1 + r/n)
            var n = Interest / 1200;
            var denominator = 1 + n;
            // P(1 + r/n)^nt
            return LoanAmount * Math.Pow(denominator, tenure);
        }
    }
}