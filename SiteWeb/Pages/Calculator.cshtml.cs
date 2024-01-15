using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SiteWeb.Pages;

public class CalculatorModel : PageModel
{
    public double TotalInvestedReturnPercentage = 0.0;

    public double InflationPercentage = 0.062;

    public double SavingsReturnPercentage = 0.005;

    public double Sp500ReturnPercentage = 0.08;

    public double OpasReturnPercentage = 0.213;

    [BindProperty]
    public int? Years { get; set; }

    [BindProperty]
    public double? InitialDeposit { get; set; }

    [BindProperty]
    public double? FurtherDeposit { get; set; }

    [BindProperty]
    public int? NumPortfolios { get; set; }

    [BindProperty]
    public double? DividendYield { get; set; }

    [BindProperty]
    public bool Reinvest { get; set; }

    [BindProperty]
    public bool Inflation { get; set; }

    public double TotalInvested { get; set; } = 0.0;

    public double SavingsReturn { get; set; } = 0.0;

    public double Sp500Return { get; set; } = 0.0;

    public double OpasReturn { get; set; } = 0.0;

    public double Sp500Dividends { get; set; } = 0.0;

    public double OpasDividends { get; set; } = 0.0;

    public void OnGet()
    {
    }

    public void OnPost()
    {
        double totalInvestedReturnPercentage = TotalInvestedReturnPercentage;
        double savingsReturnPercentage = SavingsReturnPercentage;
        double sp500ReturnPercentage = Sp500ReturnPercentage;
        double opasReturnPercentage = OpasReturnPercentage;

        TotalInvested = InitialDeposit!.Value;
        SavingsReturn = InitialDeposit!.Value;
        Sp500Return = InitialDeposit!.Value;
        OpasReturn = InitialDeposit!.Value;

        if (Inflation)
        {
            totalInvestedReturnPercentage -= InflationPercentage;
            savingsReturnPercentage -= InflationPercentage;
            sp500ReturnPercentage -= InflationPercentage;
            opasReturnPercentage -= InflationPercentage;
        }

        for (int i = 0; i < Years; i++)
        {
            if (Reinvest)
            {
                Sp500Return *= 1 + (DividendYield!.Value / 100);
                OpasReturn *= 1 + (DividendYield!.Value / 100);
            }

            TotalInvested = (TotalInvested * (1 + totalInvestedReturnPercentage)) + (FurtherDeposit!.Value * NumPortfolios!.Value);
            SavingsReturn = (SavingsReturn * (1 + savingsReturnPercentage)) + (FurtherDeposit!.Value * NumPortfolios!.Value);
            Sp500Return = (Sp500Return * (1 + sp500ReturnPercentage)) + (FurtherDeposit!.Value * NumPortfolios!.Value);
            OpasReturn = (OpasReturn * (1 + opasReturnPercentage)) + (FurtherDeposit!.Value * NumPortfolios!.Value);
        }

        Sp500Dividends = Sp500Return * (DividendYield!.Value / 100);
        OpasDividends = OpasReturn * (DividendYield!.Value / 100);
    }
}
