namespace ResellFlow.Application.Validators;

public static class CnpjValidator
{
    public static bool IsValid(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj)) return false;

        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

        if (cnpj.Length != 14) return false;

        var invalids = new[]
        {
            "00000000000000", "11111111111111", "22222222222222", "33333333333333",
            "44444444444444", "55555555555555", "66666666666666", "77777777777777",
            "88888888888888", "99999999999999"
        };

        if (invalids.Contains(cnpj)) return false;

        var digits = cnpj.Select(c => int.Parse(c.ToString())).ToArray();

        int sum1 = 0;
        int[] mult1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        for (int i = 0; i < 12; i++)
            sum1 += digits[i] * mult1[i];

        int remainder1 = sum1 % 11;
        int d1 = remainder1 < 2 ? 0 : 11 - remainder1;

        int sum2 = 0;
        int[] mult2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        for (int i = 0; i < 13; i++)
            sum2 += digits[i] * mult2[i];

        int remainder2 = sum2 % 11;
        int d2 = remainder2 < 2 ? 0 : 11 - remainder2;

        return digits[12] == d1 && digits[13] == d2;
    }
}